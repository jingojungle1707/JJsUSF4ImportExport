using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IONET.Core;
using System.Collections;

namespace JJsUSF4ImportExport
{
    public static class IONETreeViewExtensions
    {
        public static TreeNode GenerateTreeNode(this IOScene ioScene)
        {
            TreeNode n = new TreeNode()
            {
                Text = ioScene.Name,
                Tag = ioScene
            };

            foreach(IONET.Core.Model.IOModel ioModel in ioScene.Models)
            {
                n.Nodes.Add(GenerateioModelTreeNode(ioModel));
            }

            return n;
        }

        public static TreeNode GenerateioModelTreeNode(this IONET.Core.Model.IOModel ioModel, string name = default)
        {
            TreeNode n = new TreeNode()
            {
                Text = (name == default) ? ioModel.Name : name,
                Tag = ioModel
            };

            foreach (IONET.Core.Model.IOMesh ioMesh in ioModel.Meshes)
            {
                n.Nodes.Add(GenerateioMeshTreeNode(ioMesh));
            }

            return n;
        }

        private static TreeNode GenerateioMeshTreeNode(IONET.Core.Model.IOMesh iOMesh)
        {
            TreeNode n = new TreeNode()
            {
                Text = iOMesh.Name,
                Tag = iOMesh
            };

            List<string> materials = iOMesh.Polygons.Select(o => o.MaterialName).Distinct().ToList();
            foreach (string material in materials)
            {
                n.Nodes.Add(new TreeNode(material));
            }

            return n;
        }
    }
}
