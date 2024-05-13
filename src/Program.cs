﻿using CommandLine;

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

        var nodesInfo = elasticsearchService.GetNodesInfo();
        Console.WriteLine(String.Join(", ", nodesInfo.Nodes.Select(n => n.Value.Name).ToArray()));

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
            // -l URL -i Print all indexes
            var indexesTable = IndexesTable.CreateTable("Index", "Health", "Status");
            foreach (var index in response.Records)
            {
                indexesTable.AddRow(index.Index, index.Health, index.Status);
            }
            indexesTable.Write();
        }
        else if (opts.Index != null)
        {
            // -l URL -i INDEX Print one index
            var indexTable = IndexesTable.CreateTable("Index", "Health", "Status", "Docs count", "Deleted", "Store size");
            indexTable.Options.EnableCount = false;
            foreach (var index in response.Records)
            {
                indexTable.AddRow(index.Index, index.Health, index.Status, index.DocsCount, index.DocsDeleted, index.StoreSize);
            }
            indexTable.Write();
        }
        else
        {
            // -l URL Status
            Console.WriteLine("Elasticsearch cluster is up and running");
        }
    }
}
