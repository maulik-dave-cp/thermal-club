using Hangfire.Dashboard;

namespace ThermalClub.Modules.Core.Authorization
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var cookie = httpContext.Request.Cookies["auth"];
            if (cookie == null) return false;

            var user = AuthHelper.VerifyAndGetLoggedInUser(cookie);

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return user?.Id > 0;
        }
    }
}