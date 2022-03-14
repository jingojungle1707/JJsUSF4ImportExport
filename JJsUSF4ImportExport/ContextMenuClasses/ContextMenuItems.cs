using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJsUSF4ImportExport
{
    class ContextMenuItems
    {
        #region EMGCheckedListBox
        private static string EMG_SelectAll_Text = "Select all";
        private static string EMG_SelectNone_Text = "Select none";

        public static ToolStripMenuItem[] EMGListContextMenuItems = new ToolStripMenuItem[]
        {
            new ToolStripMenuItem(EMG_SelectAll_Text, null, ContextMenuFunctions.cmEMGListSelectAll_Click),
            new ToolStripMenuItem(EMG_SelectNone_Text, null, ContextMenuFunctions.cmEMGListSelectNone_Click),
        };
        #endregion

        #region TreeViewCollada
        private static string tvColladaMaterial_ChangeMaterial_Text = "Change material name";
        public static ToolStripMenuItem[] tvColladaMaterialContextMenuItems = new ToolStripMenuItem[]
        {
            new ToolStripMenuItem(tvColladaMaterial_ChangeMaterial_Text, null, ContextMenuFunctions.cmTvColladaMaterialChangeMaterial_Click),
        };
        #endregion
    }
}
