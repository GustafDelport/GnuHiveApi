using System.Data;
using GnuHiveApi.Common.config;
using GnuHiveApi.Persistence.Common;
using Microsoft.Data.SqlClient;

namespace GnuHiveApi.Persistence.Command;

public class SqlDatabaseConnectionProvider : IDatabaseConnectionProvider
{
    private readonly IApplicationConfig _appConfig;
    private IDbConnection? Connection { get; set; }
    private bool _isConnectionOpened = false;

    public SqlDatabaseConnectionProvider(
        IApplicationConfig appConfig
        )
    {
        this._appConfig = appConfig;
    }
    
    public IDbConnection GetConnection()
    {
        if (this.HasOpenConnection)
        {
            return this.Connection!;
        }

        this.Connection = new SqlConnection(this._appConfig.MainDbConnectionString);
        this.Connection.Open();

        this._isConnectionOpened = true;

        this.OnDbConnectionOpened?.Invoke(this.Connection);

        return this.Connection;
    }
    
    public event DbConnectionOpened? OnDbConnectionOpened;
    
    public bool HasOpenConnection => this.Connection != null && this._isConnectionOpened && this.Connection.State != ConnectionState.Closed;
    
    public void Dispose()
    {
        if(this.HasOpenConnection)
        {
            this.Connection?.Close();
            this.Connection?.Dispose();
            this._isConnectionOpened = false;
        }
    }
}