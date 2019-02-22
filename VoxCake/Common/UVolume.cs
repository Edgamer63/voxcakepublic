using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;
using Random = System.Random;
using UnityEngine;
using VoxCake;

public static class UVolume
{
    public static void LoadVolumeFromVXW(string name, uint innerColor, Volume volume)
    {
        byte[] bytes = File.ReadAllBytes(GetMapPath(name));

        volume.Voxel = new byte[volume.Width, volume.Height, volume.Depth];
        uint[,,] data = new uint[volume.Width, volume.Height, volume.Depth];
        var random = new Random();
        int pos = 0;

        for (int x = 0; x < volume.Width; ++x)
        {
            for (int z = 0; z < volume.Depth; ++z)
            {
                int y = 0;
                uint color;

                for (; y < volume.Height; ++y)
                {
                    uint col = innerColor;
                    col ^= 0x010101 & (uint)random.Next();
                    data[x, y, z] = col;
                }

                y = 0;
                while (true)
                {
                    int number4ByteChunks = bytes[pos];
                    int topColorStart = bytes[pos + 1];
                    int topColorEnd = bytes[pos + 2];

                    for (; y < topColorStart; ++y)
                    {
                        data[x, y, z] = 0x00000000;
                    }

                    int colorPos = pos + 4;
                    for (; y <= topColorEnd; y++)
                    {
                        color = BitConverter.ToUInt32(bytes, colorPos);
                        colorPos += 4;
                        data[x, y, z] = color;
                    }

                    if (topColorEnd == volume.Height - 2)
                    {
                        data[x, y, volume.Height - 1] = data[x, y, volume.Height - 2];
                    }

                    int lenBottom = topColorEnd - topColorStart + 1;

                    if (number4ByteChunks == 0)
                    {
                        pos += 4 * (lenBottom + 1);
                        break;
                    }

                    int lenTop = number4ByteChunks - 1 - lenBottom;

                    pos += bytes[pos] * 4;

                    int bottomColorEnd = bytes[pos + 3];
                    int bottomColorStart = bottomColorEnd - lenTop;

                    for (y = bottomColorStart; y < bottomColorEnd; y++)
                    {
                        color = BitConverter.ToUInt32(bytes, colorPos);
                        colorPos += 4;
                        data[x, y, z] = color;
                    }

                    if (bottomColorEnd == volume.Height - 1)
                    {
                        data[x, y, volume.Height - 1] = data[x, y, volume.Height - 2];
                    }
                }
            }
        }

        for (int x = 0; x < volume.Width; x++)
        {
            for (int y = 0; y < volume.Height; y++)
            {
                for (int z = 0; z < volume.Depth; z++)
                {
                    uint i = data[x, y, z];
                    if (i != 0x00000000)
                    {
                        volume.Voxel[x, volume.Height - y - 1, z] = UColor.UintToByte(i);
                    }
                }
            }
        }
    }

    public static void LoadVCMOD(string name, Volume volume)
    {
        FileStream fs = new FileStream(GetModelPath(name), FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();

        HVolume data = (HVolume)bf.Deserialize(fs);
        volume.Width = data.Width;
        volume.Height = data.Height;
        volume.Depth = data.Depth;
        volume.Voxel = data.Voxel;

        fs.Close();
    }

    public static void SaveVolumeAsVXW(string name, Volume volume)
    {
        List<byte> bytes = new List<byte>();
        bool[,,] map = new bool[volume.Width, volume.Depth, volume.Height];
        ushort[,,] color = new ushort[volume.Width, volume.Depth, volume.Height];
        for (int x = 0; x < volume.Width; x++)
        {
            for (int y = 0; y < volume.Height; y++)
            {
                for (int z = 0; z < volume.Depth; z++)
                {
                    if (volume.Voxel[z, volume.Height - y - 1, x] == 0x00000000) map[x, z, y] = false;
                    else map[x, z, y] = true;
                    color[x, z, y] = volume.Voxel[z, volume.Height - y - 1, x];
                }
            }
        }

        int i, j, k;
        for (j = 0; j < volume.Width; ++j)
        {
            for (i = 0; i < volume.Depth; ++i)
            {
                k = 0;
                while (k < volume.Height)
                {
                    int z;

                    int airStart = k;
                    while (k < volume.Height && !map[i, j, k]) ++k;

                    int topColorsStart = k;
                    while (k < volume.Height && IsSurface(i, j, k, map, volume)) ++k;
                    int topColorsEnd = k;

                    while (k < volume.Height && map[i, j, k] && !IsSurface(i, j, k, map, volume)) ++k;

                    int bottomColorsStart = k;

                    z = k;
                    while (z < volume.Height && IsSurface(i, j, z, map, volume)) ++z;

                    if (z == volume.Height || false) ;
                    else
                        while (IsSurface(i, j, k, map, volume))
                            ++k;

                    int bottomColorsEnd = k;

                    int topColorsLen = topColorsEnd - topColorsStart;
                    int bottomColorsLen = bottomColorsEnd - bottomColorsStart;

                    int colors = topColorsLen + bottomColorsLen;

                    if (k == volume.Height) WriteByte(0, bytes);
                    else WriteByte((byte)(colors + 1), bytes);

                    WriteByte((byte)topColorsStart, bytes);
                    WriteByte((byte)(topColorsEnd - 1), bytes);
                    WriteByte((byte)airStart, bytes);

                    for (z = 0; z < topColorsLen; ++z) WriteVoxel(color[i, j, topColorsStart + z], bytes);
                    for (z = 0; z < bottomColorsLen; ++z) WriteVoxel(color[i, j, bottomColorsStart + z], bytes);
                }
            }
        }

        File.WriteAllBytes(GetMapPath(name), bytes.ToArray());
    }

    public static void SaveVCMOD(string name, Volume volume)
    {
        FileStream fs = new FileStream(GetModelPath(name), FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();

        bf.Serialize(fs, new HVolume(volume));
        fs.Close();
    }

    public static string GetMapPath(string filename)
    {
        return Path.Combine(new[] { Application.streamingAssetsPath, "Maps", filename + ".vxw" });
    }

    public static string GetModelPath(string filename)
    {
        return Path.Combine(new[] { Application.streamingAssetsPath, "Models", filename + ".vcmod" });
    }

    private static void WriteByte(byte value, List<byte> bytes)
    {
        bytes.Add(value);
    }

    private static bool IsSurface(int x, int y, int z, bool[,,] map, Volume volume)
    {
        if (!map[x, y, z]) return false;
        if (x > 0 && !map[x - 1, y, z]) return true;
        if (x + 1 < volume.Width && !map[x + 1, y, z]) return true;
        if (y > 0 && !map[x, y - 1, z]) return true;
        if (y + 1 < volume.Depth && !map[x, y + 1, z]) return true;
        if (z > 0 && !map[x, y, z - 1]) return true;
        if (z + 1 < volume.Height && !map[x, y, z + 1]) return true;
        return false;
    }

    private static void WriteVoxel(uint color, List<byte> bytes)
    {
        WriteByte((byte)(color >> 0), bytes);
        WriteByte((byte)(color >> 8), bytes);
    }
}
