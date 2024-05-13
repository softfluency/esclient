using Nest;

namespace esclient
{
    public class ElasticsearchService
    {
        private readonly ElasticClientFactory _clientFactory;
        private readonly ElasticClient _client;

        public ElasticsearchService(string url, ElasticClientFactory? clientFactory = null)
        {
            _clientFactory = clientFactory ?? new ElasticClientFactory();
            var serverUri = new Uri(url);
            _client = CreateElasticClient(serverUri, true, -1);
        }

        private ElasticClient CreateElasticClient(Uri serverUri, bool enableHttpCompression, int connectionLimit)
        {
            var connectionSettings = new ConnectionSettings(serverUri)
                .EnableHttpCompression()
                .ConnectionLimit(connectionLimit);

            return _clientFactory.CreateClient(connectionSettings);
        }

        public CatResponse<CatIndicesRecord> GetIndices()
        {
            return _client.Cat.Indices(descriptor => descriptor.Index(null));
        }

        public CatResponse<CatIndicesRecord> GetIndex(string indexName)
        {
            // Vratimo niz stringova (i napravimo klasu koja ima hedere i values)
            return _client.Cat.Indices(descriptor => descriptor.Index(indexName));
        }

        public NodesInfoResponse GetNodesInfo()
        {
            return _client.Nodes.Info();
        }
    }
}
