using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using ReadWriteFile;

namespace Convertor
{
    /// <summary>
    /// Manages the migration process for multiple tables, providing UI updates and status reporting
    /// </summary>
    public class MigrationManager
    {
        private DataMigrationEngine _engine;
        private BackgroundWorker _worker;
        private List<TableMigrationInfo> _tablesToMigrate;
        private KryptonRichTextBox _outputTextBox;
        private int _currentTableIndex;
        private bool _useTransaction = true;
        private int _batchSize = 1000;
        private bool _continueOnError = false;
        
        /// <summary>
        /// Creates a new migration manager
        /// </summary>
        public MigrationManager(KryptonRichTextBox outputTextBox)
        {
            _engine = new DataMigrationEngine();
            _tablesToMigrate = new List<TableMigrationInfo>();
            _outputTextBox = outputTextBox;
            
            // Initialize background worker
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += Worker_DoWork;
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            
            // Subscribe to engine progress events
            _engine.ProgressUpdated += Engine_ProgressUpdated;
        }
        
        /// <summary>
        /// Adds a table to the migration queue
        /// </summary>
        public void AddTableToMigrate(string sourceTable, string destTable, Dictionary<string, string> columnMappings)
        {
            _tablesToMigrate.Add(new TableMigrationInfo
            {
                SourceTableName = sourceTable,
                DestinationTableName = destTable,
                ColumnMappings = columnMappings,
                Status = "Pending"
            });
        }
        
        /// <summary>
        /// Sets migration options
        /// </summary>
        public void SetOptions(bool useTransaction, int batchSize, bool continueOnError)
        {
            _useTransaction = useTransaction;
            _batchSize = batchSize;
            _continueOnError = continueOnError;
            _engine.SetOptions(useTransaction, batchSize, continueOnError);
        }
        
        /// <summary>
        /// Starts the migration process
        /// </summary>
        public void StartMigration()
        {
            if (_tablesToMigrate.Count == 0)
            {
                MessageBox.Show("No tables selected for migration.", "Migration Warning");
                return;
            }
            
            if (_worker.IsBusy)
            {
                MessageBox.Show("Migration is already in progress.", "Migration Warning");
                return;
            }
            
            // Clear output
            if (_outputTextBox != null)
            {
                _outputTextBox.Clear();
                _outputTextBox.AppendText("Starting migration...\n");
            }
            
            _currentTableIndex = 0;
            
            // Start worker
            _worker.RunWorkerAsync();
        }
        
        /// <summary>
        /// Cancels the current migration
        /// </summary>
        public void CancelMigration()
        {
            if (_worker.IsBusy && !_worker.CancellationPending)
            {
                _worker.CancelAsync();
                LogOutput("Cancellation requested. Waiting for current operation to complete...");
            }
        }
        
        /// <summary>
        /// Handler for the background worker's DoWork event
        /// </summary>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
            // Process each table
            for (_currentTableIndex = 0; _currentTableIndex < _tablesToMigrate.Count; _currentTableIndex++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                
                TableMigrationInfo tableInfo = _tablesToMigrate[_currentTableIndex];
                
                // Update status
                tableInfo.Status = "In Progress";
                worker.ReportProgress(_currentTableIndex * 100 / _tablesToMigrate.Count, 
                    $"Starting migration of table {_currentTableIndex + 1} of {_tablesToMigrate.Count}: {tableInfo.SourceTableName} -> {tableInfo.DestinationTableName}");
                
                try
                {
                    // Migrate the table
                    MigrationResult result = _engine.MigrateTable(
                        tableInfo.SourceTableName,
                        tableInfo.DestinationTableName,
                        tableInfo.ColumnMappings);
                    
                    // Update status
                    tableInfo.Status = result.Success ? "Completed" : "Failed";
                    tableInfo.Result = result;
                    
                    // Report progress
                    worker.ReportProgress((_currentTableIndex + 1) * 100 / _tablesToMigrate.Count, 
                        $"Table {tableInfo.SourceTableName}: {tableInfo.Status}. {result}");
                    
                    // Log errors
                    if (!result.Success && result.Errors.Count > 0)
                    {
                        foreach (string error in result.Errors)
                        {
                            worker.ReportProgress(0, $"  Error: {error}");
                        }
                    }
                    
                    // Break on failure if not continuing on error
                    if (!result.Success && !_continueOnError)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    // Update status
                    tableInfo.Status = "Failed";
                    tableInfo.Result = new MigrationResult
                    {
                        Success = false,
                        ErrorMessage = ex.Message
                    };
                    
                    // Report error
                    worker.ReportProgress(0, $"Error migrating table {tableInfo.SourceTableName}: {ex.Message}");
                    
                    // Break on failure if not continuing on error
                    if (!_continueOnError)
                    {
                        break;
                    }
                }
            }
        }
        
