using System;
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

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public partial class frmArchiveView : UserControl
    {

        private ResourceViewData m_loadedresource;                      // Private object for loaded resource data.
        private int m_characterindex;                                   // Private int value storing index for character list.
        private string m_filenamedata;                                  // Private string for archive filename -- Archive.DAT.
        private Archive m_archive;                                      // Private archive object.
        private FileFormat m_fileformat;                                // Private object for file format related data.

        private EditorState archivestate = new EditorState();
        


        public CityBlocks cityblocks;                                   // List of cityblocks data from game EXE.
        public BinaryReader EXE;                                        /* BinaryReader object for opening Game EXE file for city, character
                                                                           name and inventory object data.   */
        public EditorState ArchiveState
        { get { return archivestate; } set { archivestate = value; } }      
        public ResourceViewData LoadedArchive
        {
            get => m_loadedresource;
            set => m_loadedresource = value;
        }
        public int CharacterIndex
        {
            get => m_characterindex;
            set => m_characterindex = value;
        }
        public string FilenameData
        {
            get => m_filenamedata;
            set => m_filenamedata = value;
        }

       
        public Archive SaveArchive
        {
            get => m_archive;
            set =>m_archive = value;
        }
     
   
        public FileFormat CurrentFormat
        {
            get => m_fileformat;
            set=>m_fileformat=value;
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
        public frmArchiveView(ResourceViewData m_loadedresource)
        {
            InitializeComponent();
            LoadedArchive = m_loadedresource;
            CharacterIndex = m_loadedresource.ResourceNumber;
            CurrentFormat = Utils.GetCurrentFormat();
            SaveArchive = new Archive(CurrentFormat,CharacterIndex);
            FilenameData = SaveArchive.Filename;
            ArchiveState = new EditorState();
        }
        
        private void frmArchiveView_Load(object sender, EventArgs e)
        {
            InitializeCityData();
            InitializeCharacterData();
            Form cdata = new frmCityData(cityblocks);
            cdata.Show();
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
            //SaveArchive = new Archive(CurrentFormat,CharacterIndex);
            txtCharacterName.Text = LoadedArchive.Name;
            numberArmyQuantity.Value = SaveArchive.SelectedCharacter.ArmyQuantity;
            numberArmyTotal.Value = SaveArchive.SelectedCharacter.ArmyTotal;
            numberHPQuantity.Value = SaveArchive.SelectedCharacter.HPTotal;
            numberHitPointMax.Value = Byte.MaxValue;
            numberMoraleQty.Value = SaveArchive.SelectedCharacter.MoraleQuantity;
            numberMoraleTotal.Value = 9;
            numberPower.Value = SaveArchive.SelectedCharacter.PowerLevel;
            numberStealth.Value = SaveArchive.SelectedCharacter.Stealth;
            numberStealth.Maximum=byte.MaxValue;
            txtLocationX.Text = SaveArchive.SelectedCharacter.Location.x.ToString();
            txtLocationY.Text = SaveArchive.SelectedCharacter.Location.y.ToString();
            textDestinationX.Text = SaveArchive.SelectedCharacter.Destination.x.ToString();
            textDestinationY.Text = SaveArchive.SelectedCharacter.Destination.y.ToString();
            cboLocation.Text = ParseCity(SaveArchive.SelectedCharacter.Location);
            cboDestination.Text = ParseCity(SaveArchive.SelectedCharacter.Destination);
            CloseCharacter();

        }

        private void InitializeCityData()
        {
            int i;
            City.CityBlock cityBlock;                   // Object for individual city block data.
            cityblocks = new CityBlocks();              // List of city block data objects.
            City initcity = new City(CurrentFormat);
            for (i = 0; i < Constants.CITY_TOTAL; i++)
            {
                cityBlock = new City.CityBlock(initcity.EXEFile, initcity.Pointer + (initcity.BlockSize*i));
                cityblocks.Add(cityBlock);
            }
            initcity.EXEFile.Close();
        }

        private string ParseCity (Archive.Position position)
        {
            City.CityBlock parse;
            UInt16 currentX = position.x;
            UInt16 currentY = position.y;
            string result = City.Default;
            City initcity = new City(CurrentFormat);
            //InitCity initcity = new InitCity(CurrentFormat);
            int j;
            for (j = 0; j < cityblocks.Count;j++)
            {
                parse = new City.CityBlock(initcity.EXEFile, initcity.Pointer + (initcity.BlockSize * j));

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


        private void numericSprite_ValueChanged(object sender, EventArgs e)
        {
            ArchiveState.DataSaved = false;
            
            int val;
            string type;
            
            if(numericSprite.Value == 16)
                numericSprite.Value = numericSprite.Value + 64;
            else if(numericSprite.Value == 79)
                numericSprite.Value = numericSprite.Value - 64;
            val = (int)numericSprite.Value;
            type = Sprite.GetSpriteData(val);
            textSpriteName.Text = type;
        }
    }
}
