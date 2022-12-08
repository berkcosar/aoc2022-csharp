
namespace Day7
{
    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            int totalDiskSpace = 70000000;
            int minDiskSpaceRequiredForUpdate = 30000000;
            Directory baseDirectory = new("//", null);
            Directory? currentDirectory = baseDirectory;

            for (int i = 1; i < commands.Length; i++)
            {
                string currentCommand = commands[i];
                if (currentDirectory != null && currentCommand != null)
                {
                    if (currentCommand.Equals("$ cd .."))
                    {
                        currentDirectory = currentDirectory.Parent;
                    }
                    else if (currentCommand.StartsWith("$ cd "))
                    {
                        string dirName = commands[i].Split(' ')[2];
                        currentDirectory = currentDirectory.Childs[dirName] as Directory;
                    }
                    else if (currentCommand.StartsWith("dir "))
                    {
                        string dirName = currentCommand.Split(' ')[1];
                        Directory dir = new(dirName, currentDirectory);
                        currentDirectory.AddChild(dir);
                    }
                    else if (currentCommand == "$ ls")
                    {
                        continue;
                    }
                    else
                    {
                        string[] commandParts = currentCommand.Split(' ');
                        string fileName = commandParts[1];
                        int size = Convert.ToInt32(commandParts[0]);
                        File fl = new(fileName, size);
                        currentDirectory.AddChild(fl);
                    }
                }
            }

            int minSizeToDelete =minDiskSpaceRequiredForUpdate - (totalDiskSpace - Directory.Directories[0].Size);
            int minDirSizeToDelete = Int32.MaxValue;
            for (int i = 0; i < Directory.Directories.Count; i++)
            {
                if (Directory.Directories[i].Size >= minSizeToDelete)
                {
                    minDirSizeToDelete = Math.Min(minDirSizeToDelete,Directory.Directories[i].Size);
                }
            }

            return minDirSizeToDelete;
        }
    }
}