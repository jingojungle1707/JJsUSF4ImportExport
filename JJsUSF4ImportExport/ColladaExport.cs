using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using IONET;
using IONET.Core;
using JJsUSF4Library.FileClasses;
using JJsUSF4Library.FileClasses.SubfileClasses;

namespace JJsUSF4ImportExport

{
    class ColladaExport
    {
        /// <summary>
        /// <para>Creates an IONET scene from an emo.</para>
        /// <br>Includes all EMGs by default, or only those indicated by EMG_index_list if the list is not null.</br>
        /// <br>Will attempt to export .dds textures if an .emb texture pack is provided.</br>
        /// </summary>
        public static IOScene CreateIOScene(EMO emo, List<int> EMG_index_list = default, EMB texturePack = null)
        {
            IOScene ioScene = new IOScene();

            //Main IOModel to contain all our meshes
            ioScene.Models.Add(new IONET.Core.Model.IOModel()
            {
                Name = emo.Name,
            });

            IONET.Core.Model.IOModel ioModel = ioScene.Models.Last();

            //Generate ioSkeleton
            ioModel.Skeleton = EMOSkeletonToIOSkeleton(emo.Skeleton);

            //Compile a list of requested EMGs, or use the full list if request list is null
            List<EMG> local_EMGs = new List<EMG>();
            if (EMG_index_list != null)
            {
                foreach (int i in EMG_index_list) local_EMGs.Add(emo.EMGs[i]);
            }
            else local_EMGs = emo.EMGs;

            for (int i = 0; i < local_EMGs.Count; i++)
            {
                AppendEMGtoIOModel(local_EMGs[i], ioModel);
            }

            //Dictionary to store "in use" materials for later
            Dictionary<string, int> materials = new Dictionary<string, int>();

            foreach (EMG emg in local_EMGs)
            {
                foreach (Model m in emg.Models)
                {
                    for (int i = 0; i < m.SubModels.Count; i++)
                    {
                        SubModel sm = m.SubModels[i];
                        string materialName = $"{sm.Name}_{i:D2}";
                        if (!materials.TryGetValue(materialName, out _))
                        {
                            materials.Add(materialName, m.Textures[sm.EMGTextureIndex].Layers[0].TextureIndex);
                        }
                    }
                    foreach (SubModel sm in m.SubModels)
                    {
                        if (!materials.TryGetValue(sm.Name, out _))
                        {
                            materials.Add(sm.Name, m.Textures[sm.EMGTextureIndex].Layers[0].TextureIndex);
                        }
                    }
                }
            }

            foreach (string str in materials.Keys)
            {
                //Check there's enough textures in the pack
                if (texturePack != null && texturePack.Files.Count > materials[str])

                ioScene.Materials.Add(new IONET.Core.Model.IOMaterial()
                {
                    DiffuseMap = new IONET.Core.Model.IOTexture()
                    {
                        FilePath = texturePack.Files[materials[str]].Name + ".dds",
                        Name = texturePack.Files[materials[str]].Name,
                        UVChannel = 0
                    },
                    Name = str,
                    Shininess = 0f,
                    Reflectivity = 0f,
                });
            }

            return ioScene;

        }

        #region Helper Methods
        public static void AppendEMGtoIOModel(EMG emg, IONET.Core.Model.IOModel ioModel)
        {
            ioModel.Meshes.Add(new IONET.Core.Model.IOMesh()
            {
                Name = $"{emg.Name}_model",
                ParentBone = ioModel.Skeleton.GetBoneByName(emg.RootBoneName),
            });

            for (int j = 0; j < emg.Models.Count; j++)
            {
                ioModel.Meshes.Last().HasTangents = true;
                ioModel.Meshes.Last().Vertices.AddRange(USF4VerticesToIOVertices(emg.Models[j].VertexData));

                for (int k = 0; k < emg.Models[j].SubModels.Count; k++)
                {                    
                    List<int[]> indices = FaceIndicesFromDaisyChain(emg.Models[j].SubModels[k].DaisyChain, emg.Models[j].ReadMode != 1);

                    foreach (int[] face in indices)
                    {
                        List<int> face_list = face.ToList();
                        face_list.Reverse();
                        //Add to the polygon list
                        ioModel.Meshes.Last().Polygons.Add(new IONET.Core.Model.IOPolygon()
                        {
                            Indicies = face_list,
                            MaterialName = $"{emg.Models[j].SubModels[k].Name}_{k:D2}"
                        });
                    }
                }
            }
        }

