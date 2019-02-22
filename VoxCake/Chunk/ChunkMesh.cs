using System.Collections.Generic;
using UnityEngine;

namespace VoxCake.Chunk
{
    public static class ChunkMesh
    {
        private static int _cx, _cz;
        private const float Scale = 1f; // Scale of the mesh

        public static Mesh Get(int chunkX, int chunkZ, Volume volume)
        {
            _cx = chunkX*16;
            _cz = chunkZ*16;

            var vertices = new List<Vector3>();
            var colors32 = new List<Color32>();
            var triangles = new List<int>();
            var uv = new List<Vector2>();

            var dimensions = new [] {ChunkSize.X, ChunkSize.Y, ChunkSize.Z}; // Size of each axis

            for (var d = 0; d < 3; d++) // Iterate for x, y, z axis: 0 = x; 1 = y; 2 = z;
            {
                var u = (d + 1) % 3;
                var v = (d + 2) % 3;
                var x = new int[3];
                var q = new int[3]; q[d] = 1;
                var mask = new int[dimensions[u] * dimensions[v]];
                var colorMask = new byte[dimensions[u] * dimensions[v]];
                
                for (x[d] = -1; x[d] < dimensions[d];) // Do something 16 times
                {
                    var n = 0;
                    for (x[v] = 0; x[v] < dimensions[v]; ++x[v]) // x
                    {
                        for (x[u] = 0; x[u] < dimensions[u]; ++x[u], ++n) // y
                        {
                            var current = (x[d] >= 0 ? Data(x[0], x[1], x[2], volume) : 0);
                            var next = (x[d] < dimensions[d] - 1 ? Data(x[0] + q[0], x[1] + q[1], x[2] + q[2], volume) : 0);
                            if (current != next)
                            {
                                if (current > 0 && next > 0)
                                {
                                    mask[n] = 0;
                                    colorMask[n] = 0;
                                }
                                else if (current == 0)
                                {
                                    mask[n] = -1;
                                    colorMask[n] = (byte) next;
                                }
                                else if (next == 0)
                                {
                                    mask[n] = 1;
                                    colorMask[n] = (byte) current;
                                }
                                else
                                {
                                    mask[n] = 0;
                                    colorMask[n] = 0;
                                }
                            }
                            else
                            {
                                mask[n] = 0;
                                colorMask[n] = 0;
                            }
                        }
                    } // End.

                    x[d]++;
                    n = 0;
                    for (var j = 0; j < dimensions[v]; ++j)
                    {
                        for (var i = 0; i < dimensions[u];)
                        {
                            var maskValue = mask[n];
                            var colorValue = colorMask[n];
                            if (maskValue != 0)
                            {
                                int l, k;

                                var w = 1;
                                for (;
                                    n + w < mask.Length && mask[n + w] == maskValue && colorValue == colorMask[n + w] &&
                                    i + w < dimensions[u];
                                    ++w)
                                {
                                }

                                var h = 1;
                                var done = false;
                                for (; j + h < dimensions[v]; ++h)
                                {
                                    for (k = 0; k < w; ++k)
                                    {
                                        if (mask[n + k + h * dimensions[u]] != maskValue ||
                                            colorMask[n + k + h * dimensions[u]] != colorValue)
                                        {
                                            done = true;
                                            break;
                                        }
                                    }

                                    if (done) break;
                                }

                                var xp = new Vector3();
                                xp[u] = i;
                                xp[v] = j;
                                xp[d] = x[d];
                                xp *= Scale;
                                var du = new Vector3();
                                du[u] = w * Scale;
                                var dv = new Vector3();
                                dv[v] = h * Scale;
                                AddFace(
                                    new Vector3(xp[0],                 xp[1],                 xp[2]),
                                    new Vector3(xp[0] + du[0],         xp[1] + du[1],         xp[2] + du[2]),
                                    new Vector3(xp[0] + du[0] + dv[0], xp[1] + du[1] + dv[1], xp[2] + du[2] + dv[2]),
                                    new Vector3(xp[0] +         dv[0], xp[1] +         dv[1], xp[2] +         dv[2]),
                                    UColor.ByteToColor(colorValue), d, maskValue < 0,
                                    vertices, colors32, triangles, uv);
                                for (l = 0; l < h; ++l)
                                {
                                    for (k = 0; k < w; ++k)
                                    {
                                        mask[(n + k) + l * dimensions[u]] = 0;
                                        colorMask[(n + k) + l * dimensions[u]] = 0;
                                    }
                                }

                                i += w;
                                n += w;
                            }
                            else
                            {
                                ++i;
                                ++n;
                            }
                        }
                    }
                }
            }

            return new Mesh
            {
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
                colors32 = colors32.ToArray()
            };
        }

        private static void AddFace(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color32 color, int side, bool flip,
            List<Vector3> vertices, List<Color32> colors32, List<int> triangles, List<Vector2> uv)
        {
            var start = vertices.Count;
            color = CalculateFaceShading(color, side, flip);

            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(c);
            vertices.Add(d);

            //uv.Add(new Vector2());
            
            colors32.Add(color);
            colors32.Add(color);
            colors32.Add(color);
            colors32.Add(color);

            if (flip)
            {
                triangles.Add(start + 2);
                triangles.Add(start + 1);
                triangles.Add(start + 3);
                triangles.Add(start + 1);
                triangles.Add(start);
                triangles.Add(start + 3);
            }
            else
            {
                triangles.Add(start);
                triangles.Add(start + 1);
                triangles.Add(start + 2);
                triangles.Add(start);
                triangles.Add(start + 2);
                triangles.Add(start + 3);
            }
        }

        private static Color32 CalculateFaceShading(Color32 color, int side, bool flip)
        {
            float lightX = 0.2f;
            float lightPy = 0;
            float lightNy = 0.4f;
            float lightZ = 0.25f;
            
            if (side == 0) return new Color32(
                (byte) (color.r - color.r * lightX),
                (byte) (color.g - color.g * lightX),
                (byte) (color.b - color.b * lightX),
                255);
            if (side == 1 && flip == false) return new Color32(
                (byte) (color.r - color.r * lightPy),
                (byte) (color.g - color.g * lightPy),
                (byte) (color.b - color.b * lightPy),
                255);
            if (side == 1 && flip == true) return new Color32(
                (byte) (color.r - color.r * lightNy),
                (byte) (color.g - color.g * lightNy),
                (byte) (color.b - color.b * lightNy),
                255);
            if (side == 2) return new Color32(
                (byte) (color.r - color.r * lightZ),
                (byte) (color.g - color.g * lightZ),
                (byte) (color.b - color.b * lightZ),
                255);
            return new Color32();
        }

        private static byte Data(int x, int y, int z, Volume volume)
        {
            return volume.Voxel[x + _cx, y, z + _cz];
        }
    }
}