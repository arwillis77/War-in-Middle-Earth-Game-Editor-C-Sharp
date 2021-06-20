using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        FileFormat currentFormat;
        config cfg;
        public frmWIMEEditorMain()
        {
            InitializeComponent();
            TabControl explorerMain = new TabControl();
            TabControl resourceTabs;
            this.Controls.Add(explorerMain);
        }

        private void OpenButton_Click(object sender, EventArgs e)
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
            config.WriteConfig()

        }
       

        private void frmWIMEEditorMain_Load(object sender, EventArgs e)
        {
            cfg = new config();

            if (cfg.ConfigPresent)
            {
                //dir = config.ReadConfig();
                string tempGameName = config.ConfigGetFilename();
                currentFormat = Constants.GetFormatData(tempGameName);
                MessageBox.Show(currentFormat.Name);
            }
            else
            {
               

               
            }
        }
    }
}