        public static List<IONET.Core.Model.IOVertex> USF4VerticesToIOVertices(List<Vertex> vertices)
        {
            List<IONET.Core.Model.IOVertex> iOVertices = new List<IONET.Core.Model.IOVertex>();
            foreach (Vertex v in vertices)
            {
                iOVertices.Add(new IONET.Core.Model.IOVertex()
                {
                    Position = v.Position,
                    Normal = v.Normal,
                    Tangent = v.Tangent,
                });
                iOVertices.Last().SetUV(v.UV.X, v.UV.Y);
                byte[] color = BitConverter.GetBytes(v.Color.ToArgb());
                iOVertices.Last().SetColor(color[0] / 255f, color[1] / 255f, color[2] / 255f, color[3] / 255f);
                iOVertices.Last().Envelope.Weights = BoneIDWeightPairsToIOEnvelope(v.BoneIDWeightPairs);
            }

            return iOVertices;
        }
        

        public static IONET.Core.Skeleton.IOSkeleton EMOSkeletonToIOSkeleton(Skeleton emoSkeleton)
        {
            IONET.Core.Skeleton.IOSkeleton iOSkeleton = new IONET.Core.Skeleton.IOSkeleton();

            if (emoSkeleton.Type != Skeleton.SkeletonType.EMO) return iOSkeleton;

            iOSkeleton.RootBones.Add(new IONET.Core.Skeleton.IOBone()
            {
                Name = emoSkeleton.Nodes[0].Name,
                LocalTransform = emoSkeleton.Nodes[0].LocalMatrix,
            });
            List < Node> descendants = new List<Node>() { emoSkeleton.Nodes[0] };

            while (descendants.Count > 0)
            {

                descendants.AddRange(GetDescendants(descendants[0], emoSkeleton));
                IONET.Core.Skeleton.IOBone ioBone = iOSkeleton.GetBoneByName(descendants[0].Parent);
                ioBone.AddChild(new IONET.Core.Skeleton.IOBone()
                {
                    Name = descendants[0].Name,
                    LocalTransform = descendants[0].LocalMatrix,
                });
                descendants.RemoveAt(0);
            }

            return iOSkeleton;
        }

        private static List<Node> GetDescendants(Node n, Skeleton emoSkeleton)
        {
            List<Node> descendants = new List<Node>();

            if (n.Child1 != string.Empty) descendants.Add(emoSkeleton.Nodes[emoSkeleton.NodeNames.IndexOf(n.Child1)]);
            if (n.Sibling != string.Empty) descendants.Add(emoSkeleton.Nodes[emoSkeleton.NodeNames.IndexOf(n.Sibling)]);
            if (n.Child3 != string.Empty) descendants.Add(emoSkeleton.Nodes[emoSkeleton.NodeNames.IndexOf(n.Child3)]);
            if (n.Child4 != string.Empty) descendants.Add(emoSkeleton.Nodes[emoSkeleton.NodeNames.IndexOf(n.Child4)]);

            return descendants;
        }

        public static List<IOBoneWeight> BoneIDWeightPairsToIOEnvelope(List<Vertex.BoneIDWeightPair> boneIDWeightPairs)
        {
            List<IOBoneWeight> iOBoneWeights = new List<IOBoneWeight>();
            List<string> used_bones = new List<string>();
            foreach (Vertex.BoneIDWeightPair biwp in boneIDWeightPairs)
            {
                //Check for duplicated bones
                if (used_bones.Contains(biwp.BoneName)) continue;

                iOBoneWeights.Add(new IOBoneWeight()
                {
                    BoneName = biwp.BoneName,
                    Weight = biwp.Weight
                });
                used_bones.Add(biwp.BoneName);
            }

            return iOBoneWeights;
        }

        public static List<int[]> FaceIndicesFromDaisyChain(int[] DaisyChain, bool readmode = false)
        {
            List<int[]> FaceIndices = new List<int[]>();

            if (readmode == true && DaisyChain.Length % 3 == 0)
            {
                for (int i = 0; i < DaisyChain.Length / 3; i++)
                {
                    FaceIndices.Add(new int[] { DaisyChain[3 * i + 2], DaisyChain[3 * i + 1], DaisyChain[3 * i] });
                }
            }
            else
            {
                bool bForwards = true;
                for (int i = 0; i < DaisyChain.Length - 2; i++)
                {
                    if (bForwards) //This seems to be backwards?? But it works.
                    {
                        int[] temp = new int[] { DaisyChain[i + 2], DaisyChain[i + 1], DaisyChain[i] };

                        if (temp[0] != temp[1] && temp[1] != temp[2] && temp[2] != temp[0])
                        {
                            FaceIndices.Add(temp);
                        }
                    }
                    else
                    {
                        int[] temp = new int[] { DaisyChain[i], DaisyChain[i + 1], DaisyChain[i + 2] };

                        if (temp[0] != temp[1] && temp[1] != temp[2] && temp[2] != temp[0])
                        {
                            FaceIndices.Add(temp);
                        }
                    }

                    bForwards = !bForwards;
                }
            }

            return FaceIndices;
        }
        #endregion
    }
}
