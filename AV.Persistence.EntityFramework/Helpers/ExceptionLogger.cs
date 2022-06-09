using AV.Common.Entities;
using AV.Common.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AV.Persistence.EntityFramework.Helpers
{
    public class ExceptionLogger: IExceptionLogger
    {
        private readonly DbContext _dbContext;

        public ExceptionLogger(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string Log(LogLevel error, SiteException exceptionToLog)
        {
            _dbContext.Set<SiteException>().Add(exceptionToLog);
            SaveChanges();
            _dbContext.Entry(exceptionToLog).Reload();
            return exceptionToLog.Id.ToString();
        }

        private void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
