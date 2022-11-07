using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    public partial class SplashScreen : Form
    {
        static private string ApplicationTitle = "SYNERGISTIC WAR IN MIDDLE EARTH EDITOR";
        static private string ApplicationVersion = "VERSION 2.01a";
        static private string ApplicationBuild = "Build 4 VS2022 C# : 11/06/2022";
        static private string ApplicationAuthors = "PROGRAMMED BY Aaron R. Willis & Pavel Řezníček";
        static private string ApplicationCopyright = "Application Copyright (C) 2022 Aaron R. Willis";
        static private string WIMECopyright = "War in Middle Earth -- Copyright (C) 1988 Melbourne House";
        Form p_form;
        

        public SplashScreen()
        {
            InitializeComponent();
            labelApplicationTitle.Text = ApplicationTitle;
            labelVersion.Text = ApplicationVersion + " " + ApplicationBuild;
            labelCopyright1.Text = ApplicationCopyright;
            labelCopyright2.Text = WIMECopyright;
            labelProgramming.Text = ApplicationAuthors;
            progressBarSplash.Minimum = 0;
            progressBarSplash.Maximum = 400;
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            timer1.Start();     
        }


        private void StartApp()
        {
            timer1.Stop();
            p_form = new frmWIMEEditorMain();
            this.Hide();
            p_form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarSplash.Increment(10);
            if (progressBarSplash.Value == progressBarSplash.Maximum)
                StartApp();
        }  
    }
}
