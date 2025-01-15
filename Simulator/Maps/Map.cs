namespace Simulator.Maps;

/// <summary>
/// Map of points.
/// </summary>
public abstract class Map
{
    /// <summary>
    /// Check if the given point belongs to the map.
    /// </summary>
    public abstract bool Exist(Point p);

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction rotated 45 degrees clockwise.
    /// </summary>
    public abstract Point NextDiagonal(Point p, Direction d);

    /// <summary>
    /// Get all objects located at the given point on the map.
    /// </summary>
    public abstract List<IMappable> At(Point p);

    /// <summary>
    /// Add an object to the map at the specified point.
    /// </summary>
    public abstract void Add(Point p, IMappable obj);

    /// <summary>
    /// Remove an object from the map at the specified point.
    /// </summary>
    public abstract void Remove(Point p, IMappable obj);
}
