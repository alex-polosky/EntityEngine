namespace GameEditor.Editor.Controls
{
    partial class Shader
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
            this.groupMesh = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.fieldShaderLevel = new GameEditor.Editor.Controls.IField();
            this.fieldAsset = new GameEditor.Editor.Controls.IField();
            this.fieldGuid = new GameEditor.Editor.Controls.IField();
            this.groupMesh.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMesh
            // 
            this.groupMesh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMesh.Controls.Add(this.tabControl1);
            this.groupMesh.Controls.Add(this.label1);
            this.groupMesh.Controls.Add(this.fieldShaderLevel);
            this.groupMesh.Controls.Add(this.fieldAsset);
            this.groupMesh.Controls.Add(this.fieldGuid);
            this.groupMesh.Location = new System.Drawing.Point(0, 0);
            this.groupMesh.Margin = new System.Windows.Forms.Padding(0);
            this.groupMesh.MaximumSize = new System.Drawing.Size(505, 388);
            this.groupMesh.MinimumSize = new System.Drawing.Size(505, 388);
            this.groupMesh.Name = "groupMesh";
            this.groupMesh.Padding = new System.Windows.Forms.Padding(0);
            this.groupMesh.Size = new System.Drawing.Size(505, 388);
            this.groupMesh.TabIndex = 6;
            this.groupMesh.TabStop = false;
            this.groupMesh.Text = "Shader";
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(4, 107);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(498, 276);
            this.tabControl1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(0, 139);
            this.label1.MaximumSize = new System.Drawing.Size(0, 2);
            this.label1.MinimumSize = new System.Drawing.Size(0, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 2);
            this.label1.TabIndex = 3;
            this.label1.Text = "5";
            // 
            // fieldShaderLevel
            // 
            this.fieldShaderLevel.Location = new System.Drawing.Point(2, 77);
            this.fieldShaderLevel.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldShaderLevel.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldShaderLevel.Name = "fieldShaderLevel";
            this.fieldShaderLevel.Size = new System.Drawing.Size(500, 24);
            this.fieldShaderLevel.TabIndex = 2;
            // 
            // fieldAsset
            // 
            this.fieldAsset.Location = new System.Drawing.Point(2, 47);
            this.fieldAsset.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldAsset.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldAsset.Name = "fieldAsset";
            this.fieldAsset.Size = new System.Drawing.Size(500, 24);
            this.fieldAsset.TabIndex = 1;
            // 
            // fieldGuid
            // 
            this.fieldGuid.Location = new System.Drawing.Point(2, 16);
            this.fieldGuid.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldGuid.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldGuid.Name = "fieldGuid";
            this.fieldGuid.Size = new System.Drawing.Size(500, 24);
            this.fieldGuid.TabIndex = 0;
            // 
            // Shader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupMesh);
            this.MaximumSize = new System.Drawing.Size(505, 388);
            this.MinimumSize = new System.Drawing.Size(505, 388);
            this.Name = "Shader";
            this.Size = new System.Drawing.Size(505, 388);
            this.groupMesh.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMesh;
        private IField fieldGuid;
        private IField fieldAsset;
        private IField fieldShaderLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
    }
}
