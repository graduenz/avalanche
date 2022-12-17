namespace Avalanche;

public class PipelineFailure
{
    public string? Message { get; init; }

    public static implicit operator PipelineFailure(string message) =>
        new() { Message = message };
}