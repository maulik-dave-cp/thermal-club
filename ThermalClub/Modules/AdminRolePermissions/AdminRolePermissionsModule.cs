using ThermalClub.Modules.Core.Modules;
using Microsoft.AspNetCore.Routing;

namespace ThermalClub.Modules.AdminRolePermissions
{
    public class AdminRolePermissionsModule : BaseModule
    {
        public AdminRolePermissionsModule()
        {
            ModuleName = "AdminRolePermissions";

            OrderId = 101;
        }

        public override void RegisterRoutes(IEndpointRouteBuilder endpoint)
        {
            //var routeNamespaces = new [] { "ThermalClub.Web.Modules.AdminRolePermissions.Controllers" };
        }
    }
}