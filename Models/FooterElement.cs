namespace DesDer.Models;

public class FooterElement
{
    public Dictionary<string, string> Names { get; set; } = new();
    public string Link { get; set; } = null!;
    public string this[string language]
    {
        get
        {
            return Names[language];
        }
        set
        {
            Names[language] = value;
        }
    }
}
