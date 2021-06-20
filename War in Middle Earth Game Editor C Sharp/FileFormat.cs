using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    class FileFormat
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

        public FileFormat()
        {

        }

        public FileFormat(int value)
        {
            this.Name = Constants.GameFormat[value].Name;
        }


        public FileFormat(string name,int endian,int dataEndian, string exeFile,int bitPlanes,int frmlBitplanes)
        {
            this.Name = name;
            this.Endian = endian;
            this.DataEndian = dataEndian;
            this.ExeFile = exeFile;
            this.Bitplanes = bitPlanes;
            this.FrmlBitplanes = frmlBitplanes;
        }
        struct Offsets
        {
            int bannerIcons;
            int tiles;
            int exeCharacterName;
            int exeCharacterData;
            int exeCityName;
            int exeCityData;
            int exeInventoryName;
            int exeCharacterValue;
            int exeDifferenceOffset;
        }

    }
    static class Constants
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

        public static List<FileFormat> GameFormat = new List<FileFormat>
        { 
            new FileFormat(PC_VGA_FORMAT, 0, 1, PC_VGA_EXE, 5, 5 ),
            new FileFormat(PC_EGA_FORMAT, 0, 0, PC_EGA_EXE, 0, 0 ),
            new FileFormat(IIGS_FORMAT, 0, 0, IIGS_EXE, 0, 0 ),
            new FileFormat(AMIGA_FORMAT, 1, 1, AMIGA_EXE, 5, 5 ),
            new FileFormat(IIGS_FORMAT, 1, 1, ST_EXE, 4, 4 )
        };
        public static FileFormat GetFormatData(string filename)
        {
            FileFormat tempFormat = new FileFormat();
            for (int x = 0; x < GameFormat.Count;x++)
            {
                if  (GameFormat[x].ExeFile==filename)
                {
                    tempFormat = GameFormat[x];
                }
            }
            return tempFormat;
        }

    }
}


