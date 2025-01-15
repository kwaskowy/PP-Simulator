namespace Simulator;

public class Birds : Animals
{
    public bool CanFly { get; set; }

    public override char Symbol => CanFly ? 'B' : 'S';

    public override string Info
    {
        get
        {
            string flyStatus = CanFly ? "fly+" : "fly-";
            return $"{Description} ({flyStatus}) <{Size}>";
        }
    }
}
