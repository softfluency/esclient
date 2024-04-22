using CommandLine;
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

                    if (response.IsValid && opts.Index == null && args.Count() == 3)
                    {
                        foreach (var index in response.Records)
                        {
                            Console.WriteLine($"Index: {index.Index}, Health: {index.Health}, Status: {index.Status}, Docs count: {index.DocsCount}");
                        }
                    }
                    else if (response.IsValid && opts.Index != null && !string.IsNullOrEmpty(opts.Index))
                    {
                        foreach (var index in response.Records)
                        {
                            Console.WriteLine($"Index: {index.Index},\nHealth: {index.Health},\nStatus: {index.Status},\nDocs count: {index.DocsCount},\nDeleted: {index.DocsDeleted},\nStore size: {index.StoreSize}");
                        }
                    }
                    else if (response.IsValid && opts.Index == null)
                    {
                        if (opts.URL != null)
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
