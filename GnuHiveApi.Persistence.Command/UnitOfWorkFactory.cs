using System.Data;
using GnuHiveApi.Common.Logging;
using GnuHiveApi.Persistence.Common;

namespace GnuHiveApi.Persistence.Command;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDatabaseConnectionProvider _connectionProvider;
    private readonly ILogLogger _logger;

    private IUnitOfWork? _unitOfWork;

    public UnitOfWorkFactory(IDatabaseConnectionProvider connectionProvider, ILogLogger logger)
    {
        this._connectionProvider = connectionProvider;
        this._logger = logger;
    }


    public IUnitOfWork CreateUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (this.HasActiveUnitOfWork)
        {
            throw new InvalidOperationException("Can't create nested units of work");
        }

        this._unitOfWork = new SqlUnitOfWork(this._connectionProvider, this._logger, isolationLevel);

        this.OnUnitOfWorkCreated?.Invoke(this._unitOfWork);

        return this._unitOfWork;
    }

    public bool HasActiveUnitOfWork => this.UnitOfWork is { IsActive: true };

    public IUnitOfWork? UnitOfWork => this._unitOfWork;

    public event UnitOfWorkCreated? OnUnitOfWorkCreated;
}