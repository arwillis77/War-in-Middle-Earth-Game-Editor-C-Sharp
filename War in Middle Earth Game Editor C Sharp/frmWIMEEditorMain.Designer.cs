
namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    partial class frmWIMEEditorMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWIMEEditorMain));
            this.ToolStripMain = new System.Windows.Forms.ToolStrip();
            this.OpenButton = new System.Windows.Forms.ToolStripButton();
            this.PanelEditor = new System.Windows.Forms.Panel();
            this.ToolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStripMain
            // 
            this.ToolStripMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ToolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton});
            this.ToolStripMain.Location = new System.Drawing.Point(0, 0);
            this.ToolStripMain.Name = "ToolStripMain";
            this.ToolStripMain.Size = new System.Drawing.Size(1008, 39);
            this.ToolStripMain.TabIndex = 0;
            this.ToolStripMain.Text = "toolStrip1";
            // 
            // OpenButton
            // 
            this.OpenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenButton.DoubleClickEnabled = true;
            this.OpenButton.Image = global::War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.Open3232;
            this.OpenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(36, 36);
            this.OpenButton.Text = "Open File";
            this.OpenButton.ToolTipText = "Open WIME File";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // PanelEditor
            // 
            this.PanelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelEditor.Location = new System.Drawing.Point(0, 39);
            this.PanelEditor.Name = "PanelEditor";
            this.PanelEditor.Size = new System.Drawing.Size(1008, 690);
            this.PanelEditor.TabIndex = 1;
            // 
            // frmWIMEEditorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.PanelEditor);
            this.Controls.Add(this.ToolStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWIMEEditorMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "War in Middle Earth Game Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWIMEEditorMain_Load);
            this.ToolStripMain.ResumeLayout(false);
            this.ToolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolStripMain;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.Panel PanelEditor;
    }
}

