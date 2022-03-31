using IONET.Core.Model;
using JJsUSF4Library.FileClasses;
using JJsUSF4Library.FileClasses.SubfileClasses;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace JJsUSF4ImportExport
{
    partial class ContextMenuFunctions
    {
        #region EMGCheckedListBox
        public static void cmEMGListSelectAll_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    CheckedListBox clb = owner.SourceControl as CheckedListBox;
                    for (int i = 0; i < clb.Items.Count; i++) clb.SetItemChecked(i, true);
                }
            }
        }
        public static void cmEMGListSelectNone_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    CheckedListBox clb = owner.SourceControl as CheckedListBox;
                    for (int i = 0; i < clb.Items.Count; i++) clb.SetItemChecked(i, false);
                }
            }
        }

        #endregion
        #region TreeViewUSF4

        public static void cmTvUSF4QuicksaveFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    TreeView tv = owner.SourceControl as TreeView;
                    TreeNode n = tv.SelectedNode;
                    if (n.Tag != null)
                    {
                        USF4File uf = (USF4File)n.Tag;
                        uf.SaveToPath(Path.Join(Form1.lastSelectedOutputDirectory, uf.Name));

                        _ = new TimedFeedbackLabel($"Quicksaved!", 2000, tv, n);
                    }
                }
            }
        }
        public static void cmTvUSF4SaveFileAs_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    TreeView tv = owner.SourceControl as TreeView;
                    TreeNode n = tv.SelectedNode;
                    if (n.Tag != null)
                    {
                        USF4File uf = (USF4File)n.Tag;
                        using (var frm = new SaveFileDialog() {
                            InitialDirectory = Path.GetFullPath(Form1.lastSelectedOutputDirectory),
                            FileName = uf.Name,
                        })
                        {
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                uf.SaveToPath(frm.FileName);
                                _ = new TimedFeedbackLabel($"Saved!", 2000, tv, n);
                            }
                        }

                    }
                }
            }
        }

        #endregion

        #region TreeViewCollada
        public static void cmTvColladaMaterialChangeMaterial_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    TreeView tv = owner.SourceControl as TreeView;
                    ChangeLastSelectedNodeName(tv);
                }
            }
        }

        public static void ChangeLastSelectedNodeName(TreeView tv)
        {
            if (tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(IOPolygon))
            {
                IOPolygon ioPolygon = (IOPolygon)tv.SelectedNode.Tag;
                StringNameInput form = new StringNameInput(ioPolygon.MaterialName, StringNameInput.NameType.Material);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = tv.PointToScreen(tv.SelectedNode.Bounds.Location);
                try
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ioPolygon.MaterialName = form.NewName;
                        tv.SelectedNode.Text = ioPolygon.MaterialName;
                    }
                }
                finally
                {
                    form.Dispose();
                }
            }
            else if (tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(IOMesh))
            {
                IOMesh ioMesh = (IOMesh)tv.SelectedNode.Tag;
                StringNameInput form = new StringNameInput(ioMesh.Name, StringNameInput.NameType.Mesh);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = tv.PointToScreen(tv.SelectedNode.Bounds.Location);
                try
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ioMesh.Name = form.NewName;
                        tv.SelectedNode.Text = ioMesh.Name;
                    }
                }
                finally
                {
                    form.Dispose();
                }
            }
            else if (tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(SubModel))
            {
                SubModel subModel = (SubModel)tv.SelectedNode.Tag;
                StringNameInput form = new StringNameInput(subModel.Name, StringNameInput.NameType.Material);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = tv.PointToScreen(tv.SelectedNode.Bounds.Location);
                try
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        subModel.Name = form.NewName;
                        tv.SelectedNode.Text = subModel.Name;
                    }
                }
                finally
                {
                    form.Dispose();
                }
            }
            else if (tv.SelectedNode.Tag != null && tv.SelectedNode.Tag.GetType() == typeof(Material))
            {
                Material eMMmaterial = (Material)tv.SelectedNode.Tag;
                StringNameInput form = new StringNameInput(eMMmaterial.Name, StringNameInput.NameType.Material);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = tv.PointToScreen(tv.SelectedNode.Bounds.Location);
                try
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        eMMmaterial.Name = form.NewName;
                        tv.SelectedNode.Text = eMMmaterial.Name;
                    }
                }
                finally
                {
                    form.Dispose();
                }
            }
        }

        #endregion
    }
}
