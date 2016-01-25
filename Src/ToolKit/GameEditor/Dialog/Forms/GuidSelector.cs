using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor.Dialog.Forms
{
    public partial class GuidSelector : Form
    {
        private Map _map;
        private Dictionary<string, List<Asset>> _assets;

        public GuidSelector()
        {
            _assets = new Dictionary<string, List<Asset>>() { { "Entity", new List<Asset>() }};
            LoadAssets(new Map(@"P:\Code\Git\EntityEngine\Maps\Testing"));

            InitializeComponent();
        }

        public void LoadAssets(Map map)
        {
            _map = map;
            foreach (var asset in map.AssetsOfType[AssetType.Component])
            {
                if (!_assets.Keys.Contains(asset.Extension))
                    _assets.Add(asset.Extension, new List<Asset>());
                _assets[asset.Extension].Add(asset);
            }
            foreach (var asset in map.AssetsOfType[AssetType.Entity])
                _assets["Entity"].Add(asset);
        }

        private void GuidSelector_OnLoad(object sender, EventArgs e)
        {
            textAssetType.SelectedIndex = 0;
            foreach (var type in _assets.Keys)
                textAssetType.Items.Add(type);
        }

        private void textAssetType_IndexChanged(object sender, EventArgs e)
        {
            listAssets.Items.Clear();
            if (!_assets.Keys.Contains(textAssetType.Text))
            {
                foreach (var entry in _assets)
                {
                    foreach (var asset in entry.Value)
                    {
                        listAssets.Items.Add(asset.AssetPath);
                    }
                }
            }
            else
            {
                foreach (var asset in _assets[textAssetType.Text])
                {
                    listAssets.Items.Add(asset.AssetPath);
                }
            }
        }
    }
}
