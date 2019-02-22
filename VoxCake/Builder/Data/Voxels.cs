using UnityEngine;

namespace VoxCake.Builder.Data
{
    public struct Voxels
    {
        public Vector3Int V0;
        public Vector3Int V1;

        public Voxel[] Voxel;
        public readonly byte Mode;

        public Voxels(Vector3Int v0, Vector3Int v1, byte mode)
        {
            V0 = v0;
            V1 = v1;
            Voxel = new Voxel[1];
            Mode = mode;
        }
    }
}
