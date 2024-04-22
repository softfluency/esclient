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
                var settings = new ConnectionSettings(new Uri(opts.URL!))
                    .BasicAuthentication(opts.Username, opts.Password);

                var client = new ElasticClient(settings);

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
