namespace Simulator;

public class Animals
{
    private string _description = "Default";

    public required string Description
    {
        get => _description;
        init
        {
            value = value.Trim();
            if (value.Length < 3) value = value.PadRight(3, '#');
            if (value.Length > 15) value = value.Substring(0, 15).TrimEnd();
            if (value.Length < 3) value = value.PadRight(3, '#');
            _description = char.ToUpper(value[0]) + value.Substring(1);
        }
    }
    public uint Size { get; set; } = 3;

    public string Info => $"{Description} <{Size}>";
}
