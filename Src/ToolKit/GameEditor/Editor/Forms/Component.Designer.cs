namespace GameEditor.Editor.Forms
{
    partial class Component
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
            this.groupBoxCom = new System.Windows.Forms.GroupBox();
            this.fieldCom = new GameEditor.Editor.Controls.IField();
            this.fieldEntity = new GameEditor.Editor.Controls.IField();
            this.fieldGuid = new GameEditor.Editor.Controls.IField();
            this.groupBoxCom.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCom
            // 
            this.groupBoxCom.Controls.Add(this.fieldCom);
            this.groupBoxCom.Controls.Add(this.fieldEntity);
            this.groupBoxCom.Controls.Add(this.fieldGuid);
            this.groupBoxCom.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCom.Name = "groupBoxCom";
            this.groupBoxCom.Size = new System.Drawing.Size(505, 106);
            this.groupBoxCom.TabIndex = 1;
            this.groupBoxCom.TabStop = false;
            this.groupBoxCom.Text = "Component";
            // 
            // fieldCom
            // 
            this.fieldCom.Location = new System.Drawing.Point(4, 80);
            this.fieldCom.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldCom.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldCom.Name = "fieldCom";
            this.fieldCom.Size = new System.Drawing.Size(500, 24);
            this.fieldCom.TabIndex = 2;
            // 
            // fieldEntity
            // 
            this.fieldEntity.Location = new System.Drawing.Point(3, 49);
            this.fieldEntity.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldEntity.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldEntity.Name = "fieldEntity";
            this.fieldEntity.Size = new System.Drawing.Size(500, 24);
            this.fieldEntity.TabIndex = 1;
            // 
            // fieldGuid
            // 
            this.fieldGuid.Location = new System.Drawing.Point(3, 19);
            this.fieldGuid.MaximumSize = new System.Drawing.Size(0, 24);
            this.fieldGuid.MinimumSize = new System.Drawing.Size(500, 24);
            this.fieldGuid.Name = "fieldGuid";
            this.fieldGuid.Size = new System.Drawing.Size(500, 24);
            this.fieldGuid.TabIndex = 0;
            // 
            // Component
            // 
            this.Controls.Add(this.groupBoxCom);
            this.MaximumSize = new System.Drawing.Size(525, 0);
            this.MinimumSize = new System.Drawing.Size(525, 108);
            this.Name = "Component";
            this.Size = new System.Drawing.Size(525, 108);
            this.groupBoxCom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.IField fieldGuid;
        private System.Windows.Forms.GroupBox groupBoxCom;
        private Controls.IField fieldEntity;
        private Controls.IField fieldCom;
    }
}
