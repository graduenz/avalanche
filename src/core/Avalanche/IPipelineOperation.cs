namespace Avalanche;

public interface IPipelineOperation
{
    Task RunAsync(PipelineContext context, PipelineOperationDelegate next);
}