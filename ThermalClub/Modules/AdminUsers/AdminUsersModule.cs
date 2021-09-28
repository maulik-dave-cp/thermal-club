using ThermalClub.Modules.Core.Modules;
using Microsoft.AspNetCore.Routing;

namespace ThermalClub.Modules.AdminUsers
{
    public class AdminUsersModule : BaseModule
    {
        public AdminUsersModule()
        {
	        ModuleName = "AdminUsers";

            OrderId = 102;
        }

        public override void RegisterRoutes(IEndpointRouteBuilder endpoint)
        {
            //var routeNamespaces = new [] { "ThermalClub.Web.Modules.AdminUsers.Controllers" };
        }
    }
}