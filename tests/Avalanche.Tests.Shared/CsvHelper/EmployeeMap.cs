using Avalanche.Tests.Shared.Models;
using CsvHelper.Configuration;

namespace Avalanche.Tests.Shared.CsvHelper;

public sealed class EmployeeMap : ClassMap<Employee>
{
    public EmployeeMap()
    {
        Map(m => m.FirstName).Index(0);
        Map(m => m.LastName).Index(1);
        Map(m => m.Email).Index(2);
        Map(m => m.Phone).Index(3);
    }
}