using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor.Dialog
{
    public partial class SettingsForm : Form
    {
        private void SetFields()
        {
            this.textBaseMapPath.Text = Properties.Settings.Default.MapPath;
            this.textMainMenu.Text = Properties.Settings.Default.MapMainMenu;
            this.textGlobal.Text = Properties.Settings.Default.MapGlobal;

            this.textAssetAudio.Text = Properties.Settings.Default.FolderAudio;
            this.textAssetComponent.Text = Properties.Settings.Default.FolderComponents;
            this.textAssetEntity.Text = Properties.Settings.Default.FolderEntities;
            this.textAssetModel.Text = Properties.Settings.Default.FolderModels;
            this.textAssetShader.Text = Properties.Settings.Default.FolderShaders;
            this.textAssetString.Text = Properties.Settings.Default.FolderStrings;

            this.boolInferAssetFolderPath.Checked = Properties.Settings.Default.InferMapReference;
            this.boolAutoLoadMaps.Checked = Properties.Settings.Default.AutoLoadMaps;
        }

        private void SaveFields()
        {
            Properties.Settings.Default.MapPath = this.textBaseMapPath.Text;
            Properties.Settings.Default.MapMainMenu = this.textMainMenu.Text;
            Properties.Settings.Default.MapGlobal = this.textGlobal.Text;

            //Properties.Settings.Default.FolderAudio = this.textAssetAudio.Text;
            //Properties.Settings.Default.FolderComponents = this.textAssetComponent.Text;
            //Properties.Settings.Default.FolderEntities = this.textAssetEntity.Text;
            //Properties.Settings.Default.FolderModels = this.textAssetModel.Text;
            //Properties.Settings.Default.FolderShaders = this.textAssetShader.Text;
            //Properties.Settings.Default.FolderStrings = this.textAssetString.Text;

            //Properties.Settings.Default.InferMapReference = this.boolInferAssetFolderPath.Checked;
            Properties.Settings.Default.AutoLoadMaps = this.boolAutoLoadMaps.Checked;

            Properties.Settings.Default.Save();
        }

        private void SetMapPath(TextBox box)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (textBaseMapPath.Text != "")
                diag.SelectedPath = textBaseMapPath.Text;
            diag.ShowDialog();
            box.Text = diag.SelectedPath;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFields();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBrowseBaseMap_Click(object sender, EventArgs e)
        {
            SetMapPath(textBaseMapPath);
        }

        private void buttonBrowseMainMenu_Click(object sender, EventArgs e)
        {
            SetMapPath(textMainMenu);
        }

        private void buttonBrowseGlobal_Click(object sender, EventArgs e)
        {
            SetMapPath(textGlobal);
        }

        public SettingsForm()
        {
            InitializeComponent();

            SetFields();
        }
    }
}
