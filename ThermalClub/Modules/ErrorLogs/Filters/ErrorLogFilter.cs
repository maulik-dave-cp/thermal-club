using ThermalClub.Modules.ErrorLogs.Models;
using ThermalClub.Modules.ErrorLogs.Models.DTOs;
using ThermalClub.Modules.Core.Filters;
using System.Linq;

namespace ThermalClub.Modules.ErrorLogs.Filters
{
    public class ErrorLogFilter : BaseFilter<ErrorLog, ErrorLogFilterDto>
    {
        public ErrorLogFilter(IQueryable<ErrorLog> query, ErrorLogFilterDto dto) : base(query, dto)
        {
        }
        internal void ErrorType()
        {
            Query = Query.Where(w => w.ErrorType.Contains(Dto.ErrorType));
        }
        internal void Description()
        {
            Query = Query.Where(w => w.Description.Contains(Dto.Description));
        }
        internal void Stacktrace()
        {
            Query = Query.Where(w => w.Stacktrace.Contains(Dto.Stacktrace));
        }
        internal void IsEmailSent()
        {
            Query = Query.Where(w => w.IsEmailSent == Dto.IsEmailSent);
        }
        internal void FromCreatedAt()
        {
            Query = Query.Where(w => w.CreatedAt >= Dto.FromCreatedAt);
        }
        internal void ToCreatedAt()
        {
            Query = Query.Where(w => w.CreatedAt <= Dto.ToCreatedAt);
        }
    }
}