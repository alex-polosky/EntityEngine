namespace GameEditor.Dialog.Forms
{
    partial class GuidManagerForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "24",
            "est3",
            "34",
            "234"}, -1);
            this.textAssetType = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeadGuid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeadName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeadExt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeadPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textAssetType
            // 
            this.textAssetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textAssetType.FormattingEnabled = true;
            this.textAssetType.Items.AddRange(new object[] {
            "Asset Type..."});
            this.textAssetType.Location = new System.Drawing.Point(12, -48);
            this.textAssetType.MaxDropDownItems = 12;
            this.textAssetType.Name = "textAssetType";
            this.textAssetType.Size = new System.Drawing.Size(339, 21);
            this.textAssetType.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeadGuid,
            this.columnHeadName,
            this.columnHeadExt,
            this.columnHeadPath});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(814, 242);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeadGuid
            // 
            this.columnHeadGuid.Text = "Guid";
            // 
            // columnHeadName
            // 
            this.columnHeadName.Text = "Name";
            // 
            // columnHeadExt
            // 
            this.columnHeadExt.Text = "Type";
            // 
            // columnHeadPath
            // 
            this.columnHeadPath.Text = "Path";
            // 
            // GuidManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 313);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textAssetType);
            this.Name = "GuidManagerForm";
            this.Text = "GuidManagerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox textAssetType;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeadGuid;
        private System.Windows.Forms.ColumnHeader columnHeadName;
        private System.Windows.Forms.ColumnHeader columnHeadExt;
        private System.Windows.Forms.ColumnHeader columnHeadPath;
    }
}