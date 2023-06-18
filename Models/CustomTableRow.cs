namespace Destinationosh.Models;

public class CustomTableRow : Model
{
    public Guid CustomTableId
    {
        get; set;
    }
    public virtual CustomTable CustomTable
    {
        get; set;
    }

    public virtual IList<CustomValue> CustomValues
    {
        get; set;
    }
}