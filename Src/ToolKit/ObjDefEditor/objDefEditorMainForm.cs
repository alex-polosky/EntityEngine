using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace ObjDefEditor
{
    public partial class objDefEditorMainForm : Form
    {
        public TreeNode Json2Tree(dynamic obj)
        {
            TreeNode parent = new TreeNode();

            foreach (var token in obj)
            {
                TreeNode child = new TreeNode();
                child.Text = token.Name.ToString();

                if (token.Value.ToString()[0] != '{' && token.Value.ToString()[0] != '[')
                {
                    child.Nodes.Add(token.Value.ToString());
                }
                else
                {
                    var jsChild = JsonConvert.DeserializeObject(token.Value.ToString(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
                    if (jsChild.GetType().ToString().Contains("Object"))
                    {
                        child = Json2Tree(jsChild);
                        child.Text = token.Name.ToString();
                    }
                    else if (jsChild.GetType().ToString().Contains("Array"))
                    {
                        int ix = -1;
                        foreach (var itm in jsChild)
                        {
                            TreeNode objTN = new TreeNode();
                            ix++;
                            if (itm.ToString()[0] != '{' && itm.ToString()[0] != '[')
                            {
                                //child.Nodes.Add(itm.ToString());
                                objTN.Nodes.Add(itm.ToString());
                            }
                            else
                            {
                                objTN = Json2Tree(JsonConvert.DeserializeObject(itm.ToString(), new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects }));
                            }
                            objTN.Text = child.Text + "[" + ix.ToString() + "]";
                            child.Nodes.Add(objTN);
                        }
                    }
                    else
                    {
                        child.Nodes.Add(token.Value.ToString());
                    }
                }

                parent.Nodes.Add(child);
            }

            return parent;
        }

        public dynamic LoadObjDef(string filePath)
        {
            string contents;
            using (var sReader = new StreamReader(filePath))
            {
                contents = sReader.ReadToEnd();
            }
            return LoadObjDefS(contents);
        }

        public dynamic LoadObjDefS(string s)
        {
            var json = (Newtonsoft.Json.Linq.JObject)
                JsonConvert.DeserializeObject(s, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            TreeNode parent = Json2Tree(json);
            parent.Text = "Root Object";

            return parent;
        }

        public dynamic LoadObjDefDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Json file (*.js, *.json)|*.js;*.json|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            DialogResult userClickedOK = ofd.ShowDialog();
            if (userClickedOK == DialogResult.OK)
            {
                var obj = LoadObjDef(ofd.FileName);
                return obj;
            }

            return null;
        }

        public objDefEditorMainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = LoadObjDefDialog();
            treeView1.Nodes.Add(obj);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
