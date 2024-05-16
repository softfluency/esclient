namespace esclient.Elastic;

public class ConnectionSettings
{
    public Uri ServerUri { get; }
    public bool HttpCompressionEnabled { get; set; }
    public int ConnectionLimitValue { get; set; }

    public ConnectionSettings(Uri serverUri)
    {
        ServerUri = serverUri;
    }

    public ConnectionSettings EnableHttpCompression()
    {
        HttpCompressionEnabled = true;
        return this;
    }

    public ConnectionSettings ConnectionLimit(int limit)
    {
        ConnectionLimitValue = limit;
        return this;
    }
}
