namespace Avalanche.Entities;

public class StreamingItem
{
    public Guid Id { get; set; }
    public Guid StreamingId { get; set; }
    public bool Processed { get; set; }
    public int Index { get; set; }
    public string? Content { get; set; }
    public string? Error { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual Streaming? Streaming { get; set; }
}