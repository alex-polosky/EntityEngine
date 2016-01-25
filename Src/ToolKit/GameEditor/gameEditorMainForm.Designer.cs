namespace GameEditor
{
    partial class gameEditorMainForm
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
            this.TreeViewTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hierarchyTreeView = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.typeTreeView = new System.Windows.Forms.TreeView();
            this.TreeViewTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeViewTabs
            // 
            this.TreeViewTabs.Controls.Add(this.tabPage1);
            this.TreeViewTabs.Controls.Add(this.tabPage2);
            this.TreeViewTabs.Location = new System.Drawing.Point(12, 12);
            this.TreeViewTabs.Name = "TreeViewTabs";
            this.TreeViewTabs.SelectedIndex = 0;
            this.TreeViewTabs.Size = new System.Drawing.Size(776, 550);
            this.TreeViewTabs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.TreeViewTabs.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.hierarchyTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 524);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hierarchyTreeView
            // 
            this.hierarchyTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hierarchyTreeView.FullRowSelect = true;
            this.hierarchyTreeView.Location = new System.Drawing.Point(3, 3);
            this.hierarchyTreeView.Name = "hierarchyTreeView";
            this.hierarchyTreeView.Size = new System.Drawing.Size(762, 518);
            this.hierarchyTreeView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.typeTreeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 524);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // typeTreeView
            // 
            this.typeTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeTreeView.Location = new System.Drawing.Point(3, 3);
            this.typeTreeView.Name = "typeTreeView";
            this.typeTreeView.Size = new System.Drawing.Size(762, 518);
            this.typeTreeView.TabIndex = 0;
            // 
            // gameEditorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 571);
            this.Controls.Add(this.TreeViewTabs);
            this.Name = "gameEditorMainForm";
            this.Text = "Form1";
            this.TreeViewTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TreeViewTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView hierarchyTreeView;
        private System.Windows.Forms.TreeView typeTreeView;
    }
}

