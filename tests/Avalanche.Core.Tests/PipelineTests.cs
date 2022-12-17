using Avalanche.Tests.Shared;
using FluentAssertions;

namespace Avalanche.Core.Tests;

public class PipelineTests : AvalancheTestCase
{
    [Fact]
    public async Task ExecuteAsync_WhenUniqueIdIsEmpty_Throws()
    {
        // Arrange
        var pipeline = PipelineFactory.SetupPipeline().Build();

        // Act
        var act = async () =>
            await pipeline.ExecuteAsync(Guid.Empty, "test.csv", TestUtils.GetSharedEmbeddedResource(Employees50));

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithParameterName("uniqueId")
            .WithMessage("Parameter uniqueId can't be an empty Guid*");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task ExecuteAsync_WhenNameIsEmpty_Throws(string actualName)
    {
        // Arrange
        var pipeline = PipelineFactory.SetupPipeline().Build();

        // Act
        var act = async () =>
            await pipeline.ExecuteAsync(Guid.NewGuid(), actualName, TestUtils.GetSharedEmbeddedResource(Employees50));

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("name");
    }

    [Fact]
    public async Task ExecuteAsync_WhenBaseStreamIsNull_Throws()
    {
        // Arrange
        var pipeline = PipelineFactory.SetupPipeline().Build();

        // Act
        var act = async () =>
            await pipeline.ExecuteAsync(Guid.NewGuid(), "test.csv", null!);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("baseStream");
    }
}