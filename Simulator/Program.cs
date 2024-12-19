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
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Lab5a();
    }
}
