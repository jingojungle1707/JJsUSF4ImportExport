
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tbOutputDirectory = new System.Windows.Forms.TextBox();
            this.tbInputDirectory = new System.Windows.Forms.TextBox();
            this.gbWorkingDirectories = new System.Windows.Forms.GroupBox();
            this.lbFeedback = new System.Windows.Forms.Label();
            this.btnExportEMO = new System.Windows.Forms.Button();
            this.clbEMGList = new System.Windows.Forms.CheckedListBox();
            this.clbTexturePacks = new System.Windows.Forms.ComboBox();
            this.cbExportTextures = new System.Windows.Forms.CheckBox();
            this.gbExportSettings = new System.Windows.Forms.GroupBox();
            this.gbWorkingDirectories.SuspendLayout();
            this.gbExportSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(3, 118);
            this.treeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(415, 591);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 83);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 27);
            this.button1.TabIndex = 4;
            this.button1.Text = "Load Files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnLoadFiles_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(762, 53);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Output Directory...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnOutputDirectory_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(762, 23);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(146, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Input Directory...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnInputDirectory_Click);
            // 
            // tbOutputDirectory
            // 
            this.tbOutputDirectory.Location = new System.Drawing.Point(9, 53);
            this.tbOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbOutputDirectory.Name = "tbOutputDirectory";
            this.tbOutputDirectory.Size = new System.Drawing.Size(747, 23);
            this.tbOutputDirectory.TabIndex = 1;
            // 
            // tbInputDirectory
            // 
            this.tbInputDirectory.Location = new System.Drawing.Point(9, 23);
            this.tbInputDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbInputDirectory.Name = "tbInputDirectory";
            this.tbInputDirectory.Size = new System.Drawing.Size(747, 23);
            this.tbInputDirectory.TabIndex = 0;
            // 
            // gbWorkingDirectories
            // 
            this.gbWorkingDirectories.Controls.Add(this.lbFeedback);
            this.gbWorkingDirectories.Controls.Add(this.button1);
            this.gbWorkingDirectories.Controls.Add(this.button2);
            this.gbWorkingDirectories.Controls.Add(this.button3);
            this.gbWorkingDirectories.Controls.Add(this.tbOutputDirectory);
            this.gbWorkingDirectories.Controls.Add(this.tbInputDirectory);
            this.gbWorkingDirectories.Location = new System.Drawing.Point(3, 2);
            this.gbWorkingDirectories.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbWorkingDirectories.Name = "gbWorkingDirectories";
            this.gbWorkingDirectories.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbWorkingDirectories.Size = new System.Drawing.Size(915, 117);
            this.gbWorkingDirectories.TabIndex = 1;
            this.gbWorkingDirectories.TabStop = false;
            this.gbWorkingDirectories.Text = "Working Directories";
            // 
            // lbFeedback
            // 
            this.lbFeedback.AutoSize = true;
            this.lbFeedback.Location = new System.Drawing.Point(172, 89);
            this.lbFeedback.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFeedback.Name = "lbFeedback";
            this.lbFeedback.Size = new System.Drawing.Size(0, 15);
            this.lbFeedback.TabIndex = 5;
            // 
            // btnExportEMO
            // 
            this.btnExportEMO.Location = new System.Drawing.Point(733, 125);
            this.btnExportEMO.Name = "btnExportEMO";
            this.btnExportEMO.Size = new System.Drawing.Size(185, 52);
            this.btnExportEMO.TabIndex = 2;
            this.btnExportEMO.Text = "Export .emo to Collada";
            this.btnExportEMO.UseVisualStyleBackColor = true;
            this.btnExportEMO.Click += new System.EventHandler(this.btnEMoTest_Click);
            // 
            // clbEMGList
            // 
            this.clbEMGList.CheckOnClick = true;
            this.clbEMGList.FormattingEnabled = true;
            this.clbEMGList.Location = new System.Drawing.Point(6, 76);
            this.clbEMGList.Name = "clbEMGList";
            this.clbEMGList.Size = new System.Drawing.Size(290, 508);
            this.clbEMGList.TabIndex = 4;
            // 
            // clbTexturePacks
            // 
            this.clbTexturePacks.FormattingEnabled = true;
            this.clbTexturePacks.Location = new System.Drawing.Point(6, 22);
            this.clbTexturePacks.Name = "clbTexturePacks";
            this.clbTexturePacks.Size = new System.Drawing.Size(290, 23);
            this.clbTexturePacks.TabIndex = 5;
            // 
            // cbExportTextures
            // 
            this.cbExportTextures.AutoSize = true;
            this.cbExportTextures.Enabled = false;
            this.cbExportTextures.Location = new System.Drawing.Point(6, 51);
            this.cbExportTextures.Name = "cbExportTextures";
            this.cbExportTextures.Size = new System.Drawing.Size(160, 19);
            this.cbExportTextures.TabIndex = 6;
            this.cbExportTextures.Text = "Include textures in export";
            this.cbExportTextures.UseVisualStyleBackColor = true;
            // 
            // gbExportSettings
            // 
            this.gbExportSettings.Controls.Add(this.clbTexturePacks);
            this.gbExportSettings.Controls.Add(this.cbExportTextures);
            this.gbExportSettings.Controls.Add(this.clbEMGList);
            this.gbExportSettings.Location = new System.Drawing.Point(425, 118);
            this.gbExportSettings.Name = "gbExportSettings";
            this.gbExportSettings.Size = new System.Drawing.Size(302, 591);
            this.gbExportSettings.TabIndex = 7;
            this.gbExportSettings.TabStop = false;
            this.gbExportSettings.Text = "Export Settings";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 711);
            this.Controls.Add(this.gbExportSettings);
            this.Controls.Add(this.btnExportEMO);
            this.Controls.Add(this.gbWorkingDirectories);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "JJ\'s USF4 Import Export";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbWorkingDirectories.ResumeLayout(false);
            this.gbWorkingDirectories.PerformLayout();
            this.gbExportSettings.ResumeLayout(false);
            this.gbExportSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbOutputDirectory;
        private System.Windows.Forms.TextBox tbInputDirectory;
        private System.Windows.Forms.GroupBox gbWorkingDirectories;
        private System.Windows.Forms.Button btnExportEMO;
        private System.Windows.Forms.CheckedListBox clbEMGList;
        private System.Windows.Forms.ComboBox clbTexturePacks;
        private System.Windows.Forms.CheckBox cbExportTextures;
        private System.Windows.Forms.GroupBox gbExportSettings;
        private System.Windows.Forms.Label lbFeedback;
    }
}

