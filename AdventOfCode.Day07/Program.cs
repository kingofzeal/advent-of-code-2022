using AdventOfCode.Day07;

AocFolder ProcessCommands()
{
    var instructions = File.ReadAllText("input.txt")
        .Split('\n', StringSplitOptions.RemoveEmptyEntries)
        .ToList();

    var root = new AocFolder("/", null);

    var cwd = root;

    for (var index = 0; index < instructions.Count; index++)
    {
        var instruction = instructions[index];

        if (!instruction.StartsWith("$"))
        {
            //To be safe
            continue;
        }

        var command = instruction.Split(' ');

        switch (command[1])
        {
            case "cd":
                switch (command[2])
                {
                    case "/":
                        cwd = root;
                        break;
                    case "..":
                        cwd = cwd.Parent ?? throw new IndexOutOfRangeException();
                        break;
                    default:
                        cwd = cwd.Folders.First(x => x.Name == command[2]);
                        break;
                }
                break;
            case "ls":
                index++;
                while (index < instructions.Count)
                {
                    if (instructions[index].StartsWith("$"))
                    {
                        index--;
                        break;
                    }

                    var output = instructions[index].Split(' ');

                    if (output[0] == "dir")
                    {
                        cwd.Folders.Add(new AocFolder(output[1], cwd));
                    }
                    else
                    {
                        cwd.Files.Add(new AocFile { Name = output[1], Size = int.Parse(output[0]) });
                    }
                    index++;
                }
                break;
            default:
                Console.WriteLine("Derp");
                return root;
        }
    }

    return root;
}

void Part1(AocFolder folder)
{
    var currentCount = 1;

    var allFolders = folder.Folders;

    while (allFolders.Count != currentCount)
    {
        currentCount = allFolders.Count;
        allFolders = allFolders.Union(allFolders.SelectMany(x => x.Folders)).ToList();
    }

    allFolders.Add(folder);

    Console.WriteLine(allFolders.Where(x => x.Size <= 100000).Sum(x => x.Size));
}

void Part2(AocFolder folder)
{
    var totalSpace = 70000000;
    var updateReq = 30000000;
    var unused = totalSpace - folder.Size;
    var target = updateReq - unused;

    var allFolders = folder.Folders;
    var currentCount = 1;

    while (allFolders.Count != currentCount)
    {
        currentCount = allFolders.Count;
        allFolders = allFolders.Union(allFolders.SelectMany(x => x.Folders)).ToList();
    }

    Console.WriteLine(allFolders.Where(x => x.Size >= target).Min(x => x.Size));
}

var res = ProcessCommands();


Part1(res);
Part2(res);
