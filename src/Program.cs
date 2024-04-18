using CommandLine;
using Nest;

namespace esclient;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Try to parsing");
        Parser.Default.ParseArguments<Es1>(args)
            .WithParsed(opts =>
            {
                Console.WriteLine("Try to connect to ES");
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("test_index")
                .BasicAuthentication(opts.Username, opts.Password);

                var client = new ElasticClient(settings);

                Console.WriteLine("Connected!");

                // Indeksiranje dokumenta
                Console.WriteLine("Try to indexing or searching");
                Console.WriteLine($"Name is {opts.Name}");
                if (opts.Name != null)
                {
                    if (opts.Insert)
                    {
                        var response = client.IndexDocument(new { Name = opts.Name, Age = opts.Age });
                        Console.WriteLine("Indexed");
                    }
                    else if (opts.Search)
                    {
                        Console.WriteLine($"Option is {opts.Search} I'll try to search");

                        var searchResponse = client.Search<Person>(s => s
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.Name)
                                    .Query(opts.Name))));

                        Console.WriteLine(("Results"));

                        foreach (var hit in searchResponse.Hits)
                        {
                            Console.WriteLine($"Name> {hit.Source.Name} --- Age:{hit.Source.Age} --- Index:{hit.Index} --- ES Id:{hit.Id}");
                        }
                    }
                }
            })
            .WithNotParsed(errs => Console.WriteLine(errs.ToString()));
    }
}
