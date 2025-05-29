using System.Data;
using ErrorOr;

namespace GnuHiveApi.Persistence.Common;

public interface IUnitOfWork : IDisposable
{
    IDbTransaction? Transaction { get; }
    IDbConnection? Connection { get; }
    bool IsActive { get; }

    ErrorOr<Success> Commit();
}