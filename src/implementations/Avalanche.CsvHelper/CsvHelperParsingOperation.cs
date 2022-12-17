using System.Globalization;
using CsvHelper;

namespace Avalanche.CsvHelper;

public class CsvHelperParsingOperation<TModel> : IPipelineOperation
    where TModel : IStreamingItem
{
    private readonly Func<StreamReader, CsvReader> _csvReaderFactory;

    public CsvHelperParsingOperation() : this(reader => new CsvReader(reader, CultureInfo.InvariantCulture))
    {
    }

    public CsvHelperParsingOperation(Func<StreamReader, CsvReader> csvReaderFactory)
    {
        _csvReaderFactory = csvReaderFactory;
    }

    public async Task RunAsync(PipelineContext context, PipelineOperationDelegate next)
    {
        context.BaseStream.Seek(0, SeekOrigin.Begin);
        
        using var reader = new StreamReader(context.BaseStream, leaveOpen: true);
        using var csv = _csvReaderFactory(reader);
        var records = csv.GetRecords<TModel>();

        context.Items.Clear();
        foreach (var record in records)
            context.Items.Add(record);

        await next(context);
    }
}