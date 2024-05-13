using Nest;

namespace esclient.Elastic
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

        public PingResponse GetServerStatus()
        {
            return _client.Ping();
        }

        public CatResponse<CatIndicesRecord> GetIndices()
        {
            return _client.Cat.Indices(descriptor => descriptor.Index(null));
        }

        public CatResponse<CatIndicesRecord> GetIndex(string indexName)
        {
            return _client.Cat.Indices(descriptor => descriptor.Index(indexName));
        }
    }
}
