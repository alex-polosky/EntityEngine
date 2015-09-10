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
        private List<string> openFiles;
        private TreeNode selectedNode;

        public TreeNode Json2Tree(dynamic obj)
        {
            TreeNode parent = new TreeNode();

            foreach (var token in obj)
            {
                TreeNode child = new TreeNode();
                child.Text = token.Name.ToString();

                if (token.Value.ToString() == "" || (token.Value.ToString()[0] != '{' && token.Value.ToString()[0] != '['))
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
                            if (itm.ToString() == "" || (itm.ToString()[0] != '{' && itm.ToString()[0] != '['))
                            {
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
            return LoadObjDefS(contents, filePath);
        }

        public dynamic LoadObjDefS(string s, string parentText="Root Object")
        {
            var json = (Newtonsoft.Json.Linq.JObject)
                JsonConvert.DeserializeObject(s, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            TreeNode parent = Json2Tree(json);
            parent.Text = parentText;

            return parent;
        }

        public dynamic LoadObjDefDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Json file (*.js, *.json)|*.js;*.json|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            DialogResult userClickedOK = ofd.ShowDialog();
            if (userClickedOK == DialogResult.OK && !this.openFiles.Contains(ofd.FileName))
            {
                var obj = LoadObjDef(ofd.FileName);
                this.openFiles.Add(ofd.FileName);
                return obj;
            }
            else if (this.openFiles.Contains(ofd.FileName))
            {
                MessageBox.Show("File already open!\n" + ofd.FileName);
            }

            return null;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFiles.Count == 0)
            {
                var obj = LoadObjDefDialog();
                if (obj != null)
                    treeView1.Nodes.Add(obj);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.selectedNode != null && this.selectedNode != this.treeView1.Nodes[0])
            {
                string edit = this.selectedNode.Text;
                Dialogs.InputBox("Title", "Prompt", ref edit);
                this.selectedNode.Text = edit;
            }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            this.selectedNode = this.treeView1.GetNodeAt(e.X, e.Y);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFiles.Count != 0)
            {
                this.treeView1.Nodes.RemoveAt(0);
                this.openFiles.RemoveAt(0);
            }
        }

        public objDefEditorMainForm()
        {
            InitializeComponent();
            this.openFiles = new List<string>();
        }
    }
}
