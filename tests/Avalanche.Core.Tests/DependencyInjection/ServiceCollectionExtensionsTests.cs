using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Avalanche.Core.Tests.DependencyInjection;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAvalanche_AddsPackageDependencies()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAvalanche();
        
        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IPipelineFactory>();

        factory.Should().NotBeNull();
    }
}