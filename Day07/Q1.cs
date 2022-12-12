
namespace Day07
{
    public class File
    {
        public File(string name, int size)
        {
            Name = name;
            _Size = size;
        }

        public string Name { get; set; }

        protected int _Size;
        public virtual int Size { get => _Size; }
    }

    public class Directory : File
    {

        public static List<Directory> Directories = new();
        public Directory(string name, Directory? parent) : base(name, 0)
        {
            Parent = parent;
            Childs = new();
            Directories.Add(this);
        }

        private bool _SizeCalculated = false;
        public Directory? Parent { get; set; }

        public Dictionary<string, File> Childs { get; set; }

        public override int Size
        {
            get
            {
                if (!_SizeCalculated)
                {
                    foreach (var child in Childs)
                    {
                        _Size += child.Value.Size;
                    }
                    _SizeCalculated = true;
                }
                return _Size;
            }
        }

        public void AddChild(File f)
        {
            if (!Childs.ContainsKey(f.Name))
            {
                _SizeCalculated = false;
                Childs.Add(f.Name, f);
            }
        }
    }

    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            long returnVal = 0;
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

            for (int i = 0; i < Directory.Directories.Count; i++)
            {
                if (Directory.Directories[i].Size < 100000)
                {
                    returnVal += Directory.Directories[i].Size;
                }
            }

            return returnVal;
        }



    }
}