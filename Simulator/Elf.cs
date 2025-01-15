namespace Simulator;

public class Elf : Creature
{
    private int _agility;
    private int _singCount = 0;

    /// <summary>
    /// Symbol reprezentujący Elfa na mapie.
    /// </summary>
    public override char Symbol => 'E';

    public int Agility
    {
        get => _agility;
        private set => _agility = Validator.Limiter(value, 0, 10);
    }

    public override int Power => 8 * Level + 2 * Agility;

    public override string Info => $"{Name} [{Level}][{Agility}]";

    public Elf(string name = "Unknown", int level = 1, int agility = 1)
        : base(name, level)
    {
        Agility = agility;
    }

    public override void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.");
    }

    public void Sing()
    {
        Console.WriteLine($"{Name} is singing.");
        _singCount++;
        if (_singCount % 3 == 0)
        {
            Agility++;
        }
    }

    public override string ToString()
    {
        return $"{Name} (Agility: {Agility})";
    }
}
