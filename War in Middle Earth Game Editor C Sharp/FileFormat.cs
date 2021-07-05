using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public class FileFormat
    {
        public const string PC_VGA_EXE = "START.EXE";
        public const string PC_EGA_EXE = "LORD.EXE";
        public const string IIGS_EXE = "EARTH.SYS16";
        public const string AMIGA_EXE = "WARINMIDDLEEARTH";
        public const string ST_EXE = "COMMAND.PRG";
        public const string PC_VGA_FORMAT = "VGA";
        public const string PC_EGA_FORMAT = "EGA";
        public const string IIGS_FORMAT = "IIGS";
        public const string AMIGA_FORMAT = "AMIGA";
        public const string ST_FORMAT = "ST";

        public string Name;
        public int Endian;
        public int DataEndian;
        public string ExeFile;
        public int Bitplanes;
        public int FrmlBitplanes;
        public Icon Icon;
        public struct Offsets
        {
            public int bannerIcons;
            public int tiles;
            public int exeCharacterName;
            public int exeCharacterData;
            public int exeCityName;
            public int exeCityData;
            public int exeInventoryName;
            public int exeCharacterValue;
            public int exeDifferenceOffset;

            public Offsets(int bi, int tile, int exn, int exd, int ecn, int ecd, int ein, int ecv, int edo)
            {
                this.bannerIcons = bi;
                this.tiles = tile;
                this.exeCharacterName = exn;
                this.exeCharacterData = exd;
                this.exeCityName = ecn;
                this.exeCityData = ecd;
                this.exeInventoryName = ein;
                this.exeCharacterValue = ecv;
                this.exeDifferenceOffset = edo;
                
            }
        }

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
        

    }
    static class Constants
    {
        public const string Program_Name = "WAR IN MIDDLE EARTH GAME EDITOR";
        public const string Program_Version = "1.00B Build 1 VS2019 C#";
        public const string ProgramDate = "06/19/2021";
        public const string PC_VGA_EXE = "START.EXE";
        public const string PC_EGA_EXE = "LORD.EXE";
        public const string IIGS_EXE = "EARTH.SYS16";
        public const string AMIGA_EXE = "WARINMIDDLEEARTH";
        public const string ST_EXE = "COMMAND.PRG";
        public const string PC_VGA_FORMAT = "VGA";
        public const string PC_EGA_FORMAT = "EGA";
        public const string IIGS_FORMAT = "IIGS";
        public const string AMIGA_FORMAT = "AMIGA";
        public const string ST_FORMAT = "ST";

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
            new FileFormat(IIGS_FORMAT, 1, 1, ST_EXE, 4, 4,ATARI_ICON )
        };
        public static List<FileFormat.Offsets> offsets = new List<FileFormat.Offsets>
        {
            new FileFormat.Offsets(4, 43907, 113323, 104654, 114179, 112565, 115144, 0, 93300),                 // PC-VGA Offsets
            new FileFormat.Offsets(5, 301405, 131875, 123041, 132731, 131117, 133696, 0, 112832),               // PC-EGA Offsets
            new FileFormat.Offsets(5, 20, 106464, 108124, 104746, 105706, 120518, 33562, 95598),                // IIGS Offsets
            new FileFormat.Offsets(4, 8038, 37914, 122708, 39539, 131010, 80286, -18984, 40),                   // Amiga Offsets
            new FileFormat.Offsets(4, 8038, 37096, 116640, 38721, 124828, 29228, 5862, 4)                       // Atari ST Offsets
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


