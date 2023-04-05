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
        /* Form variables */
        private int[] scaleValues = { 1, 2, 3 };                /* Scale values for image sizing */
        private MapTile m_mapTile;                              /* Initialize MapTiles for export map tile */
        private int m_scale;                                    /* Image scale variable */
        bool GameLoaded = false;                                /* Flag for whether game loaded */
        /* Form objects */
        FileFormat currentFormat;                               /* Store data based on file format of game */
        Config cfg;                                             /* Store config data */
        /* Form Collections/Lists */
        CharacterNameList characternamelist;                    /* Initialize character names */
        CityNameList citynamelist;                              /* Initialize city names */
        ImageList ResourceImages;                               /* Images for resource types in tabs */
        private Image [] m_tabImages;                           
        ResourceList LoadedResourceList;                        /* List of resource types */
        ResourceList LoadedArchiveList;                         /* List of character data */
        /* Form Objects */
        frmResourceList m_frmResourceList;                      /* Form to display resource list data */
        Form m_frmCityData;                                     /* Form to display city data */
        /* Tab Objects */
        TabPage m_tabpageResourceExplorer;                      /* Main TabPage - Resource Explorer */
        TabPage m_tabpageGameTab;
        TabControl explorerMain;                                /* Main TabControl - Resource Explorer */
        TabControl resourceTabs;                                /* Secondary TabControl - Individual Resources */
        List<TabPage> GameTabs;                                 /* TabPage Collection for GameTabs */
        public frmMapView p_mapview;


        public frmWIMEEditorMain()
        {
            InitializeComponent();
            toolStrip1.Renderer = new ToolStripOverride();
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
        private void frmWIMEEditorMain_Load(object sender, EventArgs e)
        {
          
            string title = string.Concat(Constants.Program_Name, " ", Constants.Program_Version, " ", Constants.ProgramDate);
            this.Text = title;

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
        public void OpenWIMEGame()
        {
            string p_fileName;
            string p_fileDir;
            OpenFileDialog WIMEFileOpen = new OpenFileDialog 
            {
                FilterIndex = 0,
                Title = "Select the Executable for the WIME Game you wish to edit.",
                Filter = Constants.filterPCVGA+"|"+Constants.filterIIGS+"|"+Constants.filterAmiga+"|"+Constants.filterST,
                FileName = ""
            };
            DialogResult dr = WIMEFileOpen.ShowDialog();
            p_fileName = WIMEFileOpen.SafeFileName;
            p_fileDir = Path.GetDirectoryName(WIMEFileOpen.FileName);
            m_scale = scaleValues[1];
            cfg = new Config(p_fileDir, p_fileName, m_scale);
            cfg.WriteConfig(cfg.GameDirectory, cfg.GameExecutable, cfg.Scale);
            currentFormat = Constants.GetFormatData(cfg.GameExecutable);
            characternamelist = new CharacterNameList();
            //frmStringDisplay fst = new frmStringDisplay(characternamelist);
            //fst.Show();
            citynamelist = new CityNameList();
          
            
            GameLoaded = true;
            LoadGame();
        }


        public void LoadGame()
        {
            LoadedResourceList = new ResourceList(currentFormat);                               /* Intializes list of all resources for the file format */
            LoadedArchiveList = new ResourceList(currentFormat, characternamelist);             /* Initializes list of archive characters */
            ResourceImages = new ImageList   
            {
                ImageSize = new Size(16, 16),
            };
            m_tabpageResourceExplorer = new TabPage();                                          /* TabPag for ResourceExplorer Main TabPage.  Placed into the ExplorerMain TabControl */

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
                m_tabpageGameTab = new TabPage()
                {
                    Text = ResourceFile.chunkID[x],
                    ImageIndex = x,
                };
                if (ResourceFile.chunkID[x] == ResourceFile.ARCHIVE_ID)
                    m_frmResourceList = new frmResourceList(ResourceFile.chunkTypes[x], LoadedArchiveList);  
                else
                    m_frmResourceList = new frmResourceList(ResourceFile.chunkTypes[x], LoadedResourceList);
                m_tabpageGameTab.Controls.Add(m_frmResourceList);
                m_frmResourceList.Show();
                GameTabs.Add(m_tabpageGameTab);
            }
            m_tabpageResourceExplorer.Text = "Resource Explorer";
            m_tabpageResourceExplorer.ImageIndex = 7;
            PanelEditor.Controls.Add(explorerMain);
            explorerMain.TabPages.Add(m_tabpageResourceExplorer);
            m_tabpageResourceExplorer.Controls.Add(resourceTabs);
            resourceTabs.Dock = DockStyle.Fill;
            
            for (int y = 0; y < GameTabs.Count; y++)
            {
                resourceTabs.TabPages.Add(GameTabs[y]);
                GameTabs[y].ImageIndex = y;
            }
        }
        private void LoadCityList()
        {
            m_frmCityData = new frmCityData();
            m_frmCityData.Show();
        }
        private void ExportMapTiles()
        {
            m_mapTile = new MapTile();
            if (m_mapTile == null)
            {
                MessageBox.Show("MapTile is null", "Null Error!");
            }
            Form m_frmMapExport = new frmExportTile(m_mapTile, m_scale);
            m_frmMapExport.Show();
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

        private void CloseGame()

        {  
            GameTabs.Clear();
            GameLoaded = false;
            LoadedResourceList = null;
            LoadedArchiveList = null;
            //GameFiles = null;
            m_mapTile = null;
            m_frmCityData.Close();
            currentFormat = null;
            m_tabpageGameTab.Hide();
            m_tabpageGameTab = null;
            m_tabpageResourceExplorer.Hide();
            m_tabpageResourceExplorer = null;
            m_frmResourceList.Hide();
            m_frmResourceList = null;
            explorerMain.Hide();
            explorerMain = null;
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
        private void menuItemShowMenuBar_Click(object sender, EventArgs e)
        {
            if(menuItemShowMenuBar.Checked == true)
            {
                menuItemShowMenuBar.Checked = false;
                menuStrip1.Visible = false;
            }
        }

        private void filemenuCloseGame_Click(object sender, EventArgs e)
        {
            CloseGame();
        }
    }
}