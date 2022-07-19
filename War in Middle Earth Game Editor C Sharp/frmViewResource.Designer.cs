
namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    partial class frmViewResource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewResource));
            this.sstripResourceStatus = new System.Windows.Forms.StatusStrip();
            this.labelGameStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpriteColor = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.pnlMainDisplay = new System.Windows.Forms.Panel();
            this.sstripResourceStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // sstripResourceStatus
            // 
            this.sstripResourceStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelGameStatus,
            this.lblSpriteColor,
            this.toolStripDropDownButton1});
            this.sstripResourceStatus.Location = new System.Drawing.Point(0, 568);
            this.sstripResourceStatus.Name = "sstripResourceStatus";
            this.sstripResourceStatus.Size = new System.Drawing.Size(1094, 24);
            this.sstripResourceStatus.TabIndex = 0;
            this.sstripResourceStatus.Text = "statusStrip1";
            // 
            // labelGameStatus
            // 
            this.labelGameStatus.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelGameStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.labelGameStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.labelGameStatus.Name = "labelGameStatus";
            this.labelGameStatus.Size = new System.Drawing.Size(97, 19);
            this.labelGameStatus.Text = "Resource Details";
            // 
            // lblSpriteColor
            // 
            this.lblSpriteColor.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblSpriteColor.Name = "lblSpriteColor";
            this.lblSpriteColor.Size = new System.Drawing.Size(69, 19);
            this.lblSpriteColor.Text = "Sprite Color";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(13, 22);
            this.toolStripDropDownButton1.ToolTipText = "dropSpriteColor";
            // 
            // pnlMainDisplay
            // 
            this.pnlMainDisplay.BackColor = System.Drawing.Color.Black;
            this.pnlMainDisplay.Location = new System.Drawing.Point(36, 36);
            this.pnlMainDisplay.Name = "pnlMainDisplay";
            this.pnlMainDisplay.Size = new System.Drawing.Size(640, 480);
            this.pnlMainDisplay.TabIndex = 1;
            this.pnlMainDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMainDisplay_Paint);
            // 
            // frmViewResource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.pnlMainDisplay);
            this.Controls.Add(this.sstripResourceStatus);
            this.Name = "frmViewResource";
            this.Size = new System.Drawing.Size(1094, 592);
            this.Load += new System.EventHandler(this.frmViewResource_Load);
            this.sstripResourceStatus.ResumeLayout(false);
            this.sstripResourceStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sstripResourceStatus;
        private System.Windows.Forms.ToolStripStatusLabel labelGameStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblSpriteColor;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.Panel pnlMainDisplay;
    }
}
