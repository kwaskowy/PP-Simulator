namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }
    private readonly Dictionary<Point, List<IMappable>> _map = new();

    public override int SizeX => Size;
    public override int SizeY => Size;

    public SmallSquareMap(int size)
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

    public override Point Next(Point p, Direction d) => p.Next(d);

    public override Point NextDiagonal(Point p, Direction d) => p.NextDiagonal(d);

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
