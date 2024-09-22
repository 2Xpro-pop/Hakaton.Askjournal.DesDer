using System.Dynamic;
using DesDer.Models;
using DesDer.Models.DesDer2;
using Microsoft.EntityFrameworkCore;

namespace DesDer.Services;

public class CustomTableService : ICustomTableService
{
    private readonly ApplicationContext _context;

    public CustomTableService(ApplicationContext context)
    {
        _context = context;
    }
    public IQueryable<CustomTable> GetAllTables() => _context.CustomTables;
    public IEnumerable<dynamic> GetAllRows(CustomTable customTable)
    {
        foreach (var row in customTable.CustomTableRows)
        {
            dynamic dynamicRow = new ExpandoObject();
            var dicRow = row as IDictionary<string, object>;

            foreach (var value in row.CustomValues)
            {
                var fieldName = value.CustomField.EnName;
                dicRow[fieldName] = value.Value;
            }
            dynamicRow.Id = row.Id;
            yield return dynamicRow;
        }
    }

    public ValueTask<CustomTable?> GetTableById(Guid id) => _context.CustomTables.FindAsync(id);

    public async Task AddRowToTable(CustomTable table, IDictionary<string, string> keyValuePairs)
    {
        var row = new CustomTableRow
        {
            CustomTable = table,
            CustomValues = new List<CustomValue>()
        };

        foreach (var field in table.CustomFields)
        {
            var value = new CustomValue()
            {
                CustomField = field,
                Value = keyValuePairs[field.EnName],
                CustomTableRow = row,
            };

            row.CustomValues.Add(value);
        }

        await _context.AddAsync(row);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateRow(CustomTableRow row)
    {
        _context.Update(row);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveRow(CustomTableRow row)
    {
        _context.Remove(row);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveRow(Guid rowId)
    {
        var row = await _context.CustomTableRows.FindAsync(rowId);
        await RemoveRow(row);
    }

    public async Task AddField(CustomTable table, CustomField customField)
    {
        _context.Attach(table);
        table.CustomFields.Add(customField);

        await _context.SaveChangesAsync();
    }
    public async Task UpdateField(CustomField customField)
    {
        _context.Update(customField);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveField(CustomField customField)
    {
        _context.Remove(customField);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveField(Guid fieldId)
    {
        var field = await _context.CustomFields.FindAsync(fieldId);
        await RemoveField(field);
    }

    public async Task CreateTable(CustomTable table)
    {
        _context.Add(table);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateTable(CustomTable table)
    {
        _context.Update(table);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveTable(CustomTable table)
    {
        _context.Remove(table);
        await _context.SaveChangesAsync();
    }
    public async Task RemoveTable(Guid tableId)
    {
        var table = await _context.CustomTables.FindAsync(tableId);
        await RemoveTable(table);
    }

    public dynamic GetAllTabelVms(string culture)
    {
        var list = new List<dynamic>();

        foreach (var customTable in _context.CustomTables.Include(x => x.CustomFields)
                                                        .Include(x => x.CustomTableRows)
                                                        .ThenInclude(x => x.CustomValues))
        {
            var headers = new List<string>();
            var data = new List<List<string>>();

            foreach (var field in customTable.CustomFields)
            {
                switch (culture)
                {
                    case "ru":
                        headers.Add(field.RuName);
                        break;
                    case "en":
                        headers.Add(field.EnName);
                        break;
                    case "kg":
                        headers.Add(field.KgName);
                        break;
                }
            }

            foreach (var rows in customTable.CustomTableRows)
            {
                var values = new List<string>();
                foreach(var row in rows.CustomValues)
                {
                    values.Add(row.Value);
                }
                data.Add(values);
            }

            list.Add(new
            {
                id = customTable.Id,
                headers = headers,
                data = data,
            });
        }

        return list;
    }
}
