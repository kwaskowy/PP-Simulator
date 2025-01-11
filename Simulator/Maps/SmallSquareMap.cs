namespace Simulator.Maps;
/// <summary>
/// Represents a map with square boundaries.
/// </summary>
public class SmallSquareMap : Map
{
    public int Size { get; }
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

    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);
        return Exist(next) ? next : p;
    }
    public override Point NextDiagonal(Point p, Direction d)
    {
        Point next = p.NextDiagonal(d);
        return Exist(next) ? next : p;
    }
}
