namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    partial class frmExportTile
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureboxSmallTile = new System.Windows.Forms.PictureBox();
            pictureboxTileSet = new System.Windows.Forms.PictureBox();
            panelTileSmall = new System.Windows.Forms.Panel();
            labelTile = new System.Windows.Forms.Label();
            textBoxTileCurrent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureboxSmallTile).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureboxTileSet).BeginInit();
            SuspendLayout();
            // 
            // pictureboxSmallTile
            // 
            pictureboxSmallTile.BackColor = System.Drawing.Color.Black;
            pictureboxSmallTile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pictureboxSmallTile.Location = new System.Drawing.Point(294, 15);
            pictureboxSmallTile.Name = "pictureboxSmallTile";
            pictureboxSmallTile.Size = new System.Drawing.Size(48, 48);
            pictureboxSmallTile.TabIndex = 0;
            pictureboxSmallTile.TabStop = false;
            // 
            // pictureboxTileSet
            // 
            pictureboxTileSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pictureboxTileSet.Location = new System.Drawing.Point(12, 15);
            pictureboxTileSet.Name = "pictureboxTileSet";
            pictureboxTileSet.Size = new System.Drawing.Size(82, 870);
            pictureboxTileSet.TabIndex = 2;
            pictureboxTileSet.TabStop = false;
            // 
            // panelTileSmall
            // 
            panelTileSmall.Location = new System.Drawing.Point(114, 15);
            panelTileSmall.Name = "panelTileSmall";
            panelTileSmall.Size = new System.Drawing.Size(149, 130);
            panelTileSmall.TabIndex = 3;
            panelTileSmall.Paint += panelTileSmall_Paint;
            // 
            // labelTile
            // 
            labelTile.AutoSize = true;
            labelTile.Location = new System.Drawing.Point(114, 169);
            labelTile.Name = "labelTile";
            labelTile.Size = new System.Drawing.Size(25, 15);
            labelTile.TabIndex = 4;
            labelTile.Text = "Tile";
            // 
            // textBoxTileCurrent
            // 
            textBoxTileCurrent.Location = new System.Drawing.Point(145, 166);
            textBoxTileCurrent.Name = "textBoxTileCurrent";
            textBoxTileCurrent.Size = new System.Drawing.Size(37, 23);
            textBoxTileCurrent.TabIndex = 5;
            // 
            // frmExportTile
            // 
            AutoScroll = true;
            ClientSize = new System.Drawing.Size(616, 607);
            Controls.Add(textBoxTileCurrent);
            Controls.Add(labelTile);
            Controls.Add(panelTileSmall);
            Controls.Add(pictureboxTileSet);
            Controls.Add(pictureboxSmallTile);
            Name = "frmExportTile";
            Text = "Export Map Tile Set";
            Load += frmExportTile_Load;
            ((System.ComponentModel.ISupportInitialize)pictureboxSmallTile).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureboxTileSet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        //private System.Windows.Forms.Panel panelTileSet;
        // private System.Windows.Forms.Panel panelSmallTile;
        //private System.Windows.Forms.PictureBox pictureboxSmallTile;
        private System.Windows.Forms.PictureBox pictureboxSmallTile;
        private System.Windows.Forms.PictureBox pictureboxTileSet;
        private System.Windows.Forms.Panel panelTileSmall;
        private System.Windows.Forms.Label labelTile;
        private System.Windows.Forms.TextBox textBoxTileCurrent;
    }
}