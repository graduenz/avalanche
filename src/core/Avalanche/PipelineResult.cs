namespace Avalanche;

public class PipelineResult
{
    public static PipelineResult FromContext(PipelineContext context) =>
        new(context.Items, context.Failures);

    private PipelineResult(
        IEnumerable<IStreamingItem> items,
        IEnumerable<PipelineFailure> failures)
    {
        Items = items;
        Failures = failures;
    }
    
    public bool Success => Failures.Any() is false;
    public IEnumerable<IStreamingItem> Items { get; }
    public IEnumerable<PipelineFailure> Failures { get; }
}