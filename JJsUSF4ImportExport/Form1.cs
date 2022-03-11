using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IONET.Core;
using JJsUSF4Library;
using JJsUSF4Library.FileClasses;
using JJsUSF4Library.FileClasses.SubfileClasses;

namespace JJsUSF4ImportExport
{
    public partial class Form1 : Form
    {
        TreeNode LastSelectedTreeNode;
        public List<USF4File> master_USF4FileList = new List<USF4File>();

        string lastSelectedInputDirectory = string.Empty;
        string lastSelectedOutputDirectory = string.Empty;

        EMO lastSelectedEMO;

        public Form1()
        {
            InitializeComponent();
#if DEBUG
            tbInputDirectory.Text = $"D:\\Program Files (x86)\\Steam\\steamapps\\common\\Street Fighter X Tekken\\resource\\CMN\\battle\\chara\\SKR";
            tbOutputDirectory.Text = $"C:\\Users\\Durandal\\Desktop\\SF4\\Import Export Test Directory\\";
#endif
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clbEMGList.ContextMenuStrip = new ContextMenuStrip();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            EMO emo = (EMO)FindParentNodeOfType(typeof(EMO), treeView.SelectedNode);
            LastSelectedTreeNode = treeView.SelectedNode;

            if (emo != null)
            {
                if (emo == lastSelectedEMO) return;

                clbEMGList.Items.Clear();
                lastSelectedEMO = emo;
                BuildTexturePackList(emo.Name);
                foreach (EMG emg in emo.EMGs)
                {
                    clbEMGList.Items.Add($"[{clbEMGList.Items.Count.ToString("D2")}] {emg.Name}", CheckState.Checked);
                }
                SetEMGListContextMenu();
            }
        }

        public void SetEMGListContextMenu()
        {
            if (clbEMGList.Items.Count == 0) clbEMGList.ContextMenuStrip.Items.Clear();
            else
            {
                clbEMGList.ContextMenuStrip.Items.Add(new ToolStripMenuItem($"Select all", null, cmEMGListSelectAll_Click));
                clbEMGList.ContextMenuStrip.Items.Add(new ToolStripMenuItem($"Select none", null, cmEMGListSelectNone_Click));

            }
        }

