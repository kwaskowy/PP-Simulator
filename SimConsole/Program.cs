using System.Text;
using Simulator;
using Simulator.Maps;
using SimConsole;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var map = new SmallSquareMap(5);

        var creatures = new List<Creature>
        {
            new Orc("Gorbag"),
            new Elf("Elandor")
        };

        var positions = new List<Point>
        {
            new Point(2, 2),
            new Point(3, 1)
        };

        string moves = "dlru";

        var simulation = new Simulation(map, creatures, positions, moves);
        var visualizer = new MapVisualizer(map, simulation);


        while (!simulation.Finished)
        {
            visualizer.Draw();
            Console.WriteLine($"Turn: {simulation.CurrentCreature.Name} moves {simulation.CurrentMoveName}");
            simulation.Turn();
            Thread.Sleep(1000);
        }

        visualizer.Draw();
        Console.WriteLine("Simulation finished!");
    }
}
