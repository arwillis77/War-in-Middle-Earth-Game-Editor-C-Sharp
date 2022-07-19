namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    partial class frmTileView
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
            this.StatusStripResourceStatus = new System.Windows.Forms.StatusStrip();
            this.LabelGameStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTileView = new System.Windows.Forms.Panel();
            this.StatusStripResourceStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStripResourceStatus
            // 
            this.StatusStripResourceStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelGameStatus});
            this.StatusStripResourceStatus.Location = new System.Drawing.Point(0, 570);
            this.StatusStripResourceStatus.Name = "StatusStripResourceStatus";
            this.StatusStripResourceStatus.Size = new System.Drawing.Size(1094, 22);
            this.StatusStripResourceStatus.TabIndex = 1;
            this.StatusStripResourceStatus.Text = "statusStrip1";
            // 
            // LabelGameStatus
            // 
            this.LabelGameStatus.BackColor = System.Drawing.Color.Gray;
            this.LabelGameStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LabelGameStatus.ForeColor = System.Drawing.Color.White;
            this.LabelGameStatus.Name = "LabelGameStatus";
            this.LabelGameStatus.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.LabelGameStatus.Size = new System.Drawing.Size(113, 17);
            this.LabelGameStatus.Text = "Resource Details";
            // 
            // panelTileView
            // 
            this.panelTileView.BackColor = System.Drawing.Color.Black;
            this.panelTileView.Location = new System.Drawing.Point(16, 17);
            this.panelTileView.Name = "panelTileView";
            this.panelTileView.Size = new System.Drawing.Size(48, 48);
            this.panelTileView.TabIndex = 3;
            this.panelTileView.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTileView_Paint);
            // 
            // frmTileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.panelTileView);
            this.Controls.Add(this.StatusStripResourceStatus);
            this.Name = "frmTileView";
            this.Size = new System.Drawing.Size(1094, 592);
            this.Load += new System.EventHandler(this.frmTileView_Load);
            this.StatusStripResourceStatus.ResumeLayout(false);
            this.StatusStripResourceStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip StatusStripResourceStatus;
        private System.Windows.Forms.ToolStripStatusLabel LabelGameStatus;
        private System.Windows.Forms.Panel panelTileView;
    }
}
