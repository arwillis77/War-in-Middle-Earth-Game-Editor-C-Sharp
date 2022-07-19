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
    public partial class frmViewResource : UserControl
    {
        private ResourceViewData m_loadedresource;
        private string m_filenamedata;
        private int m_scale;
        
        
        public ResourceViewData loadedResource
        {
            get => m_loadedresource;
            set=>m_loadedresource = value;
        }
        public string filenameData
        {
            get => m_filenamedata;
            set =>m_filenamedata = value;
        }
        public int ResScale
        {
            get=> m_scale;
            set=> m_scale = value;  
        }

        public frmViewResource(ResourceViewData rvd, int scale)

        {
            InitializeComponent();
            loadedResource = rvd;
            if (rvd.FileName != ResourceFile.ARCHIVE_ID)
            {
                filenameData = string.Concat(rvd.FileName, ".RES");
            }
                      

            else
            { filenameData = string.Concat(rvd.FileName, ".DAT"); 
            }
            ResScale = scale;
         }

        public frmViewResource()
        {
            InitializeComponent();        
        }

        private void frmViewResource_Load(object sender, EventArgs e)
        {

            ViewIMAGResource();
        }


        public void ViewIMAGResource()
        {
            int p_endOffset;
            string p_resource = ResourceFile.chunkTypes[4];
            string p_resourceFile;
            labelGameStatus.Text = loadedResource.Name + " " + loadedResource.DataSize + " bytes, (";
            





        }



        private void pnlMainDisplay_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
