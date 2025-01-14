namespace Simulator;

public class SimulationHistory
{
    private readonly Simulation _simulation;
    private readonly List<TurnSnapshot> _history;

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
        _history = new List<TurnSnapshot>();

        RunSimulation();
    }

    public TurnSnapshot GetTurn(int turnNumber)
    {
        if (turnNumber < 1 || turnNumber > _history.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(turnNumber), "Invalid turn number.");
        }

        return _history[turnNumber - 1];
    }

    private void RunSimulation()
    {
        while (!_simulation.Finished)
        {
            // Zapisz stan przed ruchem.
            var snapshot = new TurnSnapshot(
                turnNumber: _history.Count + 1,
                creature: _simulation.CurrentCreature,
                move: _simulation.CurrentMoveName,
                positions: new Dictionary<Creature, Point>(_simulation.Creatures.Zip(_simulation.Positions, (c, p) => new KeyValuePair<Creature, Point>(c, p)))
            );

            _history.Add(snapshot);

            // Wykonaj ruch.
            _simulation.Turn();
        }
    }

    public class TurnSnapshot
    {
        public int TurnNumber { get; }
        public Creature Creature { get; }
        public string Move { get; }
        public IReadOnlyDictionary<Creature, Point> Positions { get; }

        public TurnSnapshot(int turnNumber, Creature creature, string move, Dictionary<Creature, Point> positions)
        {
            TurnNumber = turnNumber;
            Creature = creature ?? throw new ArgumentNullException(nameof(creature));
            Move = move ?? throw new ArgumentNullException(nameof(move));
            Positions = new Dictionary<Creature, Point>(positions);
        }
    }
}
