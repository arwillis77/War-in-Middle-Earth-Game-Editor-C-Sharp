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

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public partial class frmResourceList : UserControl
    {
        string ResourceType;
        string FileFormat;
        string [] GameFiles;                                /* Array which stores resource filenames for the current file format */
       
        List<ResourceViewData> resDatalist;

        Form frmResourceItem;
        SplitContainer SplitResourceList;
        TabPage Page;
        int CurrentTile;
        //string ArchiveName;
        //string EXEFile;
        //string EXEFull;
        public ListView ListResourceItems;
        

        public frmResourceList()
        {
            InitializeComponent();
        }

        public frmResourceList(string format,string resource)
        {
            InitializeComponent();
            this.ResourceType = resource;
            this.FileFormat = format;
            this.Dock = DockStyle.Fill;
            SplitResourceList = new SplitContainer
            {

                Dock = DockStyle.Fill,
                IsSplitterFixed = false,
                SplitterIncrement = 1,
            };
                this.Controls.Add(SplitResourceList);

        }

        private void frmResourceList_Load(object sender, EventArgs e)
        {
            SetupResourceTable();                                                       /* Setup table in resourcelist object */
            GameFiles = GetFileList(FileFormat);                                        /* Obtain resource file names for game format */





            //ListResourceItems.Height = this.Height - 20;
            //SplitResourceList.Refresh();

            
        }


        public void FillResourceList()
        {
           
            for (int a = 0; a < GameFiles.Count();a++)
            {
                GetResourceSummary(GameFiles[a]);
            }
        }

        public void GetResourceSummary(string filename)
        
        {
            int j;
            int quantity=0;
            int resourceSet=0;
            ResourceViewData resdata;
            BinaryReader br = new(File.Open(filename, FileMode.Open));
            ResourceFile rf = new(br);
            for(j=0;j<rf.ResourceQuantity;j++)
            {
                if(rf.resourceID[j].ID == ResourceType)
                {
                    quantity = (rf.resourceID[j].Quantity) + 1;
                    resourceSet = j;
                }
              

            }





        }




        public string [] GetFileList(string fileFormat)
        {
            string[] result = null;
            int size =0;
            
            if (fileFormat == Constants.PC_VGA_FORMAT)
            {
                //MessageBox.Show("VGA Format!");
                size = ResourceFile.VGA_FILES.Count();
                result = new string[size];
                for (int x = 0; x < size; x++)
                    result[x] = ResourceFile.VGA_FILES[x];
                
            }
            if (fileFormat == Constants.PC_EGA_FORMAT)
            {
                //MessageBox.Show("EGA Format!");

                size = ResourceFile.EGA_FILES.Count();
                result = new string[size];
                for (int x = 0; x < size; x++)
                    result[x] = ResourceFile.EGA_FILES[x];
            }  

            return result;
        }

        /// <summary>
        /// SetupResourceTable - Fills columns up for list of resource.
        /// </summary>
        private void SetupResourceTable()
        {
            ListResourceItems = new ListView
            {
                Size = new Size(395, 795),
                View = View.Details,
                GridLines = true,
                BackColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.White,
            };
            SplitResourceList.Panel1.Controls.Add(ListResourceItems);
            ListResourceItems.Columns.Add("Name");
            ListResourceItems.Columns.Add("#",50,HorizontalAlignment.Right);
            ListResourceItems.Columns.Add("Size (Bytes)",50, HorizontalAlignment.Right);
            ListResourceItems.Columns.Add("Type",50, HorizontalAlignment.Right);
            ListResourceItems.Columns.Add("Resource",50, HorizontalAlignment.Right);
            ListResourceItems.Columns.Add("Offset", 50, HorizontalAlignment.Left);
        }

        


    }
}
