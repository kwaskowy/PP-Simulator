namespace Simulator;

internal class Creature
{ 
    private string _name = "Unknown";
    private int _level = 1;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != "Unknown") throw new InvalidOperationException("Name can only be set once.");
            value = value.Trim();
            if (value.Length < 3) value = value.PadRight(3, '#');
            if (value.Length > 25) value = value.Substring(0, 25).TrimEnd();
            if (value.Length < 3) value = value.PadRight(3, '#');
            _name = char.ToUpper(value[0]) + value.Substring(1);
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            if (_level != 1) throw new InvalidOperationException("Level can only be set once.");
            _level = Math.Clamp(value, 1, 10);
        }
    }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() { }

    public string Info => $"{Name} [{Level}]";

    public void SayHi()
    {
        Console.WriteLine($"Hi, I am {Name} at level {Level}.");
    }

    public void Upgrade()
    {
        if (Level < 10) _level++;
    }

}