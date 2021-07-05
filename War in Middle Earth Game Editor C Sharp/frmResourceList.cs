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
    public partial class frmResourceList : UserControl
    {
        string ResourceType;
        Form frmResourceItem;
        SplitContainer SplitResourceList;
        TabPage Page;
        int CurrentTile;
        string ArchiveName;
        string EXEFile;
        string EXEFull;
        public ListView ListResourceItems;

        public frmResourceList()
        {
            InitializeComponent();
        }

        public frmResourceList(string resource)
        {
            InitializeComponent();
            ResourceType = resource;
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
