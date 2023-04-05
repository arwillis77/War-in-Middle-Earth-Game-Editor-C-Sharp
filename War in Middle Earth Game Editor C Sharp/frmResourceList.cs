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
 /// <summary>
 /// frmResourceList - displays list of resources.  Each resource separated by type
 /// in tabs.
 /// </summary>
    
    
    public partial class frmResourceList : UserControl
    {
        UserControl ResourceForm;
        string ResourceType;
        public ListView ListResourceItems;
        public ListViewItem lvi;
        public ResourceList SelectedResource;
        public ResourceViewData SelectedItem;                       /* Individual Resource ITem */
        public string SelectedResourceItemType;
        public MapTile mt;
        /// <summary>
        /// Default constructor for the ResourceList form 
        /// </summary>
        public frmResourceList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor for Resource List form.
        /// </summary>
        /// <param name="resource">Resource Type for list.  Determines which list resource is sent to for display.</param>
        /// <param name="rdata">List of resource summary data to display in listview.</param>
        public frmResourceList(string resource, ResourceList rdata)
        {
            InitializeComponent();
            this.ResourceType = resource;
            this.SelectedResource = rdata;
            this.SelectedResourceItemType = "";
            this.Dock = DockStyle.Fill;
        }

        private void frmResourceList_Shown(object sender, EventArgs e)
        {
           
            splitResourceContainer.SplitterDistance = listResourceItems.Width + 15;
            listResourceItems.Dock = DockStyle.Fill;
        }



        private void frmResourceList_Load(object sender, EventArgs e)
        {
            //SetupResourceTable();                                                       /* Setup table in resourcelist object */
            FillTable();                                                                /* Fill resource list and archive list */
            ResourceListResize();
        }



        private void InitializeMapTiles()
        {
            mt = new MapTile();

        }


        private void ResourceListResize()
        {
            int width = 0;
            //MessageBox.Show("count " + listResourceItems.Columns.Count);
            for (int g = 0; g <= listResourceItems.Columns.Count - 1; g++)
                listResourceItems.AutoResizeColumn(g, ColumnHeaderAutoResizeStyle.ColumnContent);
            listResourceItems.Columns[5].Width += 10;
            for (int h = 0; h <= listResourceItems.Columns.Count - 1; h++)
                width += listResourceItems.Columns[h].Width;
            listResourceItems.Width = width+5;

        }
        private void FillTable()
        {
            int x;
            string currentresource;
            for (x=0; x<SelectedResource.Count; x++)
            {
                currentresource = SelectedResource[x].Type;
                if (currentresource == ResourceType)
                {
                    lvi = new ListViewItem(SelectedResource[x].Name);
                    lvi.SubItems.Add(SelectedResource[x].ResourceNumber.ToString());
                    lvi.SubItems.Add(SelectedResource[x].DataSize.ToString());
                    lvi.SubItems.Add(SelectedResource[x].Type);
                    lvi.SubItems.Add(SelectedResource[x].FileName);
                    lvi.SubItems.Add(SelectedResource[x].Offset.ToString());
                    listResourceItems.Items.Add(lvi);                 
                }
            }
        }



        private void DisplayResource(bool TabDisplay)
        {
            UserControl ResourceForm;
            TabPage Page;
            if (listResourceItems.SelectedItems.Count == 0)
                return;
            SelectedItem = new ResourceViewData(listResourceItems);
            string resourceType = SelectedItem.Type;


            if (SelectedItem.Type == ResourceFile.chunkTypes[6])
            {
                ResourceForm = new frmArchiveView(SelectedItem);
            }
            else if (SelectedItem.Type == ResourceFile.chunkTypes[5])
            {
                if (mt == null)
                    InitializeMapTiles();
                Config cfg = new Config(true);
                ResourceForm = new frmMapView(SelectedItem,mt,cfg.Scale);
            }
            else if (SelectedItem.Type == ResourceFile.chunkTypes[0])
            {
                if (mt == null)
                    InitializeMapTiles();
                Config cfg = new Config(true);
                ResourceForm = new frmTileView(SelectedItem, mt, cfg.Scale, SelectedItem.ResourceNumber);
            }
            else
            {
                ResourceForm = new frmViewResource(SelectedItem, 3);
                MessageBox.Show("Else!");
            
            }


            //ResourceForm.Dock = DockStyle.Fill;

            ResourceForm.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom);
            
            if (TabDisplay)
            {
                MessageBox.Show("TabDisplay");
                Page = new TabPage();
                Page.Text = SelectedItem.Name;
                Page.Controls.Add(ResourceForm);
                ResourceForm.Show();
            }
            else
            {
                LoadResourceViewer(ResourceForm);
                ResourceForm.Show();
            }
        }

        private void LoadResourceViewer(UserControl control)
        {
            control.Dock = DockStyle.Fill;
            this.splitResourceContainer.Panel2.Controls.Clear();
            this.splitResourceContainer.Panel2.Controls.Add(control);
        }



        private void listResourceItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayResource(false);
        }

        private void listResourceItems_ItemActivate(object sender, EventArgs e)
        {
            DisplayResource(true);
        }
    }
}