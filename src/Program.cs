using CommandLine;
using Nest;
using Elasticsearch.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace esclient;

internal class Program
{
    static async Task Main(string[] args)
    {
        // Testiranje stanja indeksa uz argumente
        // Primer: dotnet run "http://10.7.7.2:9200/" "elastic" "DwzU0n-AD2RYuOd3MwZV" "dt_prilozi"

        //var settings = new ConnectionSettings(new Uri(args[0])
        //    .BasicAuthentication(args[1], args[2]);

        //var client = new ElasticClient(settings);

        //var response = client.Cat.Indices(descriptor => descriptor.Index(args[3]));

        //if (response.IsValid)
        //{
        //    foreach (var index in response.Records)
        //    {
        //        Console.WriteLine($"Index: {index.Index}, Health: {index.Health}, Status: {index.Status}, Docs count: {index.DocsCount}");
        //    }
        //}
        //else
        //{
        //    Console.WriteLine($"Error: {response.OriginalException}");
        //}

        // Provera dostupnosti ES

        //var elasticUrl = "http://10.7.7.2:9200/";
        //var elasticUsername = "elastic";
        //var elasticPassword = "DwzU0n-AD2RYuOd3MwZV";

        //var httpClient = new HttpClient();

        //var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{elasticUsername}:{elasticPassword}"));
        //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);

        //try
        //{
        //    var response = await httpClient.GetAsync(elasticUrl);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        Console.WriteLine("Elasticsearch is available");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Failed to connect to Elasticsearch. Status code: {response.StatusCode}");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred while connecting to Elasticsearch: {ex.Message}");
        //}

        // Opcije

        Parser.Default.ParseArguments<EsOptions>(args)
            .WithParsed(opts =>
            {
                var settings = new ConnectionSettings(new Uri(opts.URL))
                    .BasicAuthentication(opts.Username, opts.Password);

                var client = new ElasticClient(settings);

                var response = client.Cat.Indices(descriptor => descriptor.Index(opts.Index));

                if (response.IsValid)
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

                //    if (opts.Name != null)
                //    {
                //        if (opts.Insert)
                //        {
                //            var response = client.IndexDocument(new { Name = opts.Name, Age = opts.Age });
                //            Console.WriteLine("Indexed");
                //        }
                //        else if (opts.Search)
                //        {
                //            Console.WriteLine($"Option is {opts.Search} I'll try to search");

                //            var searchResponse = client.Search<Person>(s => s
                //            .Query(q => q
                //                .Match(m => m
                //                    .Field(f => f.Name)
                //                        .Query(opts.Name))));

                //            Console.WriteLine(("Results"));

                //            foreach (var hit in searchResponse.Hits)
                //            {
                //                Console.WriteLine($"Name> {hit.Source.Name} --- Age:{hit.Source.Age} --- Index:{hit.Index} --- ES Id:{hit.Id}");
                //            }
                //        }
                //    }
                //})
                //.WithNotParsed(errs => Console.WriteLine(errs.ToString()));
            });
    }
}
