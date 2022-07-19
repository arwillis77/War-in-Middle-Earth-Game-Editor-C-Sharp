
namespace War_in_Middle_Earth_Game_Editor_C_Sharp
{
    partial class frmResourceList
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
            this.splitResourceContainer = new System.Windows.Forms.SplitContainer();
            this.listResourceItems = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colResourceNum = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colType = new System.Windows.Forms.ColumnHeader();
            this.colResource = new System.Windows.Forms.ColumnHeader();
            this.colOffset = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.splitResourceContainer)).BeginInit();
            this.splitResourceContainer.Panel1.SuspendLayout();
            this.splitResourceContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitResourceContainer
            // 
            this.splitResourceContainer.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitResourceContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitResourceContainer.Location = new System.Drawing.Point(0, 0);
            this.splitResourceContainer.Name = "splitResourceContainer";
            // 
            // splitResourceContainer.Panel1
            // 
            this.splitResourceContainer.Panel1.Controls.Add(this.listResourceItems);
            this.splitResourceContainer.Size = new System.Drawing.Size(800, 600);
            this.splitResourceContainer.SplitterDistance = 329;
            this.splitResourceContainer.TabIndex = 0;
            // 
            // listResourceItems
            // 
            this.listResourceItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colResourceNum,
            this.colSize,
            this.colType,
            this.colResource,
            this.colOffset});
            this.listResourceItems.FullRowSelect = true;
            this.listResourceItems.GridLines = true;
            this.listResourceItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listResourceItems.Location = new System.Drawing.Point(0, 0);
            this.listResourceItems.MultiSelect = false;
            this.listResourceItems.Name = "listResourceItems";
            this.listResourceItems.Size = new System.Drawing.Size(308, 600);
            this.listResourceItems.TabIndex = 0;
            this.listResourceItems.UseCompatibleStateImageBehavior = false;
            this.listResourceItems.View = System.Windows.Forms.View.Details;
            this.listResourceItems.ItemActivate += new System.EventHandler(this.listResourceItems_ItemActivate);
            this.listResourceItems.SelectedIndexChanged += new System.EventHandler(this.listResourceItems_SelectedIndexChanged);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 75;
            // 
            // colResourceNum
            // 
            this.colResourceNum.Text = "#";
            this.colResourceNum.Width = 30;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSize.Width = 50;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 50;
            // 
            // colResource
            // 
            this.colResource.Text = "Resource";
            this.colResource.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colResource.Width = 50;
            // 
            // colOffset
            // 
            this.colOffset.Text = "Offset";
            this.colOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOffset.Width = 50;
            // 
            // frmResourceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.splitResourceContainer);
            this.Name = "frmResourceList";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.frmResourceList_Load);
            this.VisibleChanged += new System.EventHandler(this.frmResourceList_Shown);
            this.splitResourceContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitResourceContainer)).EndInit();
            this.splitResourceContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitResourceContainer;
        private System.Windows.Forms.ListView listResourceItems;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colResourceNum;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colResource;
        private System.Windows.Forms.ColumnHeader colOffset;
    }
}
