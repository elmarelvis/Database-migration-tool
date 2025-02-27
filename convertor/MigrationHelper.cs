using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ReadWriteFile;

namespace Convertor
{
    /// <summary>
    /// Helper class for database migration tasks
    /// </summary>
    public static class MigrationHelper
    {
        /// <summary>
        /// Creates and shows a migration button on a form
        /// </summary>
        public static void AddMigrationButtonToForm(KryptonForm form)
        {
            try
            {
                // Create migration button
                ExecuteMigrationButton migrateButton = new ExecuteMigrationButton();
                migrateButton.Name = "migrateButton";
                migrateButton.Location = new System.Drawing.Point(20, 20); // Position as needed
                
                // Add to form
                form.Controls.Add(migrateButton);
                migrateButton.BringToFront();
                
                // Log
                Tools.WriteSysLog("Added migration button to form");
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog($"Error adding migration button: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Initializes storage for migration configurations
        /// </summary>
        public static void Initialize()
        {
            try
            {
                // Initialize dictionary storage if needed
                if (Config.DictionaryStorage == null)
                {
                    Config.DictionaryStorage = new Dictionary<string, object>();
                }
                
                // Log
                Tools.WriteSysLog("Initialized migration helper");
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog($"Error initializing migration helper: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Gets column mappings for a table pair
        /// </summary>
        public static Dictionary<string, string> GetColumnMappings(string sourceTable, string destTable)
        {
            // Create key for table pair
            string mappingKey = $"{sourceTable}_{destTable}";
            
            // Check if mappings exist
            if (Config.DictionaryStorage != null && 
                Config.DictionaryStorage.ContainsKey(mappingKey) && 
                Config.DictionaryStorage[mappingKey] is Dictionary<string, string>)
            {
                return (Dictionary<string, string>)Config.DictionaryStorage[mappingKey];
            }
            
            // Return empty dictionary if no mappings found
            return new Dictionary<string, string>();
        }
        
        /// <summary>
        /// Shows the migration dialog for a specific table
        /// </summary>
        public static void ShowMigrationDialog(string sourceTable, string destTable)
        {
            try
            {
                // Get column mappings
                Dictionary<string, string> mappings = GetColumnMappings(sourceTable, destTable);
                
                // Create a new migration executor
                using (MigrationExecutor executor = new MigrationExecutor())
                {
                    executor.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Error showing migration dialog: {ex.Message}", 
                    "Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Tools.WriteSysLog($"Error showing migration dialog: {ex.Message}");
            }
        }
    }
}