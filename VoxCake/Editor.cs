using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxCake.Chunk;

namespace VoxCake
{
    public static class Editor
    {
        public static Vector3Int vec0;
        public static Vector3Int vec1;

        #region Realtime

        #region SetVector
        public static void SetVectorOn()
        {
            vec0 = GetVectorOn();
        }

        public static void SetVectorIn()
        {
            vec0 = GetVectorIn();
        }
        #endregion
        #region SetVoxel
        public static IEnumerator SetVoxelOn(byte value, Volume volume)
        {
            yield return null;
            Debug.Log("Helo");
            Vector3Int vec = GetVectorOn();
            Debug.Log(vec);
            volume.Voxel[vec.x, vec.y, vec.z] = value;
            ChunkUpdater.UpdateVoxel(vec.x, vec.y, vec.z, volume);
        }

        public static IEnumerator SetVoxelOn(Vector3Int vec, byte value, Volume volume)
        {
            yield return null;

            volume.Voxel[vec.x, vec.y, vec.z] = value;
            ChunkUpdater.UpdateVoxel(vec.x, vec.y, vec.z, volume);
            //Debug.Log(vec);
        }

        public static IEnumerator SetVoxelIn(byte value, Volume volume)
        {
            yield return null;
            Vector3Int vec = GetVectorIn();

            volume.Voxel[vec.x, vec.y, vec.z] = value;
            ChunkUpdater.UpdateVoxel(vec.x, vec.y, vec.z, volume);
            //Debug.Log(vec);
        }
        #endregion
        #region SetLine
        public static IEnumerator SetLineOn(byte value, Volume volume)
        {
            yield return null;
            vec1 = GetVectorOn();

            int x0 = vec0.x;
            int x1 = vec1.x;
            int y0 = vec0.y;
            int y1 = vec1.y;
            int z0 = vec0.z;
            int z1 = vec1.z;

            if (vec0 != vec1)
            {
                bool steepXY = Mathf.Abs(y1 - y0) > Mathf.Abs(x1 - x0);
                if (steepXY)
                {
                    Swap(ref x0, ref y0);
                    Swap(ref x1, ref y1);
                }
                bool steepXZ = Mathf.Abs(z1 - z0) > Mathf.Abs(x1 - x0);
                if (steepXZ)
                {
                    Swap(ref x0, ref z0);
                    Swap(ref x1, ref z1);
                }

                int deltaX = Mathf.FloorToInt(Mathf.Abs(x1 - x0));
                int deltaY = Mathf.FloorToInt(Mathf.Abs(y1 - y0));
                int deltaZ = Mathf.FloorToInt(Mathf.Abs(z1 - z0));

                int errorXY = deltaX / 2, errorXZ = deltaX / 2;

                int stepX = x0 > x1 ? -1 : 1;
                int stepY = y0 > y1 ? -1 : 1;
                int stepZ = z0 > z1 ? -1 : 1;

                int y = y0, z = z0;
                int xCopy, yCopy, zCopy;
                int prevX = 0;
                int prevY = 0;
                int prevZ = 0;

                for (int x = x0; stepX > 0 ? x != x1 + 1 : x != x1 - 1; x += stepX)
                {
                    xCopy = x;
                    yCopy = y;
                    zCopy = z;

                    if (steepXZ) Swap(ref xCopy, ref zCopy);
                    if (steepXY) Swap(ref xCopy, ref yCopy);

                    yield return new WaitForSeconds(0.1f);
                    SetVoxelAt(new Vector3Int(xCopy, yCopy, zCopy), value, volume);

                    errorXY -= deltaY;
                    errorXZ -= deltaZ;

                    if (errorXY < 0)
                    {
                        y += stepY;
                        errorXY += deltaX;
                    }

                    if (errorXZ < 0)
                    {
                        z += stepZ;
                        errorXZ += deltaX;
                    }

                    if (x != x0)
                    {
                        if (prevX != xCopy && prevY != yCopy && prevZ == zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, zCopy), value, volume);
                        }
                        else if (prevX != xCopy && prevY == yCopy && prevZ != zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, zCopy), value, volume);
                        }
                        else if (prevX == xCopy && prevY != yCopy && prevZ != zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(xCopy, yCopy, prevZ), value, volume);
                        }
                        else if (prevX != xCopy && prevY != yCopy && prevZ != zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, zCopy), value, volume);
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, prevZ), value, volume);
                        }
                    }
                    prevX = xCopy;
                    prevY = yCopy;
                    prevZ = zCopy;
                }
            }
            else
            {
                volume.Voxel[x0, y0, z0] = value;
                ChunkUpdater.UpdateVoxel(x0, y0, z0, volume);
            }
        }

        public static IEnumerator SetLineIn(byte value, Volume volume)
        {
            yield return null;
            vec1 = GetVectorIn();

            int x0 = vec0.x;
            int x1 = vec1.x;
            int y0 = vec0.y;
            int y1 = vec1.y;
            int z0 = vec0.z;
            int z1 = vec1.z;

            if (vec0 != vec1)
            {
                bool steepXY = Mathf.Abs(y1 - y0) > Mathf.Abs(x1 - x0);
                if (steepXY)
                {
                    Swap(ref x0, ref y0);
                    Swap(ref x1, ref y1);
                }
                bool steepXZ = Mathf.Abs(z1 - z0) > Mathf.Abs(x1 - x0);
                if (steepXZ)
                {
                    Swap(ref x0, ref z0);
                    Swap(ref x1, ref z1);
                }

                int deltaX = Mathf.FloorToInt(Mathf.Abs(x1 - x0));
                int deltaY = Mathf.FloorToInt(Mathf.Abs(y1 - y0));
                int deltaZ = Mathf.FloorToInt(Mathf.Abs(z1 - z0));

                int errorXY = deltaX / 2, errorXZ = deltaX / 2;

                int stepX = x0 > x1 ? -1 : 1;
                int stepY = y0 > y1 ? -1 : 1;
                int stepZ = z0 > z1 ? -1 : 1;

                int y = y0, z = z0;
                int xCopy, yCopy, zCopy;
                int prevX = 0;
                int prevY = 0;
                int prevZ = 0;

                for (int x = x0; stepX > 0 ? x != x1 + 1 : x != x1 - 1; x += stepX)
                {
                    xCopy = x;
                    yCopy = y;
                    zCopy = z;

                    if (steepXZ) Swap(ref xCopy, ref zCopy);
                    if (steepXY) Swap(ref xCopy, ref yCopy);

                    yield return new WaitForSeconds(0.1f);
                    SetVoxelAt(new Vector3Int(xCopy, yCopy, zCopy), value, volume);

                    errorXY -= deltaY;
                    errorXZ -= deltaZ;

                    if (errorXY < 0)
                    {
                        y += stepY;
                        errorXY += deltaX;
                    }

                    if (errorXZ < 0)
                    {
                        z += stepZ;
                        errorXZ += deltaX;
                    }

                    if (x != x0)
                    {
                        if (prevX != xCopy && prevY != yCopy && prevZ == zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, zCopy), value, volume);
                        }
                        else if (prevX != xCopy && prevY == yCopy && prevZ != zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, zCopy), value, volume);
                        }
                        else if (prevX == xCopy && prevY != yCopy && prevZ != zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(xCopy, yCopy, prevZ), value, volume);
                        }
                        else if (prevX != xCopy && prevY != yCopy && prevZ != zCopy)
                        {
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, zCopy), value, volume);
                            yield return new WaitForSeconds(0.1f);
                            SetVoxelAt(new Vector3Int(prevX, yCopy, prevZ), value, volume);
                        }
                    }
                    prevX = xCopy;
                    prevY = yCopy;
                    prevZ = zCopy;
                }
            }
            else
            {
                volume.Voxel[x0, y0, z0] = value;
                ChunkUpdater.UpdateVoxel(x0, y0, z0, volume);
            }
        }
        #endregion
        #region SetCube
        public static IEnumerator SetCubeOn(byte value, Volume volume)
        {
            yield return null;
            vec1 = GetVectorOn();

            int xMin = vec0.x < vec1.x ? vec0.x : vec1.x;
            int xMax = vec0.x > vec1.x ? vec0.x : vec1.x;
            int yMin = vec0.y < vec1.y ? vec0.y : vec1.y;
            int yMax = vec0.y > vec1.y ? vec0.y : vec1.y;
            int zMin = vec0.z < vec1.z ? vec0.z : vec1.z;
            int zMax = vec0.z > vec1.z ? vec0.z : vec1.z;

            int cxMin = xMin / ChunkSize.X;
            int cxMax = xMax / ChunkSize.X;
            int czMin = zMin / ChunkSize.Z;
            int czMax = zMax / ChunkSize.Z;

            for (int x = xMin; x < xMax + 1; x++)
            {
                for (int y = yMin; y < yMax + 1; y++)
                {
                    for (int z = zMin; z < zMax + 1; z++)
                    {
                        volume.Voxel[x,y,z] = value;
                    }
                }
            }

            if (cxMax - cxMin == 0 && czMax - czMin == 0)
            {
                ChunkUpdater.UpdateChunk(cxMin, czMin, volume);
            }
            else
            {
                for (int x = cxMin; x < cxMax + 1; x++)
                {
                    for (int z = czMin; z < czMax + 1; z++)
                    {
                        ChunkUpdater.UpdateChunk(x, z, volume);
                    }
                }
            }
        }

        public static IEnumerator SetCubeIn(byte value, Volume volume)
        {
            yield return null;
            vec1 = GetVectorIn();

            int xMin = vec0.x < vec1.x ? vec0.x : vec1.x;
            int xMax = vec0.x > vec1.x ? vec0.x : vec1.x;
            int yMin = vec0.y < vec1.y ? vec0.y : vec1.y;
            int yMax = vec0.y > vec1.y ? vec0.y : vec1.y;
            int zMin = vec0.z < vec1.z ? vec0.z : vec1.z;
            int zMax = vec0.z > vec1.z ? vec0.z : vec1.z;

            int cxMin = xMin / ChunkSize.X;
            int cxMax = xMax / ChunkSize.X;
            int czMin = zMin / ChunkSize.Z;
            int czMax = zMax / ChunkSize.Z;

            for (int x = xMin; x < xMax + 1; x++)
            {
                for (int y = yMin; y < yMax + 1; y++)
                {
                    for (int z = zMin; z < zMax + 1; z++)
                    {
                        volume.Voxel[x, y, z] = value;
                    }
                }
            }

            if (cxMax - cxMin == 0 && czMax - czMin == 0)
            {
                ChunkUpdater.UpdateChunk(cxMin, czMin, volume);
            }
            else
            {
                for (int x = cxMin; x < cxMax + 1; x++)
                {
                    for (int z = czMin; z < czMax + 1; z++)
                    {
                        ChunkUpdater.UpdateChunk(x, z, volume);
                    }
                }
            }
        }
        #endregion

        public static IEnumerator GetVoxel(Volume volume)
        {
            yield return null;
            Vector3Int vec = GetVectorIn();

            UColor.CurrentColor = volume.Voxel[vec.x, vec.y, vec.z];
            UColor.ByteToPosition();
            //Debug.Log(vec);
        }

        public static void SetVoxelAt(Vector3Int vec, byte value, Volume volume)
        {
            volume.Voxel[vec.x, vec.y, vec.z] = value;
            ChunkUpdater.UpdateVoxel(vec.x, vec.y, vec.z, volume);
        }

        public static void SetLineAt(Vector3Int vec0, Vector3Int vec1, byte value, Volume volume)
        {
            //volume.Voxel[x, y, z] = value;
            //ChunkUpdater.UpdateVoxel(x, y, z, volume);
        }

        public static void SetCubeAt(int x0, int y0, int z0, int x1, int y1, int z1, byte value, Volume volume)
        {

        }

        public static void SetSphereAt(int x0, int y0, int z0, int x1, int y1, int z1, byte value, Volume volume)
        {

        }
        #endregion
        #region Editor
        //public void Se
        #endregion
        #region GetVectors
        public static Vector3Int GetVectorOn()
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            int x = -6;
            int y = -6;
            int z = -6;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance < 128)
                {
                    Vector3 position = hit.point;
                    position += hit.normal * 0.5f;

                    x = Mathf.RoundToInt(position.x);
                    y = Mathf.RoundToInt(position.y);
                    z = Mathf.RoundToInt(position.z);
                }
            }

            return new Vector3Int(x, y, z);
        }

        public static Vector3Int GetVectorIn()
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            int x = -6;
            int y = -6;
            int z = -6;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance < 128)
                {
                    if (hit.transform.name != "World")
                    {
                        Vector3 position = hit.point;
                        position += hit.normal * -0.5f;

                        x = Mathf.RoundToInt(position.x);
                        y = Mathf.RoundToInt(position.y);
                        z = Mathf.RoundToInt(position.z);
                    }
                }
            }

            return new Vector3Int(x, y, z);
        }
        #endregion

        public static void Swap<T>(ref T x, ref T y)
        {
            T tmp = y;
            y = x;
            x = tmp;
        }
    }
}

