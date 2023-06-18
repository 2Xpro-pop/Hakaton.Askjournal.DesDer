using System.Collections;

namespace Destinationosh.Models;

public class FooterCategory : IEnumerable<FooterElement>
{
    public string Name { get; set; }
    public List<FooterElement> FooterElements { get; } = new();

    public IEnumerator<FooterElement> GetEnumerator()
    {
        return FooterElements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return FooterElements.GetEnumerator();
    }
}
