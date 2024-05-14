using CommandLine;
using esclient.Elastic;
using Nest;

namespace esclient;

[Verb("status", HelpText = "Returns the status of Elasticsearch.")]
class Status
{
    [Option('u', "url", HelpText = "Uniform Resource Locator")]
    public required string URL { get; set; }
}

[Verb("indices", HelpText = "Returns all of the indices.")]
class Indices
{
    [Option('u', "url", HelpText = "Uniform Resource Locator")]
    public required string URL { get; set; }
}

[Verb("index", HelpText = "Returns entered index.")]
class Index
{
    [Option('u', "url", HelpText = "Uniform Resource Locator")]
    public required string URL { get; set; }

    [Option('i', "index", Required = false, HelpText = "Index")]
    public required string IndexSearch { get; set; }
}

class Program
{
    static int Main(string[] args)
    {
        return Parser.Default.ParseArguments<Status, Indices, Index>(args)
            .MapResult(
                (Status opts) => ReturnStatus(opts),
                (Indices opts) => ReturnIndices(opts),
                (Index opts) => ReturnIndex(opts),
                errs => 1);
    }

    public static int ReturnStatus(Status opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var status = elasticsearchService.GetServerStatus();
        Console.WriteLine(status);
        Console.WriteLine(opts.URL);
        return 0;
    }

    public static int ReturnIndices(Indices opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndices();

        string[] headers = { "Index", "Health", "Status" };
        IndexesTable.PrintAllIndices(headers, response);

        return 0;
    }

    public static int ReturnIndex(Index opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);
        var response = elasticsearchService.GetIndex(opts.IndexSearch);

        string[] headers = { "Index", "Health", "Status", "Docs count", "Deleted", "Store size" };
        IndexesTable.PrintSingleIndex(headers, response);

        return 0;
    }

    private static void OptionsParse(EsOptions opts)
    {
        var elasticsearchService = new ElasticsearchService(opts.URL);

        var status = elasticsearchService.GetServerStatus();
        var isValid = status.IsValid;
        if (isValid)
        {
            Console.WriteLine("Elasticsearch cluster is up and running");
        }
        else
        {
            Console.WriteLine(status.ServerError);
        }

        var response = opts.Index == null 
            ? elasticsearchService.GetIndices() 
            : elasticsearchService.GetIndex(opts.Index);

        if (!response.IsValid)
        {
            Console.WriteLine($"Error: {response.OriginalException}");
            return;
        }

        if (opts.Index == null)
        {
            string[] headers = { "Index", "Health", "Status" };
            IndexesTable.PrintAllIndices(headers, response);
        }
        else if (opts.Index != null)
        {
            string[] headers = { "Index", "Health", "Status", "Docs count", "Deleted", "Store size" };
            IndexesTable.PrintSingleIndex(headers, response);
        }
        else
        {
            // -l URL Status
            Console.WriteLine("Elasticsearch cluster is up and running");
        }
    }
}
