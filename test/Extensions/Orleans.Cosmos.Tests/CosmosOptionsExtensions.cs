using CosmosDB.InMemoryEmulator;
using Microsoft.Azure.Cosmos;
using Orleans.Clustering.Cosmos;
using Orleans.Persistence.Cosmos;
using Orleans.Reminders.Cosmos;

namespace Tester.Cosmos;

public static class CosmosOptionsExtensions
{
    // Shared InMemoryCosmosClient instance so all providers (clustering, persistence, reminders)
    // see the same in-memory databases and containers within a single test run.
    private static readonly InMemoryCosmosClient _sharedClient = new();

    public static void ConfigureTestDefaults(this CosmosClusteringOptions options)
    {
        options.ConfigureCosmosClient(GetInMemoryCosmosClient());
        options.IsResourceCreationEnabled = true;
    }

    public static void ConfigureTestDefaults(this CosmosGrainStorageOptions options)
    {
        options.ConfigureCosmosClient(GetInMemoryCosmosClient());
        options.IsResourceCreationEnabled = true;
    }

    public static void ConfigureTestDefaults(this CosmosReminderTableOptions options)
    {
        options.ConfigureCosmosClient(GetInMemoryCosmosClient());
        options.IsResourceCreationEnabled = true;
    }

    private static Func<IServiceProvider, ValueTask<CosmosClient>> GetInMemoryCosmosClient()
    {
        return _ => new(_sharedClient);
    }
}
