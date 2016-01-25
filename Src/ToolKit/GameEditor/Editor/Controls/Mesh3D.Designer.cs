namespace GameEditor.Editor.Controls
{
    partial class Mesh3D
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
            this.fieldAsset1 = new GameEditor.Editor.Controls.IField();
            this.fieldGuid1 = new GameEditor.Editor.Controls.IField();
            this.groupMesh.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMesh
            // 
            this.groupMesh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupMesh.Controls.Add(this.fieldAsset1);
            this.groupMesh.Controls.Add(this.fieldGuid1);
            this.groupMesh.Location = new System.Drawing.Point(0, 0);
            this.groupMesh.Margin = new System.Windows.Forms.Padding(0);
            this.groupMesh.Name = "groupMesh";
            this.groupMesh.Padding = new System.Windows.Forms.Padding(0);
            this.groupMesh.Size = new System.Drawing.Size(505, 75);
            this.groupMesh.TabIndex = 6;
            this.groupMesh.TabStop = false;
            this.groupMesh.Text = "Mesh3D";
            // 
            // fieldAsset1
            // 
            this.fieldAsset1.Location = new System.Drawing.Point(3, 48);
            this.fieldAsset1.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldAsset1.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldAsset1.Name = "fieldAsset1";
            this.fieldAsset1.Size = new System.Drawing.Size(500, 24);
            this.fieldAsset1.TabIndex = 1;
            // 
            // fieldGuid1
            // 
            this.fieldGuid1.Location = new System.Drawing.Point(3, 18);
            this.fieldGuid1.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldGuid1.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldGuid1.Name = "fieldGuid1";
            this.fieldGuid1.Size = new System.Drawing.Size(500, 24);
            this.fieldGuid1.TabIndex = 0;
            // 
            // Mesh3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupMesh);
            this.MaximumSize = new System.Drawing.Size(505, 75);
            this.MinimumSize = new System.Drawing.Size(505, 75);
            this.Name = "Mesh3D";
            this.Size = new System.Drawing.Size(505, 75);
            this.groupMesh.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMesh;
        //private FieldAsset fieldAsset1;
        //private FieldGuid fieldGuid1;
        private IField fieldAsset1;
        private IField fieldGuid1;
    }
}
