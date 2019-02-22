using System.Collections.Generic;

namespace VoxCake.Builder
{
    public static class Buffer
    {
        private static List<ITool> commands = new List<ITool>();
        private static int lastExecuted = -1;
        private static int lastSaved = -1;

        public delegate void Changed(bool haveUnsavedChanges);

        public static event Changed OnChanged = h => { };

        public static void Clear()
        {
            commands.Clear();
            lastExecuted = -1;
            lastSaved = -1;

            OnChanged(false);
        }

        public static void Save()
        {
            lastSaved = lastExecuted;

            OnChanged(false);
        }

        public static bool Modified
        {
            get { return lastSaved != lastExecuted; }
        }

        public static int Size
        {
            get { return commands.Count; }
        }

        public static int LastExecuted
        {
            get { return lastExecuted; }
        }

        public static void Limit(int numCommands)
        {
            while (commands.Count > numCommands)
            {
                commands.RemoveAt(0);
                if (lastExecuted >= 0)
                {
                    lastExecuted--;
                }

                if (lastSaved >= 0)
                {
                    lastSaved--;
                }
            }
        }

        public static void Add(ITool tool)
        {
            if (lastExecuted + 1 < commands.Count)
            {
                int numCommandsToRemove = commands.Count
                                          - (lastExecuted + 1);
                for (int i = 0; i < numCommandsToRemove; i++)
                {
                    commands.RemoveAt(lastExecuted + 1);
                }

                lastSaved = -1;
            }


            tool.Do();


            commands.Add(tool);
            lastExecuted = commands.Count - 1;

            OnChanged(true);
        }

        public static void Undo()
        {
            if (lastExecuted >= 0)
            {
                if (commands.Count > 0)
                {
                    commands[lastExecuted].Undo();
                    lastExecuted--;
                    OnChanged(lastExecuted != lastSaved);
                }
            }
        }

        public static void Redo()
        {
            if (lastExecuted + 1 < commands.Count)
            {
                commands[lastExecuted + 1].Do();
                lastExecuted++;
                OnChanged(lastExecuted != lastSaved);
            }
        }
    }
}