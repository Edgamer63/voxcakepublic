using Unity.Collections;
using UnityEngine;
using Unity.Jobs;

public struct ChunkJob : IJob
{
    public int ChunkX, ChunkY, ChunkZ;
    public float Scale;

    public NativeArray<Vector3> vertices;
    public NativeArray<int> triangles;
    public NativeArray<Color32> colors32;
    public NativeArray<Vector2> uv;

    public void Execute()
    {

    }
}