using UnityEngine;

namespace VoxCake.Builder.Data
{
    public struct Voxel
    {
        public Vector3Int Position;
        public byte OldColor;
        public byte NewColor;
        public readonly byte Mode;

        public Voxel(Vector3Int position, byte mode)
        {
            Position = position;
            OldColor = 0;
            NewColor = 0;
            Mode = mode;
        }
        public Voxel(Vector3Int position, byte color, byte mode)
        {
            Position = position;
            OldColor = color;
            NewColor = 0;
            Mode = mode;
        }
    }
}
