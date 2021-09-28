using ThermalClub.Modules.Core.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ThermalClub.Modules.Frontend
{
    public class FrontendModule : BaseModule
    {
        public FrontendModule()
        {
            ModuleName = "Frontend";

            HasViews = true;

            OrderId = 1000;
        }

        public override void RegisterRoutes(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapControllerRoute(
                name:"Home",
                pattern:"",
                defaults: new { controller = "Home", action = "Index" }
            );

            endpoint.MapControllerRoute(
                name: "Error",
                pattern: "error",
                defaults: new { controller = "Home", action = "Error" }
            );

            //routes.MapRoute(
            //    name: "AdminLoginAs",
            //    url: "admin-login-as/{memberId}",
            //    defaults: new { controller = "Auth", action = "MemberLoginViaAdminLoginAs" },
            //    namespaces: routeNamespaces
            //);
        }

    }
}