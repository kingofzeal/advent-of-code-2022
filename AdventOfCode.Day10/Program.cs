using System.Text;

var input = File.ReadAllText("input.txt")
    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split(' '))
    .Select(x => new Instruction(x))
    .ToList();

void Part1()
{
    var cycle = 0;
    var register = 1;
    var strength = 0;

    foreach (var instruction in input)
    {
        if (instruction.Command == Command.addx)
        {
            cycle++;
            strength += CheckCycle(cycle, register);
        }

        cycle++;
        strength += CheckCycle(cycle, register);

        register += instruction.Value;
    }

    Console.WriteLine(strength);
}

int CheckCycle(int cycle, int register)
{
    if ((cycle - 20) % 40 == 0)
    {
        return cycle * register;
    }

    return 0;
}

void Part2()
{
    var register = 1;
    var position = 0;
    var output = new StringBuilder();

    foreach (var instruction in input)
    {
        if (instruction.Command == Command.addx)
        {
            output.Append(Math.Abs(register - position) <= 1 ? "#" : ".");

            position++;
            position %= 40;
        }

        output.Append(Math.Abs(register - position) <= 1 ? "#" : ".");
        position++;
        position %= 40;

        register += instruction.Value;
    }

    foreach (var line in output.ToString().Chunk(40))
    {
        Console.WriteLine(line);
    }
}

Part1();
Part2();


public class Instruction
{
    public Instruction(string[] line)
    {
        Command = line[0] switch
        {
            "addx" => Command.addx,
            "noop" => Command.noop,
            _ => Command
        };

        Value = Command == Command.addx ? int.Parse(line[1]) : 0;
    }

    public Command Command { get; set; }
    public int Value { get; set; }
}

public enum Command
{
    addx,
    noop
}
