using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
 /// <summary>
 /// FileFormat class - Organizes data for specific file format to include EXE filename, Endian data, etc.
 /// </summary>
    public class FileFormat
    {

        private string m_name;
        private int m_endian;
        private int m_dataEndian;
        private string m_exeFile;
        private int m_bitPlanes;
        private int m_frmlBitplanes;
        private Icon m_icon;

        /// <summary>
        /// Name - Name of the File Format.  PC VGA, EGA, IIGS, Amiga, etc.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public int Endian
        {
            get { return m_endian; }
            set { m_endian = value; }
        }

        public int DataEndian
        {
            get { return m_dataEndian; }
            set { m_dataEndian = value; }
        }
        public string ExeFile
        {
            get { return m_exeFile; }
            set { m_exeFile = value; }
        }
        public int Bitplanes
        {
            get { return m_bitPlanes; }
            set { m_bitPlanes = value;}
        }
        public int FrmlBitplanes
        {
            get { return m_frmlBitplanes; }
            set { m_frmlBitplanes = value;}
        }
        public Icon Icon
        {
            get { return m_icon; }
            set { m_icon = value; }
        }

        public FileFormat()
        {
        }
        public FileFormat(int value)
        {
            m_name = Constants.GameFormat[value].Name;
            m_endian = Constants.GameFormat[value].Endian;
            m_dataEndian = Constants.GameFormat[value].DataEndian;
            m_exeFile = Constants.GameFormat[value].ExeFile;
            m_bitPlanes = Constants.GameFormat[value].Bitplanes;
            m_frmlBitplanes = Constants.GameFormat[value].FrmlBitplanes;
            m_icon = Constants.GameFormat[value].Icon;
        }
        public FileFormat(string name,int endian,int dataEndian, string exeFile,int bitPlanes,int frmlBitplanes, Icon ico)
        {
            m_name = name;
            m_endian = endian;
            m_dataEndian = dataEndian;
            m_exeFile = exeFile;
            m_bitPlanes = bitPlanes;
            m_frmlBitplanes = frmlBitplanes;
            m_icon = ico;
        }


        public static int GetFormatValue()
        {
            FileFormat currentFormat = Utils.GetCurrentFormat();
            int formatval = 0;
            FileFormat tempFormat = new FileFormat();
            for (int x = 0; x < Constants.GameFormat.Count; x++)
            {
                if (Constants.GameFormat[x].ExeFile == currentFormat.ExeFile)
                {
                    formatval = x;
                    break;
                }
            }
            return formatval;
        }
    }
    /// <summary>
    /// Contains Constants used in the program.
    /// </summary>
    public static class Constants
    {
        public const string Program_Name = "WAR IN MIDDLE EARTH GAME EDITOR";
        public const string Program_Version = "1.00B Build 4 VS2022 C#";
        public const string ProgramDate = "11/11/2022";
        public const string PC_VGA_EXE = "START.EXE";
        public const string PC_EGA_EXE = "LORD.EXE";
        public const string IIGS_EXE = "EARTH.SYS16";
        public const string AMIGA_EXE = "WARINMIDDLEEARTH";
        public const string ST_EXE = "COMMAND.PRG";
        public const string PC_VGA_FORMAT = "PC VGA";
        public const string PC_EGA_FORMAT = "PC EGA";
        public const string IIGS_FORMAT = "IIGS";
        public const string AMIGA_FORMAT = "AMIGA";
        public const string ST_FORMAT = "ST";
        public enum ENDIAN {LITTLE, BIG }
        public const int TILE_MAX = 255;
        public const int CHAR_MAX = 193;
        public const int INT_MAX = 65535;
        public const int CITY_TOTAL = 84;
        public static Icon PC_ICON = Properties.Resources.msdosicon;
        public static Icon PC_EGA_ICON = Properties.Resources.msdosicon;
        public static Icon IIGS_ICON = Properties.Resources.Apple2Icon;
        public static Icon AMIGA_ICON = Properties.Resources.amigaicon;
        public static Icon ATARI_ICON = Properties.Resources.atariicon;
        public static List<FileFormat> GameFormat = new List<FileFormat>
        {
            new FileFormat(PC_VGA_FORMAT, 0, 1, PC_VGA_EXE, 5, 5, PC_ICON ),
            new FileFormat(PC_EGA_FORMAT, 0, 0, PC_EGA_EXE, 0, 0,PC_ICON ),
            new FileFormat(IIGS_FORMAT, 0, 0, IIGS_EXE, 0, 0,IIGS_ICON ),
            new FileFormat(AMIGA_FORMAT, 1, 1, AMIGA_EXE, 5, 5,AMIGA_ICON ),
            new FileFormat(ST_FORMAT, 1, 1, ST_EXE, 4, 4,ATARI_ICON )
        };
        public static FileFormat GetFormatData(string filename)
        {
            FileFormat tempFormat = new FileFormat();
            for (int x = 0; x < GameFormat.Count;x++)
            {
                if  (GameFormat[x].ExeFile==filename)
                {
                    tempFormat = GameFormat[x];
                    break;
                }
            }
            if (tempFormat == null)
            {
                MessageBox.Show("tempFormat is Null", "NULL Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
            return tempFormat;
        }

  

    }
}