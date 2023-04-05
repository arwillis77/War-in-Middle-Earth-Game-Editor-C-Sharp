namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    partial class frmMapView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            pictureboxTileSet = new System.Windows.Forms.PictureBox();
            mapPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureboxTileSet).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(pictureboxTileSet);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.AutoScroll = true;
            splitContainer1.Panel2.Controls.Add(mapPanel);
            splitContainer1.Size = new System.Drawing.Size(821, 816);
            splitContainer1.SplitterDistance = 64;
            splitContainer1.TabIndex = 0;
            // 
            // pictureboxTileSet
            // 
            pictureboxTileSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pictureboxTileSet.Location = new System.Drawing.Point(0, 0);
            pictureboxTileSet.Name = "pictureboxTileSet";
            pictureboxTileSet.Size = new System.Drawing.Size(64, 1024);
            pictureboxTileSet.TabIndex = 0;
            pictureboxTileSet.TabStop = false;
            pictureboxTileSet.Paint += pictureboxTileSet_Paint;
            // 
            // mapPanel
            // 
            mapPanel.AutoScroll = true;
            mapPanel.Location = new System.Drawing.Point(3, 3);
            mapPanel.Name = "mapPanel";
            mapPanel.Size = new System.Drawing.Size(2560, 1584);
            mapPanel.TabIndex = 0;
            mapPanel.Paint += mapPanel_Paint;
            // 
            // frmMapView
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            AutoScroll = true;
            Controls.Add(splitContainer1);
            Name = "frmMapView";
            Size = new System.Drawing.Size(821, 816);
            Load += frmMapView_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureboxTileSet).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureboxTileSet;
        private System.Windows.Forms.Panel mapPanel;
    }
}
