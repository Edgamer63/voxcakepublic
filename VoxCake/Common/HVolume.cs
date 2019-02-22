using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxCake;

[System.Serializable]
public class HVolume
{
    public int Width;
    public int Height;
    public int Depth;
    public byte[,,] Voxel;

    public HVolume(Volume volume)
    {
        Width = volume.Width;
        Height = volume.Height;
        Depth = volume.Depth;
        Voxel = volume.Voxel;
    }
}
