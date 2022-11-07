namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    partial class SplashScreen
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.progressBarSplash = new System.Windows.Forms.ProgressBar();
            this.labelApplicationTitle = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright1 = new System.Windows.Forms.Label();
            this.labelCopyright2 = new System.Windows.Forms.Label();
            this.labelProgramming = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.BackgroundImage = global::War_in_Middle_Earth_Game_Editor_C_Sharp.Properties.Resources.WIME_Editor_Splash_Screen;
            this.splitContainer1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(496, 519);
            this.splitContainer1.SplitterDistance = 365;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.progressBarSplash, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelApplicationTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelVersion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelCopyright1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelCopyright2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelProgramming, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(496, 150);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // progressBarSplash
            // 
            this.progressBarSplash.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBarSplash.Location = new System.Drawing.Point(101, 117);
            this.progressBarSplash.MarqueeAnimationSpeed = 50;
            this.progressBarSplash.Name = "progressBarSplash";
            this.progressBarSplash.Size = new System.Drawing.Size(293, 23);
            this.progressBarSplash.TabIndex = 0;
            // 
            // labelApplicationTitle
            // 
            this.labelApplicationTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelApplicationTitle.AutoSize = true;
            this.labelApplicationTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelApplicationTitle.Location = new System.Drawing.Point(92, 4);
            this.labelApplicationTitle.Name = "labelApplicationTitle";
            this.labelApplicationTitle.Size = new System.Drawing.Size(312, 20);
            this.labelApplicationTitle.TabIndex = 0;
            this.labelApplicationTitle.Text = "Synergistic War in Middle Earth Editor";
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelVersion.Location = new System.Drawing.Point(204, 30);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(87, 16);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "Version/Build";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCopyright1
            // 
            this.labelCopyright1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCopyright1.AutoSize = true;
            this.labelCopyright1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCopyright1.Location = new System.Drawing.Point(181, 50);
            this.labelCopyright1.Name = "labelCopyright1";
            this.labelCopyright1.Size = new System.Drawing.Size(134, 16);
            this.labelCopyright1.TabIndex = 2;
            this.labelCopyright1.Text = "Application Copyright";
            // 
            // labelCopyright2
            // 
            this.labelCopyright2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCopyright2.AutoSize = true;
            this.labelCopyright2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCopyright2.Location = new System.Drawing.Point(201, 70);
            this.labelCopyright2.Name = "labelCopyright2";
            this.labelCopyright2.Size = new System.Drawing.Size(94, 15);
            this.labelCopyright2.TabIndex = 3;
            this.labelCopyright2.Text = "WIME Copyright";
            // 
            // labelProgramming
            // 
            this.labelProgramming.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelProgramming.AutoSize = true;
            this.labelProgramming.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProgramming.Location = new System.Drawing.Point(186, 90);
            this.labelProgramming.Name = "labelProgramming";
            this.labelProgramming.Size = new System.Drawing.Size(124, 15);
            this.labelProgramming.TabIndex = 4;
            this.labelProgramming.Text = "Programming Credits";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 519);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelApplicationTitle;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright1;
        private System.Windows.Forms.Label labelCopyright2;
        private System.Windows.Forms.Label labelProgramming;
        private System.Windows.Forms.ProgressBar progressBarSplash;
        private System.Windows.Forms.Timer timer1;
    }
}