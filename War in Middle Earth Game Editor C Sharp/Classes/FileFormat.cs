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
        private Endianness m_endian;
        private Endianness m_dataEndian;
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
        public Endianness Endian
        {
            get { return m_endian; }
            set { m_endian = value; }
        }

        public Endianness DataEndian
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
        public FileFormat(string name,Endianness endian,Endianness dataEndian, string exeFile,int bitPlanes,int frmlBitplanes, Icon ico)
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



    public enum Endianness
    {
        endLittle,endBig
    }
    /// <summary>
    /// Contains Constants used in the program.
    /// </summary>
    public static class Constants
    {
        public const string Program_Name = "WAR IN MIDDLE EARTH GAME EDITOR";
        public const string Program_Version = "1.00B Build 5 VS2022 C#";
        public const string ProgramDate = "01/02/2023";
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
        public const string filterPCVGA = "PC-VGA EXECUTABLE|START.EXE*.*|PC-EGA EXECUTABLE|LORD.EXE*.*";
        public const string filterIIGS = "IIGS ProDOS 16(EARTH.SYS16)|earth.sys16*.*";
        public const string filterAmiga = "Amiga (warinmiddleearth|warinmiddleearth*.*";
        public const string filterST = "Atari ST (COMMAND.PRG|command.prg*.*";

        public enum ENDIAN { LITTLE, BIG }
        public const int TILE_MAX = 256;
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
            new FileFormat(PC_VGA_FORMAT, Endianness.endLittle,Endianness.endBig, PC_VGA_EXE, 5, 5, PC_ICON ),
            new FileFormat(PC_EGA_FORMAT, Endianness.endLittle, Endianness.endLittle, PC_EGA_EXE, 0, 0,PC_ICON ),
            new FileFormat(IIGS_FORMAT, Endianness.endLittle, Endianness.endLittle, IIGS_EXE, 0, 0,IIGS_ICON ),
            new FileFormat(AMIGA_FORMAT, Endianness.endBig, Endianness.endBig, AMIGA_EXE, 5, 5,AMIGA_ICON ),
            new FileFormat(ST_FORMAT, Endianness.endBig, Endianness.endBig, ST_EXE, 4, 4,ATARI_ICON )
        };

        public static readonly Dictionary<string, Offsets> CityOffsets = new Dictionary<string, Offsets>
        {
            {PC_VGA_FORMAT, new Offsets(114179, 93300, 9)},
            {Constants.PC_EGA_FORMAT, new Offsets(132731, 93300, 9)},
            {Constants.IIGS_FORMAT, new Offsets(106464, 95598, 9) },
            {Constants.AMIGA_FORMAT, new Offsets(39539, 40, 10)},
            {Constants.ST_FORMAT, new Offsets(38721, 4, 10)}
        };
        public static readonly Dictionary<string, Offsets> CharacterOffsets = new Dictionary<string, Offsets>
        {
            {Constants.PC_VGA_FORMAT, new Offsets(104654, 93300, 37) },
            {Constants.PC_EGA_FORMAT, new Offsets(123041, 112832, 37)},
            {Constants.IIGS_FORMAT, new Offsets(106464, 62036, 37)},
            {Constants.AMIGA_FORMAT, new Offsets(37874, 19024, 38)},
            {Constants.ST_FORMAT, new Offsets(37096, -5858, 38)},
        };


        public static FileFormat GetFormatData(string filename)
        {
            FileFormat tempFormat = new FileFormat();
            for (int x = 0; x < GameFormat.Count;x++)
            {
                if  (GameFormat[x].ExeFile==filename.ToUpper())
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