using Es1Class;
using Nest;
using CommandLine;

namespace es1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Try to parsing");
            Parser.Default.ParseArguments<Es1>(args)
                .WithParsed(opts =>
                {
                    Console.WriteLine("Parsed!");
                    Console.WriteLine("Try to connect to ES");
                    var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("test_index")
                    .BasicAuthentication(opts.username, opts.password);

                    var client = new ElasticClient(settings);

                    Console.WriteLine("Connected!");

                    // Indeksiranje dokumenta
                    Console.WriteLine("Try to indexing or searching");
                    Console.WriteLine($"Name is {opts.name}");
                    if ( opts.name != null )
                    {
                        if ( opts.insert )
                        {
                            Console.WriteLine("Inserting");
                            var response = client.IndexDocument(new { Name = opts.name, Age = opts.age });
                            Console.WriteLine("Indexed");
                        }
                        else if ( opts.search)
                        {
                            Console.WriteLine($"Searching {opts.name}");
                            Console.WriteLine($"Option is {opts.search} I'll try to search");

                            var searchResponse = client.Search<Person>(s => s
                            .Query(q => q
                                .Match(m => m
                                    .Field(f => f.Name)
                                        .Query(opts.name)
                                        )
                                    )
                            );
                            Console.WriteLine(("Results"));

                            foreach (var hit in searchResponse.Hits)
                            {
                                Console.WriteLine($"Name: {hit.Source.Name} --- Age:{hit.Source.Age} --- Source: {hit.Source} ---  Index:{hit.Index} --- ES Id:{hit.Id} --- Explanation: {hit.Explanation}");
                            }
                        }
                    }
                })
                .WithNotParsed(errs => Console.WriteLine(errs.ToString()));
        }
    }
}
