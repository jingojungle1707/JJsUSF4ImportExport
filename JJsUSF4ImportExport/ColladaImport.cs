using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IONET.Core;
using JJsUSF4Library.FileClasses;
using JJsUSF4Library.FileClasses.SubfileClasses;
using System.IO;
using JJsUSF4Library;

namespace JJsUSF4ImportExport
{
    class ColladaImport
    {
        public static List<Vertex> BuildVertexListFromIOMesh(IONET.Core.Model.IOMesh ioMesh)
        {
            //Build initial USF4 vertex list (boneIDs target the skeleton directly, no bone mapping yet)
            List<Vertex> usf4Vertices = new List<Vertex>();

            //Generate vertex tangents if they're missing
            if (!ioMesh.HasTangents) ioMesh.GenerateTangentsAndBitangents();
            ioMesh.HasBitangents = true;
            ioMesh.HasTangents = true;

            foreach (IONET.Core.Model.IOVertex ioV in ioMesh.Vertices)
            {
                usf4Vertices.Add(new Vertex()
                {
                    Position = ioV.Position,
                    Normal = ioV.Normal,
                    Tangent = ioV.Tangent,
                    UV = ioV.UVs[0],
                    BoneIDWeightPairs = new List<Vertex.BoneIDWeightPair>(),
                    //Dummy color data for now
                    Color = Color.FromArgb(0xFF, 0xFE, 0xFF, 0xFF),
                });

                foreach (IOBoneWeight ioB in ioV.Envelope.Weights)
                {
                    //Skip any potential junk/duplicate bone entries
                    if (usf4Vertices.Last().BoneNames.Contains(ioB.BoneName)) continue;
                    //Add the entry
                    usf4Vertices.Last().BoneIDWeightPairs.Add(new Vertex.BoneIDWeightPair(ioB.BoneName, ioB.Weight));
                }
            }

            return usf4Vertices;
        }

        public static EMG GenerateEMGfromIOMesh(IONET.Core.Model.IOMesh ioMesh, EMO emo, int textureIndex = -1, int normalMapIndex = -1)
        {
            //Make sure everything is triangles
            ioMesh.MakeTriangles();

            //Copy the emo
            EMO copy_emo = new EMO();
            using (MemoryStream ms = new MemoryStream(emo.GenerateBytes()))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    copy_emo.ReadFromStream(br);
                }
            }
            EMG template_emg = copy_emo.EMGs[0];
            Model template_model = template_emg.Models[0];
            SubModel template_subModel = template_model.SubModels[0];

            EMG emg = new EMG()
            {
                Name = "test",

                //TODO DONT THINK THIS ROOTBONE THING IS RIGHT

                RootBoneName = emo.Skeleton.Nodes.Last().Name,
                RootBoneID = emo.Skeleton.Nodes.Count - 1,
                Models = new List<Model>(),
            };

            Model model = new Model()
            {
                BitDepth = template_model.BitDepth,
                BitFlag = template_model.BitFlag,
                CullData = template_model.CullData,
                ReadMode = template_model.ReadMode,
                SubModels = new List<SubModel>(),
                Textures = new List<EMGModelTexture>(),
                VertexData = BuildVertexListFromIOMesh(ioMesh)
            };

            foreach (IONET.Core.Model.IOPolygon ioP in ioMesh.Polygons)
            {
                //Make a texture
                model.Textures.Add(new EMGModelTexture(textureIndex, normalMapIndex));

                //Convert triangle list to daisychain-able indices list
                List<int[]> indices = new List<int[]>();
                for (int i = 0; i < ioP.Indicies.Count / 3; i++)
                {
                    indices.Add(new int[] { ioP.Indicies[3 * i], ioP.Indicies[3 * i + 1], ioP.Indicies[3 * i + 2], });
                }

                SubModel sm = new SubModel()
                {
                    Name = ioP.MaterialName,
                    MysteryFloats = template_subModel.MysteryFloats,
                    EMGTextureIndex = model.SubModels.Count,
                    DaisyChain = DaisyChainFromIndices(indices).ToArray(),
                    BoneIntegersList = new List<int>(),
                };

                model.SubModels.Add(sm);
            }

            emg.Models.Add(model);

