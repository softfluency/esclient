using ConsoleTables;
using Nest;

namespace esclient
{
    public class IndexesTable
    {
        public static void PrintTable(string[] headers, params object[][] rows)
        {
            var table = new ConsoleTable(headers);
            foreach (var row in rows)
            {
                table.AddRow(row);
            }
            table.Write();
        }

        public static void PrintAllIndices(string[] headers, CatResponse<CatIndicesRecord> response)
        {
            var rows = response.Records.Select(index => new object[] { index.Index, index.Health, index.Status }).ToArray();
            PrintTable(headers, rows);
        }

        public static void PrintSingleIndex(string[] headers, CatResponse<CatIndicesRecord> response)
        {
            var rows = response.Records.Select(index => new object[] { index.Index, index.Health, index.Status, index.DocsCount, index.DocsDeleted, index.StoreSize }).ToArray();
            PrintTable(headers, rows);
        }
    }
}

