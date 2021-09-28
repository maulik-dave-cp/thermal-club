using ThermalClub.Modules.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ThermalClub.Modules.Backend
{
    public class CoreModule : BaseModule
    {
        public CoreModule()
        {
            // Module
            ModuleName = "Backend";

            HasViews = true;

            // Class order for register
            OrderId = 1;
        }

        public override void RegisterRoutes(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapControllerRoute(
                name: "Admin",
                pattern: "admin/{*any}",
                defaults: new { controller = "Admin", action = "Index" }
            );
        }
    }
}