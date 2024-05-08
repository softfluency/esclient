using Nest;

namespace esclient
{
    public class ElasticClientFactory
    {
        public ElasticClient CreateClient(ConnectionSettings settings)
        {
            var connectionSettings = new Nest.ConnectionSettings(settings.ServerUri);

            if (settings.HttpCompressionEnabled)
            {
                connectionSettings.EnableHttpCompression();
            }

            if (settings.ConnectionLimitValue != 0)
            {
                connectionSettings.ConnectionLimit(settings.ConnectionLimitValue);
            }

            return new ElasticClient(connectionSettings);
        }
    }
}
