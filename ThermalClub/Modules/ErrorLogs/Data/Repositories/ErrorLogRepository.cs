using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.ErrorLogs.Models;

namespace ThermalClub.Modules.ErrorLogs.Data.Repositories
{
    public interface IErrorLogRepository : IRepository<ErrorLog>
    {
    }
    public class ErrorLogRepository : Repository<ErrorLog>, IErrorLogRepository
    {
        public ErrorLogRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}