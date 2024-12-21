using Simulator.Maps;

namespace Simulator;

internal class Program
{
    static void Lab5a()
    {
        try
        {
            Rectangle rect1 = new Rectangle(3, 4, 7, 8);
            Console.WriteLine(rect1);

            Rectangle rect2 = new Rectangle(new Point(10, 15), new Point(5, 20));
            Console.WriteLine(rect2);

            Rectangle invalidRect = new Rectangle(5, 5, 5, 10);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Rectangle rect3 = new Rectangle(0, 0, 10, 10);
        Console.WriteLine(rect3.Contains(new Point(5, 5)));
        Console.WriteLine(rect3.Contains(new Point(15, 5)));
        Console.WriteLine(rect3.Contains(new Point(10, 10)));
    }
    static void Lab5b()
    {
        try
        {
            SmallSquareMap map = new SmallSquareMap(10);
            Point p = new Point(5, 5);

            Console.WriteLine(map.Exist(p));
            Console.WriteLine(map.Next(p, Direction.Right));
            Console.WriteLine(map.Next(p, Direction.Up));
            Console.WriteLine(map.Next(new Point(9, 9), Direction.Right));
            Console.WriteLine(map.NextDiagonal(p, Direction.Up));

            SmallSquareMap invalidMap = new SmallSquareMap(25);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Lab5a();
        Lab5b();
    }
}