        public void cmEMGListSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbEMGList.Items.Count; i++) clbEMGList.SetItemChecked(i, true);
        }
        public void cmEMGListSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbEMGList.Items.Count; i++) clbEMGList.SetItemChecked(i, false);
        }

        public void BuildTexturePackList(string emo_name)
        {
            if (master_USF4FileList != null)
            {
                clbTexturePacks.Items.Clear();
                foreach (USF4File uf in master_USF4FileList)
                {
                    if (!uf.Name.Contains(".emz") && uf.GetType() == typeof(EMB) && uf.Name.Contains(emo_name.Split('.')[0]+"_"))
                    {
                        EMB emb = (EMB)uf;

                        if (emb.Files.Select(o => o.GetType()).Contains(typeof(DDS)))
                        {
                            clbTexturePacks.Items.Add(emb.Name);
                        }
                    }
                }
                if (clbTexturePacks.Items.Count > 0)
                {
                    clbTexturePacks.SelectedIndex = 0;
                    cbExportTextures.Enabled = true;
                }
                else cbExportTextures.Enabled = false;
            }
        }


        private EMB FetchTexturePack(string embName)
        {
            if (master_USF4FileList != null)
            {
                foreach (USF4File uf in master_USF4FileList)
                {
                    if (uf.Name == embName && uf.GetType() == typeof(EMB))
                    {
                        EMB emb = (EMB)uf;
                        if (emb.Files.Select(o => o.GetType()).Contains(typeof(DDS)))
                        {
                            return emb;
                        }
                    }
                }
            }
            return null;
        }


        #region Tree Methods
        public void ClearTree(TreeView tree)
        {
            tree.Nodes.Clear();
        }

        void RefreshTree(TreeView tree)
        {
            var savedExpansionState = tree.Nodes.GetExpansionState();
            tree.BeginUpdate();
            ClearTree(tree);
            FillTreeUSF4(tree);

            tree.Nodes.SetExpansionState(savedExpansionState);

            //If either of the trees is empty, reset the relevant context menu
            if (master_USF4FileList == null || master_USF4FileList.Count == 0)
            {
                tree.ContextMenuStrip = new ContextMenuStrip();
                //tree.ContextMenuStrip.Items.Add(new ToolStripMenuItem($"Open file...", null, cmUNIVopenFileToolStripMenuItem_Click));
            }

            tree.SelectedNode = TreeViewExtensions.SelectedNodeBeforeRefresh; //Not working??
            tree.EndUpdate();
        }

        private void FillTreeUSF4(TreeView tree)
        {
            //tree.Nodes.Add("Animation Files");
            tree.Nodes.Add("Geometry Files");
            tree.Nodes.Add("Material Files");
            tree.Nodes.Add("Texture Files");
            //tree.Nodes.Add("Physics Files");
            //tree.Nodes.Add("Other Files");
            foreach (USF4File uf in master_USF4FileList)
            {
                TreeNode n = uf.GenerateTreeNode();
                if (uf.GetType() == typeof(EMO)) tree.Nodes[0].Nodes.Add(n);
                else if (uf.GetType() == typeof(EMG)) tree.Nodes[0].Nodes.Add(n);
                else if (uf.GetType() == typeof(EMM)) tree.Nodes[1].Nodes.Add(n);
                else if (uf.GetType() == typeof(EMB) && !uf.Name.Contains(".emz")) tree.Nodes[2].Nodes.Add(n);
                //TreeNode n = uf.GenerateTreeNode();
                //if (uf.GetType() == typeof(EMA)) tree.Nodes[0].Nodes.Add(n);
                //else if (uf.GetType() == typeof(EMO)) tree.Nodes[1].Nodes.Add(n);
                //else if (uf.GetType() == typeof(EMG)) tree.Nodes[1].Nodes.Add(n);
                //else if (uf.GetType() == typeof(EMM)) tree.Nodes[2].Nodes.Add(n);
                //else if (uf.GetType() == typeof(EMB) && !uf.Name.Contains(".emz")) tree.Nodes[3].Nodes.Add(n);
                //else if (uf.GetType() == typeof(BSR)) tree.Nodes[4].Nodes.Add(n);
                //else if (uf.GetType() == typeof(RY2)) tree.Nodes[4].Nodes.Add(n);
                //else tree.Nodes[5].Nodes.Add(n);
            }
            tree.Nodes[0].Expand();
        }
        
        private static Object FindParentNodeOfType(Type search_type, TreeNode n)
        {
            if (n.Tag != null && n.Tag.GetType() == search_type) return n.Tag;

            while (n.Parent != null)
            {
                if (n.Parent.Tag != null && n.Parent.Tag.GetType() == search_type)
                {
                    return n.Parent.Tag;
                }
                else n = n.Parent;
            }
            return null;
        }

        #endregion

        private void btnInputDirectory_Click(object sender, EventArgs e)
        {
            using (var frm = new OpenFolderDialog())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    lastSelectedInputDirectory = frm.Folder;
                    tbInputDirectory.Text = frm.Folder;
                }
            }
        }

        private void btnOutputDirectory_Click(object sender, EventArgs e)
        {
            using (var frm = new OpenFolderDialog())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    lastSelectedOutputDirectory = frm.Folder;
                    tbOutputDirectory.Text = frm.Folder;
                }
            }
        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            if (tbInputDirectory.Text != string.Empty)
            {
                master_USF4FileList.Clear();

                try
                {
                    foreach (string file in Directory.EnumerateFiles(tbInputDirectory.Text, "*.em*"))
                    {
                        if (file.Contains(".emo") || file.Contains(".emb") || file.Contains(".emm"))
                        {
                            try
                            {
                                master_USF4FileList.Add(USF4Utils.OpenFileStreamCheckCompression(file));
                            }
                            catch
                            {
                                Console.WriteLine($"Failed to load {Path.GetFileName(file)}.");
                            }
                        }
                    }
                }
                catch
                {
                    lbFeedback.Text = StringLibrary.STR_ERR_InvalidPath;
                }
            }
            else lbFeedback.Text = StringLibrary.STR_ERR_EmptyPath;

            RefreshTree(treeView);
        }

        private void btnEMoTest_Click(object sender, EventArgs e)
        {
            if (lastSelectedEMO != null)
            {
                List<int> checkedEMGs = clbEMGList.CheckedIndices.Cast<int>().ToList();
                EMB embPack = null;

                if (cbExportTextures.Checked) embPack = FetchTexturePack(clbTexturePacks.Text);

                IOScene ios = ColladaExport.CreateIOScene(lastSelectedEMO, checkedEMGs, embPack);

                IONET.IOManager.ExportScene(
                    ios, 
                    Path.Combine(tbOutputDirectory.Text, lastSelectedEMO.Name + ".dae"), 
                    new IONET.ExportSettings() { FlipUVs = true });

                if (embPack != null)
                {
                    foreach (USF4File uf in embPack.Files)
                    {
                        if (uf.GetType() == typeof(DDS)) uf.SaveFile(Path.Combine(tbOutputDirectory.Text, uf.Name + ".dds"));
                    }
                }
            }
        }


        private void btnImportEMG_Click(object sender, EventArgs e)
        {

        }
    }
}
