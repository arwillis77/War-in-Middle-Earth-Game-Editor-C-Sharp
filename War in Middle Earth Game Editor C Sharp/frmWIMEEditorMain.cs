﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Resources;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public partial class frmWIMEEditorMain : Form
    {
        /* Global Form Variables */
        TabControl explorerMain;
        TabControl resourceTabs;
        FileFormat currentFormat;
        ImageList ResourceImages;

        config cfg;
        GamePalettesIndex gpi;
        bool GameLoaded = false;
        public frmWIMEEditorMain()
        {
            InitializeComponent();
             explorerMain = new TabControl();
            {
                Dock = DockStyle.Fill;
                
            }
            resourceTabs = new TabControl();
            //this.Controls.Add(explorerMain);
            //this.Controls.Add(resourceTabs);


        }

        public void OpenWIMEGame()
        {
            string p_fileName;
            string p_fileDir;
            bool p_closeGame;

            OpenFileDialog WIMEFileOpen = new OpenFileDialog
            {
                FilterIndex = 0,
                Title = "Select the Executable for the WIME Game you wish to edit.",
                Filter = "WIME Executables (start.exe, lord.exe, earth.sys16 * .*, warinmiddleearth * .*, Command.PRG)|start.exe; lord.exe; earth.sys16*.*; warinmiddleearth*.*; COMMAND.PRG|All Files (*.*)|*.*)", //'WIME PC Executable|start.exe;lord.exe|Apple IIGS Prodos16|earth.sys16*.*|Amiga Executable|warinmiddleearth*.*|ATARI ST Program|COMMAND.PRG|All Files|*.*",
                //InitialDirectory = LoadedSettings.wimeDIRECTORY
                //RestoreDirectory = false
                FileName = ""
            };
            DialogResult dr = WIMEFileOpen.ShowDialog();

            p_fileName = WIMEFileOpen.SafeFileName;
            p_fileDir = Path.GetDirectoryName(WIMEFileOpen.FileName);
            config.WriteConfig(p_fileDir, p_fileName);
            GameLoaded = true;
            gpi = Palette.InitializePalette(currentFormat.Name);
            LoadGame();

        }

        public void LoadGame()
        {
            TabPage GameTab;
            List<TabPage> GameTabs;
        

            PanelEditor.Controls.Add(explorerMain);
            explorerMain.Dock = DockStyle.Fill;

            //explorerMain.TabPages.Add(GameTab);
            resourceTabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Text = "Resource 1",
            };

            ResourceImages = new ImageList
            {
                ImageSize = new Size(16, 16),



            };

            Image[] tabImages = new Image[] {
            War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menutileicon,
            War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.texticon32,
            War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.fonts,
            War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menuanimicon,
            War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menusceneiconbig,
            War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menumapicon,
            War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.menuarchiveicon,
            };
            ResourceImages.Images.AddRange(tabImages);
            
            GameTabs = new List<TabPage>();
                for(int x = 0; x < ResourceFile.chunkID.Length; x++)
                {
                GameTab = new TabPage()
                {
                    Text = ResourceFile.chunkID[x],
                    
                };
                GameTabs.Add(GameTab);
            }
            //explorerMain.TabPages.Add(GameTabs);
            for (int y = 0; y < GameTabs.Count; y++)
            {
                explorerMain.Controls.Add(GameTabs[y]);
                GameTabs[y].ImageIndex = y;
            }
            explorerMain.ImageList = ResourceImages;
                
           








        }




        /* Event Handlers */
        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenWIMEGame();
        }
        private void frmWIMEEditorMain_Load(object sender, EventArgs e)
        {
            string title = string.Concat(Constants.Program_Name, " ", Constants.Program_Version, " ", Constants.ProgramDate);
            this.Text = title;
            cfg = new config();
            if (cfg.ConfigPresent)
            {
                string tempGameName = config.ConfigGetFilename();
                currentFormat = new FileFormat();
                currentFormat = Constants.GetFormatData(tempGameName);
             }
            else
            {
                MessageBox.Show("Welcome!  Either this is your first time using the program, or something is wrong with the program.  Just open a new game file from the menu above to get started",
                "Configuration File Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
        }
    }
}