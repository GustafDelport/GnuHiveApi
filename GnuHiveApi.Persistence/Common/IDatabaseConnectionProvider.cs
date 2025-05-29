using System.Data;

namespace GnuHiveApi.Persistence.Common;

public delegate void DbConnectionOpened(IDbConnection unitOfWork);

public interface IDatabaseConnectionProvider : IDisposable
{
    IDbConnection GetConnection();
    
    event DbConnectionOpened OnDbConnectionOpened;
    
    bool HasOpenConnection { get; }
}