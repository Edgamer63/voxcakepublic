using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxCake;


namespace VoxCake.Chunk
{
    public static class ChunkUpdater
    {
        public static void UpdateChunk(int x, int z, Volume volume)
        {
            Mesh mesh = ChunkMesh.Get(x, z, volume);
            volume.Chunk[x, z].GetComponent<MeshFilter>().mesh = mesh;
            volume.Chunk[x, z].GetComponent<MeshCollider>().sharedMesh = mesh;
        }


        public static void UpdateVoxel(int x, int y, int z, Volume volume)
        {
            int ux = Mathf.FloorToInt(x / ChunkSize.X);
            int uz = Mathf.FloorToInt(z / ChunkSize.Z);

            UpdateChunk(ux, uz, volume);

            if (x - ChunkSize.X * ux == ChunkSize.X - 1 && ux != volume.Width / ChunkSize.X - 1) UpdateChunk(ux + 1, uz, volume);

            if (x - ChunkSize.X * ux == 0 && ux != 0) UpdateChunk(ux - 1, uz, volume);

            if (z - ChunkSize.Z * uz == ChunkSize.Z - 1 && uz != volume.Depth / ChunkSize.Z - 1) UpdateChunk(ux, uz + 1, volume);

            if (z - ChunkSize.Z * uz == 0 && uz != 0) UpdateChunk(ux, uz - 1, volume);

            /*
            if (x - Chunk.Width * ux == Chunk.Width - 1 && ux != Map.Data.Width / Chunk.Width - 1) SetMesh(ux + 1, uz);

            if (x - Chunk.Width * ux == 0 && ux != 0) SetMesh(ux - 1, uz);

            if (z - Chunk.Depth * uz == Chunk.Depth - 1 && uz != MapData.Depth / Chunk.Depth - 1) SetMesh(ux, uz + 1);

            if (z - Chunk.Depth * uz == 0 && uz != 0) SetMesh(ux, uz - 1);
              
            */
        }
    }
}
 