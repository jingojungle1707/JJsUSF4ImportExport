using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJsUSF4Library;
using JJsUSF4Library.FileClasses;
using JJsUSF4Library.FileClasses.SubfileClasses;

namespace JJsUSF4ImportExport
{
    /// <summary>
    /// Class to generate TreeView nodes for each filetype from JJsUSF4Library
    /// </summary>
    public static class USF4FileTreeViewExtension
    {
        public static TreeNode GenerateTreeNode(this USF4File uf)
        {
            if (uf.GetType() == typeof(EMA)) return GenerateTreeNode((EMA)uf);
            else if (uf.GetType() == typeof(EMO)) return GenerateTreeNode((EMO)uf);
            else if (uf.GetType() == typeof(EMM)) return GenerateTreeNode((EMM)uf);
            else if (uf.GetType() == typeof(EMB)) return GenerateTreeNode((EMB)uf);
            else if (uf.GetType() == typeof(BSR)) return GenerateTreeNode((BSR)uf);

            else return new TreeNode()
            {
                Text = uf.Name,
                Tag = uf
            };
        }

        private static TreeNode GenerateTreeNode(this BSR f)
        {
            TreeNode n = new TreeNode()
            {
                Text = f.Name,
                Tag = f
            };
            for (int i = 0; i < f.Physics.Count; i++)
            {
                TreeNode pn = new TreeNode()
                {
                    Text = $"Physics Object {i}",
                    Tag = f.Physics[i]
                };

                for (int j = 0; j < f.Physics[i].NodeDataBlocks.Count; j++)
                {
                    pn.Nodes.Add(new TreeNode()
                    {
                        Text = $"{f.NodeNames[f.Physics[i].NodeDataBlocks[j].ID]}",
                        Tag = f.Physics[i].NodeDataBlocks[j]
                    });
                }
                for (int j = 0; j < f.Physics[i].LimitDataBlocks.Count; j++)
                {
                    pn.Nodes.Add(new TreeNode()
                    {
                        Text = $"Limit {f.Physics[i].LimitDataBlocks[j].ID1} > {f.Physics[i].LimitDataBlocks[j].ID2}",
                        Tag = f.Physics[i].LimitDataBlocks[j]
                    });
                }

                n.Nodes.Add(pn);
            }

            return n;
        }

        private static TreeNode GenerateTreeNode(this EMA f)
        {
            TreeNode n = new TreeNode()
            {
                Text = f.Name,
                Tag = f
            };
            for (int i = 0; i < f.Animations.Count; i++)
            {
                n.Nodes.Add(new TreeNode()
                {
                    Text = $"{i} {f.Animations[i].Name}",
                    Tag = f.Animations[i]
                });
            }
            if (f.Skeleton.Nodes != null)
            {
                TreeNode nodeSkeleton = new TreeNode()
                {
                    Text = "Skeleton",
                    Tag = f.Skeleton
                };
                for (int i = 0; i < f.Skeleton.Nodes.Count; i++)
                {
                    nodeSkeleton.Nodes.Add(new TreeNode()
                    {
                        Text = $"{i} {f.Skeleton.Nodes[i].Name}",
                        Tag = f.Skeleton.Nodes[i]
                    });
                }
                if (f.Skeleton.IKNodes != null && f.Skeleton.IKNodes.Count > 0)
                {
                    for (int i = 0; i < f.Skeleton.IKNodes.Count; i++)
                    {
                        nodeSkeleton.Nodes.Add(new TreeNode()
                        {
                            Text = $"{i} {f.Skeleton.IKNodeNames[i]}",
                            Tag = f.Skeleton.IKNodes[i]
                        });
                    }
                }
                n.Nodes.Add(nodeSkeleton);
            }
            else
            {
                n.Nodes.Add(new TreeNode()
                {
                    Text = "No skeleton data",
                    Tag = new Skeleton()
                });
            }
            return n;
        }

        private static TreeNode GenerateTreeNode(this EMB f)
        {
            TreeNode n = new TreeNode()
            {
                Text = f.Name,
                Tag = f
            };

            foreach (USF4File uf in f.Files) n.Nodes.Add(uf.GenerateTreeNode());

            return n;
        }

        public static TreeNode GenerateTreeNode(this EMM f)
        {
            TreeNode n = new TreeNode()
            {
                Text = f.Name,
                Tag = f
            };
            foreach (Material m in f.Materials)
            {
                n.Nodes.Add(new TreeNode()
                {
                    Text = m.Name,
                    Tag = m
                });
            }
            return n;
        }

        private static TreeNode GenerateTreeNode(this EMO f)
        {
            TreeNode n = new TreeNode()
            {
                Text = f.Name,
                Tag = f
            };
            for (int i = 0; i < f.EMGs.Count; i++)
            {
                EMG emg = f.EMGs[i];
                TreeNode EMGn = new TreeNode()
                {
                    Text = $"EMG {i} ({emg.Name})",
                    Tag = emg
                };

                for (int j = 0; j < emg.Models.Count; j++)
                {
                    Model mod = emg.Models[j];
                    TreeNode Modn = new TreeNode()
                    {
                        Text = $"Model {j}",
                        Tag = mod
                    };

                    for (int k = 0; k < mod.SubModels.Count; k++)
                    {
                        SubModel sm = mod.SubModels[k];
                        TreeNode SMn = new TreeNode()
                        {
                            Text = sm.Name,
                            Tag = sm
                        };
                        Modn.Nodes.Add(SMn);
                    }
                    EMGn.Nodes.Add(Modn);
                }
                n.Nodes.Add(EMGn);
            }

            if (f.Skeleton.Nodes != null)
            {
                TreeNode nodeSkeleton = new TreeNode()
                {
                    Text = "Skeleton",
                    Tag = f.Skeleton
                };
                for (int i = 0; i < f.Skeleton.Nodes.Count; i++)
                {
                    nodeSkeleton.Nodes.Add(new TreeNode()
                    {
                        Text = $"{i} {f.Skeleton.NodeNames[i]}",
                        Tag = f.Skeleton.Nodes[i]
                    });
                }

                n.Nodes.Add(nodeSkeleton);
            }
            else
            {
                n.Nodes.Add(new TreeNode()
                {
                    Text = "No skeleton data",
                    Tag = new Skeleton()
                });
            }
            return n;
        }
    }
}
