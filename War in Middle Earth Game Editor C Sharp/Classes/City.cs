using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    public class City
    {
        // Private fields
        private FileFormat m_format;                        // FileFormat object data.
        private int m_formatval;                            // FileFormat type converted to numerical index for offset array selection.
        private int m_pointer;                              // Pointer to city block data.
        private int m_blockSize;                            // Size of city data block.  Array set by formats.
        private string m_fileName;                          // Filename of EXE file by format.
        private BinaryReader m_exeFile;                     // BinaryReader stream for retrieving data for EXE File.
        private CityNameList m_cityNameList;                // List of all city data blocks.
        // Public Properties
        public int Pointer
        {
            get { return m_pointer; }
            set { m_pointer = value; }
        }
        public int BlockSize
        {
            get { return m_blockSize; }
            set { m_blockSize = value; }
        }
        public BinaryReader EXEFile
        {
            get { return m_exeFile; }
            set { m_exeFile = value; }
        }
        public CityNameList NameList
        {
            get { return m_cityNameList; }
            set { m_cityNameList = value; }
        }
        /// <summary>
        /// struct CityBlock -- Structure for city block in EXE file
        /// </summary>
        public struct CityBlock
        {
            public UInt16 locationpointer;                              // 2-Byte unsigned Word -- Points to City Name.
            public UInt16 unknownword;                                  // 2-Byte unsigned Word -- Unknown.
            public UInt16 x;                                            // 2-Byte unsigned Word -- x coordinate
            public UInt16 y;                                            // 2-Byte unsigned Word -- y coordinate
            public byte pad;                                            // 1-Byte Pad Byte
            public CityBlock(BinaryReader br, int pointer)
            {
                br.BaseStream.Position = pointer;
                this.locationpointer = br.ReadUInt16();
                this.unknownword = br.ReadUInt16();
                this.x = br.ReadUInt16();
                this.y = br.ReadUInt16();
                this.pad = br.ReadByte();
                
            }
        };

        public City(FileFormat fm)
        {
            m_format = fm;
            m_formatval = FileFormat.GetFormatValue();
            m_pointer = City.Offsets.CityDataBlockStart[m_formatval];
            m_blockSize = City.Offsets.CityDataBlockSize[m_formatval];
            m_fileName = Utils.GetFullFilename(fm.ExeFile);
            m_cityNameList = CityNameList.InitializeCityNames(fm);
            m_exeFile = new BinaryReader(File.Open(m_fileName, FileMode.Open));
        }

        public string GetCityName(int value)
        {
            string result = "";
            for (int i = 0; i < NameList.Count; i++)
            {
                if (value == NameList[i].NameValue)
                {
                    result = NameList[i].Name;
                    break;
                }
            }
            return result;
        }

     
        public static class Offsets
        {
            public static int[] CityDataBlockStart = new int[] { 112565, 131117, 105706, 131010, 124828 };
            public static int[] CityDataBlockSize = new int[] { 9, 9, 9, 10, 10 };
            public static int[] PointerDifference = new int[] { 93300, 93300, 95598, 40, 4 };
        }

        public static string Default = "HINTERLAND";

    }

    public class CityBlocks
    {
        private List<City.CityBlock> blocks;
        public CityBlocks()
        {
            blocks = new List<City.CityBlock>();
        }

        public City.CityBlock this[int i]
        {
            get { return blocks[i]; }
            set { blocks[i] = value; }
        }
        public int Count => blocks.Count;
        public void Add(City.CityBlock block)
        {
            blocks.Add(block);
        }

        public CityBlocks InitializeCityData(FileFormat format)
        {
           
            City.CityBlock cityblock;
            CityBlocks cityblocks = new CityBlocks();


            return cityblocks;
        }





    }
}
