using System.Collections;
using VoxCake.Chunk;
using UnityEngine;
using UnityEngine.Rendering;


namespace VoxCake
{
    public class Volume
    {
        public int Width;
        public int Height;
        public int Depth;
        public byte[,,] Voxel;
        public GameObject[,] Chunk;
        public static Material _material = new Material(Shader.Find("Particles/Standard Unlit"));

        #region Constuctor
        public Volume(int width, int height, int depth, GameObject parent)
        {
            Width = width;
            Height = height;
            Depth = depth;
            ChunkSize.Y = Height;
            if(Width < 16 && Depth < 16)
            {
                ChunkSize.X = Width;
                ChunkSize.Z = Depth;
            }
            Voxel = new byte[Width, Height, Depth];
            Chunk = new GameObject[Width / ChunkSize.X, Depth / ChunkSize.Z];

            for (byte x = 0; x < Width / ChunkSize.X; x++)
            {
                for (byte z = 0; z < Depth / ChunkSize.Z; z++)
                {
                    Chunk[x, z] = new GameObject(new Vector2Int(x,  z).ToString());
                    Chunk[x, z].transform.Translate(new Vector3(x * ChunkSize.X - 0.5f, -0.5f, z * ChunkSize.Z - 0.5f));
                    Chunk[x, z].transform.parent = parent.transform;
                    Chunk[x, z].AddComponent<MeshFilter>();
                    Chunk[x, z].AddComponent<MeshRenderer>();
                    Chunk[x, z].AddComponent<MeshCollider>();
                    Chunk[x, z].GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
                    Chunk[x, z].GetComponent<MeshRenderer>().receiveShadows = false;
                    Chunk[x, z].GetComponent<MeshRenderer>().material = _material;
                    Chunk[x, z].isStatic = true;
                }
            }
        }
        #endregion
        #region New
        public void New()
        {

        }
        #endregion
        #region Update
        public IEnumerator Update()
        {
            if(Width < 17 && Depth < 17)
            {
                yield return null;
                ChunkUpdater.UpdateChunk(0, 0, this);
            }
            else
            {
                for (int x = 0; x < ChunkSize.X; x++)
                {
                    for (int z = 0; z < ChunkSize.Z; z++)
                    {
                        yield return null;
                        ChunkUpdater.UpdateChunk(x, z, this);
                    }
                }
            } 
        }
        #endregion
        #region Load VXW
        public void LoadVXW(string name, uint innerColor)
        {
            UVolume.LoadVolumeFromVXW(name, innerColor, this);
        }
        #endregion
        #region Load VCMOD
        public void LoadVCMOD(string name)
        {
            UVolume.LoadVCMOD(name, this);
        }
        #endregion
        #region Save VCMAP
        public void Save(string name)
        {
            UVolume.SaveVolumeAsVXW(name, this);
        }
        #endregion
        #region Save VCMOD
        public void SaveVCMOD(string name)
        {
            UVolume.SaveVCMOD(name, this);
        }
        #endregion
        #region Resize
        public void Resize()
        {

        }
        #endregion
    }
}