using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    public partial class frmStringDisplay : Form
    {


        //public CharacterNameList NameList;
        public GameStringList NameList;


        public frmStringDisplay()
        {
            InitializeComponent();
        }


        public frmStringDisplay(GameStringList olist)
        {
            InitializeComponent();
            NameList = olist;
        }


        private void frmStringDisplay_Load(object sender, EventArgs e)
        {
            ListViewItem lvi;
            for (int i = 0; i < NameList.Count;i++)
            {   
                lvi = new ListViewItem(NameList[i].NameValue.ToString());
                lvi.SubItems.Add(NameList[i].Name);
                listView1.Items.Add(lvi);
            }
        }
    }
}
