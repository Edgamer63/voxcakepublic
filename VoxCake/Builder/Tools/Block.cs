using VoxCake.Builder.Data;
using UnityEngine;
using VoxCake.Chunk;

namespace VoxCake.Builder.Tools
{
    public class Block// : ITool
    {
        private bool _firstTime = true;
        public Voxel Data;

        public void Do()
        {
            if (_firstTime)
            {
                if (Data.Mode == 0)
                {
                    //Data.OldColor = MapData.GetValue(Data.Position.x, Data.Position.y, Data.Position.z);
                    //Data.NewColor = Editor.Color;

                    //MapData.SetValue(Data.Position.x, Data.Position.y, Data.Position.z, Data.NewColor);
                    //ChunkManager.Update(Data.Position.x, Data.Position.y, Data.Position.z);
                }
                else if (Data.Mode == 1)
                {
                    //Data.OldColor = MapData.GetValue(Data.Position.x, Data.Position.y, Data.Position.z);
                    Data.NewColor = 0;

                    //Map.Data.SetType(Data.Position.x, Data.Position.y, Data.Position.z, 0);
                    //ChunkManager.Update(Data.Position.x, Data.Position.y, Data.Position.z);
                }
                else if (Data.Mode == 2)
                {
                    //Data.OldColor = MapData.GetValue(Data.Position.x, Data.Position.y, Data.Position.z);
                    //Data.NewColor = Editor.Color;

                    //MapData.SetValue(Data.Position.x, Data.Position.y, Data.Position.z, Data.NewColor);
                   // ChunkManager.Update(Data.Position.x, Data.Position.y, Data.Position.z);
                }
                else
                {
                    Debug.LogError("Unknown mode!");
                }

                _firstTime = false;
            }
            else //REDO
            {
                if (Data.Mode == 0)
                {
                    //MapData.SetValue(Data.Position.x, Data.Position.y, Data.Position.z, Data.NewColor);
                    //ChunkManager.Update(Data.Position.x, Data.Position.y, Data.Position.z);
                }
                else if (Data.Mode == 1)
                {
                    //MapData.Set(Data.Position.x, Data.Position.y, Data.Position.z, Data.NewColor, 0);   old
                    //MapData.SetValue(Data.Position.x, Data.Position.y, Data.Position.z, Data.NewColor);
                    //ChunkManager.Update(Data.Position.x, Data.Position.y, Data.Position.z);
                }
                else if (Data.Mode == 2)
                {
                    //MapData.SetValue(Data.Position.x, Data.Position.y, Data.Position.z, Data.NewColor);
                    //ChunkManager.Update(Data.Position.x, Data.Position.y, Data.Position.z);
                }
            }
        }

        public void Undo()
        {
            //MapData.SetValue(Data.Position.x, Data.Position.y, Data.Position.z, Data.OldColor);
            //ChunkManager.Update(Data.Position.x, Data.Position.y, Data.Position.z);
        }
    }
}