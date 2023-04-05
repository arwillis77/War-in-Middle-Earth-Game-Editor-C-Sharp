
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    /// <summary>
    /// Class for storing gamestrings extracted from EXE files.
    /// </summary>
    public class GameString
    {
        private int m_nameValue;
        private string m_nameString;
        public int NameValue
        {
            set { m_nameValue = value; }
            get { return m_nameValue; }
        }
        public string Name
        {
            get { return m_nameString; }
            set { m_nameString = value; }
        }
        public GameString()
        {
            m_nameValue = 0;
            m_nameString = "";
        }

        public GameString(int nameValue, string nameString)
        {
            m_nameValue = nameValue;
            m_nameString = nameString;    
        
        }

        public string ReadString(BinaryFileEndian exeFile, int ptr)
        {
            string result = "";
            int tempval = 0;
            exeFile.BaseStream.Position = ptr;
            do
            {
                tempval = exeFile.ReadByte();
                if (tempval == 0)
                    break;
                result = result + (char)tempval;
            } while (tempval != 0);
            return result;
        }



    }
    /// <summary>
    /// List of individual GameString Objects such as character names, city names, object names.
    /// </summary>
    public class GameStringList
    {
        private List<GameString> gamestringlist;
       

    
        public GameStringList()
        {
            gamestringlist = new List<GameString>();
        }
        public void Add(GameString gs)
        {
            gamestringlist.Add(gs);
        }
        public int Count => gamestringlist.Count;
        public GameString this[int i]
        {
            get { return gamestringlist[i]; }
            set { gamestringlist[i] = value; }
        }

  


        /// <summary>
        /// Offsets.  Stores data for offset values for the individual game string types.
        /// </summary>
        //public struct Offsets
        //{
        //    public int NamePointer;
        //    public int PointerOffset;
        //    public int BlockSize;
        //    /// <summary>
        //    /// Offsets for string points.
        //    /// </summary>
        //    /// <param name="np">Name Pointer - value points to string</param>
        //    /// <param name="po">Pointer Offset - Added to pointer for location.</param>
        //    /// <param name="bs">Size of data block</param>
        //    public Offsets(int np, int po, int bs)
        //    {
        //        NamePointer = np;
        //        PointerOffset = po;
        //        BlockSize = bs;
        //    }
        //}
    }
    public static class InventoryOffsets
    {
        public static Dictionary<string, int> inventoryoffsets = new Dictionary<string, int>();

        static InventoryOffsets()
        {
            inventoryoffsets.Add(Constants.PC_VGA_FORMAT, 115144);
            inventoryoffsets.Add(Constants.PC_EGA_FORMAT, 133696);
            inventoryoffsets.Add(Constants.IIGS_FORMAT, 120518);
            inventoryoffsets.Add(Constants.AMIGA_FORMAT, 80286);
            inventoryoffsets.Add(Constants.ST_FORMAT, 29228);
        }
    }
    public class CharacterNameList :GameStringList
    {
        private List<GameString> cnl;                       /* List Character Value and Names */
        private FileFormat m_fileFormat;                    /* File Format Value */
        private Offsets m_offsets;                          /* Offset values for name and value locations in EXE */
        private string m_dataFilename;                      /* Archive Filename */
        private string m_exeFilename;                       /* EXE Filename */
        private Endianness m_endianness;                    /* Endianness for file organization */
        public const int NameMax = 193;                     /* Max # of names */
        public List<GameString> CharacterNamelist
        {
            get { return cnl; }
            set { cnl = value; }
        }
        /// <summary>
        /// Initializes list of character names used in game archive files.
        /// </summary>
        /// <returns></returns>
        /// 
        public CharacterNameList()
        {   
            m_fileFormat = Utils.GetCurrentFormat();
            m_offsets = GetCharacterOffsets();
            m_dataFilename = Utils.GetFullFilename("archive.dat");
            m_exeFilename=Utils.GetEXEFilename(m_fileFormat);
            m_endianness = m_fileFormat.Endian;
            
            cnl=InitializeCharacterNames();
        }

        new public GameString this[int i] => cnl[i];

        public int GetNamePointer(string fileName, int filePtr)
        {
            int result;
            string filename = Utils.GetFullFilename(fileName);
            BinaryReader br = new(File.Open(filename, FileMode.Open));
            br.BaseStream.Position = filePtr;
            result = br.ReadUInt16();
            return result;
        }
        private GameString GetCharacterName(int index)
        {
            int fpStart;
            int exePtrStart;
            int nameVal;
            int tempval;
            string tempName = "";
            GameString result = new GameString();
            fpStart = m_offsets.BlockSize * index;
            BinaryFileEndian bfe = new(File.Open(m_dataFilename, FileMode.Open));
            bfe.BaseStream.Position = fpStart;
            nameVal = bfe.ReadUInt16(m_endianness);
            nameVal = nameVal + m_offsets.PointerOffset;
            bfe.Close();
            exePtrStart = (result.NameValue + m_offsets.PointerOffset); 
            BinaryFileEndian exeFile = new(File.Open(m_exeFilename, FileMode.Open));
            exeFile.BaseStream.Position = exePtrStart;
            do
            {
                tempval = exeFile.ReadByte();
                if (tempval == 0)
                    break;
                tempName = tempName + (char)tempval;
            } while (tempval != 0);

            result = new GameString(nameVal, tempName);
            return result;
        }
        private List<GameString> InitializeCharacterNames()
        {
            /* Variables */
            int x;
            int ptrStart;
            UInt16 filler;
            string filename;
            string exeName;
            /* Objects */
            Endianness endian;
            BinaryFileEndian br;
            BinaryFileEndian exeBr;
            GameString cns;
            FileFormat format = m_fileFormat;
            List<GameString> t_nameList;
            Offsets os = GetCharacterOffsets();
            filename = Utils.GetFullFilename("archive.dat");
            exeName = Utils.GetEXEFilename(m_fileFormat);
            br = new(File.Open(filename, FileMode.Open));           // Open BinaryReader for archive file.  Location of pointers for EXE namestrings.
            exeBr = new(File.Open(exeName, FileMode.Open));         // Open BinaryReader for Game EXE file.  Location of name strings.
            endian = m_endianness;
            ptrStart = os.BlockSize;                                // Set file pointer for archive file to block size value.
            br.BaseStream.Position = ptrStart;                      // Set BinaryReader position to value of file pointer.
            t_nameList = new List<GameString>();
            for (x = 0; x < NameMax; x++)                           // Cycle through name values.
            {
                cns = new GameString();
                switch(endian)
                {
                    case Endianness.endBig:
                        {
                            filler = br.ReadUInt16(endian);
                            break;
                        }
                    default:
                        break;
                }
                cns.NameValue = br.ReadUInt16(endian);              // Read name pointer value.
                int ptrName = (cns.NameValue + os.PointerOffset);   // Add pointer offset to namevalue for EXE pointer value.
                exeBr.BaseStream.Position = ptrName;                // Set position in EXE gamefile from value.
                cns.Name = cns.ReadString(exeBr,ptrName);
                t_nameList.Add(cns);
                ptrStart += os.BlockSize;
                br.BaseStream.Position = ptrStart;
            }
            br.Close();
            exeBr.Close();
            return t_nameList;
        }
        /// <summary>
        /// Get EXE file offsets based on game file format.
        /// </summary>
        /// <param name="formatName">File format for the game EXE file</param>
        /// <returns></returns>
        public Offsets GetCharacterOffsets()
        {
            Offsets result = Constants.CharacterOffsets[m_fileFormat.Name];
            
            return result;
        }
        //public static class CharacterOffsets
        //{
        //    public static Dictionary<string, CharacterNameList.Offsets> characteroffsets = new Dictionary<string, CharacterNameList.Offsets>();
        //    static CharacterOffsets()
        //    {
        //        characteroffsets.Add(Constants.PC_VGA_FORMAT, new CharacterNameList.Offsets(104654, 93300, 37));
        //        characteroffsets.Add(Constants.PC_EGA_FORMAT, new CharacterNameList.Offsets(123041, 112832, 37));
        //        characteroffsets.Add(Constants.IIGS_FORMAT, new CharacterNameList.Offsets(106464, 62036, 37));
        //        characteroffsets.Add(Constants.AMIGA_FORMAT, new CharacterNameList.Offsets(37874, 19024, 38));
        //        characteroffsets.Add(Constants.ST_FORMAT, new CharacterNameList.Offsets(37096, -5858, 38));
        //    }
        //}
    }
    public class CityNameList : GameStringList
    {
        public const int CityTotal = 75;
        private List<GameString> citynamelist;
        private FileFormat m_fileFormat;                    /* File Format Value */
        private Offsets m_offsets;                          /* Offset values for name and value locations in EXE */
        private string m_exeFilename;                       /* EXE Filename */

        public  List<GameString> CityNamelist
        {
            get { return citynamelist; }
            set { citynamelist = value; }
        }

        public CityNameList()
        {
            m_fileFormat = Utils.GetCurrentFormat();
            m_offsets = GetCityOffset();
            m_exeFilename = Utils.GetEXEFilename(m_fileFormat);
            citynamelist = InitializeCityNames();
        }


        private List<GameString> InitializeCityNames()
        {
            int x;
            int ptrStart;
            int p_val = 1;
            GameString cns;
            List<GameString> t_citylist = new List <GameString>();
            BinaryFileEndian exeBr = new(File.Open(m_exeFilename, FileMode.Open));
            ptrStart = m_offsets.NamePointer;
            exeBr.BaseStream.Position = ptrStart;
            for (x = 0; x < CityTotal; x++)
            {
                p_val = (int)exeBr.BaseStream.Position;
                cns = new GameString();
                cns.NameValue = p_val - m_offsets.PointerOffset;
                cns.Name=cns.ReadString(exeBr, ptrStart);
                t_citylist.Add(cns);
            }
            exeBr.Close();
            return t_citylist;
        }
        public Offsets GetCityOffset()
        {
            Offsets result = Constants.CityOffsets[m_fileFormat.Name];
            return result;
        }



        //public static class CityOffsets
        //{
        //    public static Dictionary<string, Offsets> cityoffsets = new Dictionary<string, Offsets>();
        //    static CityOffsets()
        //    {
        //        cityoffsets.Add(Constants.PC_VGA_FORMAT, new CityNameList.Offsets(114179, 93300, 9));
        //        cityoffsets.Add(Constants.PC_EGA_FORMAT, new CityNameList.Offsets(132731, 93300, 9));
        //        cityoffsets.Add(Constants.IIGS_FORMAT, new CityNameList.Offsets(106464, 95598, 9));
        //        cityoffsets.Add(Constants.AMIGA_FORMAT, new CityNameList.Offsets(39539, 40, 10));
        //        cityoffsets.Add(Constants.ST_FORMAT, new CityNameList.Offsets(38721, 4, 10));
        //    }
        //}

    }
    class ArmyMobilize
    {
        private Dictionary<int, string> m_mobilized;

        public Dictionary<int, string> Mobilized
        {
            get { return m_mobilized; }
            set { m_mobilized = value; }
        }

        public ArmyMobilize()
            {
            m_mobilized = new Dictionary<int,string>();
            m_mobilized.Add(0, "EVIL - NO");
            m_mobilized.Add(1, "GOOD - NO");
            m_mobilized.Add(2, "GOOD - NO");
            m_mobilized.Add(3, "STATIONARY NPC - NO");
            m_mobilized.Add(4, "RANDOM NPC - NO");
            m_mobilized.Add(40, "SPECIAL CHARACTER - GOLLUM");
            m_mobilized.Add(70, "Boromir - ??");
            m_mobilized.Add(99, "GOOD - YES");
        }
        public void FillMobilizeCombo(ComboBox cb, int value)
        {
            foreach (string name in m_mobilized.Values)
                cb.Items.Add(name);            
            cb.Text = GetSelectedMobility(value);
        }
        private string GetSelectedMobility(int value)
        {
            return m_mobilized[value];
        }
    }
    class Leaders
    {
        private Dictionary<int, string> m_characterLeader;

        public Dictionary<int, string> CharacterLeader
        {
            get { return m_characterLeader; }
            set { m_characterLeader = value; }
        }

        public Leaders(CharacterNameList cn)
        {
            m_characterLeader = new Dictionary<int, string>();
            m_characterLeader.Add(0, "NONE");
            for (int a = 1; a < cn.CharacterNamelist.Count; a++)
                m_characterLeader.Add(a, cn.CharacterNamelist[a-1].Name);
        }

        public string  GetLeaderName(int key)
        {
            return m_characterLeader[key];
        }
        public int GetLeaderValue(string leaderName)
        {
            int result=0;
            int count = 0;
            foreach(string name in m_characterLeader.Values)
            {
                if (name == leaderName)
                    result = count;
                else
                    count++;
            }
            return result;
        }

        public void InitializeLeaderData(ComboBox arcCombo, int value)
        {
            foreach (string item in m_characterLeader.Values)
                arcCombo.Items.Add(item);
            arcCombo.Text = GetSelectedLeader(value);
        }
        private string GetSelectedLeader(int value)
        {
            return m_characterLeader[value];
        }
    }
}