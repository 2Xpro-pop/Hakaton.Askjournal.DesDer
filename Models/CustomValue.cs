namespace DesDer.Models;

public class CustomValue : Model
{
    public string Value
    {
        get; set;
    }

    public Guid CustomFieldId
    {
        get; set;
    }
    public virtual CustomField CustomField
    {
        get; set;
    } = null!;

    public Guid CustomTableRowId
    {
        get; set;
    }
    public virtual CustomTableRow CustomTableRow
    {
        get; set;
    } = null!;
}
