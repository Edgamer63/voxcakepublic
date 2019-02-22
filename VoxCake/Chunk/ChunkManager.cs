using UnityEngine;
using UnityEngine.Rendering;

namespace VoxCake.Chunk
{
    // TODO: Make the better class name.
    public static class ChunkManager
    {
        public const int SizeX = 16;
        public const int SizeY = 128;
        public const int SizeZ = 16;

        public static void InitializeChunksForVolume(Volume volume)
        {
            for (byte x = 0; x < volume.Width / SizeX; x++)
            {
                for (byte z = 0; z < volume.Depth / SizeZ; z++)
                {
                    volume.Chunk[x, z] = new GameObject(new Vector2Int(x, z).ToString());
                    volume.Chunk[x, z].transform.Translate(new Vector3(x * ChunkManager.SizeX - 0.5f, -0.5f, z * ChunkManager.SizeZ - 0.5f));
                    //volume.Chunk[x, z].transform.parent = parent.transform;
                    volume.Chunk[x, z].AddComponent<MeshFilter>();
                    volume.Chunk[x, z].AddComponent<MeshRenderer>();
                    volume.Chunk[x, z].AddComponent<MeshCollider>();
                    volume.Chunk[x, z].GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
                    volume.Chunk[x, z].GetComponent<MeshRenderer>().receiveShadows = false;
                    //volume.Chunk[x, z].GetComponent<MeshRenderer>().material = volume._material;
                    volume.Chunk[x, z].isStatic = true;
                }
            }
        }

        /*
        public static GameObject[] Data =
            new GameObject[MapData.Width / Size * MapData.Width / Size * MapData.Depth / Size];

        //private static Texture _texture;
        private static Material _material = new Material(Shader.Find("Particles/Standard Unlit"));

        //_texture = Resources.Load<Texture>("LightyEdges1Noise");
        //_material.mainTexture = _texture;

        public static void Initialize(GameObject parent)
        {
            for (byte x = 0; x < MapData.Width / Size; x++)
            {
                for (byte y = 0; y < MapData.Height / Size; y++)
                {
                    for (byte z = 0; z < MapData.Depth / Size; z++)
                    {
                        Data[GetIndex(x, y, z)] = new GameObject(new Vector3Int(x, y, z).ToString());
                        Data[GetIndex(x, y, z)].transform.Translate(new Vector3(x * Size - 0.5f, y * Size- 0.5f, z * Size- 0.5f));
                        Data[GetIndex(x, y, z)].transform.parent = parent.transform;
                        Data[GetIndex(x, y, z)].AddComponent<MeshFilter>();
                        Data[GetIndex(x, y, z)].AddComponent<MeshRenderer>();
                        Data[GetIndex(x, y, z)].GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
                        Data[GetIndex(x, y, z)].GetComponent<MeshRenderer>().receiveShadows = false;
                        Data[GetIndex(x, y, z)].GetComponent<MeshRenderer>().material = _material;
                        Data[GetIndex(x, y, z)].AddComponent<MeshCollider>();
                        Data[GetIndex(x, y, z)].isStatic = true;
                    }
                }
            }
        }

        private static int GetIndex(int x, int y, int z)
        {
            return (x * MapData.Height / Size + y) * MapData.Depth / Size + z;
        }

        public static void SetMesh(int x, int y, int z)
        {
            Mesh mesh = ChunkMesh.Get(x, y, z);
            Data[GetIndex(x, y, z)].GetComponent<MeshFilter>().mesh = mesh;
            Data[GetIndex(x, y, z)].GetComponent<MeshCollider>().sharedMesh = mesh;
        }
        
        public static void Update(int x, int y, int z)
        {
            int ux = Mathf.FloorToInt(x / Size);
            int uy = Mathf.FloorToInt(y / Size);
            int uz = Mathf.FloorToInt(z / Size);
    
            SetMesh(ux, uy, uz);
    
            if (x - Size * ux == Size - 1 && ux != MapData.Width / Size - 1) SetMesh(ux + 1, uy, uz);
    
            if (x - Size * ux == 0 && ux != 0) SetMesh(ux - 1, uy, uz);
    
            if (y - Size * uy == Size - 1 && uy != MapData.Height / Size - 1) SetMesh(ux, uy + 1, uz);
    
            if (y - Size * uy == 0 && uy != 0) SetMesh(ux, uy - 1, uz);
    
            if (z - Size * uz == Size - 1 && uz != MapData.Depth / Size - 1) SetMesh(ux, uy, uz + 1);
    
            if (z - Size * uz == 0 && uz != 0) SetMesh(ux, uy, uz - 1);
        }*/
    }
}