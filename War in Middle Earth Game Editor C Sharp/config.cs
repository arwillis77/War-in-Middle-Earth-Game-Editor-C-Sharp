using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{

    class config
    {
        const string CONFIG_FILE = "wimeed.cfg";
        public bool ConfigPresent;
        public string GameDirectory;
        public string GameExecutable;

        public config()
        {

            ConfigPresent = ConfigExist(CONFIG_FILE);

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
                //confile.WriteElementString("directory", path);
                confile.WriteElementString("EXECUTABLEFILE", fileName);
                confile.WriteEndElement();
                confile.WriteEndDocument();
                confile.Close();
            }


        }
        
        /* ConfigGetFilename - Gets executable filename from config file. */
        public static string ConfigGetFilename()
        {
            string result = "NULL";
            string filler;
            using (XmlReader confile = XmlReader.Create(CONFIG_FILE))
            {
                if (confile.ReadToDescendant("WIMEDIRECTORYY"))
                {
                    confile.ReadStartElement("WIMEDIRECTORY");
                    filler = confile.ReadElementContentAsString();
                    result = confile.ReadElementContentAsString();
                    //MessageBox.Show(result.ToString(), "Existing GameFileFound!");
                }

            }
            return result;

        }



    }

    class Game
    {
        public string filepath;
        public string filename;
        public string format;
        public int formatVal;
        public int endian;
        public int dataEndian;

        public Game()
        {


        }
        public Game(string fileName)
        {
            this.filename = fileName;
            this.filepath = Path.GetDirectoryName(filename);


        }





    }



}
