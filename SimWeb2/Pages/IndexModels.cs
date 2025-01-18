using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Simulator;

namespace SimWeb2.Pages;

public class IndexModels : PageModel
{
    private readonly SimulationCacheManager _cacheManager;

    public IndexModels(IMemoryCache cache)
    {
        _cacheManager = new SimulationCacheManager(cache);
    }

    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public int CurrentTurn { get; set; }
    public int TotalTurns { get; set; }
    public Dictionary<Point, char> Symbols { get; set; } = new();

    public void OnGet(int? turn = null)
    {
        var history = _cacheManager.GetHistory();
        SizeX = history.SizeX;
        SizeY = history.SizeY;
        TotalTurns = history.TurnLogs.Count;

        // Jeśli `turn` jest podane, ustaw jako aktualną turę
        if (turn.HasValue && turn > 0 && turn <= TotalTurns)
        {
            _cacheManager.SetCurrentTurn(turn.Value);
            CurrentTurn = turn.Value;
        }
        else
        {
            CurrentTurn = _cacheManager.GetCurrentTurn();
        }

        var selectedLog = history.TurnLogs.Find(log => log.TurnNumber == CurrentTurn);
        Symbols = selectedLog?.Symbols ?? new Dictionary<Point, char>();
    }
}
