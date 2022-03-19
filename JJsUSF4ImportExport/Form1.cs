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

        public static string lastSelectedInputDirectory = string.Empty;
        public static string lastSelectedOutputDirectory = string.Empty;
        public static string lastSelectedColladaDirectory = string.Empty;

        EMO lastSelectedEMO;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();
            rblTargetEMG.Enabled = false;

            //Initialise context menus where applicable
            Control[] controls = new Control[]
            {
                clbEMGList,
                tvColladaFiles,
                tvUSF4Files,
            };
            foreach (Control c in controls) c.ContextMenuStrip = new ContextMenuStrip();
        }

        private void LoadConfig()
        {
            if (File.Exists(StringLibrary.STR_IO_Config))
            {
                string[] lines = File.ReadAllLines(StringLibrary.STR_IO_Config);
                tbInputDirectory.Text = Path.GetFullPath(lines[0]);
                tbOutputDirectory.Text = Path.GetFullPath(lines[1]);
                tbColladaDirectory.Text = Path.GetFullPath(lines[2]);

                lastSelectedInputDirectory = tbInputDirectory.Text;
                lastSelectedOutputDirectory = tbOutputDirectory.Text;
                lastSelectedColladaDirectory = tbColladaDirectory.Text;
                
            }
            else 
            { 
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                tbInputDirectory.Text = baseDirectory;
                tbOutputDirectory.Text = baseDirectory;
                tbColladaDirectory.Text = baseDirectory;
                lastSelectedInputDirectory = baseDirectory;
                lastSelectedOutputDirectory = baseDirectory;
                lastSelectedColladaDirectory = baseDirectory;
            }
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
            IOPolygon ioPolygon = default;
            TreeNode polygonParentNode = FindParentNodeOfType(typeof(IOPolygon), tvColladaFiles.SelectedNode);
            if (polygonParentNode != null) ioPolygon = (IOPolygon)polygonParentNode.Tag;

            LastSelectedTreeNodeCollada = tvColladaFiles.SelectedNode;
            tvColladaFiles.ContextMenuStrip.Items.Clear();
            if (ioPolygon != null)
            {
                tvColladaFiles.ContextMenuStrip.Items.AddRange(ContextMenuItems.tvColladaMaterialContextMenuItems);
            }
        }

        private void tvTreeUpdateSelectionUSF4()
        {
            SetUSF4FileContextMenu(tvUSF4Files.SelectedNode.Tag);

            EMO emo = default;
            TreeNode emoParentNode = FindParentNodeOfType(typeof(EMO), tvUSF4Files.SelectedNode);
            if (emoParentNode != null) emo = (EMO)emoParentNode.Tag;
            LastSelectedTreeNodeUSF4 = tvUSF4Files.SelectedNode;

            if (emo != null)
            {
                //If nothing's changed, don't rebuild lists
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

        private void SetUSF4FileContextMenu(object tag)
        {
            tvUSF4Files.ContextMenuStrip.Items.Clear();
            //If there's no node tag or it's not a USF4File, return
            if (tag == null || !tag.GetType().IsSubclassOf(typeof(USF4File))) return;
            tvUSF4Files.ContextMenuStrip.Items.AddRange(ContextMenuItems.tvUSF4FileContextMenuItems);
        }
        private void SetEMGListContextMenu()
        {
            clbEMGList.ContextMenuStrip.Items.Clear();
            if (clbEMGList.Items.Count > 0) 
            {
                clbEMGList.ContextMenuStrip.Items.AddRange(ContextMenuItems.EMGListContextMenuItems);
            }
        }

        public void BuildTexturePackList(string emo_name)
        {
            if (master_USF4FileList != null)
            {
                clbTexturePacks.Items.Clear();
                cbTextureIndex.Items.Clear();
                cbNormalMapIndex.Items.Clear();

                int maxTextures = 0;

                string character_code = emo_name.Split('.')[0];

                if (!emo_name.Contains("skl.emo") && !emo_name.Contains("shd.emo"))
                {
                    foreach (USF4File uf in master_USF4FileList)
                    {
                        if (uf.GetType() == typeof(EMB) && !uf.Name.EndsWith(".emz") && !uf.Name.Contains(".nml") && uf.Name.StartsWith(character_code))
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
                    while (cbTextureIndex.Items.Count < maxTextures)
                    {
                        cbTextureIndex.Items.Add(cbTextureIndex.Items.Count);
                        cbNormalMapIndex.Items.Add(cbNormalMapIndex.Items.Count + maxTextures);
                    }
                    cbExportTextures.Enabled = true;
                    cbTextureIndex.Enabled = true;
                    cbNormalMapIndex.Enabled = true;
                    clbTexturePacks.SelectedIndex = 0;
                    cbTextureIndex.SelectedIndex = 0;
                    cbNormalMapIndex.SelectedIndex = 0;
                }
                else
                {
                    cbExportTextures.Enabled = false;
                    cbTextureIndex.Enabled = false;
                    cbNormalMapIndex.Enabled = false;
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
            string selectedNodePath = string.Empty;
            if (tree.SelectedNode != null) selectedNodePath = tree.SelectedNode.FullPath;
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
            if (selectedNodePath != string.Empty)
            {
                IEnumerable<TreeNode> treeNodes = TreeViewExtensions.Descendants(tree.Nodes);
                foreach (TreeNode tn in treeNodes)
                {
                    if (selectedNodePath == tn.FullPath)
                    {
                        if (tree == tvUSF4Files) LastSelectedTreeNodeUSF4 = tn;
                        else if (tree == tvColladaFiles) LastSelectedTreeNodeCollada = tn;

                        //tn.BackColor = Color.LightBlue;
                    }
                }
            }
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
                foreach (IOModel ioM in ioS.Models)
                {
                    tvColladaFiles.Nodes.Add(ioM.GenerateioModelTreeNode(ioS.Name));
                }
            }
        }
        
        private static TreeNode FindParentNodeOfType(Type search_type, TreeNode n)
        {
            if (n.Tag != null && n.Tag.GetType() == search_type) return n;

            List<TreeNode> nodeHeirarchy = new List<TreeNode>() { n };

            while (nodeHeirarchy.Last().Parent != null)
            {
                nodeHeirarchy.Add(nodeHeirarchy.Last().Parent);
            }

            foreach (TreeNode node in nodeHeirarchy)
            {
                if (node.Tag != null && node.Tag.GetType() == search_type) return node;
            }

            //Return the original node if we couldn't find a result
            return null;
        }

        #endregion

        private void btnInputDirectory_Click(object sender, EventArgs e)
        {

            using (var frm = new OpenFolderDialog() { InitialFolder = Directory.Exists(tbInputDirectory.Text) ? tbInputDirectory.Text : default })
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
            using (var frm = new OpenFolderDialog() { InitialFolder = Directory.Exists(tbInputDirectory.Text) ? tbInputDirectory.Text : default })
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
            using (var frm = new OpenFolderDialog() { InitialFolder = Directory.Exists(tbColladaDirectory.Text) ? tbColladaDirectory.Text : default })
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    lastSelectedColladaDirectory = frm.Folder;
                    tbColladaDirectory.Text = frm.Folder;
                }
            }
        }

        private async Task<List<IOScene>> LoadColladaFilesAsync(IProgress<LoadColladaProgress> progress)
        {
            List<IOScene> loadedFiles = new List<IOScene>();

            DirectoryInfo directoryInfo = new DirectoryInfo(tbColladaDirectory.Text);
            string[] files = Directory.EnumerateFiles(tbColladaDirectory.Text, "*.dae").ToArray();
            int totalSize = (int)directoryInfo.EnumerateFiles("*.dae").Sum(f => f.Length);
            int fileSizeLoaded = 0;
            LoadColladaProgress report = new LoadColladaProgress();

            await Task.Run(() =>
            {
                foreach (string file in files)
                {
                    try
                    {
                        IOScene ioS = IONET.IOManager.LoadScene(file, new IONET.ImportSettings()
                        {
                            Optimize = false,
                            Triangulate = true,
                            FlipUVs = true,
                        });
                        ioS.Name = Path.GetFileName(file);
                        loadedFiles.Add(ioS);

                        fileSizeLoaded += (int)directoryInfo.EnumerateFiles(Path.GetFileName(file)).Sum(f => f.Length);

                        report.ScenesLoaded = loadedFiles;
                        report.Percent = (100 * fileSizeLoaded) / totalSize;
                        progress.Report(report);
                    }
                    catch
                    {
                        lblStatusBarFeedback.Text = StringLibrary.STR_ERR_InvalidColladaFile + Path.GetFileName(file);
                    }
                }
            });

            return loadedFiles;
        }

        private async void btnLoadColladaFiles_Click(object sender, EventArgs e)
        {
            if (tbColladaDirectory.Text != string.Empty)
            {
                //Collection of controls we need to lock out during file loading
                Control[] ColladaControls = new Control[]
                {
                    tbColladaDirectory,
                    btnColladaDirectory,
                    btnLoadColladaFiles,
                    btnClearColladaFiles,
                    tvColladaFiles,
                    gbImportSettings
                };

                foreach (Control control in ColladaControls)
                {
                    control.Enabled = false;
                }
                progressBarCollada.Value = 0;
                progressBarCollada.Visible = true;
                Progress<LoadColladaProgress> progress = new Progress<LoadColladaProgress>();
                progress.ProgressChanged += ReportProgress;

                string[] files = Directory.EnumerateFiles(tbColladaDirectory.Text, "*.dae").ToArray();

                List<IOScene> task = await LoadColladaFilesAsync(progress);

                progressBarCollada.Visible = false;

                foreach (Control control in ColladaControls)
                {
                    control.Enabled = true;
                }
            }
            else lblStatusBarFeedback.Text = StringLibrary.STR_ERR_EmptyPath;
        }

        private void ReportProgress(object sender, LoadColladaProgress e)
        {
            progressBarCollada.Value = e.Percent;
            //Populate the tree as we go - tree is disabled so user can't click anything, but can see what's loaded
            foreach(IOScene ioScene in e.ScenesLoaded)
            {
                if (!master_ColladaFileList.Contains(ioScene)) master_ColladaFileList.Add(ioScene);
            }
            RefreshTree(tvColladaFiles);
        }

        private async Task<List<USF4File>> LoadUSF4FilesAsync(string[] files)
        {
            List<Task<USF4File>> tasks = new List<Task<USF4File>>();

            foreach (string file in files)
            {
                if (file.Contains(".emo") || file.Contains(".emb") || file.Contains(".emm"))
                    tasks.Add(Task.Run(() => USF4Utils.OpenFileStreamCheckCompression(file)));
            }

            var loadedFiles = await Task.WhenAll(tasks);

            return loadedFiles.ToList();
        }

        private async void btnLoadGameFiles_Click(object sender, EventArgs e)
        {
            if (tbInputDirectory.Text != string.Empty)
            {
                //Collection of controls we need to lock out during file loading
                Control[] USF4Controls = new Control[]
                {
                    tbInputDirectory,
                    btnInputDirectory,
                    btnLoadInputFiles,
                    btnClearInputFiles,
                    gbExportSettings,
                    gbImportSettings,
                    tvUSF4Files,
                };

                foreach(Control control in USF4Controls)
                {
                    control.Enabled = false;
                }

                master_USF4FileList.Clear();

                string[] files = Directory.EnumerateFiles(tbInputDirectory.Text, "*.em*").ToArray();

                List<USF4File> task = await LoadUSF4FilesAsync(files);

                master_USF4FileList.AddRange(task);
                //Re-enable controls
                foreach (Control control in USF4Controls)
                {
                    control.Enabled = true;
                }
                RefreshTree(tvUSF4Files);
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
                    
                    string feedback = $"Exported {lastSelectedEMO.Name} to .dae. ";

                    if (embPack != null)
                    {
                        feedback += $"{embPack.Name} extracted to .dds.";
                        foreach (USF4File uf in embPack.Files)
                        {
                            if (uf.GetType() == typeof(DDS)) uf.SaveFile(Path.Combine(tbColladaDirectory.Text, uf.Name + ".dds"));
                        }
                    }
                    lblStatusBarFeedback.Text = feedback;
                }
                finally
                {
                    // Restore the saved culture
                    CultureInfo.CurrentCulture = oCurrentCulture;
                }
            }
        }

        private void btnImportIOMesh_Click(object sender, EventArgs e)
        {
            IOMesh ioMesh = default;
            TreeNode ioMeshParentNode = FindParentNodeOfType(typeof(IOMesh), tvColladaFiles.SelectedNode);
            if (ioMeshParentNode != null) ioMesh = (IOMesh)ioMeshParentNode.Tag;

            if (lastSelectedEMO != null && ioMesh != null)
            {
                try
                {
                    //TODO Check mesh/polygon size! Sakura's jersey is blowing it up if you import it as a single piece
                    //So obviously the limit is not that high!!
                    int textureIndex = cbTextureIndex.SelectedIndex;
                    int normalMapIndex = cbNormalMapIndex.SelectedIndex;

                    EMG new_emg = ColladaImport.GenerateEMGfromIOMesh(ioMesh, lastSelectedEMO, textureIndex, normalMapIndex);

                    if (rblTargetEMG.SelectedIndex < lastSelectedEMO.EMGs.Count)
                    {
                        lastSelectedEMO.EMGs.RemoveAt(rblTargetEMG.SelectedIndex);
                        lastSelectedEMO.EMGs.Insert(rblTargetEMG.SelectedIndex, new_emg);
                    }
                    else lastSelectedEMO.EMGs.Add(new_emg);

                    RefreshTree(tvUSF4Files);
                    foreach (TreeNode n in TreeViewExtensions.Descendants(tvUSF4Files.Nodes))
                    {
                        if (n.Tag != null && n.Tag == lastSelectedEMO)
                        {
                            TreeNode emgNode = n.Nodes[rblTargetEMG.SelectedIndex];
                            //Scroll up to the EMO, then down to the EMG node to push the window to the right position
                            n.EnsureVisible();
                            emgNode.EnsureVisible();
                            TimedFeedbackLabel label = new TimedFeedbackLabel("EMG created!", 2000, tvUSF4Files, emgNode);
                            break;
                        }
                    }

                    clbEMGList.Items.Clear();
                    rblTargetEMG.Items.Clear();
                    foreach (EMG emg in lastSelectedEMO.EMGs)
                    {
                        clbEMGList.Items.Add($"[{rblTargetEMG.Items.Count:D2}] {emg.Name}", CheckState.Checked);
                        rblTargetEMG.Items.Add($"[{rblTargetEMG.Items.Count:D2}] {emg.Name}");
                    }
                    rblTargetEMG.Items.Add("As new EMG");
                    rblTargetEMG.SelectedIndex = rblTargetEMG.Items.Count - 1;
                }
                catch (Exception exception)
                {
                    string[] message = exception.Message.Split('#');
                    lblStatusBarFeedback.Text = message[0] + $" ({message[1]})";
                }
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
            SetEMGListContextMenu();
            RefreshTree(tvUSF4Files);
        }

        private void btnClearColladaFiles_Click(object sender, EventArgs e)
        {
            master_ColladaFileList.Clear();
            LastSelectedTreeNodeCollada = null;
            RefreshTree(tvColladaFiles);
        }

        private void tvTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (sender == tvUSF4Files) tvUSF4Files.SelectedNode = tvUSF4Files.GetNodeAt(e.Location);
            else if (sender == tvColladaFiles) tvColladaFiles.SelectedNode = tvColladaFiles.GetNodeAt(e.Location);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string data = string.Empty;

            data += lastSelectedInputDirectory + Environment.NewLine;
            data += lastSelectedOutputDirectory + Environment.NewLine;
            data += lastSelectedColladaDirectory + Environment.NewLine;

            File.WriteAllText(StringLibrary.STR_IO_Config, data);
        }

        private void tvColladaFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            TreeFileRPress(sender, e);
        }

        private void tvUSF4Files_KeyPress(object sender, KeyPressEventArgs e)
        {
            TreeFileRPress(sender, e);
        }

        private void TreeFileRPress(object sender, KeyPressEventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                if (tv.SelectedNode.Tag != null)
                {
                    ContextMenuFunctions.ChangeLastSelectedNodeName(tv);
                }
            }
        }

        private void btnLoadSingleFile_Click(object sender, EventArgs e)
        {
            using (var frm = new OpenFileDialog() {
                InitialDirectory = Directory.Exists(tbInputDirectory.Text) ? tbInputDirectory.Text : default,
                Filter = "*.emo; *.emm; *.emb|*.emo;*.emm;*.emb"
            })
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    USF4File uf = USF4Utils.OpenFileStreamCheckCompression(frm.FileName);
                    master_USF4FileList.Add(USF4Utils.OpenFileStreamCheckCompression(frm.FileName));
                    RefreshTree(tvUSF4Files);
                }
            }
        }
    }
}
