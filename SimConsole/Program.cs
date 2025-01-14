﻿using System.Text;
using SimConsole;
using Simulator;
using Simulator.Maps;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var map = new BigBounceMap(8, 6);

        var creatures = new List<Creature>
        {
            new Elf("Elandor"),
            new Orc("Gorbag")
        };

        var animals = new List<IMappable>
        {
            new Animals { Description = "Rabbits", Size = 10 },
            new Birds { Description = "Eagles", Size = 5, CanFly = true },
            new Birds { Description = "Ostriches", Size = 3, CanFly = false }
        };

        // Pozycje początkowe
        var positions = new List<Point>
        {
            new Point(2, 2), // Elf
            new Point(5, 3), // Orc
            new Point(3, 4), // Rabbits
            new Point(0, 0), // Eagles
            new Point(7, 5)  // Ostriches
        };

        // Dodanie stworzeń i zwierząt na mapę
        for (int i = 0; i < positions.Count; i++)
        {
            if (i < creatures.Count)
                map.Add(positions[i], creatures[i]);
            else
                map.Add(positions[i], animals[i - creatures.Count]);
        }

        string moves = "ruldldrruudurdlldluur";

        var simulation = new Simulation(map, creatures, positions.Take(creatures.Count).ToList(), moves);
        var simulationHistory = new SimulationHistory(simulation);

        // Wyświetl wybrane tury: 5, 10, 15, 20
        int[] turnsToDisplay = { 5, 10, 15, 20 };
        foreach (int turn in turnsToDisplay)
        {
            Console.Clear();
            var snapshot = simulationHistory.GetTurn(turn);
            Console.WriteLine($"Turn {snapshot.TurnNumber}:");
            Console.WriteLine($"Creature: {snapshot.Creature.Name} moves {snapshot.Move}");

            // Odtwórz stan mapy na podstawie tury
            simulation.LoadTurn(snapshot);

            var visualizer = new MapVisualizer(map, simulation);
            visualizer.Draw();

            Console.WriteLine("\nPress any key to see the next turn...");
            Console.ReadKey();
        }

        Console.WriteLine("Finished displaying selected turns.");
    }
}
