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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupboxControls = new System.Windows.Forms.GroupBox();
            this.buttonSaveToBitmap = new System.Windows.Forms.Button();
            this.comboBoxImageScale = new System.Windows.Forms.ComboBox();
            this.labelImageScale = new System.Windows.Forms.Label();
            this.StatusStripResourceStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupboxControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusStripResourceStatus
            // 
            this.StatusStripResourceStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelGameStatus});
            this.StatusStripResourceStatus.Location = new System.Drawing.Point(0, 746);
            this.StatusStripResourceStatus.Name = "StatusStripResourceStatus";
            this.StatusStripResourceStatus.Size = new System.Drawing.Size(1024, 22);
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
            this.panelTileView.Location = new System.Drawing.Point(13, 13);
            this.panelTileView.Name = "panelTileView";
            this.panelTileView.Size = new System.Drawing.Size(48, 48);
            this.panelTileView.TabIndex = 3;
            this.panelTileView.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTileView_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.splitContainer1.Panel1.Controls.Add(this.panelTileView);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupboxControls);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 746);
            this.splitContainer1.SplitterDistance = 569;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupboxControls
            // 
            this.groupboxControls.Controls.Add(this.buttonSaveToBitmap);
            this.groupboxControls.Controls.Add(this.comboBoxImageScale);
            this.groupboxControls.Controls.Add(this.labelImageScale);
            this.groupboxControls.ForeColor = System.Drawing.Color.White;
            this.groupboxControls.Location = new System.Drawing.Point(13, 13);
            this.groupboxControls.Margin = new System.Windows.Forms.Padding(5);
            this.groupboxControls.Name = "groupboxControls";
            this.groupboxControls.Padding = new System.Windows.Forms.Padding(5);
            this.groupboxControls.Size = new System.Drawing.Size(165, 155);
            this.groupboxControls.TabIndex = 1;
            this.groupboxControls.TabStop = false;
            this.groupboxControls.Text = "Controls";
            // 
            // buttonSaveToBitmap
            // 
            this.buttonSaveToBitmap.BackColor = System.Drawing.Color.Gray;
            this.buttonSaveToBitmap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveToBitmap.Location = new System.Drawing.Point(34, 74);
            this.buttonSaveToBitmap.Name = "buttonSaveToBitmap";
            this.buttonSaveToBitmap.Size = new System.Drawing.Size(102, 23);
            this.buttonSaveToBitmap.TabIndex = 2;
            this.buttonSaveToBitmap.Text = "Save to Bitmap";
            this.buttonSaveToBitmap.UseVisualStyleBackColor = false;
            // 
            // comboBoxImageScale
            // 
            this.comboBoxImageScale.BackColor = System.Drawing.Color.Gray;
            this.comboBoxImageScale.FormattingEnabled = true;
            this.comboBoxImageScale.Location = new System.Drawing.Point(95, 39);
            this.comboBoxImageScale.Name = "comboBoxImageScale";
            this.comboBoxImageScale.Size = new System.Drawing.Size(50, 23);
            this.comboBoxImageScale.TabIndex = 1;
            this.comboBoxImageScale.SelectedIndexChanged += new System.EventHandler(this.comboBoxImageScale_SelectedIndexChanged);
            // 
            // labelImageScale
            // 
            this.labelImageScale.AutoSize = true;
            this.labelImageScale.Location = new System.Drawing.Point(23, 43);
            this.labelImageScale.Name = "labelImageScale";
            this.labelImageScale.Size = new System.Drawing.Size(70, 15);
            this.labelImageScale.TabIndex = 1;
            this.labelImageScale.Text = "Image Scale";
            // 
            // frmTileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusStripResourceStatus);
            this.Name = "frmTileView";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.frmTileView_Load);
            this.StatusStripResourceStatus.ResumeLayout(false);
            this.StatusStripResourceStatus.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupboxControls.ResumeLayout(false);
            this.groupboxControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip StatusStripResourceStatus;
        private System.Windows.Forms.ToolStripStatusLabel LabelGameStatus;
        private System.Windows.Forms.Panel panelTileView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupboxControls;
        private System.Windows.Forms.Button buttonSaveToBitmap;
        private System.Windows.Forms.ComboBox comboBoxImageScale;
        private System.Windows.Forms.Label labelImageScale;
    }
}
