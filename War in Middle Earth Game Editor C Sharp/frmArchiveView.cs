using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes.SpriteFormData;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public partial class frmArchiveView : UserControl
    {
        private ResourceViewData m_loadedResource;                      // Private object for loaded resource data.
        private int m_characterIndex;                                   // Private int value storing index for character list.
        private Archive m_currentArchive;                                      // Private archive object.
        private FileFormat m_fileFormat;                                // Private object for file format related data.
        private EditorState archivestate = new EditorState();
        private Leaders m_leaders;
        private SpriteColorCycleSets m_spriteCycles;
        public CityBlocks cityblocks;                                   // List of cityblocks data from game EXE.
        public BinaryReader EXE;                                        /* BinaryReader object for opening Game EXE file for city, character */
        private BitArray inventoryBitArray;
        private CharacterNameList cnl;
        private ObjectNameList objectNameList;
        private ImageList mapIcons;
        private Endianness m_endian;
        
        public EditorState ArchiveState
        { get { return archivestate; } set { archivestate = value; } } 
        public Archive SaveArchive
        {
            get => m_currentArchive;
            set =>m_currentArchive = value;
        }
        /// <summary>
        /// Default constructor for Archive Data form.
        /// </summary>

        public frmArchiveView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// frmArchiveView Constructor using loaded resource summary data.  I.E. archive name, offset, etc.
        /// </summary>
        /// <param name="m_loadedresource">ResourceViewData</param>
        public frmArchiveView(ResourceViewData loadedResource)
        {
            InitializeComponent();
            
            m_loadedResource = loadedResource;
            m_characterIndex = m_loadedResource.ResourceNumber;
            m_fileFormat = Utils.GetCurrentFormat();
            m_endian = m_fileFormat.Endian;
            //cnl = CharacterNameList.InitializeCharacterNames(m_fileFormat);
            cnl = new CharacterNameList();
            m_spriteCycles = new SpriteColorCycleSets(m_fileFormat.Name);
            m_currentArchive = new Archive(m_fileFormat,m_characterIndex);
            objectNameList = ObjectNameList.InitializeObjectNames(m_fileFormat);
            ArchiveState = new EditorState();
        }
        
        private void frmArchiveView_Load(object sender, EventArgs e)
        {  
            InitializeCityData();
            InitializeCharacterData();
        }
        private void CloseCharacter()
        {
            SaveArchive.SaveGame.Close();
        }
        /// <summary>
        /// InitializeCharacterData -- Fills textboxes, lists, graphic areas with individual character data.
        /// </summary>
        private void InitializeCharacterData()
        {
            txtCharacterName.Text = m_loadedResource.Name;
            textBoxCharacterValue.Text = SaveArchive.SelectedCharacter.NameCode.ToString();
            textBoxByte34.Text = SaveArchive.SelectedCharacter.Bytes3and4.ToString();
            numberArmyQuantity.Value = SaveArchive.SelectedCharacter.ArmyQuantity;
            numberArmyTotal.Value = SaveArchive.SelectedCharacter.ArmyTotal;
            numberHPQuantity.Value = SaveArchive.SelectedCharacter.HPTotal;
            numberHitPointMax.Value = Byte.MaxValue;
            numberMoraleQty.Value = SaveArchive.SelectedCharacter.MoraleQuantity;
            numberMoraleTotal.Value = 9;
            textInventoryValue.Text = SaveArchive.SelectedCharacter.GameObjects.ToString();
            LoadObjects();
            numberPower.Value = SaveArchive.SelectedCharacter.PowerLevel;
            numberStealth.Value = SaveArchive.SelectedCharacter.Stealth;
            numberStealth.Maximum=byte.MaxValue;
            checkBoxVisible.Checked = SaveArchive.GetVisibility();
            txtLocationX.Text = SaveArchive.SelectedCharacter.Location.x.ToString();
            txtLocationY.Text = SaveArchive.SelectedCharacter.Location.y.ToString();
            textDestinationX.Text = SaveArchive.SelectedCharacter.Destination.x.ToString();
            textDestinationY.Text = SaveArchive.SelectedCharacter.Destination.y.ToString();
            cboLocation.Text = ParseCity(SaveArchive.SelectedCharacter.Location);
            cboDestination.Text = ParseCity(SaveArchive.SelectedCharacter.Destination);
            ArmyMobilize armyMobilize = new ArmyMobilize();
            armyMobilize.FillMobilizeCombo(dropMobilized,SaveArchive.SelectedCharacter.ValueMobilize);
            numericSprite.Value = SaveArchive.SelectedCharacter.SpriteType;
            textSpriteName.Text = SpriteType.GetSpriteData(SaveArchive.SelectedCharacter.SpriteType);
            textFollowValue.Text = SaveArchive.SelectedCharacter.LeaderFollow.ToString();
            textBoxByte24.Text = SaveArchive.SelectedCharacter.Byte24.ToString();
            textBoxByte31.Text = SaveArchive.SelectedCharacter.Byte31.ToString();
            textBoxByte32.Text = SaveArchive.SelectedCharacter.Byte33.ToString();
            textBoxByte33.Text = SaveArchive.SelectedCharacter.Byte33.ToString();
            textBoxByte37.Text = SaveArchive.SelectedCharacter.Byte37.ToString();
            InitializeSpriteColors();
            m_leaders = new Leaders(cnl);
            m_leaders.InitializeLeaderData(comboBoxFollow, SaveArchive.SelectedCharacter.LeaderFollow);
            CloseCharacter();

        }

        private void LoadObjects()
        {
            UInt16 p_objectValue = SaveArchive.SelectedCharacter.GameObjects;
            GameString TransferObjects = new GameString();
            inventoryBitArray = new BitArray(BitConverter.GetBytes(p_objectValue));
             listInventory.ForeColor = Color.White;
            for(int d=0;d<inventoryBitArray.Length;d++)
            {
                if (inventoryBitArray[d].Equals(true))
                {
                    TransferObjects.Name = objectNameList[d].Name;
                    TransferObjects.NameValue = objectNameList[d].NameValue;
                    ListViewItem lvi = listInventory.Items.Add(TransferObjects.Name);
                    lvi.SubItems.Add(TransferObjects.NameValue.ToString());
                }
            }
            frmStringDisplay fs = new frmStringDisplay(objectNameList);
            fs.Show();
        }
        private void InitializeSpriteColors()
        {
            int value = SaveArchive.SelectedCharacter.SpriteType;
            for (int u = 0; u < SpriteColorCycleSets.SpriteTotal; u++)
            {
                if(u==value)
                {
                    foreach (int val in m_spriteCycles.ColorCycleList[u].SpriteColors)
                        comboSpriteColor.Items.Add(val);
                }
            }
            comboSpriteColor.Text = SaveArchive.SelectedCharacter.SpriteColor.ToString();
        }

        /* TO DO -- MOVE CITY METHODS TO CITY CLASS */

        private void InitializeCityData()
        {
            int i;
            CityBlock cityBlock;                   // Object for individual city block data.
            cityblocks = new CityBlocks();              // List of city block data objects.
            City initcity = new City(m_fileFormat);
            for (i = 0; i < Constants.CITY_TOTAL; i++)
            {
                cityBlock = new CityBlock(initcity.EXEFile, initcity.Pointer + (initcity.BlockSize*i), m_endian);
                cityblocks.Add(cityBlock);
            }
            initcity.EXEFile.Close();
        }
        private string ParseCity (Archive.Position position)
        {
            CityBlock parse;
            UInt16 currentX = position.x;
            UInt16 currentY = position.y;
            string result = City.Default;
            City initcity = new City(m_fileFormat);
            //InitCity initcity = new InitCity(CurrentFormat);
            int j;
            for (j = 0; j < cityblocks.Count;j++)
            {
                parse = new CityBlock(initcity.EXEFile, initcity.Pointer + (initcity.BlockSize * j),m_endian);

                if (currentX == parse.x )
                {
                    if (currentY == parse.y)
                    {
                        result = initcity.GetCityName(parse.locationpointer);
                        break;
                    }
                }

            }
            initcity.EXEFile.Close ();
            return result;
        }
       

        /* ************************* EVENT HANDLERS FOR FORM ************************************************/
        private void numericSprite_ValueChanged(object sender, EventArgs e)
        {
            ArchiveState.DataSaved = false;
            int val;
            if(numericSprite.Value == 16)
                numericSprite.Value += 64;
            else if(numericSprite.Value == 79)
                numericSprite.Value -= 64;
            val = (int)numericSprite.Value;
            textSpriteName.Text = SpriteType.GetSpriteData(val);
        }

        private void comboBoxFollow_SelectedValueChanged(object sender, EventArgs e)
        {
            int result = m_leaders.GetLeaderValue(comboBoxFollow.Text);
            textFollowValue.Text = result.ToString();
        }
    }
}
