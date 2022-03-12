using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JJsUSF4ImportExport
{
    public static class TreeViewExtensions
    {
        public static TreeNode SelectedNodeBeforeRefresh;
        public static List<string> GetExpansionState(this TreeNodeCollection nodes)
        {
            return nodes.Descendants().Where(n => n.IsExpanded).Select(n => n.FullPath).ToList();
        }

        public static void SetExpansionState(this TreeNodeCollection nodes, List<string> savedExpansionState)
        {

            foreach (var node in nodes.Descendants().Where(n => savedExpansionState.Contains(n.FullPath)))
            {
                node.Expand();
            }
            foreach (var node in nodes.Descendants().Where(n => n.IsSelected))
            {
                if (node.IsSelected) SelectedNodeBeforeRefresh = node;
                break;
            }
        }

        public static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c)
        {
            foreach (var node in c.OfType<TreeNode>())
            {
                //if (node.IsSelected) SelectedNodeBeforeRefresh = node;
                yield return node;
                foreach (var child in node.Nodes.Descendants())
                {
                    //if (child.IsSelected) SelectedNodeBeforeRefresh = child;
                    yield return child;
                }
            }
        }

    }
}
