using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    public Map Map { get; }
    public List<Creature> Creatures { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished { get; private set; } = false;
    private int _currentMoveIndex = 0;

    public Creature CurrentCreature => Creatures[_currentMoveIndex % Creatures.Count];
    public string CurrentMoveName => Moves[_currentMoveIndex % Moves.Length].ToString().ToLower();

    public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
    {
        if (creatures == null || creatures.Count == 0)
        {
            throw new ArgumentException("The list of creatures cannot be empty.");
        }

        if (creatures.Count != positions.Count)
        {
            throw new ArgumentException("The number of creatures must match the number of positions.");
        }

        Map = map ?? throw new ArgumentNullException(nameof(map));
        Creatures = creatures;
        Positions = new List<Point>(positions);
        Moves = string.Concat(moves.Where(c => "urdlURDL".Contains(c)));
    }

    public void Turn()
    {
        if (Finished)
        {
            throw new InvalidOperationException("The simulation has already finished.");
        }

        var currentCreature = CurrentCreature;
        var currentMove = DirectionParser.Parse(CurrentMoveName).First();

        int index = Creatures.IndexOf(currentCreature);
        var currentPosition = Positions[index];
        var newPosition = Map.Next(currentPosition, currentMove);

        Map.Remove(currentPosition, currentCreature);
        Map.Add(newPosition, currentCreature);

        Positions[index] = newPosition;
        _currentMoveIndex++;

        if (_currentMoveIndex >= Moves.Length)
        {
            Finished = true;
        }
    }

    public void LoadTurn(SimulationTurnLog log)
    {
        // Wyczyszczenie mapy
        foreach (var position in Positions)
        {
            Map.Remove(position, Creatures[Positions.IndexOf(position)]);
        }

        // Odtworzenie mapy z loga
        foreach (var kvp in log.Symbols)
        {
            var point = kvp.Key;
            var symbol = kvp.Value;

            foreach (var creature in Creatures.Where(c => c.Symbol == symbol))
            {
                Map.Add(point, creature);
            }
        }
    }
}