            return emg;
        }

        public static void AppendIOMeshToEMO(IONET.Core.Model.IOMesh ioMesh, EMO emo, int textureIndex = -1, int normalMapIndex = -1)
        {
            emo.EMGs.Add(GenerateEMGfromIOMesh(ioMesh, emo, textureIndex, normalMapIndex));
        }

        public static EMO CreateEMO(IONET.Core.IOScene ioScene)
        {
            EMO emo = new EMO() 
            {
                Name = ioScene.Name,
                EMGs = new List<EMG>(),
            };

            Skeleton s = new Skeleton()
            {
                FFList = new List<byte[]>(),
                Nodes = new List<Node>(),
                IKDataBlocks = new List<IKDataBlock>(),
                IKNodes = new List<IKNode>(),
            };

            IONET.Core.Skeleton.IOSkeleton ioSkel = ioScene.Models[0].Skeleton;

            s.Nodes.Add(new Node()
            {
                Name = ioSkel.RootBones[0].Name,
            });

            return emo;
        }

        public static List<int> DaisyChainFromIndices(List<int[]> nIndices)
        {
            List<int> Chain = new List<int>();
            bool bForwards = false;

            int compression = nIndices.Count * 3;
            int compression_zero = compression;

            //Initialise start of chain
            int buffer1 = nIndices[0][2];
            int buffer2 = nIndices[0][1];
            Chain.AddRange(new List<int> { nIndices[0][0], nIndices[0][1], nIndices[0][2] });
            nIndices.RemoveAt(0);

            int[] workingArray;

            while (nIndices.Count > 0)
            {
                for (int i = 0; i < nIndices.Count; i++)
                {
                    workingArray = nIndices[i];

                    for (int j = 0; j < 3; j++)
                    {
                        int x1 = (j > 0) ? -3 : 0;
                        int x2 = (j == 2) ? -3 : 0;
                        if (bForwards == true && workingArray[1 + j + x2] == buffer1 && workingArray[0 + j] == buffer2)
                        {
                            compression -= 2;
                            buffer2 = buffer1;
                            buffer1 = workingArray[2 + j + x1];
                            Chain.Add(buffer1);
                            nIndices.RemoveAt(i);
                            i = -1;
                            bForwards = !bForwards;
                            //TimeEstimate(TStrings.STR_ReorderingFaces, count, Chain.Count);
                            break;
                        }
                        if (bForwards == false && workingArray[1 + j + x2] == buffer1 && workingArray[2 + j + x1] == buffer2)
                        {
                            compression -= 2;
                            buffer2 = buffer1;
                            buffer1 = workingArray[0 + j];
                            Chain.Add(buffer1);
                            nIndices.RemoveAt(i);
                            i = -1;
                            bForwards = !bForwards;
                            //TimeEstimate(TStrings.STR_ReorderingFaces, count, Chain.Count);
                            break;
                        }

                    }
                }
                //No match found - if we've run out of faces, great, if not, re-initialise
                if (nIndices.Count > 0)
                {
                    compression += 2;
                    Random random = new Random();

                    int rnd = random.Next(0, nIndices.Count);

                    workingArray = nIndices[rnd];

                    Chain.Add(buffer1);
                    if (bForwards)
                    {
                        //Create chain break
                        Chain.Add(workingArray[0]);
                        Chain.Add(workingArray[0]);
                        Chain.Add(workingArray[1]);
                        Chain.Add(workingArray[2]);
                        //Re-initialise buffer
                        buffer1 = workingArray[2];
                        buffer2 = workingArray[1];
                    }
                    if (!bForwards)
                    {
                        //Create chain break
                        Chain.Add(workingArray[2]);
                        Chain.Add(workingArray[2]);
                        Chain.Add(workingArray[1]);
                        Chain.Add(workingArray[0]);
                        //Re-initialise buffer
                        buffer1 = workingArray[0];
                        buffer2 = workingArray[1];
                    }
                    //Clear the used face and flip the flag
                    nIndices.RemoveAt(rnd);
                    bForwards = !bForwards;
                    //progressBar1.Value += 1;
                    //TimeEstimate(TStrings.STR_ReorderingFaces, count, Chain.Count);
                }
            }

            Console.WriteLine($"Compression {compression}/{compression_zero} = {100 * compression / compression_zero}%");

            return Chain;
        }
    }
}
