namespace GameEditor.Editor.Controls
{
    partial class RenderTypeFlag
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
            this.boolWireframe = new System.Windows.Forms.CheckBox();
            this.groupMesh.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMesh
            // 
            this.groupMesh.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMesh.Controls.Add(this.boolWireframe);
            this.groupMesh.Location = new System.Drawing.Point(0, 0);
            this.groupMesh.Margin = new System.Windows.Forms.Padding(0);
            this.groupMesh.Name = "groupMesh";
            this.groupMesh.Padding = new System.Windows.Forms.Padding(0);
            this.groupMesh.Size = new System.Drawing.Size(505, 40);
            this.groupMesh.TabIndex = 6;
            this.groupMesh.TabStop = false;
            this.groupMesh.Text = "RenderTypeFlag";
            // 
            // boolWireframe
            // 
            this.boolWireframe.AutoSize = true;
            this.boolWireframe.Location = new System.Drawing.Point(8, 16);
            this.boolWireframe.Name = "boolWireframe";
            this.boolWireframe.Size = new System.Drawing.Size(74, 17);
            this.boolWireframe.TabIndex = 0;
            this.boolWireframe.Text = "Wireframe";
            this.boolWireframe.UseVisualStyleBackColor = true;
            this.boolWireframe.CheckedChanged += new System.EventHandler(this.boolWireframe_CheckedChanged);
            // 
            // RenderTypeFlag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupMesh);
            this.MaximumSize = new System.Drawing.Size(505, 40);
            this.MinimumSize = new System.Drawing.Size(505, 40);
            this.Name = "RenderTypeFlag";
            this.Size = new System.Drawing.Size(505, 40);
            this.groupMesh.ResumeLayout(false);
            this.groupMesh.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMesh;
        private System.Windows.Forms.CheckBox boolWireframe;
    }
}
