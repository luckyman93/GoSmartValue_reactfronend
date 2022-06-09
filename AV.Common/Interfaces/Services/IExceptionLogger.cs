using AV.Common.Entities;
using Microsoft.Extensions.Logging;

namespace AV.Common.Interfaces.Services
{
    public interface IExceptionLogger
    {
        string Log(LogLevel error, SiteException exceptionToLog);
    }
}
