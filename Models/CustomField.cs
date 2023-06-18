namespace Destinationosh.Models;

public class CustomField : Model
{
    public string RuName
    {
        get; set;
    }

    public string KgName
    {
        get; set;
    }

    public string EnName
    {
        get; set;
    }


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
