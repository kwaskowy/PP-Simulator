namespace Simulator.Maps;

public class BigBounceMap : BigMap
{
    public BigBounceMap(int width, int height) : base(width, height) { }

    public override Point Next(Point p, Direction d)
    {
        var nextPoint = p.Next(d);

        if (Exist(nextPoint))
        {
            return nextPoint;
        }

        // Odbicie
        return p.Next(OppositeDirection(d));
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        var nextPoint = p.NextDiagonal(d);

        if (Exist(nextPoint))
        {
            return nextPoint;
        }

        // Odbicie
        return p.NextDiagonal(OppositeDirection(d));
    }

    private static Direction OppositeDirection(Direction d)
    {
        return d switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => d
        };
    }
}
