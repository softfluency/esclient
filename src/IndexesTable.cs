using ConsoleTables;

namespace esclient
{
    public class IndexesTable
    {
        public static ConsoleTable CreateTable(params string[] headers)
        {
            var table = new ConsoleTable(headers);

            return table; 
        }
    
        public static void AddRow(ConsoleTable table, params object[] values)
        {
            table.AddRow(values);
        }
    }
}

