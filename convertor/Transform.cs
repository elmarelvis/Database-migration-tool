using ComponentFactory.Krypton.Toolkit;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using ReadWriteFile;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Convertor
{
    public partial class Transform : DBConvertor
    {
        public Transform()
        {
            InitializeComponent();
        }

        private void Transform_Load(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            MaincomboBox1.Visible = false;
            MaincomboBox2.Visible = false;
            kryptonLabel11.Visible = false;
            kryptonLabel12.Visible = false;
            switch (MaincomboBox1.SelectedIndex)
            {


                //oralce DB connection
                case 0:
                    {
                        Config.connectionString = null;
                        Config.connectionString = "Data Source=" + $"{Config.DB};User Id=" + $"{Config.User};Password=" + $"{ Config.password};";
                        //connetionString = "Data Source=NEWDAMDB;User Id=PQDT_TEST;Password=pqdt;";

                        //backgroundWorker1.WorkerReportsProgress = true;
                        try
                        {

                            OracleConnection cnn = new OracleConnection(Config.connectionString);
                            TransformView.ClearSelection();
                            for (int i = 0; i < TransformView.Columns.Count; i++)
                                TransformView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;


                            OracleCommand command = new OracleCommand();
                            command.CommandText = "SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME=:tableName";
command.Parameters.Add(new OracleParameter("tableName", Config.choosenDbTableOfDataView1));
                            command.Connection = cnn;
                            cnn.Open();
                            OracleDataReader dr = command.ExecuteReader();
                            int index = 0;
                            int index2 = 0;
                            try
                            {
                                while (dr.Read())
                                {
                                    TransformView.Rows.Add();
                                    TransformView["Source", index].Value = dr.GetValue(0);
                                    index++;
                                    Config.indexOfRowsInTableViewTransform++;
                                }
                            }
                            finally
                            {
                                dr.Close();
                            }
                            command.CommandText = "SELECT DATA_TYPE FROM ALL_TAB_COLUMNS WHERE TABLE_NAME=:tableName";
command.Parameters.Clear();
command.Parameters.Add(new OracleParameter("tableName", Config.choosenDbTableOfDataView1));
                            command.Connection = cnn;
                            dr = command.ExecuteReader();
                            try
                            {


                                while (dr.Read())
                                {
                                    //while (Config.indexOfRowsInTableViewTransform <= index)
                                    //{
                                    //    TransformView.Rows.Add();
                                    //    Config.indexOfRowsInTableViewTransform++;
                                    //}

                                    TransformView["Source_Type", index2].Value = "<" + dr.GetValue(0) + ">";
                                    //Destination.Items.Add(dr.GetValue(0).ToString());
                                    index2++;

                                }
                            }
                            finally
                            {
                                dr.Close();
                            }

                            cnn.Close();
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(ex.Message, "Error with Source Database");
                        }
                        break;
                    }
                //    //postgre DB1 connection
                case 1:
                    {
                        Config.connectionString = null;
                        Config.connectionString = "Server=" + $"{Config.Server}; Port=" + $"{Config.ServerPort};User Id=" + $"{Config.User};Password=" + $"{ Config.password};Database=" + $"{Config.DB}";
                        //connetionString = "Server = 192.168.1.33; Port = 5432; User Id = postgres; Password = egc6720EGC; Database = Enesy";
                        int index = 0;
                        int index2 = 0;
                        try
                        {
                            var cnn = new NpgsqlConnection(Config.connectionString);
                            cnn.Open();
                            string sql = "SET search_path = 'enesy';" +
                                " select column_name from information_schema.columns " +
                                " where table_schema = 'Enesy' and  table_name = '" + $"{ Config.choosenDbTableOfDataView1}'";
                            string sql2 = "SET search_path = 'enesy';" +
                                " select data_type from information_schema.columns " +
                                " where table_schema = 'Enesy' and  table_name = '" + $"{ Config.choosenDbTableOfDataView1}'";
                            NpgsqlCommand cmd;
                            cmd = new NpgsqlCommand(sql, cnn);
                            NpgsqlDataReader npgsqlDataReader;
                            npgsqlDataReader = cmd.ExecuteReader();
                            cmd = new NpgsqlCommand(sql2, cnn);

                            try
                            {
                                while (npgsqlDataReader.Read())
                                {

                                    TransformView.Rows.Add();

                                    TransformView["Source", index].Value = npgsqlDataReader.GetValue(0);
                                    index++;
                                    Config.indexOfRowsInTableViewTransform++;

                                }
                            }
                            finally
                            {
                                npgsqlDataReader.Close();
                            }
                            try
                            {
                                npgsqlDataReader = cmd.ExecuteReader();
                                while (npgsqlDataReader.Read())
                                {

                                    //TransformView.Rows.Add();

                                    TransformView["Source_Type", index2].Value = "<" + npgsqlDataReader.GetValue(0) + ">";
                                    index2++;
                                    //Config.indexOfRowsInTableViewTransform++;

                                }
                            }
                            finally
                            {
                                npgsqlDataReader.Close();
                            }
                            cnn.Close();
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
                        }
                        break;
                    }
                //ms sql DB1 connection
                case 2:
                    {
                        Config.connectionString = null;
                        Config.connectionString = "Server=" + $"{Config.Server};Database=" + $"{Config.DB};User Id=" + $"{Config.User};Password=" + $"{ Config.password};";
                        //connetionString = "Server=192.168.1.33\\EGC_MSSQL;Database=enesy;User Id=sa;Password=egc6720EGC;";
                        int index = 0;
                        int index2 = 0;


                        SqlConnection cnn;
                        try
                        {
                            cnn = new SqlConnection(Config.connectionString);
                            cnn.Open();
                            SqlCommand cmd;
                            SqlDataReader reader;
                            String sql = "select COLUMN_NAME  from enesy.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + $"{ Config.choosenDbTableOfDataView1}'";
                            String sql2 = "select data_type from enesy.information_schema.columns where table_name = '" + $"{ Config.choosenDbTableOfDataView1}'";
                            cmd = new SqlCommand(sql, cnn);
                            reader = cmd.ExecuteReader();
                            cmd = new SqlCommand(sql2, cnn);
                            try
                            {
                                while (reader.Read())
                                {

                                    TransformView.Rows.Add();

                                    TransformView["Source", index].Value = reader.GetValue(0);
                                    index++;
                                    Config.indexOfRowsInTableViewTransform++;

                                }

                            }
                            finally
                            {
                                reader.Close();
                            }
                            try
                            {
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {

                                    //TransformView.Rows.Add();

                                    TransformView["Source_Type", index2].Value = "<" + reader.GetValue(0) + ">";
                                    index2++;
                                    //Config.indexOfRowsInTableViewTransform++;

                                }
                            }
                            finally
                            {
                                reader.Close();
                            }

                            cnn.Close();
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
                        }
                        break;
                    }
                default:
                    break;
            }
            switch (MaincomboBox2.SelectedIndex)
            {


                //oralce DB2 connection
                case 0:
                    {
                        Config.connectionString2 = null;
                        Config.connectionString2 = "Data Source=" + $"{Config.DB2};User Id=" + $"{Config.User2};Password=" + $"{ Config.password2};";
                        //connetionString = "Data Source=NEWDAMDB;User Id=PQDT_TEST;Password=pqdt;";


                        try
                        {
                            int indexValueDestinationType = 1;
                            string[] valueDestinationType = new string[0];
                            int indexKeyDestination = 1;
                            string[] keyDestination = new string[0];
                            // slovnik s klicem "destination" a value s "destination_type" pro aktualni prehled v transform pri zmene dataGridView radku ve sloupci "destination"
                            Config.DestinationTypeActual = new System.Collections.Generic.Dictionary<string, string>();
                            OracleConnection cnn = new OracleConnection(Config.connectionString2);
                            OracleCommand command = new OracleCommand();
                            command.CommandText = "SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME='" + $"{Config.choosenDbTableOfDataView2}'";

                            command.Connection = cnn;
                            cnn.Open();
                            OracleDataReader dr = command.ExecuteReader();
                            int index = 0;
                            int index2 = 0;
                            //Destination.Items.Clear();
                            try
                            {


                                while (dr.Read())
                                {
                                    while (Config.indexOfRowsInTableViewTransform <= index)
                                    {
                                        TransformView.Rows.Add();
                                        Config.indexOfRowsInTableViewTransform++;
                                    }
                                    if (keyDestination.Length < indexKeyDestination)
                                    {
                                        Array.Resize(ref keyDestination, keyDestination.Length + 1);
                                        indexKeyDestination++;
                                    }
                                    keyDestination[indexKeyDestination - 2] = dr.GetValue(0).ToString();
                                    TransformView["Destination", index].Value = dr.GetValue(0);
                                    Destination.Items.Add(dr.GetValue(0).ToString());
                                    index++;

                                }
                            }
                            finally
                            {
                                dr.Close();
                            }
                            command.CommandText = "SELECT DATA_TYPE FROM ALL_TAB_COLUMNS WHERE TABLE_NAME='" + $"{Config.choosenDbTableOfDataView2}'";
                            command.Connection = cnn;
                            dr = command.ExecuteReader();
                            try
                            {


                                while (dr.Read())
                                {
                                    //while (Config.indexOfRowsInTableViewTransform <= index)
                                    //{
                                    //    TransformView.Rows.Add();
                                    //    Config.indexOfRowsInTableViewTransform++;
                                    //}
                                    if (valueDestinationType.Length < indexValueDestinationType)
                                    {
                                        Array.Resize(ref valueDestinationType, valueDestinationType.Length + 1);
                                        indexValueDestinationType++;

                                    }
                                    valueDestinationType[indexValueDestinationType - 2] = "<" + dr.GetValue(0).ToString() + ">";
                                    TransformView["Destination_Type", index2].Value = "<" + dr.GetValue(0) + ">";
                                    //Destination.Items.Add(dr.GetValue(0).ToString());
                                    index2++;

                                }
                            }
                            finally
                            {
                                dr.Close();
                            }
                            cnn.Close();
                            for (int i = 0; i < keyDestination.Length; i++)
                            {
                                Config.DestinationTypeActual.Add(keyDestination[i], valueDestinationType[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
                        }
                        break;

                    }
                //postgre DB connection
                case 1:
                    {
                        int indexValueDestinationType = 1;
                        string[] valueDestinationType = new string[0];
                        int indexKeyDestination = 1;
                        string[] keyDestination = new string[0];
                        // slovnik s klicem "destination" a value s "destination_type" pro aktualni prehled v transform pri zmene dataGridView radku ve sloupci "destination"
                        Config.DestinationTypeActual = new System.Collections.Generic.Dictionary<string, string>();
                        Config.connectionString2 = null;
                        Config.connectionString2 = "Server = " + $"{Config.Server2}; Port = " +
                            $"{Config.ServerPort2};User Id=" + $"{Config.User2};Password=" +
                            $"{ Config.password2};Database=" + $"{Config.DB2}";
                        //connetionString = " Server = 192.168.1.33; Port = 5432; User Id = postgres; Password = egc6720EGC; Database = Enesy";
                        int index = 0;
                        int index2 = 0;
                        //Destination.Items.Clear();
                        try
                        {
                            var cnn = new NpgsqlConnection(Config.connectionString2);
                            cnn.Open();
                            string sql = "SET search_path = 'enesy';" +
                                " select column_name from information_schema.columns " +
                                " where table_schema = 'Enesy' and  table_name = '" + $"{ Config.choosenDbTableOfDataView2}'";
                            string sql2 = "SET search_path = 'enesy';" +
                                " select data_type from information_schema.columns " +
                                " where table_schema = 'Enesy' and  table_name = '" + $"{ Config.choosenDbTableOfDataView2}'";
                            NpgsqlCommand cmd;
                            cmd = new NpgsqlCommand(sql, cnn);
                            NpgsqlDataReader npgsqlDataReader;
                            npgsqlDataReader = cmd.ExecuteReader();
                            cmd = new NpgsqlCommand(sql2, cnn);

                            try
                            {
                                while (npgsqlDataReader.Read())
                                {
                                    while (Config.indexOfRowsInTableViewTransform <= index)
                                    {
                                        TransformView.Rows.Add();
                                        Config.indexOfRowsInTableViewTransform++;

                                    }
                                    if (keyDestination.Length < indexKeyDestination)
                                    {
                                        Array.Resize(ref keyDestination, keyDestination.Length + 1);
                                        indexKeyDestination++;
                                    }
                                    keyDestination[indexKeyDestination - 2] = npgsqlDataReader.GetValue(0).ToString();
                                    TransformView["Destination", index].Value = npgsqlDataReader.GetValue(0);
                                    Destination.Items.Add(npgsqlDataReader.GetValue(0).ToString());
                                    index++;
                                }
                            }
                            finally
                            {
                                npgsqlDataReader.Close();
                            }
                            try
                            {
                                npgsqlDataReader = cmd.ExecuteReader();
                                while (npgsqlDataReader.Read())
                                {

                                    //TransformView.Rows.Add();
                                    if (valueDestinationType.Length < indexValueDestinationType)
                                    {
                                        Array.Resize(ref valueDestinationType, valueDestinationType.Length + 1);
                                        indexValueDestinationType++;

                                    }
                                    valueDestinationType[indexValueDestinationType - 2] = "<" + npgsqlDataReader.GetValue(0).ToString() + ">";
                                    TransformView["Destination_Type", index2].Value = "<" + npgsqlDataReader.GetValue(0) + ">";
                                    index2++;
                                    //Config.indexOfRowsInTableViewTransform++;

                                }
                            }
                            finally
                            {
                                npgsqlDataReader.Close();
                            }
                            cnn.Close();
                            for (int i = 0; i < keyDestination.Length; i++)
                            {
                                Config.DestinationTypeActual.Add(keyDestination[i], valueDestinationType[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
                        }
                        break;
                    }
                //ms sql DB connection
                case 2:
                    {
                        int indexValueDestinationType = 1;
                        string[] valueDestinationType = new string[0];
                        int indexKeyDestination = 1;
                        string[] keyDestination = new string[0];
                        // slovnik s klicem "destination" a value s "destination_type" pro aktualni prehled v transform pri zmene dataGridView radku ve sloupci "destination"
                        Config.DestinationTypeActual = new System.Collections.Generic.Dictionary<string, string>();
                        Config.connectionString2 = null;
                        Config.connectionString2 = "Server=" + $"{Config.Server2};Database=" + $"{Config.DB2};User Id=" + $"{Config.User2};Password=" + $"{ Config.password2};";
                        //connetionString = "Server=192.168.1.33\\EGC_MSSQL;Database=enesy;User Id=sa;Password=egc6720EGC;";
                        int index = 0;
                        int index2 = 0;
                        SqlConnection cnn;
                        try
                        {
                            cnn = new SqlConnection(Config.connectionString2);
                            cnn.Open();
                            SqlCommand cmd;
                            SqlDataReader reader;
                            String sql = "select COLUMN_NAME  from enesy.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + $"{ Config.choosenDbTableOfDataView2}'";
                            String sql2 = "select data_type from enesy.information_schema.columns where table_name = '" + $"{ Config.choosenDbTableOfDataView2}'";

                            cmd = new SqlCommand(sql, cnn);
                            reader = cmd.ExecuteReader();
                            cmd = new SqlCommand(sql2, cnn);

                            try
                            {
                                while (reader.Read())
                                {

                                    while (Config.indexOfRowsInTableViewTransform <= index)
                                    {
                                        TransformView.Rows.Add();
                                        Config.indexOfRowsInTableViewTransform++;

                                    }
                                    if (keyDestination.Length < indexKeyDestination)
                                    {
                                        Array.Resize(ref keyDestination, keyDestination.Length + 1);
                                        indexKeyDestination++;
                                    }
                                    keyDestination[indexKeyDestination - 2] = reader.GetValue(0).ToString();

                                    TransformView["Destination", index].Value = reader.GetValue(0);
                                    Destination.Items.Add(reader.GetValue(0).ToString());
                                    index++;
                                }
                            }
                            finally
                            {
                                reader.Close();
                            }
                            try
                            {
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {

                                    //TransformView.Rows.Add();
                                    if (valueDestinationType.Length < indexValueDestinationType)
                                    {
                                        Array.Resize(ref valueDestinationType, valueDestinationType.Length + 1);
                                        indexValueDestinationType++;

                                    }
                                    valueDestinationType[indexValueDestinationType - 2] = "<" + reader.GetValue(0).ToString() + ">";
                                    TransformView["Destination_Type", index2].Value = "<" + reader.GetValue(0) + ">";
                                    index2++;
                                    //Config.indexOfRowsInTableViewTransform++;

                                }
                            }
                            finally
                            {
                                reader.Close();
                            }
                            cnn.Close();
                            for (int i = 0; i < keyDestination.Length; i++)
                            {
                                Config.DestinationTypeActual.Add(keyDestination[i], valueDestinationType[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            KryptonMessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
                        }
                        break;
                    }
                default:
                    break;
            }

        }

        private void OkTransForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseTransForm_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void TransformView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int index = -1;
            if (TransformView.Focused == true && Config.DestinationTypeActual != null )
            {
                foreach (var item in Config.DestinationTypeActual)
                {
                    index++;
                    //if (item.Key != TransformView["Destination", index].Value.ToString())
                    //{
                        foreach (var item2 in Config.DestinationTypeActual) 
                        {
                            if (item2.Key == TransformView["Destination", index].Value.ToString())
                            {
                                TransformView["Destination_Type", index].Value = item2.Value;
                                break;
                            }
                    //    }
                    }
                }
            }

        }
    }

}