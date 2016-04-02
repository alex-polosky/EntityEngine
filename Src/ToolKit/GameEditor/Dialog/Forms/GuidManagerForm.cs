using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EntityFramework;
using EntityEngine;

namespace GameEditor.Dialog.Forms
{
    public partial class GuidManagerForm : Form
    {
        public GuidManagerForm()
        {
            InitializeComponent();

            listView1.Clear();

            //Map _m = new Map(@"P:\Code\Git\EntityEngine\Maps\Testing");

            foreach (var s in new string[] { "Guid", "Name", "Type", "Path" })
            {
                listView1.Columns.Add(s);
                listView1.Columns[listView1.Columns.Count - 1].Width = -2;
            }

            foreach (var guid in GuidManager.ActiveGuids)
            {
                Asset asset = EntityEngine.FileManagerNS.FileManager.GetAssetFromGuid(guid);
                string guidPath = GuidManager.GetFromGuid(guid);
                if (guidPath != asset.AssetPath)
                {
                    string name = asset.Name;
                    string type = "";
                    string path = guidPath;
                    if (guidPath.Split(':').Count() == 3)
                    {
                        name += ":" + guidPath.Split(':')[2];
                        type = "." + guidPath.Split(':')[2].Split(new string[] { "Component" }, StringSplitOptions.None)[0].ToLower();
                    }
                    listView1.Items.Add(new ListViewItem(new string[] { guid.ToString(), name, type, path }));
                }
                else
                {
                    listView1.Items.Add(new ListViewItem(new string[] { guid.ToString(), asset.Name, asset.Extension, asset.AssetPath }));
                }
            }
            listView1.GridLines = true;
        }
    }
}
