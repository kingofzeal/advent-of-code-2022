using System.Xml;

var input = File.ReadAllText("input.txt")
    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .ToList();

var map = new List<Tree>();

for (var i = 0; i < input.Count; i++)
{
    var line = input[i];
    for (var j = 0; j < line.Length; j++)
    {
        map.Add(new Tree
        {
            X = i,
            Y = j,
            Height = int.Parse(line[j].ToString())
        });
    }
}

void Part1()
{
    for (var i = 0; i < input.Count; i++)
    {
        var line = map.Where(x => x.X == i).ToList();

        foreach (var tree in line)
        {
            if (tree.Visible)
            {
                continue;
            }

            if (!line.Any(x => x.Y < tree.Y) ||
                line.Where(x => x.Y < tree.Y).Select(x => x.Height).All(x => x < tree.Height))
            {
                tree.Visible = true;
            }

            if (!line.Any(x => x.Y > tree.Y) ||
                line.Where(x => x.Y > tree.Y).Select(x => x.Height).All(x => x < tree.Height))
            {
                tree.Visible = true;
            }
        }
    }

    for (var j = 0; j < map.Max(x => x.Y); j++)
    {
        var column = map.Where(x => x.Y == j).ToList();

        foreach (var tree in column)
        {
            if (tree.Visible)
            {
                continue;
            }

            if (!column.Any(x => x.X < tree.X) ||
                column.Where(x => x.X < tree.X).Select(x => x.Height).All(x => x < tree.Height))
            {
                tree.Visible = true;
            }

            if (!column.Any(x => x.X > tree.X) ||
                column.Where(x => x.X > tree.X).Select(x => x.Height).All(x => x < tree.Height))
            {
                tree.Visible = true;
            }
        }
    }

    Console.WriteLine(map.Count(x => x.Visible));
}

void Part2()
{
    for (var i = 0; i < input.Count; i++)
    {
        Console.WriteLine($"...Line {i}/{input.Count}");
        var line = map.Where(x => x.X == i).ToList();

        for (var j = 0; j < line.Count; j++)
        {
            var tree = map.Single(x => x.X == i && x.Y == j);

            //Look Up
            var tmpX = i;
            var tmpY = j;
            var vis = 0;

            while (tmpX > 0)
            {
                vis++;
                tmpX--;
                if (map.Single(x => x.X == tmpX && x.Y == tmpY).Height >= tree.Height)
                {
                    break;
                }

            }

            if (vis == 0)
            {
                continue;
            }
            tree.DistUp = vis;

            //Look Down
            tmpX = i;
            vis = 0;

            while (tmpX < map.Max(x => x.Y))
            {
                vis++;
                tmpX++;
                if (map.Single(x => x.X == tmpX && x.Y == tmpY).Height >= tree.Height)
                {
                    break;
                }

            }
            if (vis == 0)
            {
                continue;
            }
            tree.DistDown = vis;

            //Look Left
            tmpX = i;
            vis = 0;

            while (tmpY > 0)
            {
                vis++;
                tmpY--;
                if (map.Single(x => x.X == tmpX && x.Y == tmpY).Height >= tree.Height)
                {
                    break;
                }

            }
            if (vis == 0)
            {
                continue;
            }
            tree.DistLeft = vis;

            //Look Right
            tmpY = j;
            vis = 0;

            while (tmpY < map.Max(x => x.X))
            {
                vis++;
                tmpY++;
                if (map.Single(x => x.X == tmpX && x.Y == tmpY).Height >= tree.Height)
                {
                    break;
                }

            }
            if (vis == 0)
            {
                continue;
            }
            tree.DistRight = vis;
        }
    }

    Console.WriteLine(map.Max(x => x.SenicScore));
}

Part1();
Part2();

public class Tree
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Height { get; set; }
    public bool Visible { get; set; }

    public int DistUp { get; set; }
    public int DistDown { get; set; }
    public int DistLeft { get; set; }
    public int DistRight { get; set; }

    public int SenicScore => DistUp * DistDown * DistLeft * DistRight;
}
