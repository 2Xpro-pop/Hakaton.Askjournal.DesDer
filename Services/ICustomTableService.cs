using DesDer.Models;

namespace DesDer.Services;
public interface ICustomTableService
{
    Task AddField(CustomTable table, CustomField customField);
    Task AddRowToTable(CustomTable table, IDictionary<string, string> keyValuePairs);
    Task CreateTable(CustomTable table);
    IEnumerable<dynamic> GetAllRows(CustomTable customTable);
    dynamic GetAllTabelVms(string culture);
    IQueryable<CustomTable> GetAllTables();
    ValueTask<CustomTable?> GetTableById(Guid id);
    Task RemoveField(CustomField customField);
    Task RemoveField(Guid fieldId);
    Task RemoveRow(CustomTableRow row);
    Task RemoveRow(Guid rowId);
    Task RemoveTable(CustomTable table);
    Task RemoveTable(Guid tableId);
    Task UpdateField(CustomField customField);
    Task UpdateRow(CustomTableRow row);
    Task UpdateTable(CustomTable table);
}