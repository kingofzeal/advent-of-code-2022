var monkeys = File.ReadAllText("input.txt")
    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Chunk(6)
    .Select(x =>
    {
        var monkey = new Monkey();
        monkey.Number = int.Parse(x[0].Split(' ')[1].TrimEnd(':'));
        monkey.MonkeyTrue = int.Parse(x[4].Split(' ').Last());
        monkey.MonkeyFalse = int.Parse(x[5].Split(' ').Last());
        monkey.Test = y => y % int.Parse(x[3].Split(' ').Last()) == 0;
        monkey.Items = x[1].Split(":")[1].Split(", ").Select(int.Parse).Select(x => new Item(x)).ToList();
        monkey.Operation = y =>
        {
            var test = x[2].Split("=")[1];
            var members = test.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var val = y;

            switch (members[1])
            {
                case "+":
                    val += members[2] switch
                    {
                        "old" => val,
                        _ => int.Parse(members[2])
                    };
                    break;
                case "*":
                    val *= members[2] switch
                    {
                        "old" => val,
                        _ => int.Parse(members[2])
                    };
                    break;
            }

            return val;
        };
        return monkey;
    })
    .ToList();

void Part1()
{
    for (var round = 1; round <= 20; round++)
    {
        foreach (var monkey in monkeys)
        {
            foreach (var item in monkey.Items.ToList())
            {
                item.Worry = monkey.Operation(item.Worry);
                monkey.ExaminedItems++;
                item.Worry /= 3;
                var testRes = monkey.Test(item.Worry);
                var newMonkey = testRes ? monkey.MonkeyTrue : monkey.MonkeyFalse;

                monkey.Items.Remove(item);
                monkeys.First(x => x.Number == newMonkey).Items.Add(item);
            }
        }
    }

    var topMonkeys = monkeys.OrderByDescending(x => x.ExaminedItems).Take(2).ToList();
    Console.WriteLine(topMonkeys[0].ExaminedItems * topMonkeys[1].ExaminedItems);
}

Part1();

public class Monkey
{
    public int Number { get; set; }
    public Func<long, long> Operation { get; set; }

    public Func<long, bool> Test { get; set; }
    public int MonkeyTrue { get; set; }
    public int MonkeyFalse { get; set; }

    public List<Item> Items { get; set; }
    public long ExaminedItems { get; set; }
}

public class Item
{
    public Item(long worry)
    {
        Worry = worry;
    }

    public long Worry { get; set; }
}
