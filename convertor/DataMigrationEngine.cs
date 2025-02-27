using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using ReadWriteFile;

namespace Convertor
{
    /// <summary>
    /// Core engine that handles the actual data migration between different database systems.
    /// Supports Oracle, PostgreSQL, MS SQL Server, and MySQL databases.
    /// </summary>
    public class DataMigrationEngine
    {
        // Constants for database types
        public const int DB_TYPE_ORACLE = 0;
        public const int DB_TYPE_POSTGRESQL = 1;
        public const int DB_TYPE_MSSQL = 2;
        public const int DB_TYPE_MYSQL = 3;

        // Migration settings
        private bool _useTransaction = true;
        private int _batchSize = 1000;
        private bool _continueOnError = false;
        private List<string> _migrationErrors = new List<string>();
        private Dictionary<string, string> _columnMappings = new Dictionary<string, string>();

        // Event for progress reporting
        public event EventHandler<MigrationProgressEventArgs> ProgressUpdated;

        public DataMigrationEngine()
        {
            // Constructor
        }

        /// <summary>
        /// Migrates data from source table to destination table
        /// </summary>
        /// <param name="sourceTableName">Name of the source table</param>
        /// <param name="destTableName">Name of the destination table</param>
        /// <param name="columnMappings">Dictionary mapping source columns to destination columns</param>
        /// <returns>MigrationResult containing statistics and errors</returns>
        public MigrationResult MigrateTable(string sourceTableName, string destTableName, Dictionary<string, string> columnMappings)
        {
            _columnMappings = columnMappings;
            _migrationErrors.Clear();
            MigrationResult result = new MigrationResult();
            
            try
            {
                // Log the start of migration
                Tools.WriteSysLog($"Starting migration: {sourceTableName} -> {destTableName}");
                
                // Get source connection based on Config.choosen_Db
                using (IDbConnection sourceConnection = GetConnection(Config.choosen_Db))
                {
                    sourceConnection.Open();
                    
                    // Get destination connection based on Config.choosen_Db2
                    using (IDbConnection destConnection = GetConnection(Config.choosen_Db2))
                    {
                        destConnection.Open();
                        
                        // Begin transaction if enabled
                        IDbTransaction destTransaction = null;
                        if (_useTransaction)
                        {
                            destTransaction = destConnection.BeginTransaction();
                        }
                        
                        try
                        {
                            // Read data from source
                            DataTable sourceData = ReadSourceData(sourceConnection, sourceTableName);
                            result.TotalRows = sourceData.Rows.Count;
                            
                            // Fire progress event
                            OnProgressUpdated(0, result.TotalRows, "Reading source data complete");
                            
                            // Write data to destination
                            result.MigratedRows = WriteDestinationData(destConnection, destTransaction, destTableName, sourceData);
                            
                            // Commit transaction if enabled
                            if (_useTransaction && destTransaction != null)
                            {
                                destTransaction.Commit();
                            }
                            
                            // Set success flag
                            result.Success = true;
                            
                            // Log completion
                            Tools.WriteSysLog($"Migration completed successfully: {sourceTableName} -> {destTableName}. Rows: {result.MigratedRows}");
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction if enabled
                            if (_useTransaction && destTransaction != null)
                            {
                                destTransaction.Rollback();
                            }
                            
                            // Log error
                            string errorMessage = $"Migration failed: {ex.Message}";
                            Tools.WriteSysLog(errorMessage);
                            _migrationErrors.Add(errorMessage);
                            
                            // Set error in result
                            result.Success = false;
                            result.ErrorMessage = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                string errorMessage = $"Migration failed: {ex.Message}";
                Tools.WriteSysLog(errorMessage);
                _migrationErrors.Add(errorMessage);
                
                // Set error in result
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            
            // Set errors in result
            result.Errors = _migrationErrors;
            
            return result;
        }

        /// <summary>
        /// Creates a database connection based on the database type
        /// </summary>
        private IDbConnection GetConnection(int dbType)
        {
            switch (dbType)
            {
                case DB_TYPE_ORACLE:
                    string oracleConnString = "Data Source=" + (dbType == Config.choosen_Db ? Config.DB : Config.DB2) +
                                             ";User Id=" + (dbType == Config.choosen_Db ? Config.User : Config.User2) +
                                             ";Password=" + (dbType == Config.choosen_Db ? Config.password : Config.password2) + ";";
                    return new OracleConnection(oracleConnString);
                
                case DB_TYPE_POSTGRESQL:
                    string pgConnString = "Server=" + (dbType == Config.choosen_Db ? Config.Server : Config.Server2) +
                                         ";Port=" + (dbType == Config.choosen_Db ? Config.ServerPort : Config.ServerPort2) +
                                         ";User Id=" + (dbType == Config.choosen_Db ? Config.User : Config.User2) +
                                         ";Password=" + (dbType == Config.choosen_Db ? Config.password : Config.password2) +
                                         ";Database=" + (dbType == Config.choosen_Db ? Config.DB : Config.DB2);
                    return new NpgsqlConnection(pgConnString);
                
                case DB_TYPE_MSSQL:
                    string mssqlConnString = "Server=" + (dbType == Config.choosen_Db ? Config.Server : Config.Server2) +
                                           ";Database=" + (dbType == Config.choosen_Db ? Config.DB : Config.DB2) +
                                           ";User Id=" + (dbType == Config.choosen_Db ? Config.User : Config.User2) +
                                           ";Password=" + (dbType == Config.choosen_Db ? Config.password : Config.password2) + ";";
                    return new SqlConnection(mssqlConnString);
                
                case DB_TYPE_MYSQL:
                    string mysqlConnString = "server=" + (dbType == Config.choosen_Db ? Config.Server : Config.Server2) +
                                          ":" + (dbType == Config.choosen_Db ? Config.ServerPort : Config.ServerPort2) +
                                          ";uid=" + (dbType == Config.choosen_Db ? Config.User : Config.User2) +
                                          ";pwd=" + (dbType == Config.choosen_Db ? Config.password : Config.password2) +
                                          ";database=" + (dbType == Config.choosen_Db ? Config.DB : Config.DB2);
                    return new MySqlConnection(mysqlConnString);
                
                default:
                    throw new ArgumentException("Unsupported database type: " + dbType);
            }
        }

        /// <summary>
        /// Reads data from the source table into a DataTable
        /// </summary>
        private DataTable ReadSourceData(IDbConnection connection, string tableName)
        {
            DataTable result = new DataTable();
            
            // Create a command to select data from the source table
            IDbCommand command = connection.CreateCommand();
            
            // Build column list from mappings
            StringBuilder columnList = new StringBuilder();
            foreach (string sourceColumn in _columnMappings.Keys)
            {
                if (columnList.Length > 0)
                    columnList.Append(", ");
                
                // Add quotes appropriate for the database type
                switch (Config.choosen_Db)
                {
                    case DB_TYPE_ORACLE:
                        columnList.Append("\"").Append(sourceColumn).Append("\"");
                        break;
                    case DB_TYPE_POSTGRESQL:
                        columnList.Append("\"").Append(sourceColumn).Append("\"");
                        break;
                    case DB_TYPE_MSSQL:
                        columnList.Append("[").Append(sourceColumn).Append("]");
                        break;
                    case DB_TYPE_MYSQL:
                        columnList.Append("`").Append(sourceColumn).Append("`");
                        break;
                }
            }
            
            // Build the SQL query
            string sql = $"SELECT {columnList} FROM ";
            
            // Add proper quoting for table name
            switch (Config.choosen_Db)
            {
                case DB_TYPE_ORACLE:
                    sql += $"\"{tableName}\"";
                    break;
                case DB_TYPE_POSTGRESQL:
                    sql += $"\"Enesy\".\"{tableName}\""; // Using hardcoded schema from original code
                    break;
                case DB_TYPE_MSSQL:
                    sql += $"[enesy].[{tableName}]"; // Using hardcoded schema from original code
                    break;
                case DB_TYPE_MYSQL:
                    sql += $"`{tableName}`";
                    break;
            }
            
            command.CommandText = sql;
            
            // Create a data adapter to fill the DataTable
            IDataAdapter adapter = GetDataAdapter(command, Config.choosen_Db);
            adapter.Fill(result);
            
            return result;
        }

        /// <summary>
        /// Creates a data adapter based on the database type
        /// </summary>
        private IDataAdapter GetDataAdapter(IDbCommand command, int dbType)
        {
            switch (dbType)
            {
                case DB_TYPE_ORACLE:
                    return new Oracle.ManagedDataAccess.Client.OracleDataAdapter((OracleCommand)command);
                case DB_TYPE_POSTGRESQL:
                    return new NpgsqlDataAdapter((NpgsqlCommand)command);
                case DB_TYPE_MSSQL:
                    return new SqlDataAdapter((SqlCommand)command);
                case DB_TYPE_MYSQL:
                    return new MySqlDataAdapter((MySqlCommand)command);
                default:
                    throw new ArgumentException("Unsupported database type: " + dbType);
            }
        }

        /// <summary>
        /// Writes data to the destination table
        /// </summary>
        private int WriteDestinationData(IDbConnection connection, IDbTransaction transaction, string tableName, DataTable data)
        {
            int rowsInserted = 0;
            
            // Process data in batches
            for (int startRow = 0; startRow < data.Rows.Count; startRow += _batchSize)
            {
                // Determine end row for current batch
                int endRow = Math.Min(startRow + _batchSize, data.Rows.Count);
                int rowsInBatch = endRow - startRow;
                
                // Fire progress event
                OnProgressUpdated(rowsInserted, data.Rows.Count, $"Processing rows {startRow + 1} to {endRow}");
                
                try
                {
                    // Insert batch
                    InsertBatch(connection, transaction, tableName, data, startRow, rowsInBatch);
                    rowsInserted += rowsInBatch;
                }
                catch (Exception ex)
                {
                    string errorMessage = $"Error inserting batch (rows {startRow + 1} to {endRow}): {ex.Message}";
                    _migrationErrors.Add(errorMessage);
                    Tools.WriteSysLog(errorMessage);
                    
                    if (!_continueOnError)
                        throw;
                }
            }
            
            return rowsInserted;
        }

        /// <summary>
        /// Inserts a batch of rows into the destination table
        /// </summary>
        private void InsertBatch(IDbConnection connection, IDbTransaction transaction, string tableName, DataTable data, int startRow, int rowCount)
        {
            // Build column lists for the INSERT statement
            StringBuilder destColumns = new StringBuilder();
            StringBuilder paramNames = new StringBuilder();
            
            // Maps to store column name mappings
            Dictionary<string, string> columnToParamMap = new Dictionary<string, string>();
            int paramIndex = 0;
            
            // Build column lists
            foreach (var mapping in _columnMappings)
            {
                string sourceColumn = mapping.Key;
                string destColumn = mapping.Value;
                
                if (destColumns.Length > 0)
                {
                    destColumns.Append(", ");
                    paramNames.Append(", ");
                }
                
                // Add quotes appropriate for the destination database type
                switch (Config.choosen_Db2)
                {
                    case DB_TYPE_ORACLE:
                        destColumns.Append("\"").Append(destColumn).Append("\"");
                        paramNames.Append(":p").Append(paramIndex);
                        break;
                    case DB_TYPE_POSTGRESQL:
                        destColumns.Append("\"").Append(destColumn).Append("\"");
                        paramNames.Append("@p").Append(paramIndex);
                        break;
                    case DB_TYPE_MSSQL:
                        destColumns.Append("[").Append(destColumn).Append("]");
                        paramNames.Append("@p").Append(paramIndex);
                        break;
                    case DB_TYPE_MYSQL:
                        destColumns.Append("`").Append(destColumn).Append("`");
                        paramNames.Append("@p").Append(paramIndex);
                        break;
                }
                
                columnToParamMap.Add(sourceColumn, "p" + paramIndex);
                paramIndex++;
            }
            
            // Build the INSERT statement
            string insertSql = "INSERT INTO ";
            
            // Add proper quoting for table name
            switch (Config.choosen_Db2)
            {
                case DB_TYPE_ORACLE:
                    insertSql += $"\"{tableName}\"";
                    break;
                case DB_TYPE_POSTGRESQL:
                    insertSql += $"\"Enesy\".\"{tableName}\""; // Using hardcoded schema from original code
                    break;
                case DB_TYPE_MSSQL:
                    insertSql += $"[enesy].[{tableName}]"; // Using hardcoded schema from original code
                    break;
                case DB_TYPE_MYSQL:
                    insertSql += $"`{tableName}`";
                    break;
            }
            
            insertSql += $" ({destColumns}) VALUES ({paramNames})";
            
            // Process each row in the batch
            for (int i = 0; i < rowCount; i++)
            {
                DataRow row = data.Rows[startRow + i];
                
                // Create a command for the INSERT
                IDbCommand command = connection.CreateCommand();
                command.CommandText = insertSql;
                
                if (transaction != null)
                    command.Transaction = transaction;
                
                // Add parameters for the row values
                foreach (var mapping in _columnMappings)
                {
                    string sourceColumn = mapping.Key;
                    string paramName = columnToParamMap[sourceColumn];
                    
                    // Create parameter with appropriate prefix based on destination database
                    IDbDataParameter param;
                    switch (Config.choosen_Db2)
                    {
                        case DB_TYPE_ORACLE:
                            param = command.CreateParameter();
                            param.ParameterName = ":" + paramName;
                            break;
                        default:
                            param = command.CreateParameter();
                            param.ParameterName = "@" + paramName;
                            break;
                    }
                    
                    // Set parameter value (handle DBNull)
                    param.Value = row[sourceColumn] ?? DBNull.Value;
                    
                    command.Parameters.Add(param);
                }
                
                // Execute the INSERT
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Raises the ProgressUpdated event
        /// </summary>
        protected virtual void OnProgressUpdated(int currentRow, int totalRows, string status)
        {
            ProgressUpdated?.Invoke(this, new MigrationProgressEventArgs
            {
                CurrentRow = currentRow,
                TotalRows = totalRows,
                PercentComplete = totalRows > 0 ? (int)((double)currentRow / totalRows * 100) : 0,
                Status = status
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
        }
    }

    /// <summary>
    /// Event arguments for migration progress updates
    /// </summary>
    public class MigrationProgressEventArgs : EventArgs
    {
        public int CurrentRow { get; set; }
        public int TotalRows { get; set; }
        public int PercentComplete { get; set; }
        public string Status { get; set; }
    }

    /// <summary>
    /// Result of a table migration operation
    /// </summary>
    public class MigrationResult
    {
        public bool Success { get; set; }
        public int TotalRows { get; set; }
        public int MigratedRows { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        
        public override string ToString()
        {
            if (Success)
                return $"Success: Migrated {MigratedRows} of {TotalRows} rows";
            else
                return $"Failed: {ErrorMessage}";
        }
    }
}