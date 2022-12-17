namespace Avalanche;

public class PipelineContext
{
    public PipelineContext(Guid uniqueId, string name, Stream baseStream, IReadOnlyDictionary<string, string> metadata)
    {
        UniqueId = uniqueId;
        Name = name;
        BaseStream = baseStream;
        Metadata = metadata;
        Items = new List<IStreamingItem>();
        Failures = new List<PipelineFailure>();
    }

    public Guid UniqueId { get; }
    public string Name { get; }
    public Stream BaseStream { get; }
    public IReadOnlyDictionary<string, string> Metadata { get; }
    public IList<IStreamingItem> Items { get; }
    public IList<PipelineFailure> Failures { get; }
}