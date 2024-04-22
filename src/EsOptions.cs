using CommandLine;

namespace esclient;

public class EsOptions
{
    [Option('l', "url", Required = true, HelpText = "Uniform Resource Locator")]
    public string? URL { get; set; }

    [Option('i', "index", Required = false, HelpText = "Index")]
    public string? Index { get; set; }
}
