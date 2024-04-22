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
                //var esUrl = "https://elastic:KLY*E6yJ4YeD47nivktw@194.146.57.203:9200";

                var esUrl = opts.URL;
                var server = new Uri(esUrl);

                var conn = new ConnectionSettings(server);

                //var settings = new ConnectionSettings(new Uri(opts.URL!))
                //    .BasicAuthentication(opts.Username, opts.Password);

                conn.EnableHttpCompression();                
                conn.ConnectionLimit(-1);

                var client = new ElasticClient(conn);

                var response = client.Cat.Indices(descriptor => descriptor.Index(opts.Index));

                if (response.IsValid && opts.Index != null)
                {
                    foreach (var index in response.Records)
                    {
                        Console.WriteLine($"Index: {index.Index},\nHealth: {index.Health},\nStatus: {index.Status},\nDocs count: {index.DocsCount},\nDeleted: {index.DocsDeleted},\nStore size: {index.StoreSize}");
                    }
                }
                else if (response.IsValid)
                {
                    foreach (var index in response.Records)
                    {
                        Console.WriteLine($"Index: {index.Index}, Health: {index.Health}, Status: {index.Status}, Docs count: {index.DocsCount}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.OriginalException}");
                }
            })
            .WithNotParsed(errs => Console.WriteLine(errs.ToString()));
    }
}
