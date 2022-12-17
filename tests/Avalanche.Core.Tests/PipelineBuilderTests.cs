using FluentAssertions;
using Moq;

namespace Avalanche.Core.Tests;

public class PipelineBuilderTests
{
    [Fact]
    public void WithOperation_WhenOperationIsNotNull_DoesNotThrow()
    {
        // Arrange
        var pipelineBuilder = new PipelineBuilder();

        var operationMock = new Mock<IPipelineOperation>();
        
        // Act
        var act = () => pipelineBuilder.WithOperation(operationMock.Object);

        // Assert
        act.Should().NotThrow();
    }
    [Fact]
    public void WithOperation_WhenOperationIsNull_Throws()
    {
        // Arrange
        var pipelineBuilder = new PipelineBuilder();
        
        // Act
        var act = () => pipelineBuilder.WithOperation<IPipelineOperation>(null!);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("operation");
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WithMetadata_WhenNameIsEmpty_Throws(string actualName)
    {
        // Arrange
        var pipelineBuilder = new PipelineBuilder();
        
        // Act
        var act = () => pipelineBuilder.WithMetadata(actualName, "val");

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithParameterName("name");
    }
    
    [Fact]
    public void WithMetadata_WhenNameIsNotEmpty_DoesNotThrow()
    {
        // Arrange
        var pipelineBuilder = new PipelineBuilder();
        
        // Act
        var act = () => pipelineBuilder.WithMetadata("key", "val");

        // Assert
        act.Should().NotThrow();
    }
}