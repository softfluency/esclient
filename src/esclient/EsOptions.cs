using CommandLine;

namespace esclient;

public class EsOptions
{
    public class Url
    {
        [Option('u', "url", Required = true, HelpText = "ElasticSearch Server https://[username]:[password]@url")]
        public required string URL { get; set; }
    }

    [Verb("status", HelpText = "Returns the status of Elasticsearch.")]    
    public class Status : Url
    {
    }

    [Verb("indices", HelpText = "Returns all of the indices.")]
    public class Indices: Url
    {
    }

    [Verb("index", HelpText = "Returns entered index.")]
    public class Index : Url
    {
        [Option('i', "index", Required = true, HelpText = "Index")]
        public required string IndexSearch { get; set; }
    }
}
