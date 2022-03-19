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

        #region TreeViewUSF4

        #endregion
        private static string tvUSF4File_QuicksaveFile_Text = "Quicksave";
        private static string tvUSF4File_SaveFile_Text = "Save as...";
        public static ToolStripMenuItem[] tvUSF4FileContextMenuItems = new ToolStripMenuItem[]
        {
            new ToolStripMenuItem(tvUSF4File_QuicksaveFile_Text, null, ContextMenuFunctions.cmTvUSF4QuicksaveFile_Click),
            new ToolStripMenuItem(tvUSF4File_SaveFile_Text, null, ContextMenuFunctions.cmTvUSF4SaveFileAs_Click),
        };
        #region TreeViewCollada
        private static string tvColladaMaterial_ChangeMaterial_Text = "Change material name";
        public static ToolStripMenuItem[] tvColladaMaterialContextMenuItems = new ToolStripMenuItem[]
        {
            new ToolStripMenuItem(tvColladaMaterial_ChangeMaterial_Text, null, ContextMenuFunctions.cmTvColladaMaterialChangeMaterial_Click),
        };
        #endregion
    }
}
