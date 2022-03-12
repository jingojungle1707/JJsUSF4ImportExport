using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IONET.Core;
using IONET.Core.Model;
using JJsUSF4Library;
using JJsUSF4Library.FileClasses;
using JJsUSF4Library.FileClasses.SubfileClasses;

namespace JJsUSF4ImportExport
{

    public partial class Form1 : Form
    {
        TreeNode LastSelectedTreeNodeUSF4;
        TreeNode LastSelectedTreeNodeCollada;
        public List<USF4File> master_USF4FileList = new List<USF4File>();
        public List<IOScene> master_ColladaFileList = new List<IOScene>();

        string lastSelectedInputDirectory = string.Empty;
        string lastSelectedOutputDirectory = string.Empty;
        string lastSelectedColladaDirectory = string.Empty;

        EMO lastSelectedEMO;

        public Form1()
        {
            InitializeComponent();
#if DEBUG
            tbInputDirectory.Text = $"D:\\Program Files (x86)\\Steam\\steamapps\\common\\Super Street Fighter IV - Arcade Edition\\resource\\battle\\chara\\SKR\\";
            tbOutputDirectory.Text = $"D:\\Program Files (x86)\\Steam\\steamapps\\common\\Super Street Fighter IV - Arcade Edition\\patch_ae2_tu3\\battle\\chara\\SKR\\";
            tbColladaDirectory.Text = $"C:\\Users\\Durandal\\Desktop\\SF4\\Import Export Test Directory\\";
#endif
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rblTargetEMG.Enabled = false;
            clbEMGList.ContextMenuStrip = new ContextMenuStrip();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (sender == tvUSF4Files)
            {
                tvTreeUpdateSelectionUSF4();
            }
            else if (sender == tvColladaFiles) tvTreeUpdateSelectionCollada();
        }

        private void tvTreeUpdateSelectionCollada()
        {
            IOScene ioScene = (IOScene)FindParentNodeOfType(typeof(IOScene), tvColladaFiles.SelectedNode);
            LastSelectedTreeNodeCollada = tvColladaFiles.SelectedNode;

            if (ioScene != null)
            {
            }
        }

        private void tvTreeUpdateSelectionUSF4()
        {
            EMO emo = (EMO)FindParentNodeOfType(typeof(EMO), tvUSF4Files.SelectedNode);
            LastSelectedTreeNodeUSF4 = tvUSF4Files.SelectedNode;

            if (emo != null)
            {
                if (emo == lastSelectedEMO) return;

                clbEMGList.Items.Clear();
                rblTargetEMG.Items.Clear();
                lastSelectedEMO = emo;
                BuildTexturePackList(emo.Name);
                foreach (EMG emg in emo.EMGs)
                {
                    clbEMGList.Items.Add($"[{clbEMGList.Items.Count:D2}] {emg.Name}", CheckState.Checked);
                    rblTargetEMG.Items.Add($"[{rblTargetEMG.Items.Count:D2}] {emg.Name}");
                }
                SetEMGListContextMenu();
                if (rblTargetEMG.Items.Count > 0)
                {
                    rblTargetEMG.Enabled = true;
                    rblTargetEMG.Items.Add("As new EMG");
                    rblTargetEMG.SelectedIndex = rblTargetEMG.Items.Count - 1;
                }
                else rblTargetEMG.Enabled = false;

            }
        }

        private void SetEMGListContextMenu()
        {
            clbEMGList.ContextMenuStrip.Items.Clear();
            if (clbEMGList.Items.Count > 0) 
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
                cbTextureIndex.Items.Clear();

                int maxTextures = 0;

                string character_code = emo_name.Split('.')[0] + "_";

                if (!emo_name.Contains("skl.emo") && !emo_name.Contains("shd.emo"))
                {
                    foreach (USF4File uf in master_USF4FileList)
                    {
                        if (uf.GetType() == typeof(EMB) && !uf.Name.EndsWith(".emz") && uf.Name.StartsWith(character_code))
                        {
                            EMB emb = (EMB)uf;

                            if (emb.Files.Select(o => o.GetType()).Contains(typeof(DDS)))
                            {
                                maxTextures = Math.Max(maxTextures, emb.Files.Count);
                                clbTexturePacks.Items.Add(emb.Name);
                            }
                        }
                    }
                }
                if (clbTexturePacks.Items.Count > 0)
                {
                    while (cbTextureIndex.Items.Count < maxTextures) cbTextureIndex.Items.Add(cbTextureIndex.Items.Count);
                    cbExportTextures.Enabled = true;
                    cbTextureIndex.Enabled = true;
                    clbTexturePacks.SelectedIndex = 0;
                    cbTextureIndex.SelectedIndex = 0;
                }
                else
                {
                    cbExportTextures.Enabled = false;
                    cbTextureIndex.Enabled = false;
                }
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

            if (tree == tvUSF4Files) FillTreeUSF4(tree);
            else if (tree == tvColladaFiles) FillTreeCollada(tree);

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
            if (master_USF4FileList.Count > 0)
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
        }

        private void FillTreeCollada(TreeView tree)
        {
            foreach (IOScene ioS in master_ColladaFileList)
            {
                foreach (IONET.Core.Model.IOModel ioM in ioS.Models)
                {
                    tvColladaFiles.Nodes.Add(ioM.GenerateioModelTreeNode(ioS.Name));
                }
            }
        }
        
