using ThermalClub.Modules.Core.Filters;
using ThermalClub.Modules.Core.ListOrders;
using ThermalClub.Modules.ErrorLogs.Models;
using System.Linq;

namespace ThermalClub.Modules.ErrorLogs.ListOrders
{
    public class ErrorLogListOrder : BaseListOrder<ErrorLog>
    {
        public ErrorLogListOrder(IQueryable<ErrorLog> query, BaseFilterDto dto) : base(query, dto)
        {
        }

        internal void ErrorType()
        {
            Query = OrderBy(o => o.ErrorType);
        }
        internal void Description()
        {
            Query = OrderBy(o => o.Description);
        }
        internal void Stacktrace()
        {
            Query = OrderBy(o => o.Stacktrace);
        }
        internal void IsEmailSent()
        {
            Query = OrderBy(o => o.IsEmailSent);
        }
        internal void CreatedAt()
        {
            Query = OrderBy(o => o.CreatedAt);
        }
    }
}