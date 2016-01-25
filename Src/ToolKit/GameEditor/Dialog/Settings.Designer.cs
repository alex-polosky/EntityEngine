namespace GameEditor.Dialog
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textGlobal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textMainMenu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBaseMapPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textAssetAudio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textAssetComponent = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textAssetEntity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textAssetModel = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textAssetShader = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textAssetString = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.boolInferAssetFolderPath = new System.Windows.Forms.CheckBox();
            this.boolAutoLoadMaps = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonBrowseBaseMap = new System.Windows.Forms.Button();
            this.buttonBrowseMainMenu = new System.Windows.Forms.Button();
            this.buttonBrowseGlobal = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseGlobal);
            this.groupBox1.Controls.Add(this.buttonBrowseMainMenu);
            this.groupBox1.Controls.Add(this.buttonBrowseBaseMap);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textGlobal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textMainMenu);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBaseMapPath);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Map Paths";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Global";
            // 
            // textGlobal
            // 
            this.textGlobal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textGlobal.Location = new System.Drawing.Point(92, 71);
            this.textGlobal.Name = "textGlobal";
            this.textGlobal.Size = new System.Drawing.Size(455, 20);
            this.textGlobal.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Main Menu";
            // 
            // textMainMenu
            // 
            this.textMainMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textMainMenu.Location = new System.Drawing.Point(92, 45);
            this.textMainMenu.Name = "textMainMenu";
            this.textMainMenu.Size = new System.Drawing.Size(455, 20);
            this.textMainMenu.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Base Map Path";
            // 
            // textBaseMapPath
            // 
            this.textBaseMapPath.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBaseMapPath.Location = new System.Drawing.Point(92, 19);
            this.textBaseMapPath.Name = "textBaseMapPath";
            this.textBaseMapPath.Size = new System.Drawing.Size(455, 20);
            this.textBaseMapPath.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textAssetString);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textAssetShader);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textAssetModel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textAssetEntity);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textAssetComponent);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textAssetAudio);
            this.groupBox2.Location = new System.Drawing.Point(13, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(553, 183);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Asset Folders";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Audio";
            // 
            // textAssetAudio
            // 
            this.textAssetAudio.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textAssetAudio.Enabled = false;
            this.textAssetAudio.Location = new System.Drawing.Point(89, 19);
            this.textAssetAudio.Name = "textAssetAudio";
            this.textAssetAudio.Size = new System.Drawing.Size(455, 20);
            this.textAssetAudio.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Component";
            // 
            // textAssetComponent
            // 
            this.textAssetComponent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textAssetComponent.Enabled = false;
            this.textAssetComponent.Location = new System.Drawing.Point(89, 45);
            this.textAssetComponent.Name = "textAssetComponent";
            this.textAssetComponent.Size = new System.Drawing.Size(455, 20);
            this.textAssetComponent.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Entity";
            // 
            // textAssetEntity
            // 
            this.textAssetEntity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textAssetEntity.Enabled = false;
            this.textAssetEntity.Location = new System.Drawing.Point(89, 71);
            this.textAssetEntity.Name = "textAssetEntity";
            this.textAssetEntity.Size = new System.Drawing.Size(455, 20);
            this.textAssetEntity.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Model";
            // 
            // textAssetModel
            // 
            this.textAssetModel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textAssetModel.Enabled = false;
            this.textAssetModel.Location = new System.Drawing.Point(89, 97);
            this.textAssetModel.Name = "textAssetModel";
            this.textAssetModel.Size = new System.Drawing.Size(455, 20);
            this.textAssetModel.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Shader";
            // 
            // textAssetShader
            // 
            this.textAssetShader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textAssetShader.Enabled = false;
            this.textAssetShader.Location = new System.Drawing.Point(89, 123);
            this.textAssetShader.Name = "textAssetShader";
            this.textAssetShader.Size = new System.Drawing.Size(455, 20);
            this.textAssetShader.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 152);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "String";
            // 
            // textAssetString
            // 
            this.textAssetString.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textAssetString.Enabled = false;
            this.textAssetString.Location = new System.Drawing.Point(89, 149);
            this.textAssetString.Name = "textAssetString";
            this.textAssetString.Size = new System.Drawing.Size(455, 20);
            this.textAssetString.TabIndex = 17;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.boolAutoLoadMaps);
            this.groupBox3.Controls.Add(this.boolInferAssetFolderPath);
            this.groupBox3.Location = new System.Drawing.Point(13, 310);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(171, 71);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other";
            // 
            // boolInferAssetFolderPath
            // 
            this.boolInferAssetFolderPath.AutoSize = true;
            this.boolInferAssetFolderPath.Enabled = false;
            this.boolInferAssetFolderPath.Location = new System.Drawing.Point(9, 19);
            this.boolInferAssetFolderPath.Name = "boolInferAssetFolderPath";
            this.boolInferAssetFolderPath.Size = new System.Drawing.Size(138, 17);
            this.boolInferAssetFolderPath.TabIndex = 1;
            this.boolInferAssetFolderPath.Text = "Infer Asset Folder Paths";
            this.boolInferAssetFolderPath.UseVisualStyleBackColor = true;
            // 
            // boolAutoLoadMaps
            // 
            this.boolAutoLoadMaps.AutoSize = true;
            this.boolAutoLoadMaps.Location = new System.Drawing.Point(9, 42);
            this.boolAutoLoadMaps.Name = "boolAutoLoadMaps";
            this.boolAutoLoadMaps.Size = new System.Drawing.Size(157, 17);
            this.boolAutoLoadMaps.TabIndex = 2;
            this.boolAutoLoadMaps.Text = "Auto Load Reference Maps";
            this.boolAutoLoadMaps.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(329, 358);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(410, 358);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonBrowseBaseMap
            // 
            this.buttonBrowseBaseMap.Location = new System.Drawing.Point(524, 17);
            this.buttonBrowseBaseMap.Name = "buttonBrowseBaseMap";
            this.buttonBrowseBaseMap.Size = new System.Drawing.Size(23, 23);
            this.buttonBrowseBaseMap.TabIndex = 5;
            this.buttonBrowseBaseMap.Text = "..";
            this.buttonBrowseBaseMap.UseVisualStyleBackColor = true;
            this.buttonBrowseBaseMap.Click += new System.EventHandler(this.buttonBrowseBaseMap_Click);
            // 
            // buttonBrowseMainMenu
            // 
            this.buttonBrowseMainMenu.Location = new System.Drawing.Point(524, 43);
            this.buttonBrowseMainMenu.Name = "buttonBrowseMainMenu";
            this.buttonBrowseMainMenu.Size = new System.Drawing.Size(23, 23);
            this.buttonBrowseMainMenu.TabIndex = 6;
            this.buttonBrowseMainMenu.Text = "..";
            this.buttonBrowseMainMenu.UseVisualStyleBackColor = true;
            this.buttonBrowseMainMenu.Click += new System.EventHandler(this.buttonBrowseMainMenu_Click);
            // 
            // buttonBrowseGlobal
            // 
            this.buttonBrowseGlobal.Location = new System.Drawing.Point(524, 69);
            this.buttonBrowseGlobal.Name = "buttonBrowseGlobal";
            this.buttonBrowseGlobal.Size = new System.Drawing.Size(23, 23);
            this.buttonBrowseGlobal.TabIndex = 7;
            this.buttonBrowseGlobal.Text = "..";
            this.buttonBrowseGlobal.UseVisualStyleBackColor = true;
            this.buttonBrowseGlobal.Click += new System.EventHandler(this.buttonBrowseGlobal_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 392);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBaseMapPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textGlobal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textMainMenu;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textAssetAudio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textAssetString;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textAssetShader;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textAssetModel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textAssetEntity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textAssetComponent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox boolAutoLoadMaps;
        private System.Windows.Forms.CheckBox boolInferAssetFolderPath;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonBrowseGlobal;
        private System.Windows.Forms.Button buttonBrowseMainMenu;
        private System.Windows.Forms.Button buttonBrowseBaseMap;
    }
}