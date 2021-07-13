using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{

    public class config
    {
        const string CONFIG_FILE = "wimeed.cfg";
        public bool ConfigPresent;
        public string GameDirectory;
        public string GameExecutable;

        public config()
        {
            this.ConfigPresent = ConfigExist(CONFIG_FILE);
            this.GameDirectory = "C:/WIME";
            this.GameExecutable = "WIME.EXE";

        }


        private bool ConfigExist(string path)
        {
            bool result;
            result = File.Exists(path);
            return result;
        }

        public static void WriteConfig(string path, string fileName)

        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Async = true;

            using (XmlWriter confile = XmlWriter.Create(CONFIG_FILE, settings))
            {
                confile.WriteStartDocument();
                confile.WriteStartElement("WIMEDIRECTORY");
                confile.WriteElementString("directory", path);
                confile.WriteElementString("EXECUTABLEFILE", fileName);
                confile.WriteEndElement();
                confile.WriteEndDocument();
                confile.Close();
            }


        }
        
        /* ConfigGetFilename - Gets executable filename from config file. */
        public static string ConfigGetFilename()
        {
            string result = null;
            string filler;
            using (XmlReader confile = XmlReader.Create(CONFIG_FILE))
            {
                if (confile.ReadToDescendant("WIMEDIRECTORY"))
                {
                    confile.ReadStartElement("WIMEDIRECTORY");
                    filler = confile.ReadElementContentAsString();
                    result = confile.ReadElementContentAsString();
                    //MessageBox.Show(result.ToString(), "Existing GameFileFound!");
                }

            }
            return result;

        }
        
        public string ConfigGetDirectory()
        {
            string result = null;
            using (XmlReader confile = XmlReader.Create(CONFIG_FILE))
            {
                if (confile.ReadToDescendant("WIMEDIRECTORY"))
                {
                   confile.ReadStartElement("WIMEDIRECTORY");
                   GameDirectory = confile.ReadElementContentAsString();
                   MessageBox.Show(GameDirectory.ToString(), "Existing GameFileFound!");
                }

            }

            return result;
        }

    }

    


}
