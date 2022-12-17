using Avalanche.Tests.Shared;
using Avalanche.Tests.Shared.CsvHelper;
using Avalanche.Tests.Shared.Models;
using FluentAssertions;

namespace Avalanche.CsvHelper.Tests;

public class CsvHelperParsingOperationTests : AvalancheTestCase
{
    [Fact]
    public async Task ExecuteAsync_WhenCsvIsValid_SetsParsedItemsToContext()
    {
        // Arrange
        var pipeline = PipelineFactory.SetupPipeline()
            .WithOperation(new CsvHelperParsingOperation<Employee, EmployeeMap>())
            .Build();

        var baseStream = TestUtils.GetSharedEmbeddedResource(Employees50);

        var expectedItems = ParseCsvManually(baseStream, ',');

        // Act
        var result = await pipeline.ExecuteAsync(Guid.NewGuid(), "test.csv", baseStream);

        // Assert
        result.Should().NotBeNull();
        result.Failures.Should().BeEmpty();
        result.Success.Should().BeTrue();
        
        result.Items.Should().HaveCount(50);
        result.Items.Should().BeEquivalentTo(expectedItems);
    }

    private static IList<Employee> ParseCsvManually(Stream baseStream, char delimiter)
    {
        baseStream.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(baseStream, leaveOpen: true);
        
        var csvText = reader.ReadToEnd();
        var csvLines = csvText.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);

        return csvLines.Skip(1).Select(line =>
        {
            var columns = line.Split(delimiter);
            return new Employee
            {
                FirstName = columns[0],
                LastName = columns[1],
                Email = columns[2],
                Phone = columns[3]
            };
        }).ToList();
    }
}