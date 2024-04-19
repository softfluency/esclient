using CommandLine;
using Nest;
using Elasticsearch.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System;

namespace esclient;

internal class Program
{
    static async Task Main(string[] args)
    {
        Parser.Default.ParseArguments<EsOptions>(args)
            .WithParsed(async opts =>
            {
                var settings = new ConnectionSettings(new Uri(opts.URL))
                    .BasicAuthentication(opts.Username, opts.Password);

                var client = new ElasticClient(settings);

                var response = client.Cat.Indices(descriptor => descriptor.Index(opts.Index));

                if (opts.Availability)
                {
                    Console.WriteLine("Checking Elasticsearch availability...");

                    //var httpClient = new HttpClient();

                    //var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{opts.Username}:{opts.Password}"));
                    //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);

                    //try
                    //{
                    //    var availabilityResponse = await httpClient.GetAsync(opts.URL);

                    //    if (availabilityResponse.IsSuccessStatusCode)
                    //    {
                    //        Console.WriteLine("Elasticsearch is available");
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine($"Failed to connect to Elasticsearch. Status code: {availabilityResponse.StatusCode}");
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine($"An error occurred while connecting to Elasticsearch: {ex.Message}");
                    //}
                }

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
