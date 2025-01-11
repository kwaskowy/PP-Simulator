using Simulator;
using Simulator.Maps;

namespace SimConsole;

public class MapVisualizer
{
    private readonly Map _map;
    private readonly Simulation _simulation;

    public MapVisualizer(Map map, Simulation simulation)
    {
        _map = map ?? throw new ArgumentNullException(nameof(map));
        _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
    }

    public void Draw()
    {
        Console.Clear();

        DrawLegend();

        var size = (_map as SmallSquareMap)?.Size ?? 0;

        Console.Write(Box.TopLeft);
        for (int i = 0; i < size; i++)
        {
            Console.Write(Box.Horizontal);
        }
        Console.WriteLine(Box.TopRight);

        for (int y = 0; y < size; y++)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < size; x++)
            {
                var symbol = GetSymbolAt(new Point(x, y));
                Console.Write(symbol);
            }
            Console.WriteLine(Box.Vertical);
        }
        Console.Write(Box.BottomLeft);
        for (int i = 0; i < size; i++)
        {
            Console.Write(Box.Horizontal);
        }
        Console.WriteLine(Box.BottomRight);
    }

    private void DrawLegend()
    {
        Console.WriteLine("Legend:");
        Console.WriteLine("O = Orc");
        Console.WriteLine("E = Elf");
        Console.WriteLine("X = Multiple creatures");
        Console.WriteLine();
    }

    private char GetSymbolAt(Point point)
    {
        var positions = _simulation.Positions;
        var creatures = _simulation.Creatures;

        char symbol = ' ';

        int countAtPoint = 0;
        foreach (var (position, index) in positions.Select((pos, idx) => (pos, idx)))
        {
            if (position == point)
            {
                countAtPoint++;
                symbol = creatures[index] is Orc ? 'O' : 'E';
            }
        }

        if (countAtPoint > 1)
        {
            symbol = 'X';
        }

        return symbol;
    }
}