        private static object FindParentNodeOfType(Type search_type, TreeNode n)
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

        private void btnColladaDirectory_Click(object sender, EventArgs e)
        {
            using (var frm = new OpenFolderDialog())
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    lastSelectedColladaDirectory = frm.Folder;
                    tbColladaDirectory.Text = frm.Folder;
                }
            }
        }

        private void btnLoadColladaFiles_Click(object sender, EventArgs e)
        {
            if (tbColladaDirectory.Text != string.Empty)
            {
                master_ColladaFileList.Clear();

                try
                {
                    foreach (string file in Directory.EnumerateFiles(tbColladaDirectory.Text, "*.dae"))
                    {
                        try
                        {
                            master_ColladaFileList.Add(IONET.IOManager.LoadScene(file, new IONET.ImportSettings()
                            {
                                Optimize = false,
                                Triangulate = true,
                                FlipUVs = true,
                            }));
                            master_ColladaFileList.Last().Name = Path.GetFileName(file);
                        }
                        catch
                        {
                            lblStatusBarFeedback.Text = StringLibrary.STR_ERR_InvalidColladaFile + Path.GetFileName(file);
                        }
                    }

                    RefreshTree(tvColladaFiles);
                }
                catch
                {
                    lblStatusBarFeedback.Text = StringLibrary.STR_ERR_InvalidColladaPath;
                }
            }
            else lblStatusBarFeedback.Text = StringLibrary.STR_ERR_EmptyPath;
        }

        private void btnLoadGameFiles_Click(object sender, EventArgs e)
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

                    RefreshTree(tvUSF4Files);
                }
                catch
                {
                    lblStatusBarFeedback.Text = StringLibrary.STR_ERR_InvalidPath;
                }
            }
            else lblStatusBarFeedback.Text = StringLibrary.STR_ERR_EmptyPath;
        }

        private void btnExportEMOtoCollada_Click(object sender, EventArgs e)
        {
            if (lastSelectedEMO != null)
            {
                List<int> checkedEMGs = clbEMGList.CheckedIndices.Cast<int>().ToList();
                EMB embPack = null;

                if (cbExportTextures.Checked) embPack = FetchTexturePack(clbTexturePacks.Text);

                IOScene ios = ColladaExport.CreateIOScene(lastSelectedEMO, checkedEMGs, embPack);

                CultureInfo oCurrentCulture;

                // Save the current culture
                oCurrentCulture = CultureInfo.CurrentCulture;
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);
                    IONET.IOManager.ExportScene(
                    ios,
                    Path.Combine(tbColladaDirectory.Text, lastSelectedEMO.Name + ".dae"),
                    new IONET.ExportSettings() { FlipUVs = true });
                }
                finally
                {
                    // Restore the saved culture
                    CultureInfo.CurrentCulture = oCurrentCulture;
                }
                if (embPack != null)
                {
                    foreach (USF4File uf in embPack.Files)
                    {
                        if (uf.GetType() == typeof(DDS)) uf.SaveFile(Path.Combine(tbColladaDirectory.Text, uf.Name + ".dds"));
                    }
                }

            }
        }


        private void btnImportEMG_Click(object sender, EventArgs e)
        {

        }

        private void btnImportIOMesh_Click(object sender, EventArgs e)
        {
            if (lastSelectedEMO != null && tvColladaFiles.SelectedNode.Tag.GetType() == typeof(IOMesh))
            {

                //TODO Check mesh/polygon size! Sakura's jersey is blowing it up if you import it as a single piece
                //So obviously the limit is not that high!!
                int textureIndex = cbTextureIndex.SelectedIndex;
                int normalMapIndex = cbTextureIndex.SelectedIndex + cbTextureIndex.Items.Count;

                if (rblTargetEMG.SelectedIndex < lastSelectedEMO.EMGs.Count)
                {
                    lastSelectedEMO.EMGs.RemoveAt(rblTargetEMG.SelectedIndex);
                    lastSelectedEMO.EMGs.Insert(rblTargetEMG.SelectedIndex, ColladaImport.GenerateEMGfromIOMesh((IOMesh)tvColladaFiles.SelectedNode.Tag, lastSelectedEMO, textureIndex, normalMapIndex));
                }
                else ColladaImport.AppendIOMeshToEMO((IOMesh)tvColladaFiles.SelectedNode.Tag, lastSelectedEMO, textureIndex, normalMapIndex);

                RefreshTree(tvUSF4Files);
                lastSelectedEMO.SaveFile(tbOutputDirectory.Text + lastSelectedEMO.Name);
                //EMO emo = (EMO)USF4Utils.OpenFileStreamCheckCompression(tbOutputDirectory.Text + lastSelectedEMO.Name);
            }
        }

        private void btnClearInputFiles_Click(object sender, EventArgs e)
        {
            lastSelectedEMO = null;
            cbExportTextures.Enabled = false;
            master_USF4FileList.Clear();
            clbEMGList.Items.Clear();
            clbTexturePacks.Items.Clear();
            clbTexturePacks.Text = string.Empty;
            rblTargetEMG.Items.Clear();
            RefreshTree(tvUSF4Files);
        }

        private void btnClearColladaFiles_Click(object sender, EventArgs e)
        {
            master_ColladaFileList.Clear();
            LastSelectedTreeNodeCollada = null;
            RefreshTree(tvColladaFiles);
        }
    }
}
