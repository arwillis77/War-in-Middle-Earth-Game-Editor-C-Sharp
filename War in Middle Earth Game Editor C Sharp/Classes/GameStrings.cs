
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
        private int namevalue;
        private string name;
        public int NameValue
        {
            set { namevalue = value; }
            get { return namevalue; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public GameString()
        {
            namevalue = 0;
            name = "";
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
        public struct Offsets
        {
            public int NamePointer;
            public int PointerOffset;
            public int BlockSize;
            /// <summary>
            /// Offsets for string points.
            /// </summary>
            /// <param name="np">Name Pointer - value points to string</param>
            /// <param name="po">Pointer Offset - Added to pointer for location.</param>
            /// <param name="bs">Size of data block</param>
            public Offsets(int np, int po, int bs)
            {
                NamePointer = np;
                PointerOffset = po;
                BlockSize = bs;
            }
        }
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


    public class CharacterNameList : GameStringList
    {
        public const int NameMax = 193;
        /// <summary>
        /// Initializes list of character names used in game archive files.
        /// </summary>
        /// <param name="format">File format of game.  File format determines offset locations.</param>
        /// <returns></returns>
        /// 

        public static CharacterNameList InitializeCharacterNames(FileFormat format)
        {
            int x;     
            int tempval = 0;
            GameString cns;
            CharacterNameList t_nameList = new CharacterNameList();
            Offsets os = GetCharacterOffsets(format.Name);
            string filename = Utils.GetFullFilename("archive.dat");
            string EXEName = Utils.GetEXEFilename(format);
            BinaryReader br = new(File.Open(filename, FileMode.Open));                                          // Open BinaryReader for archive file.  Location of pointers for EXE namestrings.
            BinaryReader exeBr = new(File.Open(EXEName, FileMode.Open));                                        // Open BinaryReader for Game EXE file.  Location of name strings.
            int ptrStart = os.BlockSize;                                                                        // Set file pointer for archive file to block size value.
            br.BaseStream.Position = ptrStart;                                                                  // Set BinaryReader position to value of file pointer.
            for (x = 0; x < NameMax; x++)                                                                       // Cycle through name values.
            {
                string tempName = "";
                cns = new GameString();
                cns.NameValue = br.ReadUInt16();                                                                // Read name pointer value.
                int ptrName = (cns.NameValue + os.PointerOffset);                                               // Add pointer offset to namevalue for EXE pointer value.
                exeBr.BaseStream.Position = ptrName;                                                            // Set position in EXE gamefile from value.
                                                                                                                // Cycle through char bytes until string terminates with 0.
                do
                {
                    tempval = exeBr.ReadByte();
                    if (tempval == 0)
                        break;
                    tempName = tempName + (char)tempval;
                } while (tempval != 0);
                cns.Name = tempName;
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
        public static Offsets GetCharacterOffsets(string formatName)
        {
            Offsets result = CharacterOffsets.characteroffsets[formatName];
            return result;
        }
        public static class CharacterOffsets
        {
            public static Dictionary<string, CharacterNameList.Offsets> characteroffsets = new Dictionary<string, CharacterNameList.Offsets>();
            static CharacterOffsets()
            {
                characteroffsets.Add(Constants.PC_VGA_FORMAT, new CharacterNameList.Offsets(104654, 93300, 37));
                characteroffsets.Add(Constants.PC_EGA_FORMAT, new CharacterNameList.Offsets(123041, 112832, 37));
                characteroffsets.Add(Constants.IIGS_FORMAT, new CharacterNameList.Offsets(106464, 95598, 37));
                characteroffsets.Add(Constants.AMIGA_FORMAT, new CharacterNameList.Offsets(37914, 40, 38));
                characteroffsets.Add(Constants.ST_FORMAT, new CharacterNameList.Offsets(37096, 4, 38));
            }
        }
    }

    public class CityNameList : GameStringList
    {
        public const int CityTotal = 75;
        public static CityNameList InitializeCityNames(FileFormat format)
        {
            int ptrStart;
            int tempval;
            int p_val = 1;
            string tempName = "";
            GameString cns;
            CityNameList t_citylist = new CityNameList();
            string EXEName = Utils.GetEXEFilename(format);
            BinaryReader exeBr = new(File.Open(EXEName, FileMode.Open));

            Offsets os = GetCityOffset(format.Name);
            ptrStart = os.NamePointer;
            exeBr.BaseStream.Position = ptrStart;
            for (int x = 0; x < CityTotal; x++)
            {
                p_val = (int)exeBr.BaseStream.Position;
                cns = new GameString();
                cns.NameValue = p_val - os.PointerOffset;
                tempName = "";
                do
                {
                    tempval = exeBr.ReadByte();
                    if (tempval == 0)
                        break;
                    tempName = tempName + (char)tempval;
                } while (tempval != 0);

                cns.Name = tempName;

                t_citylist.Add(cns);
            }
            exeBr.Close();
            return t_citylist;
        }
        public static Offsets GetCityOffset(string formatName)
        {
            Offsets result = CityOffsets.cityoffsets[formatName];
            return result;
        }

        public static class CityOffsets
        {
            public static Dictionary<string, Offsets> cityoffsets = new Dictionary<string, Offsets>();
            static CityOffsets()
            {
                cityoffsets.Add(Constants.PC_VGA_FORMAT, new CityNameList.Offsets(114179, 93300, 9));
                cityoffsets.Add(Constants.PC_EGA_FORMAT, new CityNameList.Offsets(132731, 93300, 9));
                cityoffsets.Add(Constants.IIGS_FORMAT, new CityNameList.Offsets(106464, 95598, 9));
                cityoffsets.Add(Constants.AMIGA_FORMAT, new CityNameList.Offsets(39539, 40, 10));
                cityoffsets.Add(Constants.ST_FORMAT, new CityNameList.Offsets(38721, 4, 10));
            }
        }

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
            m_mobilized.Add(3, "STATIONARY NPC - NO");
            m_mobilized.Add(4, "RANDOM NPC - NO");
            m_mobilized.Add(40, "SPECIAL CHARACTER - GOLLUM");
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
            for (int a = 1; a < cn.Count; a++)
                m_characterLeader.Add(a, cn[a-1].Name);
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






