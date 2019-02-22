using VoxCake;
using UnityEngine;

namespace VoxCake.Chunk
{
    public static class ChunkMesh2
    {
        private static int _cx, _cz;
        private const float Scale = 1f; // Scale of the mesh


        public static Mesh Get(int chunkX, int chunkZ, Volume volume)
        {
            int faceCount = 0;

            _cx *= 16;
            _cz *= 16;

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        if (Data(x, y, z, volume) != 0)
                        {
                            if (Data(x + 1, y, z, volume) == 0) faceCount++;
                            if (Data(x - 1, y, z, volume) == 0) faceCount++;
                            if (Data(x, y + 1, z, volume) == 0) faceCount++;
                            if (Data(x, y - 1, z, volume) == 0) faceCount++;
                            if (Data(x, y, z + 1, volume) == 0) faceCount++;
                            if (Data(x, y, z - 1, volume) == 0) faceCount++;
                        }
                    }
                }
            }

            Vector3[] vertices = new Vector3[faceCount * 4];
            int[] triangles = new int[faceCount * 6];
            Color32[] colors32 = new Color32[faceCount * 4];

            int faceIndex = 0;

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        byte color = Data(x, y, z, volume);

                        if (color != 0)
                        {
                            uint block01 = Data(x - 1, y - 1, z + 1, volume);
                            uint block02 = Data(x, y - 1, z + 1, volume);
                            uint block03 = Data(x + 1, y - 1, z + 1, volume);
                            uint block04 = Data(x - 1, y - 1, z, volume);
                            uint block05 = Data(x, y - 1, z, volume);
                            uint block06 = Data(x + 1, y - 1, z, volume);
                            uint block07 = Data(x - 1, y - 1, z - 1, volume);
                            uint block08 = Data(x, y - 1, z - 1, volume);
                            uint block09 = Data(x + 1, y - 1, z - 1, volume);

                            uint block11 = Data(x - 1, y, z + 1, volume);
                            uint block12 = Data(x, y, z + 1, volume);
                            uint block13 = Data(x + 1, y, z + 1, volume);
                            uint block14 = Data(x - 1, y, z, volume);
                            uint block16 = Data(x + 1, y, z, volume);
                            uint block17 = Data(x - 1, y, z - 1, volume);
                            uint block18 = Data(x, y, z - 1, volume);
                            uint block19 = Data(x + 1, y, z - 1, volume);

                            uint block21 = Data(x - 1, y + 1, z + 1, volume);
                            uint block22 = Data(x, y + 1, z + 1, volume);
                            uint block23 = Data(x + 1, y + 1, z + 1, volume);
                            uint block24 = Data(x - 1, y + 1, z, volume);
                            uint block25 = Data(x, y + 1, z, volume);
                            uint block26 = Data(x + 1, y + 1, z, volume);
                            uint block27 = Data(x - 1, y + 1, z - 1, volume);
                            uint block28 = Data(x, y + 1, z - 1, volume);
                            uint block29 = Data(x + 1, y + 1, z - 1, volume);

                            if (block16 == 0)
                            {
                                AddFace(x, y, z, faceIndex, color, 1,
                                    block01, block02, block03,
                                    block04, block05, block06,
                                    block07, block08, block09,
                                    block11, block12, block13,
                                    block14, block16,
                                    block17, block18, block19,
                                    block21, block22, block23,
                                    block24, block25, block26,
                                    block27, block28, block29,
                                    vertices, triangles, colors32);
                                faceIndex++;
                            }

                            if (block14 == 0)
                            {
                                AddFace(x, y, z, faceIndex, color, 2,
                                    block01, block02, block03,
                                    block04, block05, block06,
                                    block07, block08, block09,
                                    block11, block12, block13,
                                    block14, block16,
                                    block17, block18, block19,
                                    block21, block22, block23,
                                    block24, block25, block26,
                                    block27, block28, block29,
                                    vertices, triangles, colors32);
                                faceIndex += 1;
                            }

                            if (block25 == 0)
                            {
                                AddFace(x, y, z, faceIndex, color, 3,
                                    block01, block02, block03,
                                    block04, block05, block06,
                                    block07, block08, block09,
                                    block11, block12, block13,
                                    block14, block16,
                                    block17, block18, block19,
                                    block21, block22, block23,
                                    block24, block25, block26,
                                    block27, block28, block29,
                                    vertices, triangles, colors32);
                                faceIndex += 1;
                            }

                            if (block05 == 0)
                            {
                                AddFace(x, y, z, faceIndex, color, 4,
                                    block01, block02, block03,
                                    block04, block05, block06,
                                    block07, block08, block09,
                                    block11, block12, block13,
                                    block14, block16,
                                    block17, block18, block19,
                                    block21, block22, block23,
                                    block24, block25, block26,
                                    block27, block28, block29,
                                    vertices, triangles, colors32);
                                faceIndex += 1;
                            }

                            if (block12 == 0)
                            {
                                AddFace(x, y, z, faceIndex, color, 5,
                                    block01, block02, block03,
                                    block04, block05, block06,
                                    block07, block08, block09,
                                    block11, block12, block13,
                                    block14, block16,
                                    block17, block18, block19,
                                    block21, block22, block23,
                                    block24, block25, block26,
                                    block27, block28, block29,
                                    vertices, triangles, colors32);
                                faceIndex += 1;
                            }

                            if (block18 == 0)
                            {
                                AddFace(x, y, z, faceIndex, color, 6,
                                    block01, block02, block03,
                                    block04, block05, block06,
                                    block07, block08, block09,
                                    block11, block12, block13,
                                    block14, block16,
                                    block17, block18, block19,
                                    block21, block22, block23,
                                    block24, block25, block26,
                                    block27, block28, block29,
                                    vertices, triangles, colors32);
                                faceIndex += 1;
                            }
                        }
                    }
                }
            }

            return new Mesh
            {
                vertices = vertices,
                triangles = triangles,
                colors32 = colors32
            };
        }
            

        private static void AddFace(int x, int y, int z, int index, byte inputColor, byte faceId,
        uint block01, uint block02, uint block03,
        uint block04, uint block05, uint block06,
        uint block07, uint block08, uint block09,
        uint block11, uint block12, uint block13,
        uint block14, uint block16,
        uint block17, uint block18, uint block19,
        uint block21, uint block22, uint block23,
        uint block24, uint block25, uint block26,
        uint block27, uint block28, uint block29,
        Vector3[] vertices, int[] triangles, Color32[] colors32)
        {
            float ao1F = 3.5f; //3.5  // 4
            float ao2F = 1.5f; //1.5  //1.2
            float lightX = 0.20f; //0.20f   /0.15f
            float lightPy = 0;
            float lightNy = 0.5f; //0.5f  //0.35f
            float lightZ = 0.25f; //0.25f  //0.2


            bool flipped = false;
            uint empty = 0x00000000;
            Color32 color = UColor.ByteToColor(inputColor);
            index = index * 4;

            float xScale = x / Scale;
            float yScale = y / Scale;
            float zScale = z / Scale;
            float i = 1 / Scale;


            if (faceId == 1)
            {
                color = new Color32((byte)(color.r - color.r * lightX), (byte)(color.g - color.g * lightX),
                    (byte)(color.b - color.b * lightX), 255);
                Color32 ao1 = CalculateAo(color.r, color.g, color.b, ao1F);
                Color32 ao2 = CalculateAo(color.r, color.g, color.b, ao2F);

                if (block19 != empty && block06 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale); //1                                    
                    colors32[index] = ao2;
                    flipped = true;
                }
                else if (block19 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale); //1      
                    colors32[index] = ao1;
                }
                else if (block06 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale); //1      
                    colors32[index] = ao1;
                }
                else if (block09 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale); //1      
                    colors32[index] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale); //1  
                    colors32[index] = color;
                }

                if (block19 != empty && block26 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale); //2                                         
                    colors32[index + 1] = ao2;
                }
                else if (block19 != empty)
                {
                    vertices[index + 1] = new Vector3(xScale + i, yScale, zScale); //2                                     
                    colors32[index + 1] = ao1;
                }
                else if (block26 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale); //2                                         
                    colors32[index + 1] = ao1;
                }
                else if (block29 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale); //2                                         
                    colors32[index + 1] = ao1;
                }
                else
                {
                    vertices[index + 1] = new Vector3(xScale + i, yScale, zScale); //2 
                    colors32[index + 1] = color;
                }

                if (block13 != empty && block26 != empty)
                {
                    vertices[index + 2] =
                        new Vector3(xScale + i, yScale, zScale + i); //3                                     
                    colors32[index + 2] = ao2;
                    flipped = true;
                }
                else if (block13 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale + i); //3 
                    colors32[index + 2] = ao1;
                }
                else if (block26 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale + i); //3 
                    colors32[index + 2] = ao1;
                }
                else if (block23 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale + i); //3 
                    colors32[index + 2] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale + i); //3 
                    colors32[index + 2] = color;
                }

                if (block13 != empty && block06 != empty)
                {
                    vertices[index + 3] =
                        new Vector3(xScale + i, yScale - i, zScale + i); //4                                  
                    colors32[index + 3] = ao2;
                }
                else if (block13 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale + i); //4
                    colors32[index + 3] = ao1;
                }
                else if (block06 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale + i); //4
                    colors32[index + 3] = ao1;
                }
                else if (block03 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale + i); //4
                    colors32[index + 3] = ao1;
                }
                else
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale + i); //4
                    colors32[index + 3] = color;
                }
            }

            if (faceId == 2)
            {
                color = new Color32((byte)(color.r - color.r * lightX), (byte)(color.g - color.g * lightX),
                    (byte)(color.b - color.b * lightX), 255);
                Color32 ao1 = CalculateAo(color.r, color.g, color.b, ao1F);
                Color32 ao2 = CalculateAo(color.r, color.g, color.b, ao2F);

                if (block11 != empty && block04 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale + i); //1                                     
                    colors32[index] = ao2;
                    flipped = true;
                }
                else if (block11 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale + i); //1  
                    colors32[index] = ao1;
                }
                else if (block04 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale + i); //1  
                    colors32[index] = ao1;
                }
                else if (block01 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale + i); //1  
                    colors32[index] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale + i); //1  
                    colors32[index] = color;
                }

                if (block11 != empty && block24 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale, yScale, zScale + i); //2                                            
                    colors32[index + 1] = ao2;
                }
                else if (block11 != empty)
                {
                    vertices[index + 1] = new Vector3(xScale, yScale, zScale + i); //2                                      
                    colors32[index + 1] = ao1;
                }
                else if (block24 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale, yScale, zScale + i); //2                                         
                    colors32[index + 1] = ao1;
                }
                else if (block21 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale, yScale, zScale + i); //2                                       
                    colors32[index + 1] = ao1;
                }
                else
                {
                    vertices[index + 1] = new Vector3(xScale, yScale, zScale + i); //2   
                    colors32[index + 1] = color;
                }

                if (block17 != empty && block24 != empty)
                {
                    vertices[index + 2] =
                        new Vector3(xScale, yScale, zScale); //3                                              
                    colors32[index + 2] = ao2;
                    flipped = true;
                }
                else if (block17 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale); //3
                    colors32[index + 2] = ao1;
                }
                else if (block24 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale); //3
                    colors32[index + 2] = ao1;
                }
                else if (block27 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale); //3
                    colors32[index + 2] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale); //3
                    colors32[index + 2] = color;
                }

                if (block17 != empty && block04 != empty)
                {
                    vertices[index + 3] =
                        new Vector3(xScale, yScale - i, zScale); //4                                       
                    colors32[index + 3] = ao2;
                }
                else if (block17 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale); //4
                    colors32[index + 3] = ao1;
                }
                else if (block04 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale); //4
                    colors32[index + 3] = ao1;
                }
                else if (block07 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale); //4
                    colors32[index + 3] = ao1;
                }
                else
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale); //4
                    colors32[index + 3] = color;
                }
            }

            if (faceId == 3)
            {
                color = new Color32((byte)(color.r - color.r * lightPy), (byte)(color.g - color.g * lightPy),
                    (byte)(color.b - color.b * lightPy), 255);
                Color32 ao1 = CalculateAo(color.r, color.g, color.b, ao1F);
                Color32 ao2 = CalculateAo(color.r, color.g, color.b, ao2F);

                if (block22 != empty && block24 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale, zScale + i); //1                                  
                    colors32[index] = ao2;
                    flipped = true;
                }
                else if (block22 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale, zScale + i); //1 
                    colors32[index] = ao1;
                }
                else if (block24 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale, zScale + i); //1 
                    colors32[index] = ao1;
                }
                else if (block21 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale, zScale + i); //1 
                    colors32[index] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index] = new Vector3(xScale, yScale, zScale + i); //1 
                    colors32[index] = color;
                }

                if (block22 != empty && block26 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                   
                    colors32[index + 1] = ao2;
                }
                else if (block22 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                        
                    colors32[index + 1] = ao1;
                }
                else if (block26 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                        
                    colors32[index + 1] = ao1;
                }
                else if (block23 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                          
                    colors32[index + 1] = ao1;
                }
                else
                {
                    vertices[index + 1] = new Vector3(xScale + i, yScale, zScale + i); //2    
                    colors32[index + 1] = color;
                }

                if (block28 != empty && block26 != empty)
                {
                    vertices[index + 2] =
                        new Vector3(xScale + i, yScale, zScale); //3                                        
                    colors32[index + 2] = ao2;
                    flipped = true;
                }
                else if (block28 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3
                    colors32[index + 2] = ao1;
                }
                else if (block26 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3
                    colors32[index + 2] = ao1;
                }
                else if (block29 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3
                    colors32[index + 2] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3
                    colors32[index + 2] = color;
                }

                if (block28 != empty && block24 != empty)
                {
                    vertices[index + 3] =
                        new Vector3(xScale, yScale, zScale); //4                                            
                    colors32[index + 3] = ao2;
                }
                else if (block28 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale, zScale); //4  
                    colors32[index + 3] = ao1;
                }
                else if (block24 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale, zScale); //4  
                    colors32[index + 3] = ao1;
                }
                else if (block27 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale, zScale); //4  
                    colors32[index + 3] = ao1;
                }
                else
                {
                    vertices[index + 3] = new Vector3(xScale, yScale, zScale); //4  
                    colors32[index + 3] = color;
                }
            }

            if (faceId == 4)
            {
                color = new Color32((byte)(color.r - color.r * lightNy), (byte)(color.g - color.g * lightNy),
                    (byte)(color.b - color.b * lightNy), 255);
                Color32 ao1 = CalculateAo(color.r, color.g, color.b, ao1F);
                Color32 ao2 = CalculateAo(color.r, color.g, color.b, ao2F);

                if (block08 != empty && block04 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1                                      
                    colors32[index] = ao2;
                    flipped = true;
                }
                else if (block08 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1 
                    colors32[index] = ao1;
                }
                else if (block04 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1 
                    colors32[index] = ao1;
                }
                else if (block07 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1 
                    colors32[index] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1 
                    colors32[index] = color;
                }

                if (block08 != empty && block06 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale - i, zScale); //2                                      
                    colors32[index + 1] = ao2;
                }
                else if (block08 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale - i, zScale); //2                                   
                    colors32[index + 1] = ao1;
                }
                else if (block06 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale - i, zScale); //2                                     
                    colors32[index + 1] = ao1;
                }
                else if (block09 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale - i, zScale); //2                                     
                    colors32[index + 1] = ao1;
                }
                else
                {
                    vertices[index + 1] = new Vector3(xScale + i, yScale - i, zScale); //2
                    colors32[index + 1] = color;
                }

                if (block02 != empty && block06 != empty)
                {
                    vertices[index + 2] =
                        new Vector3(xScale + i, yScale - i, zScale + i); //3                                 
                    colors32[index + 2] = ao2;
                    flipped = true;
                }
                else if (block02 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale - i, zScale + i); //3     
                    colors32[index + 2] = ao1;
                }
                else if (block06 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale - i, zScale + i); //3     
                    colors32[index + 2] = ao1;
                }
                else if (block03 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale - i, zScale + i); //3     
                    colors32[index + 2] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale - i, zScale + i); //3     
                    colors32[index + 2] = color;
                }

                if (block02 != empty && block04 != empty)
                {
                    vertices[index + 3] =
                        new Vector3(xScale, yScale - i, zScale + i); // 4                                      
                    colors32[index + 3] = ao2;
                }
                else if (block02 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); // 4  
                    colors32[index + 3] = ao1;
                }
                else if (block04 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); // 4  
                    colors32[index + 3] = ao1;
                }
                else if (block01 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); // 4  
                    colors32[index + 3] = ao1;
                }
                else
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); // 4 
                    colors32[index + 3] = color;
                }
            }

            if (faceId == 5)
            {
                color = new Color32((byte)(color.r - color.r * lightZ), (byte)(color.g - color.g * lightZ),
                    (byte)(color.b - color.b * lightZ), 255);
                Color32 ao1 = CalculateAo(color.r, color.g, color.b, ao1F);
                Color32 ao2 = CalculateAo(color.r, color.g, color.b, ao2F);

                if (block13 != empty && block02 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale + i); //1                                
                    colors32[index] = ao2;
                    flipped = true;
                }
                else if (block13 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale + i); //1 
                    colors32[index] = ao1;
                }
                else if (block02 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale + i); //1 
                    colors32[index] = ao1;
                }
                else if (block03 != empty)
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale + i); //1 
                    colors32[index] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index] = new Vector3(xScale + i, yScale - i, zScale + i); //1 
                    colors32[index] = color;
                }

                if (block13 != empty && block22 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                     
                    colors32[index + 1] = ao2;
                }
                else if (block13 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                     
                    colors32[index + 1] = ao1;
                }
                else if (block22 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                        
                    colors32[index + 1] = ao1;
                }
                else if (block23 != empty)
                {
                    vertices[index + 1] =
                        new Vector3(xScale + i, yScale, zScale + i); //2                                        
                    colors32[index + 1] = ao1;
                }
                else
                {
                    vertices[index + 1] = new Vector3(xScale + i, yScale, zScale + i); //2  
                    colors32[index + 1] = color;
                }

                if (block11 != empty && block22 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale + i); //3                                     
                    colors32[index + 2] = ao2;
                    flipped = true;
                }
                else if (block11 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale + i); //3    
                    colors32[index + 2] = ao1;
                }
                else if (block22 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale + i); //3    
                    colors32[index + 2] = ao1;
                }
                else if (block21 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale + i); //3    
                    colors32[index + 2] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index + 2] = new Vector3(xScale, yScale, zScale + i); //3    
                    colors32[index + 2] = color;
                }

                if (block11 != empty && block02 != empty)
                {
                    vertices[index + 3] =
                        new Vector3(xScale, yScale - i, zScale + i); //4                                     
                    colors32[index + 3] = ao2;
                }
                else if (block11 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); //4    
                    colors32[index + 3] = ao1;
                }
                else if (block02 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); //4    
                    colors32[index + 3] = ao1;
                }
                else if (block01 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); //4    
                    colors32[index + 3] = ao1;
                }
                else
                {
                    vertices[index + 3] = new Vector3(xScale, yScale - i, zScale + i); //4    
                    colors32[index + 3] = color;
                }
            }

            if (faceId == 6)
            {
                color = new Color32((byte)(color.r - color.r * lightZ), (byte)(color.g - color.g * lightZ),
                    (byte)(color.b - color.b * lightZ), 255);
                Color32 ao1 = CalculateAo(color.r, color.g, color.b, ao1F);
                Color32 ao2 = CalculateAo(color.r, color.g, color.b, ao2F);

                if (block17 != empty && block08 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1                                         
                    colors32[index] = ao2;
                    flipped = true;
                }
                else if (block17 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1     
                    colors32[index] = ao1;
                }
                else if (block08 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1     
                    colors32[index] = ao1;
                }
                else if (block07 != empty)
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1     
                    colors32[index] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index] = new Vector3(xScale, yScale - i, zScale); //1 
                    colors32[index] = color;
                }

                if (block17 != empty && block28 != empty)
                {
                    vertices[index + 1] = new Vector3(xScale, yScale, zScale); //2                                       
                    colors32[index + 1] = ao2;
                }
                else if (block17 != empty)
                {
                    vertices[index + 1] = new Vector3(xScale, yScale, zScale); //2                                    
                    colors32[index + 1] = ao1;
                }
                else if (block28 != empty)
                {
                    vertices[index + 1] = new Vector3(xScale, yScale, zScale); //2                                        
                    colors32[index + 1] = ao1;
                }
                else if (block27 != empty)
                {
                    vertices[index + 1] = new Vector3(xScale, yScale, zScale); //2                                        
                    colors32[index + 1] = ao1;
                }
                else
                {
                    vertices[index + 1] = new Vector3(xScale, yScale, zScale); //2  
                    colors32[index + 1] = color;
                }

                if (block19 != empty && block28 != empty)
                {
                    vertices[index + 2] =
                        new Vector3(xScale + i, yScale, zScale); //3                                       
                    colors32[index + 2] = ao2;
                    flipped = true;
                }
                else if (block19 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3  
                    colors32[index + 2] = ao1;
                }
                else if (block28 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3  
                    colors32[index + 2] = ao1;
                }
                else if (block29 != empty)
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3  
                    colors32[index + 2] = ao1;
                    flipped = true;
                }
                else
                {
                    vertices[index + 2] = new Vector3(xScale + i, yScale, zScale); //3  
                    colors32[index + 2] = color;
                }

                if (block19 != empty && block08 != empty)
                {
                    vertices[index + 3] =
                        new Vector3(xScale + i, yScale - i, zScale); //4                                    
                    colors32[index + 3] = ao2;
                }
                else if (block19 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale); //4
                    colors32[index + 3] = ao1;
                }
                else if (block08 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale); //4
                    colors32[index + 3] = ao1;
                }
                else if (block09 != empty)
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale); //4
                    colors32[index + 3] = ao1;
                }
                else
                {
                    vertices[index + 3] = new Vector3(xScale + i, yScale - i, zScale); //4
                    colors32[index + 3] = color;
                }
            }

            int face = index / 4;
            index = index / 4 * 6;
            if (!flipped)
            {
                triangles[index] = face * 4;
                triangles[index + 1] = face * 4 + 1;
                triangles[index + 2] = face * 4 + 2;
                triangles[index + 3] = face * 4;
                triangles[index + 4] = face * 4 + 2;
                triangles[index + 5] = face * 4 + 3;
            }
            else
            {
                triangles[index] = face * 4 + 3;
                triangles[index + 1] = face * 4;
                triangles[index + 2] = face * 4 + 1;
                triangles[index + 3] = face * 4 + 3;
                triangles[index + 4] = face * 4 + 1;
                triangles[index + 5] = face * 4 + 2;
            }

            flipped = false;
        }

        private static Color32 CalculateAo(byte r, byte g, byte b, float ao)
        {
            float fr = r - r / ao;
            float fg = g - g / ao;
            float fb = b - b / ao;

            if (fr > 255) fr = 255;
            if (fr < 0) fr = 0;
            if (fg > 255) fg = 255;
            if (fg < 0) fg = 0;
            if (fb > 255) fb = 255;
            if (fb < 0) fb = 0;

            return new Color32((byte)fr, (byte)fg, (byte)fb, 255);
        }

        private static byte Data(int x, int y, int z, Volume volume)
        {
            return volume.Voxel[x + _cx, y, z + _cz];
        }
    }
}