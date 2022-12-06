var buffer = File.ReadAllText("input.txt");

void Search(int i)
{
    for (var j = i; j < buffer.Length; j++)
    {
        if (buffer[(j - i)..j].Distinct().Count() == i)
        {
            Console.WriteLine(j);
            break;
        }
    }
}

void Part1()
{
    Search(4);
}

void Part2()
{
    Search(14);
}

Part1();
Part2();
