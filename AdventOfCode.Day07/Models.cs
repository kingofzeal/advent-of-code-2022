using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day07;
public class AocFolder
{
    public AocFolder(string name, AocFolder? parent)
    {
        Name = name;
        Parent = parent;
    }

    public string Name { get; set; }
    public List<AocFolder> Folders { get; set; } = new();
    public List<AocFile> Files { get; set; } = new();
    public AocFolder? Parent { get; set; }

    public int Size => Folders.Sum(x => x.Size) + Files.Sum(x => x.Size);
}

public class AocFile
{
    public int Size { get; set; }
    public string Name { get; set; }
}
