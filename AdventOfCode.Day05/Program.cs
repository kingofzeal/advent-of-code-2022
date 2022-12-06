var stacksFile = File.ReadAllText("input.txt")
    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Take(8)
    .Reverse()
    .Select(x => x.Chunk(4))
    .Select(x => x.Select(y => y[1].ToString()))
    .SelectMany(x => x)
    .ToList();

var stacks = new List<List<string>>
        {
            new() { stacksFile[0], stacksFile[9],  stacksFile[18], stacksFile[27], stacksFile[36], stacksFile[45], stacksFile[54], stacksFile[63] },
            new() { stacksFile[1], stacksFile[10], stacksFile[19], stacksFile[28], stacksFile[37], stacksFile[46], stacksFile[55], stacksFile[64] },
            new() { stacksFile[2], stacksFile[11], stacksFile[20], stacksFile[29], stacksFile[38], stacksFile[47], stacksFile[56], stacksFile[65] },
            new() { stacksFile[3], stacksFile[12], stacksFile[21], stacksFile[30], stacksFile[39], stacksFile[48], stacksFile[57], stacksFile[66] },
            new() { stacksFile[4], stacksFile[13], stacksFile[22], stacksFile[31], stacksFile[40], stacksFile[49], stacksFile[58], stacksFile[67] },
            new() { stacksFile[5], stacksFile[14], stacksFile[23], stacksFile[32], stacksFile[41], stacksFile[50], stacksFile[59], stacksFile[68] },
            new() { stacksFile[6], stacksFile[15], stacksFile[24], stacksFile[33], stacksFile[42], stacksFile[51], stacksFile[60], stacksFile[69] },
            new() { stacksFile[7], stacksFile[16], stacksFile[25], stacksFile[34], stacksFile[43], stacksFile[52], stacksFile[61], stacksFile[70] },
            new() { stacksFile[8], stacksFile[17], stacksFile[26], stacksFile[35], stacksFile[44], stacksFile[53], stacksFile[62], stacksFile[71] }
        }
    .Select(x => x.Where(y => !string.IsNullOrWhiteSpace(y.ToString())).Reverse().ToList())
    .ToList();

var instructions = File.ReadAllText("input.txt")
    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Skip(9)
    .Select(x => x.Split(' '))
    .Select(x => new ValueTuple<int, int, int>(
        int.Parse(x[1]),
        int.Parse(x[3]) - 1,
        int.Parse(x[5]) - 1))
    .ToList();

void Part1()
{
    foreach (var (ct, from, to) in instructions)
    {
        for (var i = 1; i <= ct; i++)
        {
            stacks[to].Reverse();
            stacks[to].Add(stacks[from].First());
            stacks[to].Reverse();
            stacks[from].RemoveAt(0);
        }
    }

    Console.WriteLine(string.Join(string.Empty, stacks.Select(x => x.First())));
}

void Part2()
{
    foreach (var (ct, from, to) in instructions)
    {
        stacks[to].Reverse();
        stacks[to].AddRange(stacks[from].Take(ct).Reverse());
        stacks[to].Reverse();
        stacks[from].RemoveRange(0, ct);
    }

    Console.WriteLine(string.Join(string.Empty, stacks.Select(x => x.First())));
}

Part2();
