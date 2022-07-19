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
    /// <summary>
    /// Class for program configuration file.
    /// </summary>
    public class Config
    {
        const string CONFIG_FILE = "wimeed.cfg";

        private string m_gameDirectory;
        private string m_gameExecutable;
        private int m_scale;
        private bool m_configPresent;

        public bool ConfigPresent
        {
            get { return m_configPresent; }
            set { m_configPresent = value; }    
        }

        public int Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }


        public string GameDirectory
        {
            get { return m_gameDirectory; }
            set { m_gameDirectory = value; }
        }
        public string GameExecutable
        {
            get { return m_gameExecutable; }
            set { m_gameExecutable = value; }
        }

        public Config(string directory, string filename, int scale)
        {
            //this.m_configPresent= ConfigExist(CONFIG_FILE);
            this.m_scale = scale;
            this.m_gameDirectory = directory;
            this.GameExecutable = filename;
        }

        public Config(bool read)
        {
            if (read)
            {
                m_gameDirectory = ConfigGetDirectory();
                m_gameExecutable = ConfigGetFilename();
                m_scale = GetScaleValue();
            }
            else
                return;

        }

        /// <summary>
        /// Checks for existing configuration file.
        /// </summary>
        /// <param name="path">Path where configuration file would be located.</param>
        /// <returns>True is exists, false if not.</returns>
        private bool ConfigExist(string path)
        {
            bool result;
            result = File.Exists(path);
            return result;
        }
        public void WriteConfig(string path, string fileName,int scale)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Async = true;
            using (XmlWriter confile = XmlWriter.Create(CONFIG_FILE, settings))
            {
                confile.WriteStartDocument();
                confile.WriteStartElement("WIMEDIRECTORY");
                confile.WriteElementString("directory", path);
                confile.WriteElementString("EXECUTABLEFILE", fileName);
                confile.WriteElementString("SCALE", scale.ToString());
                confile.WriteEndElement();
                confile.WriteEndDocument();
                confile.Close();
            }
        }


        private int GetScaleValue()

        {
            int result = 0;
            string val="";
            string filler;
            using (XmlReader confile = XmlReader.Create(CONFIG_FILE))
            {
                if (confile.ReadToDescendant("WIMEDIRECTORY"))
                {
                    confile.ReadStartElement("WIMEDIRECTORY");
                    filler = confile.ReadElementContentAsString();
                    filler = confile.ReadElementContentAsString();
                    val = confile.ReadElementContentAsString();
                }
            }
            result = Convert.ToInt16(val);
            return result;


        }
        /// <summary>
        /// Gets executable filename stored in the config file.
        /// </summary>
        /// <returns>String containing filename.</returns>
        private string ConfigGetFilename()
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
        private string ConfigGetDirectory()
        {
            string result = null;
            using (XmlReader confile = XmlReader.Create(CONFIG_FILE))
            {
                if (confile.ReadToDescendant("WIMEDIRECTORY"))
                {
                   confile.ReadStartElement("WIMEDIRECTORY");
                   GameDirectory = confile.ReadElementContentAsString();
                }
            }
            result = GameDirectory;
            return result;
        }
    }
}
