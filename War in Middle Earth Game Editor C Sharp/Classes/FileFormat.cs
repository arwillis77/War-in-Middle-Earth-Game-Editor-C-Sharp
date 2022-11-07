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
        public string Name;
        public int Endian;
        public int DataEndian;
        public string ExeFile;
        public int Bitplanes;
        public int FrmlBitplanes;
        public Icon Icon;

        public FileFormat()
        {
        }
        public FileFormat(int value)
        {
            this.Name = Constants.GameFormat[value].Name;
            this.Endian = Constants.GameFormat[value].Endian;
            this.DataEndian = Constants.GameFormat[value].DataEndian;
            this.ExeFile = Constants.GameFormat[value].ExeFile;
            this.Bitplanes = Constants.GameFormat[value].Bitplanes;
            this.FrmlBitplanes = Constants.GameFormat[value].FrmlBitplanes;
            this.Icon = Constants.GameFormat[value].Icon;
        }
        public FileFormat(string name,int endian,int dataEndian, string exeFile,int bitPlanes,int frmlBitplanes, Icon ico)
        {
            this.Name = name;
            this.Endian = endian;
            this.DataEndian = dataEndian;
            this.ExeFile = exeFile;
            this.Bitplanes = bitPlanes;
            this.FrmlBitplanes = frmlBitplanes;
            this.Icon = ico;
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
        public const string ProgramDate = "03/19/2022";
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