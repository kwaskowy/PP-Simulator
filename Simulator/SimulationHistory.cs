using Simulator.Maps;

namespace Simulator;

public class SimulationHistory
{
    private readonly Simulation _simulation;
    public int SizeX { get; }
    public int SizeY { get; }
    public List<SimulationTurnLog> TurnLogs { get; } = new();

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        SizeX = GetMapSizeX(simulation.Map);
        SizeY = GetMapSizeY(simulation.Map);
        Run();
    }

    private void Run()
    {
        // Zapisz stan początkowy mapy
        LogTurn();

        // Przeprowadź całą symulację
        while (!_simulation.Finished)
        {
            _simulation.Turn();
            LogTurn();
        }
    }

    private void LogTurn()
    {
        var symbols = new Dictionary<Point, char>();

        // Iteruj przez punkty na mapie i zapisuj symbole obiektów
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                var point = new Point(x, y);
                var objectsAtPoint = _simulation.Map.At(point);

                if (objectsAtPoint.Any())
                {
                    symbols[point] = objectsAtPoint.Count > 1 ? 'X' : objectsAtPoint.First().Symbol;
                }
            }
        }

        // Zapisz stan tury
        TurnLogs.Add(new SimulationTurnLog
        {
            TurnNumber = TurnLogs.Count + 1,
            Creature = _simulation.CurrentCreature,
            Move = _simulation.CurrentMoveName,
            Symbols = symbols
        });
    }

    public SimulationTurnLog GetTurn(int turnNumber)
    {
        if (turnNumber < 1 || turnNumber > TurnLogs.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(turnNumber), "Invalid turn number.");
        }

        return TurnLogs[turnNumber - 1];
    }

    private static int GetMapSizeX(Map map)
    {
        return map is SmallSquareMap smallMap ? smallMap.Size
             : map is BigMap bigMap ? bigMap.Width
             : throw new InvalidOperationException("Unsupported map type.");
    }

    private static int GetMapSizeY(Map map)
    {
        return map is SmallSquareMap smallMap ? smallMap.Size
             : map is BigMap bigMap ? bigMap.Height
             : throw new InvalidOperationException("Unsupported map type.");
    }
}
