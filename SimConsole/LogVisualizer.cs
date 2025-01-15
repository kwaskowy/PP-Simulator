using Simulator;
using Simulator.Maps;

namespace SimConsole;

/// <summary>
/// Visualizes the history of a simulation based on SimulationHistory.
/// </summary>
public class LogVisualizer
{
    private readonly SimulationHistory _log;

    public LogVisualizer(SimulationHistory log)
    {
        _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public void Draw(int turnIndex)
    {
        if (turnIndex < 0 || turnIndex >= _log.TurnLogs.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(turnIndex), "Invalid turn index.");
        }

        // Pobierz stan mapy z historii
        var logEntry = _log.TurnLogs[turnIndex];

        Console.Clear();

        // Wyświetlenie legendy
        DrawLegend();

        // Pobranie rozmiaru mapy
        int width = _log.SizeX;
        int height = _log.SizeY;

        // Wyświetlenie informacji o turze
        Console.WriteLine($"Turn {logEntry.TurnNumber}:");
        Console.WriteLine($"Creature: {logEntry.Creature.Name} moves {logEntry.Move}");

        // Rysowanie górnej krawędzi mapy
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
                var point = new Point(x, y);
                logEntry.Symbols.TryGetValue(point, out char symbol);
                Console.Write(symbol == '\0' ? ' ' : symbol); // Jeśli brak symbolu, wstaw spację
            }
            Console.WriteLine(Box.Vertical);
        }

        // Rysowanie dolnej krawędzi mapy
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
}
