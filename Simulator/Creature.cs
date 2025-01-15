namespace Simulator;

public abstract class Creature : IMappable
{
    private string _name = "Unknown";
    private int _level = 1;

    /// <summary>
    /// Symbol reprezentujący stwora na mapie.
    /// </summary>
    public abstract char Symbol { get; }

    public string Name
    {
        get => _name;
        set
        {
            if (_name != "Unknown") throw new InvalidOperationException("Name can only be set once.");
            _name = Validator.Shortener(value, 3, 25, '#');
            _name = char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            if (_level != 1) throw new InvalidOperationException("Level can only be set once.");
            _level = Validator.Limiter(value, 1, 10);
        }
    }

    public abstract int Power { get; }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() { }

    public abstract string Info { get; }

    /// <summary>
    /// Zwraca opis stwora.
    /// </summary>
    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }

    public virtual void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, and my level is {Level}.");
    }

    public void Upgrade()
    {
        if (Level < 10) _level++;
    }

    public void Go(Direction direction)
    {
        Console.WriteLine($"{Name} goes {direction.ToString().ToLower()}.");
    }

    public void Go(Direction[] directions)
    {
        foreach (var direction in directions)
        {
            Go(direction);
        }
    }

    public void Go(string input)
    {
        var directions = DirectionParser.Parse(input);
        Go(directions);
    }

    /// <summary>
    /// Zwraca opis do wizualizacji.
    /// </summary>
    public string Description => $"{Name} (Level {Level})";
}
