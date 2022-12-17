namespace Avalanche;

public class PipelineFactory : IPipelineFactory
{
    public IPipelineBuilder SetupPipeline() => new PipelineBuilder();
}