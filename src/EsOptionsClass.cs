using CommandLine;

namespace esclient
{
    public class Person
    {
            public string? Name { get; set; }
            public int? Age { get; set; }
    }
    public class Es1
    {
            [Option('u', "username", Required = true, HelpText = "User name")]
            public string? Username { get; set; }

            [Option('l', "password", Required = true, HelpText = "Password")]
            public string? Password { get; set; }

            [Option('i', "insert", Required = false, HelpText = "Index Document")]
            public bool Insert { get; set; }

            [Option('s', "search", Required = false, HelpText = "Search")]
            public bool Search { get; set; }

            [Option('n', "name", Required = false, HelpText = "Name")]
            public string? Name { get; set; }

            [Option('y', "age", Required = false, HelpText = "Age")]
            public int Age { get; set; }
    }
}
