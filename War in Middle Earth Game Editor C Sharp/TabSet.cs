using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    internal class TabSet
    {

        private TabControl m_resourceTabs;



        public TabControl ResourceTabs
        {
            get { return m_resourceTabs; }
            set { m_resourceTabs = value; }

        }

        public TabSet()
        { 
            m_resourceTabs = new TabControl();
        }



    }
}
