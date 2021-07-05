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
        public int[] ScaleValues = { 1, 2, 3 };

        



        public frmViewResource()
        {
            InitializeComponent();
            for (int x = 0; x < ScaleValues.Count(); x++)
                this.dropdownScale.DropDownItems.Add(ScaleValues[x].ToString());


        }
    }
}
