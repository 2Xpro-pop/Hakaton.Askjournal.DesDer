namespace DesDer.Models;

public class CustomTable : Model
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

    public bool IsPublic
    {
        get; set;
    } = true;

    public virtual List<CustomField> CustomFields
    {
        get; set;
    }

    public virtual List<CustomTableRow> CustomTableRows
    {
        get; set;
    }
}