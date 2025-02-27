using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ReadWriteFile
{
    class Config
    {


        public static string Version = "4.5.8";              // cislo verze aplikace
        public static ushort AppMode = 0;                    // 0 - import Meg40, 10 - import vyvodu, 20 - import odbocek, 30 - import KMB
        public static ushort DebugMode = 0;                  // zapina (1) nebo vypina (0) diagnosticky rezim
        public static ushort EnableLog = 1;                  // uplne vypina nebo zapina logovani
        //public static string Provider = "";                  // cast connect stringu - provider
        //public static string DataSource = "";                // cast connect stringu - data source
        public static string User = "";                      // cast connect stringu - login uzivatele
        public static string User2 = "";
        // public static string Password = "";                  // cast connect stringu - heslo uzivatele
        //public static string ConnectString = "";             // pripojovaci retezec do databaze
        //public static ushort BlockFolder = 0;
        //public static ushort AutoStart = 0;
        //public static ushort AutoStartTimeout = 30;
        //public static string InstanceName = "";
        //public static ushort DopocetPlt = 0;
        //public static ushort RepeatMode = 0;
        //public static ushort RepeatPause = 120;
        //public static ushort UseEmailNotify = 0;
        //public static string SmtpServer = "";
        public static int ServerPort = 0;
        public static int ServerPort2 = 0;
        //public static string SmtpUser = "";
        //public static string SmtpPassword = "";
        //public static string MailAddrA = "";
        public static string DB = "";
        public static string DB2 = "";
        public static string password = "";
        public static string password2 = "";
        //public static string Name = "";
        //public static ushort TransformU = 0;
        public static string Server = "";
        public static string Server2 = "";
        public static string ProtocolFolder = Application.StartupPath + "\\Protokoly\\";
        public static string openedFile = "";
        public static int choosen_Db = 0;
        public static int choosen_Db2 = 0;
        public static string connectionString = "";
        public static string connectionString2 = "";
        public static int GoNextButtonClicked = 0;
        public static int GoBackButtonClicked = 0;
        public static int indexOfRowsInTableView = 0;
        public static int indexOfRowsInTableViewTransform = 0;
        public static int indexOfRowsInTableViewTransformCellChanged = 0;
        public static int indexOfRow = -1;
        public static string[] actualListOfDbs = { null };
        public static string choosenDbTableOfDataView1;
        public static string choosenDbTableOfDataView2;
        public static Dictionary<string,string> DestinationTypeActual;




        /****************************************************************************************************
         * Konvertuje hodnotu v retezcove podobe na hodnotu typu ushort. Pokud dojde pri konverzi k chybe,  *
         * napriklad proto, ze retezec neni interpretovatelny jako cislo, vrati se hodnota, predana         *
         * parametrem jako default (uDefVal).                                                               *
         ****************************************************************************************************/
        public static ushort getUshortValue(string sVal, ushort uDefVal)
        {
            try
            {
                return ushort.Parse(sVal);
            }
            catch (Exception)
            {
                return uDefVal;
            }
        }



        public static double getDoubleValue(string sVal, double uDefVal)
        {
            try
            {
                return double.Parse(sVal);
            }
            catch (Exception)
            {
                return uDefVal;
            }
        }
        /*        public static void LoadConditionKmb()
                {
                    FileStream fs;
                    StreamReader reader;
                    string sLine = "";   // jedna prectena radka z konfiguracniho souboru
                    Condition = "";
                    try
                    {
                        // Vychozi konfiguracni soubor je ulozen v adresari aplikace a lisi se jen priponou.
                        fs = new FileStream(Application.StartupPath + "\\cfg\\conditionkmb.cfg", FileMode.Open, FileAccess.Read);
                        reader = new StreamReader(fs);
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        while (reader.Peek() > -1)
                        {
                            sLine = reader.ReadLine();
                            sLine = sLine.Trim();
                            if (sLine.StartsWith(";")) continue; // komentar
                            if (sLine.Length == 0) continue;    // prazdna radka
                            if (ConditionKmb.Length == 0) ConditionKmb = sLine;
                            else ConditionKmb = ConditionKmb + " " + sLine;
                        }
                        // uzavreme konfiguracni soubor
                        reader.Close();
                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Tools.WriteSysLog("E", "Config.LoadConditionKmb: Exception: " + ex.Message);
                        ConditionKmb = "";
                    }
                }*/
        /*      public static void LoadCondition()
              {
                  FileStream fs;
                  StreamReader reader;
                  string sLine = "";   // jedna prectena radka z konfiguracniho souboru
                  Condition = "";
                  try
                  {
                      // Vychozi konfiguracni soubor je ulozen v adresari aplikace a lisi se jen priponou.
                      fs = new FileStream(Application.StartupPath + "\\cfg\\condition.cfg", FileMode.Open, FileAccess.Read);
                      reader = new StreamReader(fs);
                      reader.BaseStream.Seek(0, SeekOrigin.Begin);
                      while (reader.Peek() > -1)
                      {
                          sLine = reader.ReadLine();
                          sLine = sLine.Trim();
                          if (sLine.StartsWith(";")) continue; // komentar
                          if (sLine.Length == 0 ) continue;    // prazdna radka
                          if (Condition.Length == 0) Condition = sLine;
                          else Condition = Condition + " " + sLine;
                      }
                      // uzavreme konfiguracni soubor
                      reader.Close();
                      reader.Dispose();
                  }
                  catch (Exception ex)
                  {
                      Tools.WriteSysLog("E", "Config.LoadCondition: Exception: " + ex.Message);
                      Condition = "";
                  }
              }
      */

        /****************************************************************************************************
         * Nacita konfiguracni soubor a aktualizuje konfiguracni promenne.                                  *
         * Staticka metoda - vola se pres tridu.                                                            *
         ****************************************************************************************************/
        public static void LoadConfig()
        {
            StreamReader reader;
            // prednastavime vychozi hodnoty, kdyby v konfiguracnim souboru
            // dana volba chybela. Vychozi hopdnoty nemusi byt nutne funkcni,
            // ale nesmi shodit celou aplikaci
            string sLine = "";   // jedna prectena radka z konfiguracniho souboru
            string[] lineParts;  // jednotlive casti konfiguracni radky
            ushort uTmp;         // pomocna promenna pro cteni konfiguracniho souboru 
            int addLenghtOfListOfDb = 1; // 
            int index = 1;
            int indexOflistOfDbs =0;
            // nastaveni vychozich konfiguracnich hodnot, predpoklada se, ze budou zmeneny
            DebugMode = 0;                    // diagnosticky rezim je defaultne vypnuty
                                              // ProtocolFolder = Application.StartupPath + "\\protokoly\\";

            // cteni konfiguracniho souboru - zpracovava se radek po radku
            try
            {
                // Vychozi konfiguracni soubor je ulozen v adresari aplikace a lisi se jen priponou.

                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    Config.openedFile = dialog.FileName;
                    reader = new StreamReader(Config.openedFile);
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    // cteme soubor radek po radku a kazdou radku analyzujeme na pritomnost
                    // konfiguracni volby. Pokud tam platna volba neni, nic se nestane, nejde o chybu.                                                                                                        // To je mozne vyuzit treba pro vkladani komentaru - beznych textovych radek
                    while (reader.Peek() > -1)
                    {
                        // precte celou radku a rozdeli ji na dvojici klic a hodnota.
                        // oddelovacem je znak rovna se "=" 
                        sLine = reader.ReadLine();
                        lineParts = sLine.Split('=');
                        if (lineParts.Length > 1)                                                 // jsou tam alespon dve casti?
                        {                                                                                             // radka se rozdelila na alespon dve casti, odpovida tedy schematu klic = hodnot  
                                                                                                                      // pokud je klic definovany jako identifikator, precte se z nej hodnota


                            if (lineParts[0].Trim().ToLower() == "actualdb" + index)
                            {
                                if (addLenghtOfListOfDb == 1)
                                {
                                    actualListOfDbs = new string[addLenghtOfListOfDb];
                                    addLenghtOfListOfDb++;
                                }
                                else
                                {
                                    Array.Resize(ref actualListOfDbs, actualListOfDbs.Length + addLenghtOfListOfDb-1);

                                }                               
                                    actualListOfDbs[indexOflistOfDbs] = lineParts[1].Trim();
                                    index++;
                                    indexOflistOfDbs++; 
                            }
                            switch (lineParts[0].Trim().ToLower())
                            {
                                case "server":
                                    Server = lineParts[1].Trim();
                                    break;
                                case "user":
                                    User = lineParts[1].Trim();
                                    break;
                                case "password":
                                    password = lineParts[1].Trim();
                                    break;
                                case "port":
                                    ServerPort = int.Parse(lineParts[1]);
                                    break;
                                case "server2":
                                    Server2 = lineParts[1].Trim();
                                    break;
                                case "user2":
                                    User2 = lineParts[1].Trim();
                                    break;
                                case "password2":
                                    password2 = lineParts[1].Trim();
                                    break;
                                case "port2":
                                    ServerPort2 = int.Parse(lineParts[1]);
                                    break;
                                case "database":
                                    DB = lineParts[1].Trim();
                                    break;
                                case "database2":
                                    DB2 = lineParts[1].Trim();
                                    break;
                                case "choosen_db":
                                    choosen_Db = int.Parse(lineParts[1]);
                                    break;
                                case "choosen_db2":
                                    choosen_Db2 = int.Parse(lineParts[1]);
                                    break;
                            }
                        }
                    }                                                                                                 // uzavreme konfiguracni soubor               
                /*                        ConnectString = "provider=" + Provider + ";data source=" + DataSource +
                                                            ";user id=" + User + ";password=" + Password;
                                            //uprava - nactei podminky SQL dotazu ze souboru
                                                            LoadCondition();
                                                            LoadConditionKmb();*/
                    reader.Close();
                    reader.Dispose();
                }
            }
            catch (Exception ex)
            {
                // chybu pri cteni konfiguraku zalogujeme a doufame, ze se aplikace i presto
                // rozjede - nic zde tedy nasilne neukoncujeme, pokracujeme i s neuplnou konfiguraci
                MessageBox.Show("Chyba při čtení konfiguračního souboru.", "Chyba aplikace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Tools.WriteSysLog("E", "Config.LoadConfig: Error reading configuration file.");
                Tools.WriteSysLog("E", "Config.LoadConfig: Exception: " + ex.Message);
            }
        }
        /****************************************************************************************************
         * Vraci index radky v kolekci, na ktere se nachazi konfiguracni volba key.                         *
         * Pokud tam volba vubec neni, vrati hodnotu -1                                                     *
         ****************************************************************************************************/
        private static int getConfigIndex(ArrayList fb, string key)
        {
            int result = -1;
            int index = 0;
            try
            {
                foreach (string cfgLine in fb)
                {
                    index++;  // index polohy potrebuje zvysit vzdy
                    if (cfgLine.Trim().StartsWith("#")) continue; // komentare preskakujeme 
                    if (cfgLine.Trim().Length == 0) continue; // prazdne radky take nechame jak jsou
                    string[] parts = cfgLine.Split('=');
                    if (parts.Length < 2) continue; // neni to klic a hodnota
                    if (parts[0].ToLower() == key.ToLower())
                    {
                        result = index - 1;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                result = -1;
            }
            return result;
        }



        /****************************************************************************************************
         * Aktualizuje polozku v konfiguracnim souboru - v pametovem bufferu                                *
         * Pokud v nem polozka neni, tak ji tam prida                                                       *
         ****************************************************************************************************/
        private static void updateConfigFile(ArrayList fb, string key, string value)
        {
            int index = 0;
            string newLine;
            StreamWriter writer;
            writer = new StreamWriter(openedFile, true);
            try
            {
                if ((index = getConfigIndex(fb, key)) >= 0)
                {
                    string cfgLine = (string)fb[index];
                    string[] parts = cfgLine.Split('=');

                    if (parts.Length == 2)
                    {
                        newLine = parts[0] + " = " + value;  // dame novou hodnotu volby
                        fb[index - 1] = newLine;  // novym radkem nahradime stavajici

                        try
                        {
                            writer.BaseStream.Seek(index, SeekOrigin.Begin);
                            writer.WriteLine(parts[0] + " = " + value);
                            writer.Close();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Spatne zadany format", "Format", MessageBoxButtons.OK);
                        }
                    }
                }
                else
                {
                    fb.Add(key + " = " + value);
                    try
                    {
                        writer.WriteLine(key + " = " + value);
                        writer.Close();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Config.updateConfigFile: Error updating configuration file.");
                Tools.WriteSysLog("E", "Config.updateConfigFile: Key = " + key);
                Tools.WriteSysLog("E", "Config.updateConfigFile: Value = " + value);
                Tools.WriteSysLog("E", "Config.updateConfigFile: Exception: " + ex.Message);
            }
        }

        /****************************************************************************************************
         * Uklada konfiguracni soubor a aktualizuje konfiguracni volby.                                     *
         * Uklada tak, aby pokud mozno zustalo zachovano formatovani, ktere si udelel v souboru uzivatel    *
         ****************************************************************************************************/
        public static void ClearFile(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            fs.SetLength(0);
            fs.Close();
        }

        public static void SaveConfig( string[] DbsList)
        {
            // FileStream fs;
            StreamReader reader;
            string sLine = "";    // jedna prectena radka z konfiguracniho souboru
            ArrayList fileBuffer;
            // cteni konfiguracniho souboru do bufferu, ve kterem budeme aktualizovat
            try
            {
                fileBuffer = new ArrayList();
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    openedFile = dialog.FileName;
                    ClearFile(openedFile);
                    reader = new StreamReader(openedFile);
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    // Vychozi konfiguracni soubor je ulozen v adresari aplikace a lisi se jen priponou.
                    //     fs = new FileStream(Application.StartupPath + "\\cfg\\damiv.cfg", FileMode.Open, FileAccess.Read);
                    while (reader.Peek() > -1)
                    {
                        sLine = reader.ReadLine();
                        fileBuffer.Add(sLine);
                    }
                    // uzavreme konfiguracni soubor
                    reader.Close();
                    updateConfigFile(fileBuffer, "Server ", Server);
                    updateConfigFile(fileBuffer, "User ", User);
                    updateConfigFile(fileBuffer, "Password ", password);
                    updateConfigFile(fileBuffer, "Port ", ServerPort.ToString());
                    updateConfigFile(fileBuffer, "Server2 ", Server2);
                    updateConfigFile(fileBuffer, "User2 ", User2);
                    updateConfigFile(fileBuffer, "Password2 ", password2);
                    updateConfigFile(fileBuffer, "Port2 ", ServerPort2.ToString());
                    updateConfigFile(fileBuffer, "Database ", DB.ToString());
                    updateConfigFile(fileBuffer, "Database2", DB2.ToString());
                    updateConfigFile(fileBuffer, "choosen_Db ", choosen_Db.ToString());
                    updateConfigFile(fileBuffer, "choosen_Db2 ", choosen_Db2.ToString());
                    for (int i = 0, j = 1; i < DbsList.Length; i++)
                    {
                        if (DbsList[i] != null)
                        {
                            updateConfigFile(fileBuffer, "actualDb" + j, DbsList[i]);
                            j++;
                        }
                    }
                }
            }
            // ted mame soubor v bufferu tak, jak je ulozeny na disku, se vsemi
            // komentari, formatovanim a podobne. Kazdou volbu se tam pokusime najit
            // a pokud tam je, tak pouze aktualizujeme jeji hodnotu. 
            // neni-li v nem pozadovana volba, prida se 

            catch (Exception ex)
            {
                // chybu pri cteni konfiguraku zalogujeme a doufame, ze se aplikace i presto
                // rozjede - nic zde tedy nasilne neukoncujeme, pokracujeme i s neuplnou konfiguraci
                MessageBox.Show("Chyba při čtení konfiguračního souboru.", "Chyba aplikace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Tools.WriteSysLog("E", "Config.LoadConfig: Error reading configuration file.");
                Tools.WriteSysLog("E", "Config.LoadConfig: Exception: " + ex.Message);
            }
        }
    }
}
