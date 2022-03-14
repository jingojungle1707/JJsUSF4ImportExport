using IONET.Core.Model;
using System;
using System.Windows.Forms;

namespace JJsUSF4ImportExport
{
    class ContextMenuFunctions
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
        }

        #endregion
    }
}
