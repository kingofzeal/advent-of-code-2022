var input = File.ReadAllText("input.txt")
    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split(' '))
    .Select(x => new ValueTuple<string, int>(x[0], int.Parse(x[1])))
    .ToList();

void Part1()
{
    var posHX = 0;
    var posHY = 0;

    var posTX = 0;
    var posTY = 0;

    var tHistory = new List<ValueTuple<int, int>> { (0, 0) };

    foreach (var (direction, length) in input)
    {
        for (var i = 0; i < length; i++)
        {
            switch (direction)
            {
                case "R": posHX++; break;
                case "D": posHY--; break;
                case "U": posHY++; break;
                case "L": posHX--; break;
                default: throw new ArgumentOutOfRangeException();
            }

            var (newTX, newTY) = CalculateTail((posHX, posHY), (posTX, posTY));

            if (newTX != posTX || newTY != posTY)
            {
                //Console.WriteLine($"{(posHX, posHY)} {(posTX, posTY)} => {(newTX, newTY)}");
                tHistory.Add((newTX, newTY));
                posTX = newTX;
                posTY = newTY;
            }
        }
    }

    Console.WriteLine(tHistory.Distinct().Count());
}
void Part2()
{
    var knots = new List<Knot>
    {
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
        new(0, 0),
    };

    var head = knots[0];
    var tail = knots[9];

    var tHistory = new List<ValueTuple<int, int>> { (tail.posX, tail.posY) };

    foreach (var (direction, length) in input)
    {
        for (var i = 0; i < length; i++)
        {
            switch (direction)
            {
                case "R": head.posX++; break;
                case "D": head.posY--; break;
                case "U": head.posY++; break;
                case "L": head.posX--; break;
                default: throw new ArgumentOutOfRangeException();
            }

            for (var j = 1; j < knots.Count; j++)
            {
                var currentKnot = knots[j];
                var prevKnot = knots[j - 1];
                var newPos = CalculateTail((prevKnot.posX, prevKnot.posY), (currentKnot.posX, currentKnot.posY));

                if ((currentKnot.posX, currentKnot.posY) == newPos)
                {
                    break;
                }

                currentKnot.posX = newPos.newTX;
                currentKnot.posY = newPos.newTY;

                if (j == knots.Count - 1)
                {
                    tHistory.Add((currentKnot.posX, currentKnot.posY));
                }
            }
        }
    }

    Console.WriteLine(tHistory.Distinct().Count());
}

(int newTX, int newTY) CalculateTail((int X, int Y) headPos, (int X, int Y) tailPos)
{
    var newX = tailPos.X;
    var newY = tailPos.Y;

    if (Math.Abs(headPos.X - tailPos.X) <= 1 &&
        Math.Abs(headPos.Y - tailPos.Y) <= 1)
    {
        return tailPos;
    }

    if (headPos.Y > tailPos.Y)
    {
        newY++;
    }

    if (headPos.Y < tailPos.Y)
    {
        newY--;
    }

    if (headPos.X > tailPos.X)
    {
        newX++;
    }

    if (headPos.X < tailPos.X)
    {
        newX--;
    }

    return (newX, newY);
}

//Console.WriteLine(CalculateTail((2, 2), (1, 1)));
Part1();
Part2();

public class Knot
{
    public Knot(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public int posX { get; set; }
    public int posY { get; set; }
}
