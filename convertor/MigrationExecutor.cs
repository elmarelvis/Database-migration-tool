using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReadWriteFile;

namespace Convertor
{
    /// <summary>
    /// Form for executing and monitoring database migrations
    /// </summary>
    public partial class MigrationExecutor : KryptonForm
    {
        private MigrationManager _migrationManager;
        private bool _migrationInProgress = false;
        
        // UI Components
        private KryptonPanel mainPanel;
        private KryptonProgressBar progressBar;
        private KryptonRichTextBox logTextBox;
        private KryptonButton startButton;
        private KryptonButton cancelButton;
        private KryptonButton closeButton;
        private KryptonGroupBox optionsGroupBox;
        private KryptonCheckBox useTransactionCheckBox;
        private KryptonCheckBox continueOnErrorCheckBox;
        private KryptonLabel batchSizeLabel;
        private KryptonNumericUpDown batchSizeUpDown;
        
        public MigrationExecutor()
        {
            InitializeComponent();
            
            // Create migration manager
            _migrationManager = new MigrationManager(logTextBox);
            
            // Load tables from Config
            LoadTablesFromConfig();
        }
        
        /// <summary>
        /// Initializes the form components
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.progressBar = new ComponentFactory.Krypton.Toolkit.KryptonProgressBar();
            this.logTextBox = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.startButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cancelButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.closeButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.optionsGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.useTransactionCheckBox = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.continueOnErrorCheckBox = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.batchSizeLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.batchSizeUpDown = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionsGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optionsGroupBox.Panel)).BeginInit();
            this.optionsGroupBox.Panel.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            
            // mainPanel
            this.mainPanel.Controls.Add(this.progressBar);
            this.mainPanel.Controls.Add(this.logTextBox);
            this.mainPanel.Controls.Add(this.startButton);
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Controls.Add(this.closeButton);
            this.mainPanel.Controls.Add(this.optionsGroupBox);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(800, 600);
            this.mainPanel.TabIndex = 0;
            
            // progressBar
            this.progressBar.Location = new System.Drawing.Point(12, 12);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(776, 24);
            this.progressBar.TabIndex = 0;
            
            // logTextBox
            this.logTextBox.Location = new System.Drawing.Point(12, 42);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(776, 400);
            this.logTextBox.TabIndex = 1;
            this.logTextBox.Text = "Ready to start migration...";
            
            // optionsGroupBox
            this.optionsGroupBox.Location = new System.Drawing.Point(12, 448);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(776, 100);
            this.optionsGroupBox.TabIndex = 2;
            this.optionsGroupBox.Values.Heading = "Migration Options";
            
            // Options inside the group box
            this.optionsGroupBox.Panel.Controls.Add(this.useTransactionCheckBox);
            this.optionsGroupBox.Panel.Controls.Add(this.continueOnErrorCheckBox);
            this.optionsGroupBox.Panel.Controls.Add(this.batchSizeLabel);
            this.optionsGroupBox.Panel.Controls.Add(this.batchSizeUpDown);
            
            // useTransactionCheckBox
            this.useTransactionCheckBox.Checked = true;
            this.useTransactionCheckBox.Location = new System.Drawing.Point(10, 10);
            this.useTransactionCheckBox.Name = "useTransactionCheckBox";
            this.useTransactionCheckBox.Size = new System.Drawing.Size(200, 20);
            this.useTransactionCheckBox.TabIndex = 0;
            this.useTransactionCheckBox.Values.Text = "Use Transaction";
            
            // continueOnErrorCheckBox
            this.continueOnErrorCheckBox.Location = new System.Drawing.Point(10, 40);
            this.continueOnErrorCheckBox.Name = "continueOnErrorCheckBox";
            this.continueOnErrorCheckBox.Size = new System.Drawing.Size(200, 20);
            this.continueOnErrorCheckBox.TabIndex = 1;
            this.continueOnErrorCheckBox.Values.Text = "Continue On Error";
            
            // batchSizeLabel
            this.batchSizeLabel.Location = new System.Drawing.Point(250, 10);
            this.batchSizeLabel.Name = "batchSizeLabel";
            this.batchSizeLabel.Size = new System.Drawing.Size(100, 20);
            this.batchSizeLabel.TabIndex = 2;
            this.batchSizeLabel.Values.Text = "Batch Size:";
            
            // batchSizeUpDown
            this.batchSizeUpDown.Location = new System.Drawing.Point(350, 10);
            this.batchSizeUpDown.Name = "batchSizeUpDown";
            this.batchSizeUpDown.Size = new System.Drawing.Size(100, 22);
            this.batchSizeUpDown.TabIndex = 3;
            this.batchSizeUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            this.batchSizeUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.batchSizeUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            
            // startButton
            this.startButton.Location = new System.Drawing.Point(12, 554);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(150, 34);
            this.startButton.TabIndex = 3;
            this.startButton.Values.Text = "Start Migration";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            
            // cancelButton
            this.cancelButton.Enabled = false;
            this.cancelButton.Location = new System.Drawing.Point(178, 554);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(150, 34);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Values.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            
            // closeButton
            this.closeButton.Location = new System.Drawing.Point(638, 554);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(150, 34);
            this.closeButton.TabIndex = 5;
            this.closeButton.Values.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            
            // MigrationExecutor
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.mainPanel);
            this.Name = "MigrationExecutor";
            this.Text = "Database Migration Executor";
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.optionsGroupBox.Panel)).EndInit();
            this.optionsGroupBox.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.optionsGroupBox)).EndInit();
            this.optionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        
        /// <summary>
        /// Loads tables and column mappings from Config
        /// </summary>
        private void LoadTablesFromConfig()
        {
            try
            {
                // Check if there are selected tables
                if (Config.actualListOfDbs == null || Config.actualListOfDbs.Length == 0)
                {
                    logTextBox.Text = "No tables selected for migration. Please select tables from the main window first.";
                    startButton.Enabled = false;
                    return;
                }
                
                logTextBox.Text = "Selected tables for migration:\n";
                
                // Add each table to the migration manager
                foreach (string sourceTable in Config.actualListOfDbs)
                {
                    if (string.IsNullOrEmpty(sourceTable))
                        continue;
                    
                    // For simplicity, we'll use the same name for source and destination
                    // In a real implementation, you'd get the destination table name from the UI mapping
                    string destTable = sourceTable;
                    
                    // Get column mappings from the Transform form
                    // In a real implementation, you'd retrieve the actual mappings from the UI
                    Dictionary<string, string> columnMappings = new Dictionary<string, string>();
                    
                    // Add the table to the migration manager
                    _migrationManager.AddTableToMigrate(sourceTable, destTable, columnMappings);
                    
                    // Log the table
                    logTextBox.AppendText($"- {sourceTable} -> {destTable}\n");
                }
                
                logTextBox.AppendText("\nReady to start migration. Click 'Start Migration' to begin.");
            }
            catch (Exception ex)
            {
                logTextBox.Text = $"Error loading tables: {ex.Message}";
                startButton.Enabled = false;
            }
        }
        
        /// <summary>
        /// Starts the migration process
        /// </summary>
        private void startButton_Click(object sender, EventArgs e)
        {
            if (_migrationInProgress)
                return;
            
            _migrationInProgress = true;
            
            // Update UI
            startButton.Enabled = false;
            cancelButton.Enabled = true;
            closeButton.Enabled = false;
            useTransactionCheckBox.Enabled = false;
            continueOnErrorCheckBox.Enabled = false;
            batchSizeUpDown.Enabled = false;
            
            // Set options
            _migrationManager.SetOptions(
                useTransactionCheckBox.Checked,
                (int)batchSizeUpDown.Value,
                continueOnErrorCheckBox.Checked);
            
            // Start migration
            _migrationManager.StartMigration();
        }
        
        /// <summary>
        /// Cancels the migration process
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (!_migrationInProgress)
                return;
            
            // Cancel migration
            _migrationManager.CancelMigration();
            
            // Update UI
            cancelButton.Enabled = false;
        }
        
        /// <summary>
        /// Closes the form
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (_migrationInProgress)
            {
                DialogResult result = MessageBox.Show(
                    "Migration is in progress. Are you sure you want to close?",
                    "Confirm Close",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                
                if (result == DialogResult.No)
                    return;
                
                // Cancel migration
                _migrationManager.CancelMigration();
            }
            
            this.Close();
        }
    }
}