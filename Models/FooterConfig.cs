namespace Destinationosh.Models;

public class FooterConfig
{
    public List<FooterCategory> FooterCategories { get; set; } = new();
    public Dictionary<string, string> Descriptions { get; set; } = new();
}
