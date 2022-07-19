namespace War_in_Middle_Earth_Game_Editor_C_Sharp.Classes
{
    partial class frmCityData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCityData));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnPointer = new System.Windows.Forms.ColumnHeader();
            this.columnCityName = new System.Windows.Forms.ColumnHeader();
            this.columnX = new System.Windows.Forms.ColumnHeader();
            this.columnY = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textCityBlockStart = new System.Windows.Forms.TextBox();
            this.labelCityBlockLength = new System.Windows.Forms.Label();
            this.textDataBlockSize = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPointer,
            this.columnCityName,
            this.columnX,
            this.columnY});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(125, 154);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(339, 426);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnPointer
            // 
            this.columnPointer.Text = "Name Pointer";
            this.columnPointer.Width = 85;
            // 
            // columnCityName
            // 
            this.columnCityName.Text = "Name";
            this.columnCityName.Width = 150;
            // 
            // columnX
            // 
            this.columnX.Text = "X";
            this.columnX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnX.Width = 50;
            // 
            // columnY
            // 
            this.columnY.Text = "Y";
            this.columnY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnY.Width = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textDataBlockSize);
            this.groupBox1.Controls.Add(this.labelCityBlockLength);
            this.groupBox1.Controls.Add(this.textCityBlockStart);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(539, 112);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "City Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "City Data Block Range";
            // 
            // textCityBlockStart
            // 
            this.textCityBlockStart.Location = new System.Drawing.Point(153, 28);
            this.textCityBlockStart.Name = "textCityBlockStart";
            this.textCityBlockStart.Size = new System.Drawing.Size(100, 23);
            this.textCityBlockStart.TabIndex = 1;
            // 
            // labelCityBlockLength
            // 
            this.labelCityBlockLength.AutoSize = true;
            this.labelCityBlockLength.Location = new System.Drawing.Point(24, 63);
            this.labelCityBlockLength.Name = "labelCityBlockLength";
            this.labelCityBlockLength.Size = new System.Drawing.Size(110, 15);
            this.labelCityBlockLength.TabIndex = 2;
            this.labelCityBlockLength.Text = "City Data Block Size";
            // 
            // textDataBlockSize
            // 
            this.textDataBlockSize.Location = new System.Drawing.Point(153, 60);
            this.textDataBlockSize.Name = "textDataBlockSize";
            this.textDataBlockSize.Size = new System.Drawing.Size(100, 23);
            this.textDataBlockSize.TabIndex = 3;
            // 
            // frmCityData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 602);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCityData";
            this.Text = "WIME City Data";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnPointer;
        private System.Windows.Forms.ColumnHeader columnCityName;
        private System.Windows.Forms.ColumnHeader columnX;
        private System.Windows.Forms.ColumnHeader columnY;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textDataBlockSize;
        private System.Windows.Forms.Label labelCityBlockLength;
        private System.Windows.Forms.TextBox textCityBlockStart;
        private System.Windows.Forms.Label label1;
    }
}