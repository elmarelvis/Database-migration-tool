using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ReadWriteFile;

namespace Convertor
{
    /// <summary>
    /// A user control that provides a button to execute migrations
    /// </summary>
    public class ExecuteMigrationButton : KryptonButton
    {
        public ExecuteMigrationButton()
        {
            this.Text = "Execute Migration";
            this.Size = new System.Drawing.Size(150, 34);
            this.Click += ExecuteMigrationButton_Click;
        }

        private void ExecuteMigrationButton_Click(object sender, EventArgs e)
        {
            // Check if tables are selected
            if (Config.actualListOfDbs == null || Config.actualListOfDbs.Length == 0)
            {
                KryptonMessageBox.Show("No tables selected for migration. Please select at least one table.", 
                    "Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            try
            {
                // Create column mappings for each table
                foreach (string sourceTable in Config.actualListOfDbs)
                {
                    if (string.IsNullOrEmpty(sourceTable))
                        continue;
                    
                    // For this example, we'll use the same name for source and destination
                    string destTable = sourceTable;
                    
                    // Create a default mapping where each column maps to itself
                    Dictionary<string, string> columnMappings = GetDefaultColumnMappings(sourceTable);
                    
                    // Store the mappings
                    StoreColumnMappings(sourceTable, destTable, columnMappings);
                }
                
                // Launch the migration executor
                using (MigrationExecutor executor = new MigrationExecutor())
                {
                    executor.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"Error launching migration executor: {ex.Message}", 
                    "Migration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Tools.WriteSysLog($"Error launching migration executor: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Gets default column mappings for a table, where each column maps to itself
        /// </summary>
        private Dictionary<string, string> GetDefaultColumnMappings(string tableName)
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            
            try
            {
                switch (Config.choosen_Db)
                {
                    case 0: // Oracle
                        using (var conn = new Oracle.ManagedDataAccess.Client.OracleConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME=:tableName";
                                cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("tableName", tableName));
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName;
                                    }
                                }
                            }
                        }
                        break;
                        
                    case 1: // PostgreSQL
                        using (var conn = new Npgsql.NpgsqlConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT column_name FROM information_schema.columns " +
                                                 "WHERE table_schema = 'Enesy' AND table_name = @tableName";
                                cmd.Parameters.AddWithValue("@tableName", tableName);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName;
                                    }
                                }
                            }
                        }
                        break;
                        
                    case 2: // MSSQL
                        using (var conn = new System.Data.SqlClient.SqlConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
                                                 "WHERE TABLE_NAME = @tableName";
                                cmd.Parameters.AddWithValue("@tableName", tableName);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName;
                                    }
                                }
                            }
                        }
                        break;
                        
                    case 3: // MySQL
                        using (var conn = new MySql.Data.MySqlClient.MySqlConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
                                                 "WHERE TABLE_NAME = @tableName";
                                cmd.Parameters.AddWithValue("@tableName", tableName);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog($"Error getting column mappings for {tableName}: {ex.Message}");
            }
            
            return mappings;
        }
        
        /// <summary>
        /// Stores column mappings for later use by the migration engine
        /// </summary>
        private void StoreColumnMappings(string sourceTable, string destTable, Dictionary<string, string> mappings)
        {
            try
            {
                // Initialize dictionary if needed
                if (Config.DictionaryStorage == null)
                {
                    Config.DictionaryStorage = new Dictionary<string, object>();
                }
                
                // Create key for table pair
                string mappingKey = $"{sourceTable}_{destTable}";
                
                // Store mappings
                Config.DictionaryStorage[mappingKey] = mappings;
                
                Tools.WriteSysLog($"Stored {mappings.Count} column mappings for {mappingKey}");
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog($"Error storing column mappings: {ex.Message}");
            }
        }
    }
}