using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor.Editor.Controls
{
    public partial class IField : UserControl
    {
        public enum FieldType
        {
            None,
            PathAsset,
            Guid,
            Dropdown
        }

        private FieldType _type;
        private DictionaryOTO<string, string> _dictMap; //display, data

        public virtual void LoadData(string data)
        {
            if (_type == FieldType.Dropdown)
                try
                {
                    dropField.Text = _dictMap.GetBySecond(data);
                }
                catch
                {
                    dropField.Text = data;
                }
            else
                textField.Text = data;
        }

        public virtual string GetData()
        {
            if (_type == FieldType.Dropdown)
                return _dictMap.GetByFirst(dropField.Text);
            return textField.Text;
        }

        public virtual void LoadDropDictionary(Dictionary<string, string> dictMap)
        {
            //0: displayed as text
            //1: the data returned
            _dictMap = new DictionaryOTO<string, string>();
            foreach (var key in dictMap.Keys)
            {
                _dictMap.Add(key, dictMap[key]);
                dropField.Items.Add(key);
            }
        }

        public virtual void SetFields(FieldType type=FieldType.None, 
            string labelText=null, bool? textEnabled=null, bool? buttonEnabled=null)
        {
            if (type != FieldType.None)
                _type = type;
            if (labelText != null)
                labelField.Text = labelText;
            if (textEnabled != null)
                textField.Enabled = (bool)textEnabled;
            if (buttonEnabled != null)
                buttonField.Enabled = (bool)buttonEnabled;

            if (type == FieldType.Dropdown)
            {
                Controls.Remove(textField);
                Controls.Remove(buttonField);
                Controls.Add(dropField);
            }
        }

        public IField()
        {
            InitializeComponent();
            _type = FieldType.None;
        }

        protected virtual void textField_TextChanged(object sender, EventArgs e)
        {
            //TODO: Add validation
        }

        protected virtual void buttonField_OnClick(object sender, EventArgs e)
        {
            //TODO: Add hook
            switch (this._type)
            {
                case FieldType.Guid:
                    buttonField_OnClick_Guid(sender, e);
                    break;
                case FieldType.PathAsset:
                    buttonField_OnClick_PathAsset(sender, e);
                    break;
                default:
                    break;
            }
        }

        protected virtual void buttonField_OnClick_PathAsset(object sender, EventArgs e)
        {
        }

        protected virtual void buttonField_OnClick_Guid(object sender, EventArgs e)
        {
        }
    }

    partial class IField
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

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
        protected void InitializeComponent()
        {
            this.labelField = new System.Windows.Forms.Label();
            this.textField = new System.Windows.Forms.TextBox();
            this.buttonField = new System.Windows.Forms.Button();
            this.dropField = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelField
            // 
            this.labelField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelField.Location = new System.Drawing.Point(0, 0);
            this.labelField.MaximumSize = new System.Drawing.Size(127, 24);
            this.labelField.MinimumSize = new System.Drawing.Size(127, 24);
            this.labelField.Name = "labelField";
            this.labelField.Size = new System.Drawing.Size(127, 24);
            this.labelField.TabIndex = 0;
            this.labelField.Text = "LABEL";
            this.labelField.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textField
            // 
            this.textField.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textField.Location = new System.Drawing.Point(127, 2);
            this.textField.Name = "textField";
            this.textField.Size = new System.Drawing.Size(350, 20);
            this.textField.TabIndex = 1;
            this.textField.TextChanged += new System.EventHandler(this.textField_TextChanged);
            // 
            // buttonField
            // 
            this.buttonField.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonField.Enabled = false;
            this.buttonField.Location = new System.Drawing.Point(476, 0);
            this.buttonField.Name = "buttonField";
            this.buttonField.Size = new System.Drawing.Size(24, 24);
            this.buttonField.TabIndex = 2;
            this.buttonField.Text = "..";
            this.buttonField.UseVisualStyleBackColor = true;
            this.buttonField.Click += new System.EventHandler(this.buttonField_OnClick);
            //
            // dropField
            //
            this.dropField.FormattingEnabled = true;
            this.dropField.Location = new System.Drawing.Point(127, 2);
            this.dropField.Name = "dropField";
            this.dropField.Size = new System.Drawing.Size(370, 20);
            //this.dropField.TabIndex = 0;

            // 
            // IField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelField);
            this.Controls.Add(this.textField);
            this.Controls.Add(this.buttonField);
            //this.Controls.Add(this.dropField);
            this.MaximumSize = new System.Drawing.Size(0, 24);
            this.MinimumSize = new System.Drawing.Size(500, 24);
            this.Name = "IField";
            this.Size = new System.Drawing.Size(500, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label labelField;
        protected System.Windows.Forms.TextBox textField;
        protected System.Windows.Forms.Button buttonField;
        protected System.Windows.Forms.ComboBox dropField;
    }
}
