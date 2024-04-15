using CommandLine;

namespace Es1Class
{
    public class Person
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
    }
    public class Es1
    {
        [Option('u', "username", Required = true, HelpText = "User name")]
        public string? username { get; set; }

        [Option('l', "password", Required =true, HelpText = "Password")]
        public string? password { get; set; }

        [Option('i',"insert",Required =false, HelpText ="Index Document")]
        public bool insert { get; set; }

        [Option('s', "search", Required =false, HelpText ="Search")]
        public bool search { get; set; }

        [Option('n', "name", Required =false, HelpText = "Name")]
        public string? name { get; set; }

        [Option('y',"age", Required =false, HelpText = "Age")]
        public int age { get; set; }
    }
}
