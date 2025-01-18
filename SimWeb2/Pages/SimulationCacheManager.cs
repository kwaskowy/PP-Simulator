using Microsoft.Extensions.Caching.Memory;
using Simulator;
using Simulator.Maps;

public class SimulationCacheManager
{
    private readonly IMemoryCache _cache;

    public SimulationCacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }

    public SimulationHistory GetHistory()
    {
        if (!_cache.TryGetValue("SimulationHistory", out SimulationHistory history))
        {
            history = CreateDefaultHistory();
            SaveHistory(history);
        }

        return history;
    }

    public int GetCurrentTurn()
    {
        if (!_cache.TryGetValue("CurrentTurn", out int currentTurn))
        {
            currentTurn = 1; // Domyślnie zaczynamy od pierwszej tury
            _cache.Set("CurrentTurn", currentTurn);
        }

        return currentTurn;
    }

    public void SetCurrentTurn(int turn)
    {
        _cache.Set("CurrentTurn", turn);
    }

    private SimulationHistory CreateDefaultHistory()
    {
        // Tworzenie domyślnej symulacji
        var map = new BigBounceMap(5, 5); // Rozmiar mapy
        var creatures = new List<Creature>
        {
            new Elf("Default Elf"),
            new Orc("Default Orc")
        };

        var positions = new List<Point>
        {
            new Point(0, 0),
            new Point(4, 4)
        };

        foreach (var (creature, position) in creatures.Zip(positions, (c, p) => (c, p)))
        {
            map.Add(position, creature);
        }

        var moves = "rrddlluurr";
        var simulation = new Simulation(map, creatures, positions, moves);
        return new SimulationHistory(simulation);
    }

    public void SaveHistory(SimulationHistory history)
    {
        _cache.Set("SimulationHistory", history);
    }
}
