using ThermalClub.Modules.Core.Modules;
using ThermalClub.Modules.ErrorLogs.Services;
using Hangfire;
using Microsoft.AspNetCore.Routing;
using System;

namespace ThermalClub.Modules.ErrorLogs
{
    public class ErrorLogsModule : BaseModule
    {
        public ErrorLogsModule()
        {
            ModuleName = "ErrorLogs";
            HasViews = false;
            OrderId = 220;
        }

        public override void RegisterRoutes(IEndpointRouteBuilder endpoint)
        {
        }

        public override void RegisterBackgroundJobs()
        {           
            RecurringJob.AddOrUpdate<IErrorLogService>("errorlog-email-notification",
                x => x.SendErrorLogEmail(), "*/5 * * * *", TimeZoneInfo.Local);
        }
    }
}