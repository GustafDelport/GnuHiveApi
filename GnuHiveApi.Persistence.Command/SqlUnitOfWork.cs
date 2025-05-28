using System.Data;
using ErrorOr;
using GnuHiveApi.Common.Extensions;
using GnuHiveApi.Common.Logging;
using GnuHiveApi.Persistence.Common;

namespace GnuHiveApi.Persistence.Command;

public class SqlUnitOfWork : IUnitOfWork
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly IDatabaseConnectionProvider _connectionProvider;
    private readonly ILogLogger _logger;
    private IDbTransaction _transaction;

    public SqlUnitOfWork(
        IDatabaseConnectionProvider connectionProvider,
        ILogLogger logger,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        this._connectionProvider = connectionProvider;
        this._logger = logger;
        this._transaction = this._connectionProvider.GetConnection().BeginTransaction(isolationLevel);
        this.IsActive = true;
    }
    
    public IDbTransaction? Transaction => this._transaction;
    public IDbConnection? Connection => this._transaction?.Connection;
    public bool IsActive { get; private set; }
    
    public ErrorOr<Success> Commit()
    {
        try
        {
            this._transaction?.Commit();
            return Result.Success;
        }
        catch (Exception e)
        {
            this._logger.Fatal(e.GetFullErrorMessage(), e);
            return Error.Unexpected(description: e.GetFullErrorMessage());
        }
        finally
        {
            this.IsActive = false;
        }
    }

    public void Dispose()
    {
        this._transaction?.Dispose();
        this.IsActive = false;
    }
}