namespace Avalanche;

public interface IPipeline
{
    Task<PipelineResult> ExecuteAsync(Guid uniqueId, string name, Stream baseStream);
}