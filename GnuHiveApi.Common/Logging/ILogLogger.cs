using ErrorOr;

namespace GnuHiveApi.Common.Logging;

public interface ILogLogger
{
    void Debug(string message);
    void Debug(string message, Exception exception);
    
    void DebugFormat(string format, params object[] args);
    void DebugFormat(Exception exception, string format, params object[] args);
    
    void Error(string message);
    void Error(Error error);
    
    void Errors(IEnumerable<Error> errors);
    void Error(string message, Exception exception);
    
    void ErrorFormat(string format, params object[] args);
    void ErrorFormat(Exception exception, string format, params object[] args);
    
    void Fatal(string message);
    void Fatal(string message, Exception exception);
    
    void FatalFormat(string format, params object[] args);
    void FatalFormat(Exception exception, string format, params object[] args);
    
    void Info(string message);
    void Info(string message, Exception exception);
    
    void InfoFormat(string format, params object[] args);
    void InfoFormat(Exception exception, string format, params object[] args);
    
    void Warn(string message);
    void Warn(string message, Exception exception);
    
    void WarnFormat(string format, params object[] args);
    void WarnFormat(Exception exception, string format, params object[] args);
    
    void Progress(float progress);
    void Progress(float progress, string processName);
}