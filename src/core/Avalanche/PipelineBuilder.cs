namespace Avalanche;

public class PipelineBuilder : IPipelineBuilder
{
    private readonly List<IPipelineOperation> _operations = new();
    private readonly Dictionary<string, string> _metadata = new();

    public IPipelineBuilder WithOperation<T>(T operation) where T : IPipelineOperation
    {
        if (operation == null)
            throw new ArgumentNullException(nameof(operation));
        
        _operations.Add(operation);

        return this;
    }

    public IPipelineBuilder WithMetadata(string name, string value)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        _metadata[name] = value;
        return this;
    }

    public IPipeline Build()
    {
        return new Pipeline(_operations, _metadata);
    }
}