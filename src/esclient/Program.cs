using CommandLine;
using esclient.Elastic;

namespace esclient;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<EsOptions.Status, EsOptions.Indices, EsOptions.Index>(args)
            .WithParsed<EsOptions.Status>(ReturnStatus)
            .WithParsed<EsOptions.Indices>(ReturnIndices)
            .WithParsed<EsOptions.Index>(ReturnIndex);
    }

    static void ReturnStatus(EsOptions.Status opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var status = elasticsearchService.GetServerStatus();

        if (status.IsValid)
        {
            Console.WriteLine("Elasticsearch cluster is up and running");            
            Console.WriteLine($"{status.ClusterName} {status.Version.Number}");
        }
        else
        {
            Console.WriteLine("Elasticsearch cluster is not accessible");

            if (status.OriginalException != null)
                Console.WriteLine(status.OriginalException?.Message);
        }
    }

    static void ReturnIndices(EsOptions.Indices opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndices();

        string[] headers = { "Index", "Health", "Status" };
        IndexesTable.PrintAllIndices(headers, response);
    }

    static void ReturnIndex(EsOptions.Index opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndex(opts.IndexSearch);

        string[] headers = { "Index", "Health", "Status", "Docs count", "Deleted", "Store size" };
        IndexesTable.PrintSingleIndex(headers, response);
    }
}