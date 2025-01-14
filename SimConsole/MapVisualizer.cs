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

        // Wyświetlenie legendy
        DrawLegend();

        // Pobranie rozmiaru mapy
        if (!(_map is BigMap bigMap))
        {
            Console.WriteLine("Invalid map type.");
            return;
        }

        int width = bigMap.Width;
        int height = bigMap.Height;

        // Rysowanie górnej krawędzi
        Console.Write(Box.TopLeft);
        for (int x = 0; x < width; x++)
        {
            Console.Write(Box.Horizontal);
        }
        Console.WriteLine(Box.TopRight);

        // Rysowanie wierszy mapy
        for (int y = 0; y < height; y++)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < width; x++)
            {
                var symbol = GetSymbolAt(new Point(x, y));
                Console.Write(symbol);
            }
            Console.WriteLine(Box.Vertical);
        }

        // Rysowanie dolnej krawędzi
        Console.Write(Box.BottomLeft);
        for (int x = 0; x < width; x++)
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
        Console.WriteLine("R = Rabbits");
        Console.WriteLine("B = Eagles");
        Console.WriteLine("S = Ostriches");
        Console.WriteLine("X = Multiple creatures");
    }


    private char GetSymbolAt(Point point)
    {
        // Pobierz wszystkie obiekty na danym punkcie
        var objectsAtPoint = _simulation.Map.At(point);

        if (objectsAtPoint.Count > 1)
        {
            return 'X'; // Pole zajęte przez wiele obiektów
        }

        if (objectsAtPoint.Count == 1)
        {
            var obj = objectsAtPoint.First();
            return obj switch
            {
                Orc => 'O',
                Elf => 'E',
                Birds b when b.CanFly => 'B', // Orły (ptaki, które latają)
                Birds => 'S', // Strusie (ptaki nieloty)
                Animals => 'R', // Króliki
                _ => ' ' // Nieznany obiekt
            };
        }

        return ' '; // Puste pole
    }

}
