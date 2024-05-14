using CommandLine;

namespace esclient;

public class EsOptions
{
    [Verb("status", HelpText = "Returns the status of Elasticsearch.")]
    public class Status
    {
        [Option('u', "url", HelpText = "Uniform Resource Locator")]
        public required string URL { get; set; }
    }

    [Verb("indices", HelpText = "Returns all of the indices.")]
    public class Indices
    {
        [Option('u', "url", HelpText = "Uniform Resource Locator")]
        public required string URL { get; set; }
    }

    [Verb("index", HelpText = "Returns entered index.")]
    public class Index
    {
        [Option('u', "url", HelpText = "Uniform Resource Locator")]
        public required string URL { get; set; }

        [Option('i', "index", Required = false, HelpText = "Index")]
        public required string IndexSearch { get; set; }
    }
}
