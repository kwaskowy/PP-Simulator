namespace Simulator;

/// <summary>
/// Reprezentuje obiekt, który można umieścić na mapie.
/// </summary>
public interface IMappable
{
    /// <summary>
    /// Opis obiektu mapowalnego.
    /// </summary>
    string Description { get; }
}