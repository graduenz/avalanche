using Avalanche;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAvalanche(this IServiceCollection services) => services
        .AddScoped<IPipelineFactory, PipelineFactory>();
}