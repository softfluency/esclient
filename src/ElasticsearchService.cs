using Nest;

namespace esclient
{
    public class ElasticsearchService
    {
        private readonly ElasticClientFactory _clientFactory;

        public ElasticsearchService(ElasticClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public ElasticClient CreateElasticClient(Uri serverUri, bool enableHttpCompression, int connectionLimit)
        {
            var connectionSettings = new ConnectionSettings(serverUri)
                .EnableHttpCompression()
                .ConnectionLimit(connectionLimit);

            return _clientFactory.CreateClient(connectionSettings);
        }

        public CatResponse<CatIndicesRecord> GetIndices(ElasticClient client, string? indexName)
        {
            return client.Cat.Indices(descriptor => descriptor.Index(indexName));
        }
    }
}
