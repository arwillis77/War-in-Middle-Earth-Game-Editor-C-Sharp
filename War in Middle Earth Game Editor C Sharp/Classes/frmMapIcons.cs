using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    public partial class frmMapIcons : Form
    {
        private IMemory memory;                                             /* Storage array for image data */
        private bool m_escape;                                              /* Flag to escape method */
        private bool firstLoad;                                             /* Flag for whether this is the first load for the form. */
        private bool formLoaded;                                            /* Flag for whether form is loaded. */
        private IMAG_Resource m_IMAGResource;                               /* Class object for Image resources */
        private BinaryReader m_reader;                                      /* Binary Reader object for i/o operations for resource binary file */

        public frmMapIcons()
        {
            InitializeComponent();
        }

        public void LoadMapData()
        {


        }


    }
}
