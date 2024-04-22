using CommandLine;

namespace esclient;

public class EsOptions
{
    [Option('u', "username", Required = true, HelpText = "Username")]
    public string? Username { get; set; }

    [Option('w', "password", Required = true, HelpText = "Password")]
    public string? Password { get; set; }

    [Option('l', "url", Required = true, HelpText = "Uniform Resource Locator")]
    public string? URL { get; set; }

    [Option('i', "index", Required = false, HelpText = "Index")]
    public string? Index { get; set; }

    [Option('z', "availability", Required = false, HelpText = "Availability")]
    public bool Availability { get; set; }
}
