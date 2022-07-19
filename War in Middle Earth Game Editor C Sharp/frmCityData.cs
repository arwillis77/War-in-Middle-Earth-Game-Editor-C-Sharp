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
    public partial class frmCityData : Form
    {       
        public frmCityData()
        {
            InitializeComponent();
        }
        public frmCityData(CityBlocks cb)
        {
            InitializeComponent();
            ListViewItem lvi;
            FileFormat CurrentFormat = Utils.GetCurrentFormat();
            City initcity = new City(CurrentFormat);
            initcity.EXEFile.Close();
            //InitCity initcity = new InitCity(CurrentFormat);
            string cityName;            
            textCityBlockStart.Text = initcity.Pointer.ToString();          
            textDataBlockSize.Text = initcity.BlockSize.ToString();    
            for (int i = 0; i < cb.Count; i++)
            {
                lvi = new ListViewItem(cb[i].locationpointer.ToString());
                cityName = initcity.GetCityName(cb[i].locationpointer);
                lvi.SubItems.Add(cityName);
                lvi.SubItems.Add(cb[i].x.ToString());
                lvi.SubItems.Add(cb[i].y.ToString());
                listView1.Items.Add(lvi);
            }
        }
    }
}
