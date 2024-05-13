using CommandLine;
using esclient.Elastic;

namespace esclient;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<EsOptions>(args)
            .WithParsed(opts =>
            {
                OptionsParse(args, opts);
            })
            .WithNotParsed(errs => Console.Write(""));
    }

    private static void OptionsParse(string[] args, EsOptions opts)
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

        if (opts.Index == null && args.Length == 3)
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
