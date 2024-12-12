namespace Simulator;

internal class Creature
{
    private string Name { get; set; }
    private int Level { get; set; }

  
    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    
    public Creature() { }


    public void SayHi()
    {
        Console.WriteLine($"Hi, I am {Name} at level {Level}.");
    }

    public string Info => $"{Name} [{Level}]";
}