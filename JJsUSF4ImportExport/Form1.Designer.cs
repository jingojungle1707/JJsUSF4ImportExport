
namespace JJsUSF4ImportExport
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tvUSF4Files = new System.Windows.Forms.TreeView();
            this.btnLoadInputFiles = new System.Windows.Forms.Button();
            this.btnOutputDirectory = new System.Windows.Forms.Button();
            this.btnInputDirectory = new System.Windows.Forms.Button();
            this.tbOutputDirectory = new System.Windows.Forms.TextBox();
            this.tbInputDirectory = new System.Windows.Forms.TextBox();
            this.gbWorkingDirectories = new System.Windows.Forms.GroupBox();
            this.btnColladaDirectory = new System.Windows.Forms.Button();
            this.lbDirectoryDivider = new System.Windows.Forms.Label();
            this.tbColladaDirectory = new System.Windows.Forms.TextBox();
            this.lbFeedback = new System.Windows.Forms.Label();
            this.btnExportEMO = new System.Windows.Forms.Button();
            this.clbEMGList = new System.Windows.Forms.CheckedListBox();
            this.clbTexturePacks = new System.Windows.Forms.ComboBox();
            this.cbExportTextures = new System.Windows.Forms.CheckBox();
            this.gbExportSettings = new System.Windows.Forms.GroupBox();
            this.gbImportSettings = new System.Windows.Forms.GroupBox();
            this.lbUseNormalMapIndex = new System.Windows.Forms.Label();
            this.cbNormalMapIndex = new System.Windows.Forms.ComboBox();
            this.cbSFxT = new System.Windows.Forms.CheckBox();
            this.cbTextureIndex = new System.Windows.Forms.ComboBox();
            this.lbUseTextureIndex = new System.Windows.Forms.Label();
            this.rblTargetEMG = new JJsUSF4ImportExport.RadioButtonList();
            this.btnImportIOMesh = new System.Windows.Forms.Button();
            this.tvColladaFiles = new System.Windows.Forms.TreeView();
            this.btnLoadColladaFiles = new System.Windows.Forms.Button();
            this.ssGeneralStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatusBarFeedback = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnClearInputFiles = new System.Windows.Forms.Button();
            this.btnClearColladaFiles = new System.Windows.Forms.Button();
            this.pnlUserControls = new System.Windows.Forms.Panel();
            this.progressBarCollada = new System.Windows.Forms.ProgressBar();
            this.btnLoadSingleFile = new System.Windows.Forms.Button();
            this.gbWorkingDirectories.SuspendLayout();
            this.gbExportSettings.SuspendLayout();
            this.gbImportSettings.SuspendLayout();
            this.ssGeneralStatus.SuspendLayout();
            this.pnlUserControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvUSF4Files
            // 
            this.tvUSF4Files.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvUSF4Files.Location = new System.Drawing.Point(3, 160);
            this.tvUSF4Files.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tvUSF4Files.Name = "tvUSF4Files";
            this.tvUSF4Files.Size = new System.Drawing.Size(311, 487);
            this.tvUSF4Files.TabIndex = 0;
            this.tvUSF4Files.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.tvUSF4Files.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTree_NodeMouseClick);
            this.tvUSF4Files.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvUSF4Files_KeyPress);
            // 
            // btnLoadInputFiles
            // 
            this.btnLoadInputFiles.Location = new System.Drawing.Point(3, 127);
            this.btnLoadInputFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLoadInputFiles.Name = "btnLoadInputFiles";
            this.btnLoadInputFiles.Size = new System.Drawing.Size(98, 27);
            this.btnLoadInputFiles.TabIndex = 4;
            this.btnLoadInputFiles.Text = "Load Input Files";
            this.btnLoadInputFiles.UseVisualStyleBackColor = true;
            this.btnLoadInputFiles.Click += new System.EventHandler(this.btnLoadGameFiles_Click);
            // 
            // btnOutputDirectory
            // 
            this.btnOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutputDirectory.Location = new System.Drawing.Point(1001, 53);
            this.btnOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOutputDirectory.Name = "btnOutputDirectory";
            this.btnOutputDirectory.Size = new System.Drawing.Size(146, 23);
            this.btnOutputDirectory.TabIndex = 3;
            this.btnOutputDirectory.Text = "Output Directory...";
            this.btnOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOutputDirectory.Click += new System.EventHandler(this.btnOutputDirectory_Click);
            // 
            // btnInputDirectory
            // 
            this.btnInputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInputDirectory.Location = new System.Drawing.Point(1001, 23);
            this.btnInputDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnInputDirectory.Name = "btnInputDirectory";
            this.btnInputDirectory.Size = new System.Drawing.Size(146, 23);
            this.btnInputDirectory.TabIndex = 2;
            this.btnInputDirectory.Text = "Input Directory...";
            this.btnInputDirectory.UseVisualStyleBackColor = true;
            this.btnInputDirectory.Click += new System.EventHandler(this.btnInputDirectory_Click);
            // 
            // tbOutputDirectory
            // 
            this.tbOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputDirectory.Location = new System.Drawing.Point(9, 53);
            this.tbOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbOutputDirectory.Name = "tbOutputDirectory";
            this.tbOutputDirectory.Size = new System.Drawing.Size(986, 23);
            this.tbOutputDirectory.TabIndex = 1;
            // 
            // tbInputDirectory
            // 
            this.tbInputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputDirectory.Location = new System.Drawing.Point(9, 23);
            this.tbInputDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbInputDirectory.Name = "tbInputDirectory";
            this.tbInputDirectory.Size = new System.Drawing.Size(986, 23);
            this.tbInputDirectory.TabIndex = 0;
            // 
            // gbWorkingDirectories
            // 
            this.gbWorkingDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWorkingDirectories.Controls.Add(this.btnColladaDirectory);
            this.gbWorkingDirectories.Controls.Add(this.lbDirectoryDivider);
            this.gbWorkingDirectories.Controls.Add(this.tbColladaDirectory);
            this.gbWorkingDirectories.Controls.Add(this.btnOutputDirectory);
            this.gbWorkingDirectories.Controls.Add(this.btnInputDirectory);
            this.gbWorkingDirectories.Controls.Add(this.tbOutputDirectory);
            this.gbWorkingDirectories.Controls.Add(this.tbInputDirectory);
            this.gbWorkingDirectories.Location = new System.Drawing.Point(6, 3);
            this.gbWorkingDirectories.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbWorkingDirectories.Name = "gbWorkingDirectories";
            this.gbWorkingDirectories.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbWorkingDirectories.Size = new System.Drawing.Size(1154, 116);
            this.gbWorkingDirectories.TabIndex = 1;
            this.gbWorkingDirectories.TabStop = false;
            this.gbWorkingDirectories.Text = "Working Directories";
            // 
            // btnColladaDirectory
            // 
            this.btnColladaDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColladaDirectory.Location = new System.Drawing.Point(1000, 85);
            this.btnColladaDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnColladaDirectory.Name = "btnColladaDirectory";
            this.btnColladaDirectory.Size = new System.Drawing.Size(146, 23);
            this.btnColladaDirectory.TabIndex = 8;
            this.btnColladaDirectory.Text = "Collada Directory...";
            this.btnColladaDirectory.UseVisualStyleBackColor = true;
            this.btnColladaDirectory.Click += new System.EventHandler(this.btnColladaDirectory_Click);
            // 
            // lbDirectoryDivider
            // 
            this.lbDirectoryDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDirectoryDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbDirectoryDivider.Location = new System.Drawing.Point(-1, 80);
            this.lbDirectoryDivider.Name = "lbDirectoryDivider";
            this.lbDirectoryDivider.Size = new System.Drawing.Size(1156, 2);
            this.lbDirectoryDivider.TabIndex = 7;
            // 
            // tbColladaDirectory
            // 
            this.tbColladaDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbColladaDirectory.Location = new System.Drawing.Point(8, 85);
            this.tbColladaDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbColladaDirectory.Name = "tbColladaDirectory";
            this.tbColladaDirectory.Size = new System.Drawing.Size(986, 23);
            this.tbColladaDirectory.TabIndex = 6;
            // 
            // lbFeedback
            // 
            this.lbFeedback.AutoSize = true;
            this.lbFeedback.Location = new System.Drawing.Point(201, 133);
            this.lbFeedback.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFeedback.Name = "lbFeedback";
            this.lbFeedback.Size = new System.Drawing.Size(0, 15);
            this.lbFeedback.TabIndex = 5;
            // 
            // btnExportEMO
            // 
            this.btnExportEMO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportEMO.Location = new System.Drawing.Point(6, 22);
            this.btnExportEMO.Name = "btnExportEMO";
            this.btnExportEMO.Size = new System.Drawing.Size(242, 27);
            this.btnExportEMO.TabIndex = 2;
            this.btnExportEMO.Text = "Export .emo to Collada";
            this.btnExportEMO.UseVisualStyleBackColor = true;
            this.btnExportEMO.Click += new System.EventHandler(this.btnExportEMOtoCollada_Click);
            // 
            // clbEMGList
            // 
            this.clbEMGList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbEMGList.CheckOnClick = true;
            this.clbEMGList.FormattingEnabled = true;
            this.clbEMGList.Location = new System.Drawing.Point(6, 108);
            this.clbEMGList.Name = "clbEMGList";
            this.clbEMGList.Size = new System.Drawing.Size(242, 418);
            this.clbEMGList.TabIndex = 4;
            // 
            // clbTexturePacks
            // 
            this.clbTexturePacks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbTexturePacks.FormattingEnabled = true;
            this.clbTexturePacks.Location = new System.Drawing.Point(6, 80);
            this.clbTexturePacks.Name = "clbTexturePacks";
            this.clbTexturePacks.Size = new System.Drawing.Size(242, 23);
            this.clbTexturePacks.TabIndex = 5;
            // 
            // cbExportTextures
            // 
            this.cbExportTextures.AutoSize = true;
            this.cbExportTextures.Enabled = false;
            this.cbExportTextures.Location = new System.Drawing.Point(6, 55);
            this.cbExportTextures.Name = "cbExportTextures";
            this.cbExportTextures.Size = new System.Drawing.Size(160, 19);
            this.cbExportTextures.TabIndex = 6;
            this.cbExportTextures.Text = "Include textures in export";
            this.cbExportTextures.UseVisualStyleBackColor = true;
            // 
            // gbExportSettings
            // 
            this.gbExportSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbExportSettings.Controls.Add(this.clbTexturePacks);
            this.gbExportSettings.Controls.Add(this.btnExportEMO);
            this.gbExportSettings.Controls.Add(this.cbExportTextures);
            this.gbExportSettings.Controls.Add(this.clbEMGList);
            this.gbExportSettings.Location = new System.Drawing.Point(321, 119);
            this.gbExportSettings.Name = "gbExportSettings";
            this.gbExportSettings.Size = new System.Drawing.Size(254, 531);
            this.gbExportSettings.TabIndex = 7;
            this.gbExportSettings.TabStop = false;
            this.gbExportSettings.Text = "Export Settings";
            // 
            // gbImportSettings
            // 
            this.gbImportSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbImportSettings.Controls.Add(this.lbUseNormalMapIndex);
            this.gbImportSettings.Controls.Add(this.cbNormalMapIndex);
            this.gbImportSettings.Controls.Add(this.cbSFxT);
            this.gbImportSettings.Controls.Add(this.cbTextureIndex);
            this.gbImportSettings.Controls.Add(this.lbUseTextureIndex);
            this.gbImportSettings.Controls.Add(this.rblTargetEMG);
            this.gbImportSettings.Controls.Add(this.btnImportIOMesh);
            this.gbImportSettings.Location = new System.Drawing.Point(581, 119);
            this.gbImportSettings.Name = "gbImportSettings";
            this.gbImportSettings.Size = new System.Drawing.Size(261, 531);
            this.gbImportSettings.TabIndex = 8;
            this.gbImportSettings.TabStop = false;
            this.gbImportSettings.Text = "Import Settings";
            // 
            // lbUseNormalMapIndex
            // 
            this.lbUseNormalMapIndex.AutoSize = true;
            this.lbUseNormalMapIndex.Location = new System.Drawing.Point(85, 83);
            this.lbUseNormalMapIndex.Name = "lbUseNormalMapIndex";
            this.lbUseNormalMapIndex.Size = new System.Drawing.Size(107, 15);
            this.lbUseNormalMapIndex.TabIndex = 9;
            this.lbUseNormalMapIndex.Text = "Use normal map #:";
            // 
            // cbNormalMapIndex
            // 
            this.cbNormalMapIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNormalMapIndex.FormattingEnabled = true;
            this.cbNormalMapIndex.Location = new System.Drawing.Point(198, 80);
            this.cbNormalMapIndex.Name = "cbNormalMapIndex";
            this.cbNormalMapIndex.Size = new System.Drawing.Size(58, 23);
            this.cbNormalMapIndex.TabIndex = 10;
            // 
            // cbSFxT
            // 
            this.cbSFxT.AutoSize = true;
            this.cbSFxT.Location = new System.Drawing.Point(6, 55);
            this.cbSFxT.Name = "cbSFxT";
            this.cbSFxT.Size = new System.Drawing.Size(55, 19);
            this.cbSFxT.TabIndex = 7;
            this.cbSFxT.Text = "SFxT?";
            this.cbSFxT.UseVisualStyleBackColor = true;
            this.cbSFxT.Visible = false;
            // 
            // cbTextureIndex
            // 
            this.cbTextureIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextureIndex.FormattingEnabled = true;
            this.cbTextureIndex.Location = new System.Drawing.Point(197, 51);
            this.cbTextureIndex.Name = "cbTextureIndex";
            this.cbTextureIndex.Size = new System.Drawing.Size(58, 23);
            this.cbTextureIndex.TabIndex = 8;
            // 
            // lbUseTextureIndex
            // 
            this.lbUseTextureIndex.AutoSize = true;
            this.lbUseTextureIndex.Location = new System.Drawing.Point(113, 56);
            this.lbUseTextureIndex.Name = "lbUseTextureIndex";
            this.lbUseTextureIndex.Size = new System.Drawing.Size(79, 15);
            this.lbUseTextureIndex.TabIndex = 7;
            this.lbUseTextureIndex.Text = "Use texture #:";
            // 
            // rblTargetEMG
            // 
            this.rblTargetEMG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rblTargetEMG.FormattingEnabled = true;
            this.rblTargetEMG.Location = new System.Drawing.Point(6, 108);
            this.rblTargetEMG.Name = "rblTargetEMG";
            this.rblTargetEMG.Size = new System.Drawing.Size(250, 418);
            this.rblTargetEMG.TabIndex = 5;
            // 
            // btnImportIOMesh
            // 
            this.btnImportIOMesh.Location = new System.Drawing.Point(6, 22);
            this.btnImportIOMesh.Name = "btnImportIOMesh";
            this.btnImportIOMesh.Size = new System.Drawing.Size(250, 27);
            this.btnImportIOMesh.TabIndex = 2;
            this.btnImportIOMesh.Text = "Write Mesh to EMO";
            this.btnImportIOMesh.UseVisualStyleBackColor = true;
            this.btnImportIOMesh.Click += new System.EventHandler(this.btnImportIOMesh_Click);
            // 
            // tvColladaFiles
            // 
            this.tvColladaFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvColladaFiles.Location = new System.Drawing.Point(848, 160);
            this.tvColladaFiles.Name = "tvColladaFiles";
            this.tvColladaFiles.Size = new System.Drawing.Size(308, 487);
            this.tvColladaFiles.TabIndex = 9;
            this.tvColladaFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.tvColladaFiles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTree_NodeMouseClick);
            this.tvColladaFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvColladaFiles_KeyPress);
            // 
            // btnLoadColladaFiles
            // 
            this.btnLoadColladaFiles.Location = new System.Drawing.Point(848, 127);
            this.btnLoadColladaFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLoadColladaFiles.Name = "btnLoadColladaFiles";
            this.btnLoadColladaFiles.Size = new System.Drawing.Size(147, 27);
            this.btnLoadColladaFiles.TabIndex = 10;
            this.btnLoadColladaFiles.Text = "Load Collada Files";
            this.btnLoadColladaFiles.UseVisualStyleBackColor = true;
            this.btnLoadColladaFiles.Click += new System.EventHandler(this.btnLoadColladaFiles_Click);
            // 
            // ssGeneralStatus
            // 
            this.ssGeneralStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusBarFeedback,
            this.toolStripStatusLabel1});
            this.ssGeneralStatus.Location = new System.Drawing.Point(0, 653);
            this.ssGeneralStatus.Name = "ssGeneralStatus";
            this.ssGeneralStatus.Size = new System.Drawing.Size(1159, 22);
            this.ssGeneralStatus.TabIndex = 11;
            this.ssGeneralStatus.Text = "statusStrip1";
            // 
            // lblStatusBarFeedback
            // 
            this.lblStatusBarFeedback.Name = "lblStatusBarFeedback";
            this.lblStatusBarFeedback.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // btnClearInputFiles
            // 
            this.btnClearInputFiles.Location = new System.Drawing.Point(216, 127);
            this.btnClearInputFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClearInputFiles.Name = "btnClearInputFiles";
            this.btnClearInputFiles.Size = new System.Drawing.Size(98, 27);
            this.btnClearInputFiles.TabIndex = 12;
            this.btnClearInputFiles.Text = "Clear Files";
            this.btnClearInputFiles.UseVisualStyleBackColor = true;
            this.btnClearInputFiles.Click += new System.EventHandler(this.btnClearInputFiles_Click);
            // 
            // btnClearColladaFiles
            // 
            this.btnClearColladaFiles.Location = new System.Drawing.Point(1008, 127);
            this.btnClearColladaFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClearColladaFiles.Name = "btnClearColladaFiles";
            this.btnClearColladaFiles.Size = new System.Drawing.Size(148, 27);
            this.btnClearColladaFiles.TabIndex = 13;
            this.btnClearColladaFiles.Text = "Clear Files";
            this.btnClearColladaFiles.UseVisualStyleBackColor = true;
            this.btnClearColladaFiles.Click += new System.EventHandler(this.btnClearColladaFiles_Click);
            // 
            // pnlUserControls
            // 
            this.pnlUserControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlUserControls.Controls.Add(this.btnLoadSingleFile);
            this.pnlUserControls.Controls.Add(this.progressBarCollada);
            this.pnlUserControls.Controls.Add(this.btnClearColladaFiles);
            this.pnlUserControls.Controls.Add(this.btnClearInputFiles);
            this.pnlUserControls.Controls.Add(this.btnLoadColladaFiles);
            this.pnlUserControls.Controls.Add(this.tvColladaFiles);
            this.pnlUserControls.Controls.Add(this.gbImportSettings);
            this.pnlUserControls.Controls.Add(this.lbFeedback);
            this.pnlUserControls.Controls.Add(this.gbExportSettings);
            this.pnlUserControls.Controls.Add(this.btnLoadInputFiles);
            this.pnlUserControls.Controls.Add(this.tvUSF4Files);
            this.pnlUserControls.Controls.Add(this.gbWorkingDirectories);
            this.pnlUserControls.Location = new System.Drawing.Point(0, 0);
            this.pnlUserControls.Name = "pnlUserControls";
            this.pnlUserControls.Size = new System.Drawing.Size(1160, 650);
            this.pnlUserControls.TabIndex = 14;
            // 
            // progressBarCollada
            // 
            this.progressBarCollada.Location = new System.Drawing.Point(848, 627);
            this.progressBarCollada.Name = "progressBarCollada";
            this.progressBarCollada.Size = new System.Drawing.Size(307, 19);
            this.progressBarCollada.TabIndex = 14;
            this.progressBarCollada.Visible = false;
            // 
            // btnLoadSingleFile
            // 
            this.btnLoadSingleFile.Location = new System.Drawing.Point(110, 127);
            this.btnLoadSingleFile.Name = "btnLoadSingleFile";
            this.btnLoadSingleFile.Size = new System.Drawing.Size(98, 27);
            this.btnLoadSingleFile.TabIndex = 15;
            this.btnLoadSingleFile.Text = "Load Single File";
            this.btnLoadSingleFile.UseVisualStyleBackColor = true;
            this.btnLoadSingleFile.Click += new System.EventHandler(this.btnLoadSingleFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 675);
            this.Controls.Add(this.ssGeneralStatus);
            this.Controls.Add(this.pnlUserControls);
            this.Name = "Form1";
            this.Text = "JJ\'s USF4 Import Export";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbWorkingDirectories.ResumeLayout(false);
            this.gbWorkingDirectories.PerformLayout();
            this.gbExportSettings.ResumeLayout(false);
            this.gbExportSettings.PerformLayout();
            this.gbImportSettings.ResumeLayout(false);
            this.gbImportSettings.PerformLayout();
            this.ssGeneralStatus.ResumeLayout(false);
            this.ssGeneralStatus.PerformLayout();
            this.pnlUserControls.ResumeLayout(false);
            this.pnlUserControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvUSF4Files;
        private System.Windows.Forms.Button btnLoadInputFiles;
        private System.Windows.Forms.Button btnOutputDirectory;
        private System.Windows.Forms.Button btnInputDirectory;
        private System.Windows.Forms.TextBox tbOutputDirectory;
        private System.Windows.Forms.TextBox tbInputDirectory;
        private System.Windows.Forms.GroupBox gbWorkingDirectories;
        private System.Windows.Forms.Button btnExportEMO;
        private System.Windows.Forms.CheckedListBox clbEMGList;
        private System.Windows.Forms.ComboBox clbTexturePacks;
        private System.Windows.Forms.CheckBox cbExportTextures;
        private System.Windows.Forms.GroupBox gbExportSettings;
        private System.Windows.Forms.Label lbFeedback;
        private System.Windows.Forms.GroupBox gbImportSettings;
        private System.Windows.Forms.Button btnImportIOMesh;
        private System.Windows.Forms.TreeView tvColladaFiles;
        private RadioButtonList rblTargetEMG;
        private System.Windows.Forms.Label lbDirectoryDivider;
        private System.Windows.Forms.TextBox tbColladaDirectory;
        private System.Windows.Forms.Button btnColladaDirectory;
        private System.Windows.Forms.Button btnLoadColladaFiles;
        private System.Windows.Forms.StatusStrip ssGeneralStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusBarFeedback;
        private System.Windows.Forms.Button btnClearInputFiles;
        private System.Windows.Forms.Button btnClearColladaFiles;
        private System.Windows.Forms.ComboBox cbTextureIndex;
        private System.Windows.Forms.Label lbUseTextureIndex;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel pnlUserControls;
        private System.Windows.Forms.ProgressBar progressBarCollada;
        private System.Windows.Forms.CheckBox cbSFxT;
        private System.Windows.Forms.ComboBox cbNormalMapIndex;
        private System.Windows.Forms.Label lbUseNormalMapIndex;
        private System.Windows.Forms.Button btnLoadSingleFile;
    }
}

