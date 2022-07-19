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
            this.pictureboxSmallTile = new System.Windows.Forms.PictureBox();
            this.pictureboxTileSet = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxSmallTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxTileSet)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureboxSmallTile
            // 
            this.pictureboxSmallTile.BackColor = System.Drawing.Color.Black;
            this.pictureboxSmallTile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureboxSmallTile.Location = new System.Drawing.Point(419, 15);
            this.pictureboxSmallTile.Name = "pictureboxSmallTile";
            this.pictureboxSmallTile.Size = new System.Drawing.Size(48, 48);
            this.pictureboxSmallTile.TabIndex = 0;
            this.pictureboxSmallTile.TabStop = false;
            this.pictureboxSmallTile.Click += new System.EventHandler(this.pictureboxSmallTile_Click);
            this.pictureboxSmallTile.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureSmallTile_Paint);
            // 
            // pictureboxTileSet
            // 
            this.pictureboxTileSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureboxTileSet.Location = new System.Drawing.Point(12, 15);
            this.pictureboxTileSet.Name = "pictureboxTileSet";
            this.pictureboxTileSet.Size = new System.Drawing.Size(82, 870);
            this.pictureboxTileSet.TabIndex = 2;
            this.pictureboxTileSet.TabStop = false;
            // 
            // frmExportTile
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(616, 607);
            this.Controls.Add(this.pictureboxTileSet);
            this.Controls.Add(this.pictureboxSmallTile);
            this.Name = "frmExportTile";
            this.Text = "Export Map Tile Set";
            this.Load += new System.EventHandler(this.frmExportTile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxSmallTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxTileSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.Panel panelTileSet;
       // private System.Windows.Forms.Panel panelSmallTile;
        //private System.Windows.Forms.PictureBox pictureboxSmallTile;
        private System.Windows.Forms.PictureBox pictureboxSmallTile;
        private System.Windows.Forms.PictureBox pictureboxTileSet;
    }
}