namespace GameEditor.Dialog.Forms
{
    partial class GuidSelector
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
            this.textAssetType = new System.Windows.Forms.ComboBox();
            this.listAssets = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textAssetType
            // 
            this.textAssetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textAssetType.FormattingEnabled = true;
            this.textAssetType.Items.AddRange(new object[] {
            "Asset Type..."});
            this.textAssetType.Location = new System.Drawing.Point(12, 12);
            this.textAssetType.MaxDropDownItems = 12;
            this.textAssetType.Name = "textAssetType";
            this.textAssetType.Size = new System.Drawing.Size(339, 21);
            this.textAssetType.TabIndex = 0;
            this.textAssetType.SelectedIndexChanged += new System.EventHandler(this.textAssetType_IndexChanged);
            // 
            // listAssets
            // 
            this.listAssets.FormattingEnabled = true;
            this.listAssets.Items.AddRange(new object[] {
            "st",
            "test",
            "test2",
            "asdf",
            "234234",
            "sadfxcv",
            "234",
            "xcv",
            "sdf 234"});
            this.listAssets.Location = new System.Drawing.Point(12, 39);
            this.listAssets.Name = "listAssets";
            this.listAssets.ScrollAlwaysVisible = true;
            this.listAssets.Size = new System.Drawing.Size(535, 173);
            this.listAssets.TabIndex = 1;
            // 
            // GuidSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 286);
            this.Controls.Add(this.listAssets);
            this.Controls.Add(this.textAssetType);
            this.Name = "GuidSelector";
            this.Text = "GuidSelector";
            this.Load += new System.EventHandler(this.GuidSelector_OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox textAssetType;
        private System.Windows.Forms.ListBox listAssets;
    }
}