        /// <summary>
        /// Handler for the background worker's ProgressChanged event
        /// </summary>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string status = e.UserState as string;
            if (!string.IsNullOrEmpty(status))
            {
                LogOutput(status);
            }
        }
        
        /// <summary>
        /// Handler for the background worker's RunWorkerCompleted event
        /// </summary>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                LogOutput("Migration was cancelled.");
            }
            else if (e.Error != null)
            {
                LogOutput($"Migration failed with error: {e.Error.Message}");
                MessageBox.Show($"Migration failed: {e.Error.Message}", "Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Count successes and failures
                int successCount = 0;
                int failureCount = 0;
                int totalRows = 0;
                int migratedRows = 0;
                
                foreach (TableMigrationInfo tableInfo in _tablesToMigrate)
                {
                    if (tableInfo.Result != null)
                    {
                        if (tableInfo.Result.Success)
                        {
                            successCount++;
                            totalRows += tableInfo.Result.TotalRows;
                            migratedRows += tableInfo.Result.MigratedRows;
                        }
                        else
                        {
                            failureCount++;
                        }
                    }
                }
                
                // Log summary
                LogOutput("\nMigration Summary:");
                LogOutput($"Tables successfully migrated: {successCount} of {_tablesToMigrate.Count}");
                LogOutput($"Tables failed: {failureCount}");
                LogOutput($"Total rows migrated: {migratedRows} of {totalRows}");
                
                // Show success message if everything succeeded
                if (failureCount == 0 && successCount > 0)
                {
                    MessageBox.Show($"Migration completed successfully. {successCount} tables and {migratedRows} rows were migrated.", 
                        "Migration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Show warning if some tables failed
                else if (failureCount > 0 && successCount > 0)
                {
                    MessageBox.Show($"Migration completed with warnings. {successCount} tables succeeded and {failureCount} tables failed.", 
                        "Migration Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // Show error if everything failed
                else if (failureCount > 0 && successCount == 0)
                {
                    MessageBox.Show($"Migration failed. All {failureCount} tables failed to migrate.", 
                        "Migration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        /// <summary>
        /// Handler for the engine's ProgressUpdated event
        /// </summary>
        private void Engine_ProgressUpdated(object sender, MigrationProgressEventArgs e)
        {
            if (_worker.IsBusy)
            {
                _worker.ReportProgress(e.PercentComplete, e.Status);
            }
        }
        
        /// <summary>
        /// Logs output to the console and the rich text box
        /// </summary>
        private void LogOutput(string message)
        {
            // Log to console
            Tools.WriteSysLog(message);
            
            // Log to rich text box if available
            if (_outputTextBox != null && !string.IsNullOrEmpty(message))
            {
                // Invoke on UI thread if necessary
                if (_outputTextBox.InvokeRequired)
                {
                    _outputTextBox.Invoke(new Action<string>(LogOutput), message);
                }
                else
                {
                    _outputTextBox.AppendText(message + "\n");
                    _outputTextBox.ScrollToCaret();
                }
            }
        }
    }
    
    /// <summary>
    /// Information about a table migration
    /// </summary>
    public class TableMigrationInfo
    {
        public string SourceTableName { get; set; }
        public string DestinationTableName { get; set; }
        public Dictionary<string, string> ColumnMappings { get; set; }
        public string Status { get; set; }
        public MigrationResult Result { get; set; }
    }
}