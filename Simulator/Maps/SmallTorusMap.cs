using Simulator.Maps;

namespace Simulator.Maps;

public class SmallTorusMap : Map
{
    public int Size { get; }
    private readonly Dictionary<Point, List<IMappable>> _map = new();

    public SmallTorusMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be between 5 and 20.");
        }

        Size = size;
    }

    public override bool Exist(Point p)
    {
        return p.X >= 0 && p.X < Size && p.Y >= 0 && p.Y < Size;
    }

    public override Point Next(Point p, Direction d)
    {
        var next = p.Next(d);
        int x = (next.X + Size) % Size;
        int y = (next.Y + Size) % Size;
        return new Point(x, y);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        var next = p.NextDiagonal(d);
        int x = (next.X + Size) % Size;
        int y = (next.Y + Size) % Size;
        return new Point(x, y);
    }

    public override List<IMappable> At(Point p)
    {
        Point wrappedPoint = WrapPoint(p);
        return _map.ContainsKey(wrappedPoint) ? _map[wrappedPoint] : new List<IMappable>();
    }

    /// <summary>
    /// Wraps a point to fit the toroidal map dimensions.
    /// </summary>
    private Point WrapPoint(Point p)
    {
        int x = (p.X + Size) % Size;
        int y = (p.Y + Size) % Size;
        return new Point(x, y);
    }

    public override void Add(Point p, IMappable obj)
    {
        Point wrappedPoint = WrapPoint(p);

        if (!_map.ContainsKey(wrappedPoint))
        {
            _map[wrappedPoint] = new List<IMappable>();
        }

        _map[wrappedPoint].Add(obj);
    }

    public override void Remove(Point p, IMappable obj)
    {
        Point wrappedPoint = WrapPoint(p);

        if (_map.ContainsKey(wrappedPoint))
        {
            _map[wrappedPoint].Remove(obj);
            if (_map[wrappedPoint].Count == 0)
            {
                _map.Remove(wrappedPoint);
            }
        }
    }
}
