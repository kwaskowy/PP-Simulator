namespace Simulator.Maps;

public class BigTorusMap : BigMap
{
    public BigTorusMap(int width, int height) : base(width, height) { }

    public override Point Next(Point p, Direction d)
    {
        var next = p.Next(d);
        int x = (next.X + Width) % Width;
        int y = (next.Y + Height) % Height;
        return new Point(x, y);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        var next = p.NextDiagonal(d);
        int x = (next.X + Width) % Width;
        int y = (next.Y + Height) % Height;
        return new Point(x, y);
    }
}
