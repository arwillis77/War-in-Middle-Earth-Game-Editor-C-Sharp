using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    internal class Editor
    {
        private Graphics p_device;
        private Bitmap p_surface;
        private PictureBox p_pb;
        private Form p_form;
        private EditorState p_state;
        public Editor(Form frm, int width, int height)
        {
            p_form = frm;
            p_form.FormBorderStyle = FormBorderStyle.FixedSingle;
            p_form.MaximizeBox = true;
            p_form.Size = new Size(width, height);
            p_state = new EditorState();
        }
        ~Editor()
        {
            p_device.Dispose();
            p_surface.Dispose();
            p_pb.Dispose();
        }



    }
}
