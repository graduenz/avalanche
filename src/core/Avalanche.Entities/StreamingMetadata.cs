namespace Avalanche.Entities;

public class StreamingMetadata
{
    public Guid StreamingId { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
    
    public virtual Streaming? Streaming { get; set; }
}