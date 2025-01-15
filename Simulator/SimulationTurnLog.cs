namespace Simulator;

/// <summary>
/// State of map after a single simulation turn.
/// </summary>
public class SimulationTurnLog
{
    /// <summary>
    /// Creature that moved in this turn.
    /// </summary>
    public required Creature Creature { get; init; }

    /// <summary>
    /// Text representation of the move in this turn.
    /// </summary>
    public required string Move { get; init; }

    /// <summary>
    /// Dictionary of IMappable.Symbol on the map in this turn.
    /// </summary>
    public required Dictionary<Point, char> Symbols { get; init; }

    /// <summary>
    /// Turn number (optional for clarity in debugging).
    /// </summary>
    public int TurnNumber { get; init; }
}
