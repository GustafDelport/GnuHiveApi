using System.Data;

namespace GnuHiveApi.Persistence.Common;

public delegate void UnitOfWorkCreated(IUnitOfWork unitOfWork);

public interface IUnitOfWorkFactory
{
    bool HasActiveUnitOfWork { get; }
    IUnitOfWork CreateUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    IUnitOfWork? UnitOfWork { get; }
    event UnitOfWorkCreated OnUnitOfWorkCreated;
}