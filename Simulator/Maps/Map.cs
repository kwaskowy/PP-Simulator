namespace Simulator.Maps;

/// <summary>
/// Abstract base class for maps.
/// </summary>
public abstract class Map
{
    /// <summary>
    /// Horizontal size of the map.
    /// </summary>
    public abstract int SizeX { get; }

    /// <summary>
    /// Vertical size of the map.
    /// </summary>
    public abstract int SizeY { get; }

    /// <summary>
    /// Check if a point exists on the map.
    /// </summary>
    public abstract bool Exist(Point p);

    /// <summary>
    /// Get the next position in a given direction.
    /// </summary>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Get the next diagonal position in a given direction.
    /// </summary>
    public abstract Point NextDiagonal(Point p, Direction d);

    /// <summary>
    /// Add an object to the map at a specific point.
    /// </summary>
    public abstract void Add(Point p, IMappable obj);

    /// <summary>
    /// Remove an object from the map at a specific point.
    /// </summary>
    public abstract void Remove(Point p, IMappable obj);

    /// <summary>
    /// Get all objects at a specific point.
    /// </summary>
    public abstract List<IMappable> At(Point p);
}
