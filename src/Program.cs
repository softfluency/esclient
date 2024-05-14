using CommandLine;
using esclient.Elastic;

namespace esclient;

class Program
{
    static int Main(string[] args)
    {
        return Parser.Default.ParseArguments<EsOptions.Status, EsOptions.Indices, EsOptions.Index>(args)
            .MapResult(
                (EsOptions.Status opts) => ReturnStatus(opts),
                (EsOptions.Indices opts) => ReturnIndices(opts),
                (EsOptions.Index opts) => ReturnIndex(opts),
                errs => 1);
    }

    public static int ReturnStatus(EsOptions.Status opts)
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

        return 0;
    }

    public static int ReturnIndices(EsOptions.Indices opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndices();

        string[] headers = { "Index", "Health", "Status" };
        IndexesTable.PrintAllIndices(headers, response);

        return 0;
    }

    public static int ReturnIndex(EsOptions.Index opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndex(opts.IndexSearch);

        string[] headers = { "Index", "Health", "Status", "Docs count", "Deleted", "Store size" };
        IndexesTable.PrintSingleIndex(headers, response);

        return 0;
    }
}
