namespace Simulator.Maps;

public class BigMap : Map
{
    public int Width { get; }
    public int Height { get; }
    private readonly Dictionary<Point, List<IMappable>> _map;

    public BigMap(int width, int height)
    {
        if (width <= 0 || width > 1000 || height <= 0 || height > 1000)
        {
            throw new ArgumentOutOfRangeException("Width and height must be between 1 and 1000.");
        }

        Width = width;
        Height = height;
        _map = new Dictionary<Point, List<IMappable>>();
    }

    public override bool Exist(Point p)
    {
        return p.X >= 0 && p.X < Width && p.Y >= 0 && p.Y < Height;
    }

    public override Point Next(Point p, Direction d)
    {
        var nextPoint = p.Next(d);
        return Exist(nextPoint) ? nextPoint : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        var nextPoint = p.NextDiagonal(d);
        return Exist(nextPoint) ? nextPoint : p;
    }

    public override void Add(Point p, IMappable obj)
    {
        if (!Exist(p))
        {
            throw new ArgumentException("Point is out of map bounds.");
        }

        if (!_map.ContainsKey(p))
        {
            _map[p] = new List<IMappable>();
        }

        _map[p].Add(obj);
    }

    public override void Remove(Point p, IMappable obj)
    {
        if (_map.ContainsKey(p))
        {
            _map[p].Remove(obj);

            if (_map[p].Count == 0)
            {
                _map.Remove(p);
            }
        }
    }
    public override List<IMappable> At(Point p)
    {
        return _map.ContainsKey(p) ? _map[p] : new List<IMappable>();
    }



}
