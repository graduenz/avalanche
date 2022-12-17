namespace Avalanche;

public interface IPipelineBuilder
{
    IPipelineBuilder WithOperation<T>(T operation)
        where T : IPipelineOperation;

    IPipelineBuilder WithMetadata(string name, string value);

    IPipeline Build();
}