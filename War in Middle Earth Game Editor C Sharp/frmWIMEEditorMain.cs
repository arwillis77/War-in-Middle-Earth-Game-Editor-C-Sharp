using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using War_in_Middle_Earth_Game_Editor_C_Sharp.Classes;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public partial class frmWIMEEditorMain : Form
    {
        /* Global Form Variables */
        private int[] scaleValues = { 1, 2, 3 };
        public string[] GameFiles;
        private MapTile m_mapTile;
        private GamePalettesIndex m_gpi;
        private int m_scale;
        public int imageScale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }
        TabControl explorerMain;
        TabControl resourceTabs;
        List<TabPage> GameTabs;
        FileFormat currentFormat;        
        CharacterNameList characternamelist;
        ObjectNameList objectnamelist;
        CityNameList citynamelist;
        ImageList ResourceImages;
        private Image [] m_tabImages;
        ResourceList LoadedResourceList;
        ResourceList LoadedArchiveList;
        Config cfg;
        bool GameLoaded = false;
        public frmWIMEEditorMain()
        {
            InitializeComponent();
            for (int x = 0; x < scaleValues.Count(); x++)
                this.toolStripComboBoxScale.Items.Add(scaleValues[x].ToString());
            m_scale = 1;
            //resourceTabs = new TabControl();
            m_tabImages = new Image[] {
                War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menutileicon,
                War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.texticon32,
                War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.fonts,
                War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menuanimicon,
                War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menusceneiconbig,
                War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menumapicon,
                War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menuarchiveicon
            };
        }
        public void OpenWIMEGame()
        {
            string p_fileName;
            string p_fileDir;
            OpenFileDialog WIMEFileOpen = new OpenFileDialog
            {
                FilterIndex = 0,
                Title = "Select the Executable for the WIME Game you wish to edit.",
                Filter = "WIME Executables (start.exe, lord.exe, earth.sys16 * .*, warinmiddleearth * .*, Command.PRG)|start.exe; lord.exe; earth.sys16*.*; warinmiddleearth*.*; COMMAND.PRG|All Files (*.*)|*.*)", //'WIME PC Executable|start.exe;lord.exe|Apple IIGS Prodos16|earth.sys16*.*|Amiga Executable|warinmiddleearth*.*|ATARI ST Program|COMMAND.PRG|All Files|*.*",
                FileName = ""
            };
            DialogResult dr = WIMEFileOpen.ShowDialog();
            p_fileName = WIMEFileOpen.SafeFileName;
            p_fileDir = Path.GetDirectoryName(WIMEFileOpen.FileName);
            m_scale = 1;
            cfg = new Config(p_fileDir, p_fileName, m_scale);
            cfg.WriteConfig(cfg.GameDirectory, cfg.GameExecutable, cfg.Scale);
            currentFormat = new FileFormat();
            currentFormat = Constants.GetFormatData(cfg.GameExecutable);
            GameLoaded = true;
            characternamelist = CharacterNameList.InitializeCharacterNames(currentFormat);
            Form newform = new frmStringDisplay(characternamelist);
            newform.Show();
            citynamelist = CityNameList.InitializeCityNames(currentFormat);
            //objectnamelist = ObjectNameList.InitializeObjectNames(currentFormat);
            LoadGame();
        }

        private void frmWIMEEditorMain_Load(object sender, EventArgs e)
        {
           
            string title = string.Concat(Constants.Program_Name, " ", Constants.Program_Version, " ", Constants.ProgramDate);
            this.Text = title;
            toolStripComboBoxScale.Text = imageScale.ToString();
            //cfg = new Config();
            //if (cfg.ConfigPresent)
            //{
            //    string tempGameName = Config.ConfigGetFilename();
            //    currentFormat = new FileFormat();
            //    currentFormat = Constants.GetFormatData(tempGameName);
            //}
            //else
            //{
            //    MessageBox.Show("Welcome!  Either this is your first time using the program, or something is wrong with the program.  Just open a new game file from the menu above to get started",
            //    "Configuration File Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        public void LoadGame()
        {
            frmResourceList p_form;
            LoadedResourceList = ResourceList.InitializeResourceList(currentFormat);                /* Intializes list of all resources for the file format */
            LoadedArchiveList = ResourceList.InitializeArchive(currentFormat, characternamelist);   /* Initializes list of archive characters */
            m_gpi = Palette.InitializePalette(currentFormat.Name);
            ResourceImages = new ImageList   
            {
                ImageSize = new Size(16, 16),
            };
            TabSet tabset = new TabSet();
            TabPage ResourceExplorer = new TabPage();                                               /* TabPag for ResourceExplorer Main TabPage.  Placed into the ExplorerMain TabControl */
                                                                                                    /* Collection of all the Game Resource Tabs */
            TabPage GameTab;                                                                        /* Individual GameTab */
            // **** TO DO -- GetFileList function */
            explorerMain = new TabControl
            {
                Dock = DockStyle.Fill,
            };
            resourceTabs = new TabControl();
            ResourceImages.Images.AddRange(m_tabImages);
            ResourceImages.Images.Add(currentFormat.Icon);
            resourceTabs.ImageList = ResourceImages;
            explorerMain.ImageList = ResourceImages;
            GameTabs = new List<TabPage>();
            for (int x = 0; x < ResourceFile.chunkID.Length; x++)
            {
                GameTab = new TabPage()
                {
                    Text = ResourceFile.chunkID[x],
                    ImageIndex = x,
                };
                if (ResourceFile.chunkID[x] == ResourceFile.ARCHIVE_ID)
                {            
                    p_form = new frmResourceList(ResourceFile.chunkTypes[x], LoadedArchiveList);                    
                }
                else
                {
                    p_form = new frmResourceList(ResourceFile.chunkTypes[x], LoadedResourceList);
                }
                GameTab.Controls.Add(p_form);
                p_form.Show();
                GameTabs.Add(GameTab);
            }
            ResourceExplorer.Text = "Resource Explorer";
            ResourceExplorer.ImageIndex = 7;
            PanelEditor.Controls.Add(explorerMain);
            explorerMain.TabPages.Add(ResourceExplorer);
            ResourceExplorer.Controls.Add(resourceTabs);
            resourceTabs.Dock = DockStyle.Fill;
            for (int y = 0; y < GameTabs.Count; y++)
            {
                resourceTabs.TabPages.Add(GameTabs[y]);
                GameTabs[y].ImageIndex = y;
            }
        }
        private void LoadCityList()
        {
            Form p_form = new frmCityData();
            p_form.Show();
        }
        private void ExportMapTiles()
        {

            m_mapTile = new MapTile(m_gpi.tile);
            if (m_mapTile == null)
            {
                MessageBox.Show("MapTile is null", "Null Error!");

            }

            Form p_form = new frmExportTile(m_mapTile, imageScale);
            p_form.Show();
        }
        /* Event Handlers */
        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenWIMEGame();
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            CloseProgram();
        }

        private void CloseProgram()
        {
            MessageBoxButtons closebuttons = MessageBoxButtons.YesNoCancel;
            
            if(GameLoaded==true)
            {
                DialogResult result = MessageBox.Show("You still have a loaded game open.  Do you still wish to close?", "Close Program without Saving?", closebuttons, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    this.Close();
                else 
                {
                    return;
                }
                
            }
        }


        private void toolCityList_Click(object sender, EventArgs e)
        {
            if (GameLoaded == true)
                LoadCityList();
            else
            {
                MessageBox.Show("In order to view city list, a gamefile needs to be loaded.  Please load game and try again", "Game Not Loaded!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }  
        }


 
        /// <summary>
        /// clickExportTiles - When clicked in either menubar or toolbar.
        /// </summary>
        private void clickExportTiles()
        {
            if (GameLoaded == true)
                ExportMapTiles();
            else
            {
                MessageBox.Show("In order to export map tiles, a gamefile needs to be loaded.  Please load game and try again", "Game Not Loaded!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void toolStripExportTileset_Click(object sender, EventArgs e)
        {
            clickExportTiles();
        }
        private void exportGameTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickExportTiles();
        }
        private void clickOpenWIMEGame()
        {
            OpenWIMEGame();
        }

        private void toolStripOpenWIMEGame_Click(object sender, EventArgs e)
        {
            clickOpenWIMEGame();
        }

        private void toolStripMenuItemOpenWIME_Click(object sender, EventArgs e)
        {
            clickOpenWIMEGame();
        }
        private void toolStripComboBoxScale_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (GameLoaded==false)
                return;
            Config m_cfg = new Config(true);

            int scaleVal = 0;
            string result = toolStripComboBoxScale.Text;
            scaleVal = Convert.ToInt16(result);
            imageScale = scaleVal;
            m_cfg.Scale = scaleVal;
            m_cfg.WriteConfig(m_cfg.GameDirectory, m_cfg.GameExecutable, m_cfg.Scale);
        }

    }
}