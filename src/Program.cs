using CommandLine;
using ConsoleTables;
using Nest;

namespace esclient;

internal class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<EsOptions>(args)
            .WithParsed(opts =>
            {
                var esUrl = opts.URL;

                if (esUrl != null)
                {
                    var server = new Uri(esUrl);

                    var conn = new ConnectionSettings(server);

                    conn.EnableHttpCompression();
                    conn.ConnectionLimit(-1);

                    var client = new ElasticClient(conn);
                    var response = client.Cat.Indices(descriptor => descriptor.Index(opts.Index));

                    if (response.IsValid && opts.Index == null && args.Length == 3)
                    {
                        var indexesTable = new ConsoleTable("Index", "Health", "Status");
                        foreach (var index in response.Records)
                        {
                            indexesTable.AddRow(index.Index, index.Health, index.Status);
                        }
                        indexesTable.Write();
                    }
                    else if (response.IsValid && opts.Index != null)
                    {
                        var indexTable = new ConsoleTable("Index", "Health", "Status", "Docs count", "Deleted", "Store size");
                        indexTable.Options.EnableCount = false;
                        foreach (var index in response.Records)
                        {
                            indexTable.AddRow(index.Index, index.Health, index.Status, index.DocsCount, index.DocsDeleted, index.StoreSize);
                        }
                        indexTable.Write();
                    }
                    else if (response.IsValid && opts.Index == null)
                    {
                        var resp = client.Ping();
                        if (resp.IsValid)
                        {
                            Console.WriteLine("Elasticsearch cluster is up and running");
                        }
                        else
                        {
                            Console.WriteLine(resp.ServerError);
                            Console.WriteLine(resp.OriginalException.ToString());
                            Console.WriteLine("Elasticsearch cluster is down");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.OriginalException}");
                    }
                }
            })
            .WithNotParsed(errs => Console.WriteLine(errs.ToString()));
    }
}
