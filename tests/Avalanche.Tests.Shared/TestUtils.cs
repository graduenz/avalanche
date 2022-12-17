using System.Reflection;

namespace Avalanche.Tests.Shared;

public static class TestUtils
{
    public static Stream GetSharedEmbeddedResource(string partialResourceName)
    {
        var assembly = typeof(TestUtils).Assembly;
        var resourceName = $"Avalanche.Tests.Shared.Resources.{partialResourceName}";

        return assembly.GetManifestResourceStream(resourceName) ??
               throw new ArgumentException($"Embedded resource {resourceName} was not found",
                   nameof(partialResourceName));
    }
}