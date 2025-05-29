namespace GnuHiveApi.EntryPoint.Common.Extensions;

public static class ConfigurationManagerExtensions
{
    //TODO Add when adding Redis for SignalR
    /*public static ConfigurationManager AddEntityConfiguration(this ConfigurationManager manager,
        IDatabaseConfigProviderTracker databaseProviderTracker,
        bool reloadOnChange = false)
    {
        var connectionString = manager.GetConnectionString(ConnectionStringLookup.MainConnectionString);
        
        (manager as IConfigurationBuilder).Add(new EntityConfigurationSource(connectionString!, databaseProviderTracker, reloadOnChange));

        return manager;
    }*/
}