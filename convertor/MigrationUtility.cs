using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using ReadWriteFile;

namespace Convertor
{
    /// <summary>
    /// Utility class for integrating the migration functionality with the existing UI
    /// </summary>
    public static class MigrationUtility
    {
        /// <summary>
        /// Launches the migration executor form
        /// </summary>
        public static void LaunchMigrationExecutor()
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
        /// Gets column mappings for a table
        /// </summary>
        public static Dictionary<string, string> GetColumnMappings(string sourceTable, string destinationTable)
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            
            try
            {
                // Add code to retrieve column mappings from the Transform form or other UI components
                // This is a placeholder - in a real implementation, you'd retrieve the mappings from the UI
                
                switch (Config.choosen_Db)
                {
                    case DataMigrationEngine.DB_TYPE_ORACLE:
                        // Get Oracle column mappings
                        using (Oracle.ManagedDataAccess.Client.OracleConnection conn = 
                               new Oracle.ManagedDataAccess.Client.OracleConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (Oracle.ManagedDataAccess.Client.OracleCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME=:tableName";
                                cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("tableName", sourceTable));
                                using (Oracle.ManagedDataAccess.Client.OracleDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName; // Map to same name for simplicity
                                    }
                                }
                            }
                        }
                        break;
                        
                    case DataMigrationEngine.DB_TYPE_POSTGRESQL:
                        // Get PostgreSQL column mappings
                        using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (Npgsql.NpgsqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT column_name FROM information_schema.columns " +
                                                 "WHERE table_schema = 'Enesy' AND table_name = @tableName";
                                cmd.Parameters.AddWithValue("@tableName", sourceTable);
                                using (Npgsql.NpgsqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName; // Map to same name for simplicity
                                    }
                                }
                            }
                        }
                        break;
                        
                    case DataMigrationEngine.DB_TYPE_MSSQL:
                        // Get MSSQL column mappings
                        using (System.Data.SqlClient.SqlConnection conn = 
                               new System.Data.SqlClient.SqlConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
                                                 "WHERE TABLE_NAME = @tableName";
                                cmd.Parameters.AddWithValue("@tableName", sourceTable);
                                using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName; // Map to same name for simplicity
                                    }
                                }
                            }
                        }
                        break;
                        
                    case DataMigrationEngine.DB_TYPE_MYSQL:
                        // Get MySQL column mappings
                        using (MySql.Data.MySqlClient.MySqlConnection conn = 
                               new MySql.Data.MySqlClient.MySqlConnection(Config.connectionString))
                        {
                            conn.Open();
                            using (MySql.Data.MySqlClient.MySqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS " +
                                                 "WHERE TABLE_NAME = @tableName";
                                cmd.Parameters.AddWithValue("@tableName", sourceTable);
                                using (MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string columnName = reader.GetString(0);
                                        mappings[columnName] = columnName; // Map to same name for simplicity
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog($"Error getting column mappings for {sourceTable}: {ex.Message}");
            }
            
            return mappings;
        }
        
        /// <summary>
        /// Adds a migration button to the form
        /// </summary>
        public static void AddMigrationButton(KryptonForm form)
        {
            try
            {
                // Create the migration button
                KryptonButton migrateButton = new KryptonButton();
                migrateButton.Name = "migrateButton";
                migrateButton.Text = "Execute Migration";
                migrateButton.Location = new System.Drawing.Point(650, 554); // Adjust position as needed
                migrateButton.Size = new System.Drawing.Size(120, 25);
                migrateButton.Click += (sender, e) => LaunchMigrationExecutor();
                
                // Add the button to the form
                form.Controls.Add(migrateButton);
                migrateButton.BringToFront();
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog($"Error adding migration button: {ex.Message}");
            }
        }
    }
}