using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;



namespace ReadWriteFile
{
    class Tools
    {

        public static bool LogEnabled = true;  // priznak, zda je logovani povoleno
        public static ListView Protokol = null;
        public static ArrayList ImportTextProt = null;




        public static void create_lock()
        {
            string FileName = Application.StartupPath + "\\lock\\LOCK.TXT";
            try
            {
                using (StreamWriter outfile = new StreamWriter(FileName, false))
                {
                    try
                    {
                        outfile.WriteLine(DateTime.Now.ToString());
                    }
                    catch (Exception ex)
                    {
                        Tools.WriteSysLog("E", "Tools.create_lock: Vytvoreni zamku selhalo");
                        Tools.WriteSysLog("E", "Tools.create_lock: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Tools.create_lock: Vytvoreni zamku selhalo");
                Tools.WriteSysLog("E", "Tools.create_lock: " + ex.Message);
            }
        }


        public static void remove_lock()
        {
            string FileName = Application.StartupPath + "\\lock\\LOCK.TXT";
            try
            {
                File.Delete(FileName);
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Tools.remove_lock: Smazani zamku selhalo");
                Tools.WriteSysLog("E", "Tools.remove_lock: " + ex.Message);
            }
        }


        public static bool exists_lock()
        {
            bool result = false;
            string FileName = Application.StartupPath + "\\lock\\LOCK.TXT";
            try
            {
                result = File.Exists(FileName);
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Tools.exists_lock: Kontrola existence zamku selhala");
                Tools.WriteSysLog("E", "Tools.exists_lock: " + ex.Message);
            }
            return result;
        }



        public static string SaveImpProtocol()
        {
            string FileName = Config.ProtocolFolder + "ProtImp_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".TXT";
            try
            {
                using (StreamWriter outfile = new StreamWriter(FileName, false))
                {
                    try
                    {
                        foreach (string line in ImportTextProt)
                        {
                            outfile.WriteLine(line);
                        }
                    }
                    catch (Exception ex)
                    {
                        Tools.WriteSysLog("E", "Tools.SaveImpProtocol: Zapis do protokolu selhal.");
                        Tools.WriteSysLog("E", "Tools.SaveImpProtocol: " + ex.Message);
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Tools.SaveImpProtocol: Zapis do protokolu selhal.");
                Tools.WriteSysLog("E", "Tools.SaveImpProtocol: " + ex.Message);
                return "";
            }
            return FileName;
        }




        public static void LogProt(string id_mereni, string id_mj, string kategorie, string zprava)
        {
            string s_kat = "";
            int img_index = 0;
            try
            {
                if (Protokol == null) return;
                switch(kategorie)
                {
                    case "I": s_kat = "Informace"; img_index = 0; break;
                    case "W": s_kat = "Varování";  img_index = 1; break;
                    case "E": s_kat = "Chyba";     img_index = 2; break;
                    default:  s_kat = "";          img_index = 3; break;
                }
                ListViewItem li = Protokol.Items.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                li.SubItems.Add(s_kat);
                li.SubItems.Add(id_mereni);
                li.SubItems.Add(id_mj);
                li.SubItems.Add(zprava);
                li.ImageIndex = img_index;
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Tools.LogProt: Zapis do protokolu selhal.");
                Tools.WriteSysLog("E", "Tools.LogProt: " + ex.Message);
            }
        }



        // ****************************************************************************************
        // ** Zapisuje udalost do systemoveho protokolu. To je soubor primo v miste instalace    **
        // ** aplikace, do ktereho se zapisuji jen ty nejzakladnejsi udalosti, zejmena kdyz      **
        // ** neni aktivni spojeni do databaze.                                                  **
        // ****************************************************************************************
        public static void WriteSysLog(string Cat, string Msg)
        {
            string SysLogFileName = "";  // jmeno souboru, kam se bude logovat
            string SysLogDir = Application.StartupPath + "\\log";           


                if (Config.EnableLog == 0)
                {
                    return;
                }

                if(Config.DebugMode == 0)
                {
                    // tady logujeme jenom chyby a warningy
                    if((Cat.Trim().ToUpper() != "E" ) && (Cat.Trim().ToUpper() != "W"))
                    {
                        return;
                    }
                }



                // slozime cestu k souboru protokolu...
                SysLogFileName = SysLogDir + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_system.log";
                // a otevreme pro pripis nove radky...
                using (StreamWriter outfile = new StreamWriter(SysLogFileName, true))
                {
                    try
                    {
                        outfile.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ";" + Cat + ";" + Msg);
                    }
                    catch (Exception)
                    {
                        // pri selhani zapisu do logu se log vypne az do restartu aplikace nebo do nuceneho
                        // zapnuti uzivatelem aplikace
                        MessageBox.Show("Chyba při zápisu do protokolu aplikace DAM Import. " + 
                                        "\nLogování do protokolu bude nyní vypnuto",
                                        "Chyba aplikace DAM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LogEnabled = false;
                    }
                }
            }

        
        // WriteSysLog




        // ****************************************************************************************
        // ** Cte Xml uzel dany parametrem nodeName ve vetvi, dane korenem node. Kdyz neexistuje,** 
        // ** vrati hodnotu predanou v parametru defaultValue.                                   **                                  
        // ****************************************************************************************
        public static string loadStringXmlNode(XmlNode node, string nodeName, string defaultValue)
        {
            string str = "";
            try
            {
                str = node.SelectSingleNode(nodeName).InnerText;
            }
            catch (Exception ex)
            {
                str = defaultValue;
                WriteSysLog("W", "XML uzel <" + nodeName + "> nebyl nalezen. " +
                            "Pouzije se vychozi hodnota: " + defaultValue);
                WriteSysLog("E", "Exception: " + ex.Message);
            }
            return str;
        }



        // ****************************************************************************************
        // Cte Xml uzel dany parametrem nodeName ve vetvi, dane korenem node. Pokud neexistuje,  **
        // vrati hodnotu predanou v parametru defaultValue.                                      **                                |
        // ****************************************************************************************
        public static int loadIntegerXmlNode(XmlNode node, string nodeName, int defaultValue)
        {
            string str = "";
            int iValue = 0;
            try
            {
                str = node.SelectSingleNode(nodeName).InnerText;
                if (!int.TryParse(str, out iValue))
                {
                    WriteSysLog("W", "XML uzel <" + nodeName + "> obsahuje hodnotu v chybnem formatu." +
                                "Pouzije se vychozi hodnota: " + defaultValue.ToString());
                    iValue = defaultValue;
                }
            }
            catch (Exception ex)
            {
                iValue = defaultValue;
                WriteSysLog("E", "XML uzel <" + nodeName + "> nelze nacist. " +
                           "Pouzije se vychozi hodnota: " + defaultValue.ToString());
                WriteSysLog("E", "Exception: " + ex.Message);
            }
            return iValue;
        }



        // ****************************************************************************************
        // Cte Xml uzel dany parametrem nodeName ve vetvi, dane korenem node. Pokud neexistuje,  **
        // vrati hodnotu predanou v parametru defaultValue.                                      **
        // ****************************************************************************************
        public static double loadDoubleXmlNode(XmlNode node, string nodeName, double defaultValue)
        {
            string str = "";
            double iValue = 0;
            try
            {
                str = node.SelectSingleNode(nodeName).InnerText;
                if (!double.TryParse(str, out iValue))
                {
                    WriteSysLog("W", "XML uzel <" + nodeName + "> obsahuje hodnotu v chybnem formatu." +
                                "Pouzije se vychozi hodnota: " + defaultValue.ToString());
                    iValue = defaultValue;
                }
            }
            catch (Exception ex)
            {
                iValue = defaultValue;
                WriteSysLog("E", "XML uzel <" + nodeName + "> nelze nacist. " +
                           "Pouzije se vychozi hodnota: " + defaultValue.ToString());
                WriteSysLog("E", "Exception: " + ex.Message);
            }
            return iValue;
        }




        // ****************************************************************************************
        // ** Cte Xml uzel dany parametrem nodeName ve vetvi, dane korenem node. Kdyz neexistuje,** 
        // ** vrati hodnotu, predanou v parametru defaultValue.                                  **                                   |
        // ****************************************************************************************
        public static bool loadBoolXmlNode(XmlNode node, string nodeName, bool defaultValue)
        {
            string str = "";
            int iValue = 0;
            bool bValue = false;
            try
            {
                str = node.SelectSingleNode(nodeName).InnerText;
                if (!int.TryParse(str, out iValue))
                {
                    WriteSysLog("W", "XML uzel <" + nodeName + "> obsahuje hodnotu v chybnem formatu." +
                                "Pouzije se vychozi hodnota: " + defaultValue.ToString());
                    iValue = 0;
                }
                if (iValue == 1) bValue = true; else bValue = false;
            }
            catch (Exception ex)
            {
                bValue = defaultValue;
                WriteSysLog("E", "XML uzel <" + nodeName + "> nelze nacist. " +
                           "Pouzije se vychozi hodnota: " + defaultValue.ToString());
                WriteSysLog("E", "Exception: " + ex.Message);
            }
            return bValue;
        }




        // ************************************************************************************
        // V retezci textMessage zameni znaky na odpovidajicich pozicich v baseChars a 
        // codeChars. Vysledny retezec vrati jako svoji navratovou hodnotu.
        // Retezec baseChar nesmi byt delsi, nez retezec codeChar.
        // ************************************************************************************
        public static string replaceChars(string textMessage, string baseChars, string codeChars)
        {
            string result = "";
            StringBuilder newMessage = new StringBuilder(textMessage);
            try
            {
                for (int i = 0; i < textMessage.Length; i++) 
                {
                    char currentChar = textMessage[i];
                    int pos = baseChars.IndexOf(currentChar);
                    if (pos >= 0)
                    {
                        currentChar = codeChars[pos];
                        newMessage[i] = currentChar;
                    }
                }
                result = newMessage.ToString();
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Tools.replaceChars: Chyba pri nahrazovani znaku v retezci");
                Tools.WriteSysLog("E", "Tools.replaceChars: " + ex.Message);
            }
            return result;
        }


        // pro zadane id merici jednotky vrati seznam souboru, ktere vyhovuji tomuto id.
        // soubory se vyhledavaji v prednastavenem adresari, danem konfiguraci
        public static string[] getFileList(string path, string idMj)
        {
            string[] result = null;
            try
            {
                string[] files = Directory.GetFiles(path, idMj + "*.csv");
            }
            catch (Exception ex)
            {
                Tools.WriteSysLog("E", "Tools.getFileList: Chyba pri vyhledavani souboru odpovidajicich masce.");
                Tools.WriteSysLog("E", "Tools.getFileList: " + ex.Message);
                result = null;
            }
            return result;
        }
        




    }
}
