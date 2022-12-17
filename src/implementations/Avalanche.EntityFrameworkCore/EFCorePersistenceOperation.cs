using System.Text.Json;
using Avalanche.Entities;
using Microsoft.EntityFrameworkCore;

namespace Avalanche.EntityFrameworkCore;

// ReSharper disable once InconsistentNaming
public class EFCorePersistenceOperation<TDbContext> : IPipelineOperation
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private readonly bool _shouldSaveChanges;

    public EFCorePersistenceOperation(TDbContext dbContext, bool shouldSaveChanges)
    {
        _dbContext = dbContext;
        _shouldSaveChanges = shouldSaveChanges;
    }

    public async Task RunAsync(PipelineContext context, PipelineOperationDelegate next)
    {
        var now = DateTime.Now;
        
        await AddStreamingAsync(context, now);
        await AddItemsAsync(context, now);
        await AddMetadataAsync(context);

        if (_shouldSaveChanges)
            await _dbContext.SaveChangesAsync();

        await next(context);
    }

    private async Task AddStreamingAsync(PipelineContext context, DateTime now) =>
        await _dbContext.Set<Streaming>().AddAsync(new Streaming
        {
            Id = context.UniqueId,
            Name = context.Name,
            Processed = false,
            CreatedAt = now,
            UpdatedAt = now
        });

    private async Task AddItemsAsync(PipelineContext context, DateTime now)
    {
        var index = 0;
        foreach (var item in context.Items)
        {
            var json = JsonSerializer.Serialize(item);
            await _dbContext.Set<StreamingItem>().AddAsync(new StreamingItem
            {
                Id = Guid.NewGuid(),
                StreamingId = context.UniqueId,
                Processed = false,
                Content = json,
                Error = null,
                Index = index++,
                CreatedAt = now,
                UpdatedAt = now
            });
        }
    }

    private async Task AddMetadataAsync(PipelineContext context)
    {
        foreach (var entry in context.Metadata)
            await _dbContext.Set<StreamingMetadata>().AddAsync(new StreamingMetadata
            {
                StreamingId = context.UniqueId,
                Name = entry.Key,
                Value = entry.Value
            });
    }
}