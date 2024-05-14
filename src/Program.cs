using CommandLine;
using esclient.Elastic;

namespace esclient;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<EsOptions.Status, EsOptions.Indices, EsOptions.Index>(args)
            .WithParsed<EsOptions.Status>(opts => ReturnStatus(opts))
            .WithParsed<EsOptions.Indices>(opts => ReturnIndices(opts))
            .WithParsed<EsOptions.Index>(opts => ReturnIndex(opts))
            .WithNotParsed(errs => HandleErrors(errs));
    }

    public static void ReturnStatus(EsOptions.Status opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var status = elasticsearchService.GetServerStatus();

        if (status.IsValid)
        {
            Console.WriteLine("Elasticsearch cluster is up and running");
            Console.WriteLine(status);
        }
        else
        {
            Console.WriteLine(status.OriginalException.Message);
        }
    }

    public static void ReturnIndices(EsOptions.Indices opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndices();

        string[] headers = { "Index", "Health", "Status" };
        IndexesTable.PrintAllIndices(headers, response);
    }

    public static void ReturnIndex(EsOptions.Index opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndex(opts.IndexSearch);

        string[] headers = { "Index", "Health", "Status", "Docs count", "Deleted", "Store size" };
        IndexesTable.PrintSingleIndex(headers, response);
    }

    public static void HandleErrors(IEnumerable<Error> errs)
    {
        foreach (var error in errs)
        {
            Console.WriteLine(error);
        }
    }
}
