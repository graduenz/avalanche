using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Avalanche.CsvHelper;

public class CsvHelperParsingOperation<TModel, TClassMap> : IPipelineOperation
    where TModel : IStreamingItem
    where TClassMap : ClassMap<TModel>
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
        csv.Context.RegisterClassMap<TClassMap>();
        
        context.Items.Clear();

        var records = csv.GetRecords<TModel>();
        foreach (var record in records)
            context.Items.Add(record);

        await next(context);
    }
}