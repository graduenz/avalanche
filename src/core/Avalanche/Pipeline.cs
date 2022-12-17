namespace Avalanche;

public class Pipeline : IPipeline
{
    private readonly IEnumerable<IPipelineOperation> _operations;
    private readonly IReadOnlyDictionary<string, string> _metadata;

    public Pipeline(IEnumerable<IPipelineOperation> operations, IReadOnlyDictionary<string, string> metadata)
    {
        _operations = operations;
        _metadata = metadata;
    }

    public Task<PipelineResult> ExecuteAsync(Guid uniqueId, string name, Stream baseStream)
    {
        if (uniqueId == Guid.Empty)
            throw new ArgumentException($"Parameter {nameof(uniqueId)} can't be an empty Guid", nameof(uniqueId));

        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (baseStream == null)
            throw new ArgumentNullException(nameof(baseStream));

        return ExecuteInternalAsync(uniqueId, name, baseStream);
    }

    private async Task<PipelineResult> ExecuteInternalAsync(Guid uniqueId, string name, Stream baseStream)
    {
        var operationStack = new Stack<PipelineOperationDelegate>();
        operationStack.Push(_ => Task.CompletedTask);
        
        foreach (var operation in _operations.Reverse())
            operationStack.Push(context => operation.RunAsync(context, operationStack.Pop()));

        var context = new PipelineContext(uniqueId, name, baseStream, _metadata);
        await operationStack.Pop()(context);

        return PipelineResult.FromContext(context);
    }
}