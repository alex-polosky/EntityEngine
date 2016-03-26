using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace GameEditor.Editor.Controls
{
    public partial class Shader : UserControl, IControlBlock
    {
        private class ShaderVar : TabPage
        {
            //TODO: Add ctrl+tab support (ya know, like normal handlers)

            public System.Windows.Forms.ComboBox textFormat;
            public System.Windows.Forms.NumericUpDown textInstanceDataStepRate;
            public System.Windows.Forms.NumericUpDown textClassification;
            public System.Windows.Forms.NumericUpDown textAlignedByteOffset;
            public System.Windows.Forms.NumericUpDown textSlot;
            public System.Windows.Forms.NumericUpDown textSemanticIndex;
            public System.Windows.Forms.TextBox textSemanticName;
            private LineControl lineControl1;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Button buttonRemoveShaderVar;
            private System.Windows.Forms.Button buttonRemoveConfirm;
            private System.Windows.Forms.Label label9;
            private System.Windows.Forms.Label label8;
            private System.Windows.Forms.Label label7;
            private System.Windows.Forms.Label label6;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label3;

            public void IntializeComponent()
            {
                this.SuspendLayout();

                this.lineControl1 = new GameEditor.Editor.Controls.LineControl();
                this.label2 = new System.Windows.Forms.Label();
                this.textInstanceDataStepRate = new System.Windows.Forms.NumericUpDown();
                this.textClassification = new System.Windows.Forms.NumericUpDown();
                this.textAlignedByteOffset = new System.Windows.Forms.NumericUpDown();
                this.textSlot = new System.Windows.Forms.NumericUpDown();
                this.textSemanticIndex = new System.Windows.Forms.NumericUpDown();
                this.textSemanticName = new System.Windows.Forms.TextBox();
                this.textFormat = new System.Windows.Forms.ComboBox();
                this.label3 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label5 = new System.Windows.Forms.Label();
                this.label6 = new System.Windows.Forms.Label();
                this.label7 = new System.Windows.Forms.Label();
                this.label8 = new System.Windows.Forms.Label();
                this.label9 = new System.Windows.Forms.Label();
                this.buttonRemoveConfirm = new System.Windows.Forms.Button();
                this.buttonRemoveShaderVar = new System.Windows.Forms.Button();

                ((System.ComponentModel.ISupportInitialize)(this.textInstanceDataStepRate)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.textClassification)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.textAlignedByteOffset)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.textSlot)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.textSemanticIndex)).BeginInit();
                // 
                // lineControl1
                // 
                this.lineControl1.Location = new System.Drawing.Point(2, 23);
                this.lineControl1.Name = "lineControl1";
                this.lineControl1.Size = new System.Drawing.Size(485, 1);
                this.lineControl1.TabIndex = 8;
                this.lineControl1.Text = "lineControl1";
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(7, 7);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(60, 13);
                this.label2.TabIndex = 7;
                this.label2.Text = "Shader Var";
                // 
                // textInstanceDataStepRate
                // 
                this.textInstanceDataStepRate.Location = new System.Drawing.Point(302, 189);
                this.textInstanceDataStepRate.Name = "textInstanceDataStepRate";
                this.textInstanceDataStepRate.Size = new System.Drawing.Size(168, 20);
                this.textInstanceDataStepRate.TabIndex = 6;
                // 
                // textClassification
                // 
                this.textClassification.Location = new System.Drawing.Point(302, 163);
                this.textClassification.Name = "textClassification";
                this.textClassification.Size = new System.Drawing.Size(168, 20);
                this.textClassification.TabIndex = 5;
                // 
                // textAlignedByteOffset
                // 
                this.textAlignedByteOffset.Location = new System.Drawing.Point(302, 137);
                this.textAlignedByteOffset.Name = "textAlignedByteOffset";
                this.textAlignedByteOffset.Size = new System.Drawing.Size(168, 20);
                this.textAlignedByteOffset.TabIndex = 4;
                // 
                // textSlot
                // 
                this.textSlot.Location = new System.Drawing.Point(302, 111);
                this.textSlot.Name = "textSlot";
                this.textSlot.Size = new System.Drawing.Size(168, 20);
                this.textSlot.TabIndex = 3;
                // 
                // textSemanticIndex
                // 
                this.textSemanticIndex.Location = new System.Drawing.Point(302, 58);
                this.textSemanticIndex.Name = "textSemanticIndex";
                this.textSemanticIndex.Size = new System.Drawing.Size(168, 20);
                this.textSemanticIndex.TabIndex = 2;
                // 
                // textSemanticName
                // 
                this.textSemanticName.Location = new System.Drawing.Point(302, 32);
                this.textSemanticName.Name = "textSemanticName";
                this.textSemanticName.Size = new System.Drawing.Size(168, 20);
                this.textSemanticName.TabIndex = 1;
                // 
                // textFormat
                // 
                this.textFormat.FormattingEnabled = true;
                this.textFormat.Location = new System.Drawing.Point(302, 84);
                this.textFormat.Name = "textFormat";
                this.textFormat.Size = new System.Drawing.Size(168, 21);
                this.textFormat.TabIndex = 0;
                // 
                // label3
                // 
                this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(214, 35);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(82, 13);
                this.label3.TabIndex = 9;
                this.label3.Text = "Semantic Name";
                // 
                // label4
                // 
                this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label4.AutoSize = true;
                this.label4.Location = new System.Drawing.Point(216, 60);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(80, 13);
                this.label4.TabIndex = 10;
                this.label4.Text = "Semantic Index";
                // 
                // label5
                // 
                this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label5.AutoSize = true;
                this.label5.Location = new System.Drawing.Point(257, 87);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(39, 13);
                this.label5.TabIndex = 11;
                this.label5.Text = "Format";
                // 
                // label6
                // 
                this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label6.AutoSize = true;
                this.label6.Location = new System.Drawing.Point(271, 113);
                this.label6.Name = "label6";
                this.label6.Size = new System.Drawing.Size(25, 13);
                this.label6.TabIndex = 12;
                this.label6.Text = "Slot";
                this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                // 
                // label7
                // 
                this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label7.AutoSize = true;
                this.label7.Location = new System.Drawing.Point(199, 139);
                this.label7.Name = "label7";
                this.label7.Size = new System.Drawing.Size(97, 13);
                this.label7.TabIndex = 13;
                this.label7.Text = "Aligned Byte Offset";
                this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                // 
                // label8
                // 
                this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label8.AutoSize = true;
                this.label8.Location = new System.Drawing.Point(228, 165);
                this.label8.Name = "label8";
                this.label8.Size = new System.Drawing.Size(68, 13);
                this.label8.TabIndex = 14;
                this.label8.Text = "Classification";
                this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                // 
                // label9
                // 
                this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.label9.AutoSize = true;
                this.label9.Location = new System.Drawing.Point(171, 191);
                this.label9.Name = "label9";
                this.label9.Size = new System.Drawing.Size(125, 13);
                this.label9.TabIndex = 15;
                this.label9.Text = "Instance Data Step Rate";
                this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                // 
                // buttonRemoveConfirm
                // 
                this.buttonRemoveConfirm.Enabled = false;
                this.buttonRemoveConfirm.Location = new System.Drawing.Point(283, 221);
                this.buttonRemoveConfirm.Name = "buttonRemoveConfirm";
                this.buttonRemoveConfirm.Size = new System.Drawing.Size(187, 23);
                this.buttonRemoveConfirm.TabIndex = 16;
                this.buttonRemoveConfirm.Text = "Confirm Remove Shader Var";
                this.buttonRemoveConfirm.UseVisualStyleBackColor = true;
                // 
                // buttonRemoveShaderVar
                // 
                this.buttonRemoveShaderVar.Location = new System.Drawing.Point(90, 221);
                this.buttonRemoveShaderVar.Name = "buttonRemoveShaderVar";
                this.buttonRemoveShaderVar.Size = new System.Drawing.Size(187, 23);
                this.buttonRemoveShaderVar.TabIndex = 17;
                this.buttonRemoveShaderVar.Text = "Remove Shader Var";
                this.buttonRemoveShaderVar.UseVisualStyleBackColor = true;
                // 
                // ShaderVar
                // 
                this.Controls.Add(this.buttonRemoveShaderVar);
                this.Controls.Add(this.buttonRemoveConfirm);
                this.Controls.Add(this.label9);
                this.Controls.Add(this.label8);
                this.Controls.Add(this.label7);
                this.Controls.Add(this.label6);
                this.Controls.Add(this.label5);
                this.Controls.Add(this.label4);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.lineControl1);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.textInstanceDataStepRate);
                this.Controls.Add(this.textClassification);
                this.Controls.Add(this.textAlignedByteOffset);
                this.Controls.Add(this.textSlot);
                this.Controls.Add(this.textSemanticIndex);
                this.Controls.Add(this.textSemanticName);
                this.Controls.Add(this.textFormat);
                this.Location = new System.Drawing.Point(4, 22);
                this.Name = "shaderVar";
                this.Padding = new System.Windows.Forms.Padding(3);
                this.Size = new System.Drawing.Size(490, 250);
                this.TabIndex = 0;
                this.Text = "ShaderVarTemplate";
                this.UseVisualStyleBackColor = true;
                ((System.ComponentModel.ISupportInitialize)(this.textInstanceDataStepRate)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.textClassification)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.textAlignedByteOffset)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.textSlot)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.textSemanticIndex)).EndInit();
                this.ResumeLayout(false);
            }
            public ShaderVar()
            {
                IntializeComponent();
                buttonRemoveShaderVar.Click += new EventHandler(buttonRemoveShaderVar_Click);
                buttonRemoveConfirm.Click += new EventHandler(buttonRemoveConfirm_Click);

                foreach (var type in Enum.GetValues(typeof(ShaderFormat)))
                    this.textFormat.Items.Add(type.ToString());
            }

            void buttonRemoveConfirm_Click(object sender, EventArgs e)
            {
                TabControl parent = (TabControl)Parent;
                parent.TabPages.Remove(this);
                foreach (TabPage page in (parent.TabPages))
                    if (!page.Text.Contains("Add"))
                        page.Text = parent.TabPages.IndexOf(page).ToString();
            }

            void buttonRemoveShaderVar_Click(object sender, EventArgs e)
            {
                buttonRemoveConfirm.Enabled = true;
            }
        }
        private class AddPage : TabPage
        {
            public AddPage(EventHandler handler)
            {
                Text = "[Add]";
                Enter += new EventHandler(handler);
            }
        }

        // This is the definition of SharpDX.DXGI.Format, copied here for ease of use
        public enum ShaderFormat
        {
            Unknown = 0,
            R32G32B32A32_Typeless = 1,
            R32G32B32A32_Float = 2,
            R32G32B32A32_UInt = 3,
            R32G32B32A32_SInt = 4,
            R32G32B32_Typeless = 5,
            R32G32B32_Float = 6,
            R32G32B32_UInt = 7,
            R32G32B32_SInt = 8,
            R16G16B16A16_Typeless = 9,
            R16G16B16A16_Float = 10,
            R16G16B16A16_UNorm = 11,
            R16G16B16A16_UInt = 12,
            R16G16B16A16_SNorm = 13,
            R16G16B16A16_SInt = 14,
            R32G32_Typeless = 15,
            R32G32_Float = 16,
            R32G32_UInt = 17,
            R32G32_SInt = 18,
            R32G8X24_Typeless = 19,
            D32_Float_S8X24_UInt = 20,
            R32_Float_X8X24_Typeless = 21,
            X32_Typeless_G8X24_UInt = 22,
            R10G10B10A2_Typeless = 23,
            R10G10B10A2_UNorm = 24,
            R10G10B10A2_UInt = 25,
            R11G11B10_Float = 26,
            R8G8B8A8_Typeless = 27,
            R8G8B8A8_UNorm = 28,
            R8G8B8A8_UNorm_SRgb = 29,
            R8G8B8A8_UInt = 30,
            R8G8B8A8_SNorm = 31,
            R8G8B8A8_SInt = 32,
            R16G16_Typeless = 33,
            R16G16_Float = 34,
            R16G16_UNorm = 35,
            R16G16_UInt = 36,
            R16G16_SNorm = 37,
            R16G16_SInt = 38,
            R32_Typeless = 39,
            D32_Float = 40,
            R32_Float = 41,
            R32_UInt = 42,
            R32_SInt = 43,
            R24G8_Typeless = 44,
            D24_UNorm_S8_UInt = 45,
            R24_UNorm_X8_Typeless = 46,
            X24_Typeless_G8_UInt = 47,
            R8G8_Typeless = 48,
            R8G8_UNorm = 49,
            R8G8_UInt = 50,
            R8G8_SNorm = 51,
            R8G8_SInt = 52,
            R16_Typeless = 53,
            R16_Float = 54,
            D16_UNorm = 55,
            R16_UNorm = 56,
            R16_UInt = 57,
            R16_SNorm = 58,
            R16_SInt = 59,
            R8_Typeless = 60,
            R8_UNorm = 61,
            R8_UInt = 62,
            R8_SNorm = 63,
            R8_SInt = 64,
            A8_UNorm = 65,
            R1_UNorm = 66,
            R9G9B9E5_Sharedexp = 67,
            R8G8_B8G8_UNorm = 68,
            G8R8_G8B8_UNorm = 69,
            BC1_Typeless = 70,
            BC1_UNorm = 71,
            BC1_UNorm_SRgb = 72,
            BC2_Typeless = 73,
            BC2_UNorm = 74,
            BC2_UNorm_SRgb = 75,
            BC3_Typeless = 76,
            BC3_UNorm = 77,
            BC3_UNorm_SRgb = 78,
            BC4_Typeless = 79,
            BC4_UNorm = 80,
            BC4_SNorm = 81,
            BC5_Typeless = 82,
            BC5_UNorm = 83,
            BC5_SNorm = 84,
            B5G6R5_UNorm = 85,
            B5G5R5A1_UNorm = 86,
            B8G8R8A8_UNorm = 87,
            B8G8R8X8_UNorm = 88,
            R10G10B10_Xr_Bias_A2_UNorm = 89,
            B8G8R8A8_Typeless = 90,
            B8G8R8A8_UNorm_SRgb = 91,
            B8G8R8X8_Typeless = 92,
            B8G8R8X8_UNorm_SRgb = 93,
            BC6H_Typeless = 94,
            BC6H_Uf16 = 95,
            BC6H_Sf16 = 96,
            BC7_Typeless = 97,
            BC7_UNorm = 98,
            BC7_UNorm_SRgb = 99,
        }

        private Newtonsoft.Json.Linq.JObject json;

        private void AddPage_Enter(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, 
                new ShaderVar() { Text = (this.tabControl1.TabPages.Count - 1).ToString() } );
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 2;
        }

        public void LoadData(Dictionary<string, string> data)
        {
            json = (Newtonsoft.Json.Linq.JObject)
                   JsonConvert.DeserializeObject(data[Editor.Forms.Component.JsonDictionaryKey], new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            this.fieldGuid.LoadData(json["guid"].ToString());
            this.fieldAsset.LoadData(json["filePath"].ToString());
            this.fieldShaderLevel.LoadData(json["shaderLevel"].ToString());
            this.tabControl1.TabPages.Clear();
            foreach (var shadvar in json["shaderVars"])
            {
                var item = JsonConvert.DeserializeObject<Dictionary<string, string>>(shadvar.ToString());
                ShaderVar page = new ShaderVar();
                page.Text = this.tabControl1.TabPages.Count.ToString();
                page.textSemanticName.Text = item["SemanticName"];
                page.textSemanticIndex.Text = item["SemanticIndex"];
                //page.textFormat.Text = Enum.Parse(typeof(ShaderFormat), item["Format"]).ToString();
                ShaderFormat format;
                if (Enum.TryParse(item["Format"], out format))
                    page.textFormat.Text = format.ToString();
                else
                    page.textFormat.Text = item["Format"];
                page.textSlot.Text = item["Slot"];
                page.textAlignedByteOffset.Text = item["AlignedByteOffset"];
                page.textClassification.Text = item["Classification"];
                page.textInstanceDataStepRate.Text = item["InstanceDataStepRate"];
                this.tabControl1.TabPages.Add(page);
            }
            this.tabControl1.TabPages.Add(new AddPage(AddPage_Enter));
        }

        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> toret = new Dictionary<string, string>();
            json["guid"] = this.fieldGuid.GetData();
            json["filePath"] = this.fieldAsset.GetData();
            json["shaderLevel"] = this.fieldShaderLevel.GetData();

            var shadvar = new List<Dictionary<string, string>>();
            foreach (TabPage page in this.tabControl1.TabPages)
            {
                ShaderVar p;
                try
                {
                    p = (ShaderVar)page;
                }
                catch
                {
                    continue;
                }
                shadvar.Add(new Dictionary<string, string>()
                    {
                        {"SemanticName", p.textSemanticName.Text},
                        {"SemanticIndex", p.textSemanticIndex.Text},
                        {"Format", p.textFormat.Text},
                        {"Slot", p.textSlot.Text},
                        {"AlignedByteOffset", p.textAlignedByteOffset.Text},
                        {"Classification", p.textClassification.Text},
                        {"InstanceDataStepRate", p.textInstanceDataStepRate.Text}
                    }
                );
            }
            json["shaderVars"] = JsonConvert.SerializeObject(shadvar);


            toret.Add(Editor.Forms.Component.JsonDictionaryKey, json.ToString());
            return toret;
        }

        public void SetGroupBoxTag(string name)
        {
            this.groupMesh.Text = name + " : " + this.groupMesh.Text;
        }

        public Shader()
        {
            InitializeComponent();
            this.fieldAsset.SetFields(IField.FieldType.PathAsset, "Asset Path", true, true);
            this.fieldGuid.SetFields(IField.FieldType.Guid, "Guid", true, true);
            this.fieldShaderLevel.SetFields(IField.FieldType.None, "Shader Level", false, false);

            this.tabControl1.TabPages.Clear();
            this.tabControl1.TabPages.Add(new ShaderVar() { Text = "0" });
            this.tabControl1.TabPages.Add(new AddPage(AddPage_Enter));
        }
    }
}
