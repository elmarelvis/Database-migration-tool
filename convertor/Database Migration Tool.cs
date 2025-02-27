using ComponentFactory.Krypton.Toolkit;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using ReadWriteFile;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Convertor
{
    public partial class DBConvertor : KryptonForm
    {

        public DBConvertor()
        {
            InitializeComponent();
        }
        private void valuesAreExactWhatIsInTextBox()
        {

            string[][] poleTextBoxu2 = new string[5][];
            poleTextBoxu2[0] = new string[] { textBoxServer.Text, textBoxServer2.Text };
            poleTextBoxu2[1] = new string[] { textBoxUser2.Text, textBoxUser.Text };
            poleTextBoxu2[2] = new string[] { textBoxPassword.Text, textBoxPassword2.Text };
            poleTextBoxu2[3] = new string[] { textBoxPort2.Text, textBoxPort.Text };
            poleTextBoxu2[4] = new string[] { DBTextBox.Text, DbComboBox2.Text };
            foreach (var item in poleTextBoxu2[0])
            {
                if (item != "")
                {
                    Config.Server = item;
                    break;
                }
            }
            foreach (var item in poleTextBoxu2[1])

            {
                if (item != "")
                {
                    Config.User = item;
                    break;
                }
            }
            foreach (var item in poleTextBoxu2[2])

            {
                if (item != "")
                {
                    Config.password = item;
                    break;
                }
            }
            foreach (var item in poleTextBoxu2[3])

            {
                if (item != "")
                {
                    Config.ServerPort = Convert.ToInt32(item);
                    break;
                }
            }
            foreach (var item in poleTextBoxu2[4])

            {
                if (item != "")
                {
                    Config.DB = item;
                    break;
                }
            }
            string[][] poleTextBoxu = new string[5][];
            poleTextBoxu[0] = new string[] { textBoxServer4.Text, textBoxServer3.Text };
            poleTextBoxu[1] = new string[] { textBoxUser3.Text, textBoxUser4.Text };
            poleTextBoxu[2] = new string[] { textBoxPassword4.Text, textBoxPassword3.Text };
            poleTextBoxu[3] = new string[] { textBoxPort3.Text, textBoxPort4.Text };
            poleTextBoxu[4] = new string[] { DbComboBox3.Text, DB2TextBox.Text };

            foreach (var item in poleTextBoxu[0])
            {
                if (item != "")
                {
                    Config.Server2 = item;
                    break;
                }
            }
            foreach (var item in poleTextBoxu[1])

            {
                if (item != "")
                {
                    Config.User2 = item;
                    break;
                }
            }
            foreach (var item in poleTextBoxu[2])

            {
                if (item != "")
                {
                    Config.password2 = item;
                    break;
                }
            }
            foreach (var item in poleTextBoxu[3])

            {
                if (item != "")
                {
                    Config.ServerPort2 = Convert.ToInt32(item);
                    break;
                }
            }
            foreach (var item in poleTextBoxu[4])

            {
                if (item != "")
                {
                    Config.DB2 = item;
                    break;
                }
            }
            switch (MaincomboBox1.SelectedIndex)
            {
                case 0:
                    Config.choosen_Db = 0;
                    break;
                case 1:
                    Config.choosen_Db = 1;
                    break;
                case 2:
                    Config.choosen_Db = 2;
                    break;
            }
            switch (MaincomboBox2.SelectedIndex)
            {
                case 0:
                    Config.choosen_Db2 = 0;
                    break;
                case 1:
                    Config.choosen_Db2 = 1;
                    break;
                case 2:
                    Config.choosen_Db2 = 2;
                    break;
            }
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



            switch (MaincomboBox1.SelectedIndex)
            {


                case 0:
                    {

                        GroupBox1.Visible = false;
                        groupBox3.Visible = true;
                        textBoxServer2.Text = "";
                        textBoxUser2.Text = "";
                        textBoxPassword2.Text = "";
                        textBoxPort2.Text = "";
                        DBTextBox.Text = "";
                        textBoxServer2.Enabled = false;
                        textBoxPort2.Enabled = false;
                        if (MaincomboBox1.Enabled == false)
                        {
                            Config.choosen_Db = 0;
                        }
                        break;
                    }
                case 1:
                    {
                        GroupBox1.Visible = false;
                        groupBox3.Visible = true;
                        textBoxServer2.Text = "";
                        textBoxUser2.Text = "";
                        textBoxPassword2.Text = "";
                        textBoxPort2.Text = "";
                        DBTextBox.Text = "";
                        textBoxServer2.Enabled = true;
                        textBoxPort2.Enabled = true;
                        if (MaincomboBox1.Enabled == false)
                        {
                            Config.choosen_Db = 1;
                        }
                        break;

                    }
                case 2:
                    {
                        GroupBox1.Visible = false;
                        groupBox3.Visible = true;
                        textBoxServer2.Text = "";
                        textBoxUser2.Text = "";
                        textBoxPassword2.Text = "";
                        textBoxPort2.Text = "";
                        DBTextBox.Text = "";
                        textBoxServer2.Enabled = true;
                        textBoxPort2.Enabled = false;
                        if (MaincomboBox1.Enabled == false)
                        {
                            Config.choosen_Db = 2;
                        }
                        break;


                    }
                case 3:
                    {
                        GroupBox1.Visible = true;
                        groupBox3.Visible = false;
                        textBoxServer.Text = "";
                        textBoxUser.Text = "";
                        textBoxPassword.Text = "";
                        textBoxPort.Text = "";
                        DbComboBox2.Text = "";
                        if (MaincomboBox1.Enabled == false)
                        {
                            Config.choosen_Db = 3;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (MaincomboBox2.SelectedIndex)
            {


                case 0:
                    {
                        GroupBox2.Visible = false;
                        groupBox4.Visible = true;
                        textBoxServer4.Text = "";
                        textBoxUser4.Text = "";
                        textBoxPassword4.Text = "";
                        textBoxPort4.Text = "";
                        DB2TextBox.Text = "";
                        textBoxServer4.Enabled = false;
                        textBoxPort4.Enabled = false;
                        if (MaincomboBox2.Enabled == false)
                        {
                            Config.choosen_Db2 = 0;
                        }
                        break;

                    }
                case 1:
                    {
                        GroupBox2.Visible = false;
                        groupBox4.Visible = true;
                        textBoxServer4.Text = "";
                        textBoxUser4.Text = "";
                        textBoxPassword4.Text = "";
                        textBoxPort4.Text = "";
                        DB2TextBox.Text = "";
                        textBoxServer4.Enabled = true;
                        textBoxPort4.Enabled = true;
                        if (MaincomboBox2.Enabled == false)
                        {
                            Config.choosen_Db2 = 1;
                        }
                        break;
                    }
                case 2:
                    {
                        GroupBox2.Visible = false;
                        groupBox4.Visible = true;
                        textBoxServer4.Text = "";
                        textBoxUser4.Text = "";
                        textBoxPassword4.Text = "";
                        textBoxPort4.Text = "";
                        DB2TextBox.Text = "";
                        textBoxServer4.Enabled = true;
                        textBoxPort4.Enabled = false;
                        if (MaincomboBox2.Enabled == false)
                        {
                            Config.choosen_Db2 = 2;
                        }
                        break;
                    }
                case 3:
                    {
                        GroupBox2.Visible = true;
                        groupBox4.Visible = false;
                        textBoxServer3.Text = "";
                        textBoxUser3.Text = "";
                        textBoxPassword3.Text = "";
                        textBoxPort3.Text = "";
                        DbComboBox3.Text = "";
                        if (MaincomboBox2.Enabled == false)
                        {
                            Config.choosen_Db2 = 3;
                        }
                        break;
                    }
                default:
                    break;
            }

        }
        private void MainWindowDefaultLoad(object sender, EventArgs e)
        {
            MaincomboBox1.SelectedIndex = Config.choosen_Db;
            MaincomboBox2.SelectedIndex = Config.choosen_Db2;
            GroupBox1.Visible = false;
            GroupBox2.Visible = false;
        }

        private void LoadFile_Click(object sender, EventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {

                Config.Server = "";
                Config.User = "";
                Config.password = "";
                Config.ServerPort = 0;
                Config.Server2 = "";
                Config.User2 = "";
                Config.password2 = "";
                Config.ServerPort2 = 0;
                Config.DB = "";
                Config.DB2 = "";
            }
            Config.LoadConfig();
            MaincomboBox1.SelectedIndex = Config.choosen_Db;
            MaincomboBox2.SelectedIndex = Config.choosen_Db2;
            if (MaincomboBox1.SelectedIndex == 0 || MaincomboBox1.SelectedIndex == 1 || MaincomboBox1.SelectedIndex == 2)
            {
                textBoxServer2.Text = Config.Server.ToString();
                textBoxUser2.Text = Config.User.ToString();
                textBoxPassword2.Text = Config.password.ToString();
                DBTextBox.Text = Config.DB.ToString();
                textBoxPort2.Text = Config.ServerPort.ToString();
            }
            if (MaincomboBox1.SelectedIndex == 3)
            {
                textBoxServer.Text = Config.Server.ToString();
                textBoxUser.Text = Config.User.ToString();
                textBoxPassword.Text = Config.password.ToString();
                textBoxPort.Text = Config.ServerPort.ToString();
                DbComboBox2.Text = Config.DB.ToString();
            }

            if (MaincomboBox2.SelectedIndex == 0 || MaincomboBox2.SelectedIndex == 1 || MaincomboBox2.SelectedIndex == 2)
            {
                textBoxServer4.Text = Config.Server2.ToString();
                textBoxUser4.Text = Config.User2.ToString();
                textBoxPassword4.Text = Config.password2.ToString();
                textBoxPort4.Text = Config.ServerPort2.ToString();
                DB2TextBox.Text = Config.DB2.ToString();
            }
            if (MaincomboBox2.SelectedIndex == 3)
            {
                textBoxServer3.Text = Config.Server2.ToString();
                textBoxUser3.Text = Config.User2.ToString();
                textBoxPassword3.Text = Config.password2.ToString();
                textBoxPort3.Text = Config.ServerPort2.ToString();
                DbComboBox3.Text = Config.DB2.ToString();
            }

        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void SaveTOFile_Click(object sender, EventArgs e)
        {
            valuesAreExactWhatIsInTextBox();
            Config.SaveConfig(Config.actualListOfDbs);
        }
        private void TestButton_Click(object sender, EventArgs e)
        {


            valuesAreExactWhatIsInTextBox();
            Config.connectionString = null;
            MySql.Data.MySqlClient.MySqlConnection cnn;
            Config.connectionString = "server=" + $"{Config.Server}" + ":" + $"{Config.ServerPort};uid=" + $"{Config.User};pwd=" + $"{ Config.password};database=" + $"{Config.DB}";
            try
            {
                cnn = new MySql.Data.MySqlClient.MySqlConnection(Config.connectionString);
                cnn.Open();
                MessageBox.Show("☻•☻ connection open ☻•☻ ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
            }
        } // testButton mysql in groupbox1
        private void TestButton3_Click(object sender, EventArgs e)
        {

            valuesAreExactWhatIsInTextBox();
            Config.connectionString2 = null;
            MySql.Data.MySqlClient.MySqlConnection cnn;
            Config.connectionString2 = "server=" + $"{Config.Server2}" + ":" + $"{Config.ServerPort2};uid=" + $"{Config.User2};pwd=" + $"{ Config.password2};database=" + $"{Config.DB2}";
            try
            {
                cnn = new MySql.Data.MySqlClient.MySqlConnection(Config.connectionString2);
                cnn.Open();
                MessageBox.Show("☻•☻ connection open ☻•☻ ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
            }
        }  // testButton mysql in groupbox2
        private void TesButton2_Click(object sender, EventArgs e)
        {
            valuesAreExactWhatIsInTextBox();
            switch (MaincomboBox1.SelectedIndex)
            {


                //oralce DB connection
                case 0:
                    {
                        Config.connectionString = null;
                        Config.connectionString = "Data Source=" + $"{Config.DB};User Id=" + $"{Config.User};Password=" + $"{ Config.password};";
                        //connetionString = "Data Source=NEWDAMDB;User Id=PQDT_TEST;Password=pqdt;";


                        try
                        {
                            OracleConnection cnn = new OracleConnection(Config.connectionString);
                            OracleCommand command = new OracleCommand();
                            //command.CommandText = "SELECT DISTINCT OBJECT_NAME FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE' ORDER BY OBJECT_NAME ";
                            command.Connection = cnn;
                            cnn.Open();
                            KryptonMessageBox.Show("☻•☻ connection open ☻•☻ ");
                            //DataView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;  // select column
                            //if (DataView.Columns.Count > 0)  
                            //    DataView.Columns[0].Selected = true;
                            //DataView.Columns[1].Selected = false;

                            //cnn.Close();
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
                        Config.connectionString = null;
                        Config.connectionString = "Server=" + $"{Config.Server}; Port=" + $"{Config.ServerPort};User Id=" + $"{Config.User};Password=" + $"{ Config.password};Database=" + $"{Config.DB}";
                        //connetionString = "Server = 192.168.1.33; Port = 5432; User Id = postgres; Password = egc6720EGC; Database = Enesy";
                        try
                        {
                            var cnn = new NpgsqlConnection(Config.connectionString);
                            cnn.Open();
                            KryptonMessageBox.Show("☻•☻ connection open ☻•☻ ");
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
                        Config.connectionString = null;
                        Config.connectionString = "Server=" + $"{Config.Server};Database=" + $"{Config.DB};User Id=" + $"{Config.User};Password=" + $"{ Config.password};";
                        //connetionString = "Server=192.168.1.33\\EGC_MSSQL;Database=enesy;User Id=sa;Password=egc6720EGC;";

                        SqlConnection cnn;
                        try
                        {
                            cnn = new SqlConnection(Config.connectionString);
                            cnn.Open();
                            KryptonMessageBox.Show("☻•☻ connection open ☻•☻ ");
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
        }          // testButton oracle,postgre,ms sql in groupbox1
        private void TestButton4_Click(object sender, EventArgs e)
        {
            valuesAreExactWhatIsInTextBox();
            switch (MaincomboBox2.SelectedIndex)
            {
                //oralce DB connection
                case 0:
                    {
                        Config.connectionString2 = null;
                        Config.connectionString2 = "Data Source=" + $"{Config.DB2};User Id=" + $"{Config.User2};Password=" + $"{ Config.password2};";
                        //connetionString = "Data Source=NEWDAMDB;User Id=PQDT_TEST;Password=pqdt;";


                        try
                        {
                            OracleConnection cnn = new OracleConnection(Config.connectionString2);
                            OracleCommand command = new OracleCommand();
                            //command.CommandText = "SELECT DISTINCT OBJECT_NAME FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE' ORDER BY OBJECT_NAME ";
                            command.Connection = cnn;
                            cnn.Open();
                            KryptonMessageBox.Show("☻•☻ connection open ☻•☻ ");
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
                        Config.connectionString2 = null;
                        Config.connectionString2 = "Server = " + $"{Config.Server2}; Port = " + $"{Config.ServerPort2};User Id=" + $"{Config.User2};Password=" + $"{ Config.password2};Database=" + $"{Config.DB2}";
                        //connetionString = "Server = 192.168.1.33; Port = 5432; User Id = postgres; Password = egc6720EGC; Database = Enesy";
                        try
                        {
                            var cnn = new NpgsqlConnection(Config.connectionString2);
                            cnn.Open();
                            KryptonMessageBox.Show("☻•☻ connection open ☻•☻ ");
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
                        Config.connectionString2 = null;
                        Config.connectionString2 = "Server=" + $"{Config.Server2};Database=" + $"{Config.DB2};User Id=" + $"{Config.User2};Password=" + $"{ Config.password2};";
                        //connetionString = "Server=192.168.1.33\\EGC_MSSQL;Database=enesy;User Id=sa;Password=egc6720EGC;";

                        SqlConnection cnn;
                        try
                        {
                            cnn = new SqlConnection(Config.connectionString2);
                            cnn.Open();
                            KryptonMessageBox.Show("☻•☻ connection open ☻•☻ ");

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
        }     // testButton oracle,postgre,ms sql in groupbox2
        private void GoNextButton_Click(object sender, EventArgs e)
        {

            switch (Config.GoNextButtonClicked)
            {
                case 0:
                    resultOfExportText.Visible = false;
                    HideButton.Visible = false;
                    valuesAreExactWhatIsInTextBox();
                    DataView.DataSource = null;
                    DataView.Rows.Clear();
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
                                    DataView.ClearSelection();
                                    for (int i = 0; i < DataView.Columns.Count; i++)
                                        DataView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;


                                    OracleCommand command = new OracleCommand();
                                    command.CommandText = "SELECT DISTINCT OBJECT_NAME FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE' ORDER BY OBJECT_NAME ";
                                    command.Connection = cnn;
                                    cnn.Open();
                                    OracleDataReader dr = command.ExecuteReader();
                                    //DataView.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
                                    //// In case you want the first column selected. 
                                    //if (DataView.Columns.Count > 0)  // Check if you have at least one column.
                                    //    DataView.Columns[0].Selected = true;
                                    //DataView.Columns[1].Selected = false;
                                    int index = 0;
                                    try
                                    {
                                        while (dr.Read())
                                        {
                                            DataView.Rows.Add();
                                            DataView["Source", index].Value = dr.GetValue(0);
                                            index++;
                                            Config.indexOfRowsInTableView++;
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
                                    KryptonMessageBox.Show(ex.Message, "CANT CONNECT TO SOURCE DATABASE!!! Connection R.I.P  † † † ");
                                }
                                break;
                            }
                        //postgre DB2 connection
                        case 1:
                            {
                                Config.connectionString = null;
                                Config.connectionString = "Server=" + $"{Config.Server}; Port=" + $"{Config.ServerPort};User Id=" + $"{Config.User};Password=" + $"{ Config.password};Database=" + $"{Config.DB}";
                                //connetionString = "Server = 192.168.1.33; Port = 5432; User Id = postgres; Password = egc6720EGC; Database = Enesy";
                                int index = 0;
                                try
                                {
                                    var cnn = new NpgsqlConnection(Config.connectionString);
                                    cnn.Open();
                                    string sql = "SET search_path = 'enesy';" +
                                        "select table_name from information_schema.tables" +
                                        " where table_catalog ='enesy' and table_schema = 'Enesy' order by table_name ";
                                    var cmd = new NpgsqlCommand(sql, cnn);
                                    NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();
                                    try
                                    {
                                        while (npgsqlDataReader.Read())
                                        {

                                            DataView.Rows.Add();

                                            DataView["Source", index].Value = npgsqlDataReader.GetValue(0);
                                            index++;
                                            Config.indexOfRowsInTableView++;

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
                        //ms sql DB2 connection
                        case 2:
                            {
                                Config.connectionString = null;
                                Config.connectionString = "Server=" + $"{Config.Server};Database=" + $"{Config.DB};User Id=" + $"{Config.User};Password=" + $"{ Config.password};";
                                //connetionString = "Server=192.168.1.33\\EGC_MSSQL;Database=enesy;User Id=sa;Password=egc6720EGC;";
                                int index = 0;


                                SqlConnection cnn;
                                try
                                {
                                    cnn = new SqlConnection(Config.connectionString);
                                    cnn.Open();
                                    SqlCommand cmd;
                                    SqlDataReader reader;
                                    String sql = "select TABLE_NAME  from enesy.INFORMATION_SCHEMA.TABLES t ";
                                    cmd = new SqlCommand(sql, cnn);
                                    reader = cmd.ExecuteReader();
                                    try
                                    {
                                        while (reader.Read())
                                        {

                                            DataView.Rows.Add();

                                            DataView["Source", index].Value = reader.GetValue(0);
                                            index++;
                                            Config.indexOfRowsInTableView++;

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
                                    OracleConnection cnn = new OracleConnection(Config.connectionString2);
                                    OracleCommand command = new OracleCommand();
                                    command.CommandText = "SELECT DISTINCT OBJECT_NAME FROM USER_OBJECTS WHERE OBJECT_TYPE = 'TABLE' ORDER BY OBJECT_NAME ";
                                    command.Connection = cnn;
                                    cnn.Open();
                                    OracleDataReader dr = command.ExecuteReader();
                                    int index = 0;
                                    Destination.Items.Clear();
                                    try
                                    {


                                        while (dr.Read())
                                        {
                                            while (Config.indexOfRowsInTableView <= index)
                                            {
                                                DataView.Rows.Add();
                                                Config.indexOfRowsInTableView++;
                                            }

                                            DataView["Destination", index].Value = dr.GetValue(0);
                                            Destination.Items.Add(dr.GetValue(0).ToString());
                                            index++;

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
                                    KryptonMessageBox.Show(ex.Message, "Connection R.I.P  † † † ");
                                }
                                break;

                            }
                        //postgre DB connection
                        case 1:
                            {
                                Config.connectionString2 = null;
                                Config.connectionString2 = "Server = " + $"{Config.Server2}; Port = " +
                                    $"{Config.ServerPort2};User Id=" + $"{Config.User2};Password=" +
                                    $"{ Config.password2};Database=" + $"{Config.DB2}";
                                //connetionString = " Server = 192.168.1.33; Port = 5432; User Id = postgres; Password = egc6720EGC; Database = Enesy";
                                int index = 0;
                                Destination.Items.Clear();
                                try
                                {
                                    var cnn = new NpgsqlConnection(Config.connectionString2);
                                    cnn.Open();
                                    string sql = "SET search_path = 'enesy';" +
                                        "select table_name from information_schema.tables" +
                                        " where table_catalog ='enesy' and table_schema = 'Enesy' order by table_name ";
                                    var cmd = new NpgsqlCommand(sql, cnn);
                                    NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();
                                    try
                                    {
                                        while (npgsqlDataReader.Read())
                                        {
                                            while (Config.indexOfRowsInTableView <= index)
                                            {
                                                DataView.Rows.Add();
                                                Config.indexOfRowsInTableView++;

                                            }
                                            DataView["Destination", index].Value = npgsqlDataReader.GetValue(0);
                                            Destination.Items.Add(npgsqlDataReader.GetValue(0).ToString());
                                            index++;
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
                        //ms sql DB connection
                        case 2:
                            {
                                Config.connectionString2 = null;
                                Config.connectionString2 = "Server=" + $"{Config.Server2};Database=" + $"{Config.DB2};User Id=" + $"{Config.User2};Password=" + $"{ Config.password2};";
                                //connetionString = "Server=192.168.1.33\\EGC_MSSQL;Database=enesy;User Id=sa;Password=egc6720EGC;";
                                int index = 0;
                                SqlConnection cnn;
                                try
                                {
                                    cnn = new SqlConnection(Config.connectionString2);
                                    cnn.Open();
                                    SqlCommand cmd;
                                    SqlDataReader reader;
                                    String sql = "select TABLE_NAME  from enesy.INFORMATION_SCHEMA.TABLES t ";
                                    cmd = new SqlCommand(sql, cnn);
                                    reader = cmd.ExecuteReader();
                                    try
                                    {
                                        while (reader.Read())
                                        {

                                            while (Config.indexOfRowsInTableView <= index)
                                            {
                                                DataView.Rows.Add();
                                                Config.indexOfRowsInTableView++;

                                            }

                                            DataView["Destination", index].Value = reader.GetValue(0);
                                            Destination.Items.Add(reader.GetValue(0).ToString());
                                            index++;
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
                    break;


                case 1:
                    HideButton.Visible = true;
                    resultOfExportText.Visible = true;
                    Config.GoNextButtonClicked = 0;
                    break;

                default:
                    Config.GoNextButtonClicked = 0;
                    break;
            }
            if (Config.actualListOfDbs == null)
            {

            }
            else
            {
                //nastavuje dle pole "Config.actualListOfDbs vybrane databaze

                for (int i = 0; i < Config.actualListOfDbs.Length; i++)
                {
                    for (int j = 0; j < Config.indexOfRowsInTableView; j++)
                    {
                        if ((string)DataView.Rows[j].Cells[1].Value != null && Config.actualListOfDbs[i] == DataView["Source", j].Value.ToString())
                        {
                            try
                            {
                                DataGridViewRow row = DataView.Rows[j];
                                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[0];
                                cell.Value = cell.TrueValue;
                                j = 0;
                                break;
                            }
                            catch (Exception ex)
                            {

                                KryptonMessageBox.Show(ex.Message, "Error in checking checkboxes");
                            }
                        }
                    }
                }
            }
            Config.GoNextButtonClicked++;
            kryptonPanel1.Visible = true;
            GoBackButton.Enabled = true;
            MaincomboBox1.Enabled = false;
            MaincomboBox2.Enabled = false;
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            if (resultOfExportText.Visible == true)
            {
                resultOfExportText.Visible = false;
                HideButton.Visible = false;
            }
            else
            {
                Config.GoNextButtonClicked--;
                Config.indexOfRow = -1;
                MaincomboBox1.Enabled = true;
                MaincomboBox2.Enabled = true;
                Config.actualListOfDbs = new string[0]; ;

                switch (Config.GoBackButtonClicked)
                {
                    case 0:
                        kryptonPanel1.Visible = false;
                        GoBackButton.Enabled = false;
                        Config.indexOfRowsInTableView = 0;
                        break;
                    default:
                        Config.GoBackButtonClicked = 0;
                        break;
                }



            }
        }
        //open new window cross link cell
        private void DataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // zapina pri macknuti na link v dataView (dataGridview) nove okno "transform"
            if (e.RowIndex != -1 && e.ColumnIndex == 3)
            {

                Transform transform = new Transform();

                Config.choosenDbTableOfDataView1 = (string)DataView["Source", e.RowIndex].Value;
                Config.choosenDbTableOfDataView2 = (string)DataView["Destination", e.RowIndex].Value;
                Config.indexOfRowsInTableViewTransform = 0;

                DataGridViewLinkCell cell = (DataGridViewLinkCell)DataView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                transform.Show();
            }
        }
        private void DataView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                // kooriguje data na zaklade vybranych radek v dataView (dataGridView) do pole Config.actualListOfDbs
                if (kryptonPanel1.Visible == true && e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    DataGridViewRow row = DataView.Rows[e.RowIndex];
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (cell.Value == cell.TrueValue)
                    {
                        Array.Resize(ref Config.actualListOfDbs, Config.actualListOfDbs.Length + 1);
                        for (int i = 0; i < Config.actualListOfDbs.Length; i++)
                        {
                            if (Config.actualListOfDbs[i] == null)
                            {
                                Config.actualListOfDbs[i] = (string)DataView[1, e.RowIndex].Value;
                                break;
                            }
                        }
                    }
                    else if (cell.Value == cell.FalseValue)
                    {
                        for (int i = 0; i < Config.actualListOfDbs.Length; i++)
                        {
                            if ((string)DataView[1, e.RowIndex].Value == Config.actualListOfDbs[i])
                            {
                                Array.Resize(ref Config.actualListOfDbs, Config.actualListOfDbs.Length + 1);
                                Config.actualListOfDbs[i] = null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                KryptonMessageBox.Show(ex.Message, "Errrrrrror!!!");
            }
               DataView.CommitEdit(DataGridViewDataErrorContexts.Commit);


        }
        private void HideButton_Click(object sender, EventArgs e)
        {
            resultOfExportText.Visible = false; ;
            HideButton.Visible = false;
        }
    }

}


