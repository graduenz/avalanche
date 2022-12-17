namespace Avalanche.Entities;

public class Streaming
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool Processed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual List<StreamingItem>? Items { get; set; }
    public virtual List<StreamingMetadata>? Metadata { get; set; }
}