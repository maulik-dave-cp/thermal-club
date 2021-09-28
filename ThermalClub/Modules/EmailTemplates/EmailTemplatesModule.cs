using ThermalClub.Modules.Core.Modules;
using Microsoft.AspNetCore.Routing;

namespace ThermalClub.Modules.EmailTemplates
{
    public class EmailTemplatesModule : BaseModule
    {
        public EmailTemplatesModule()
        {
            ModuleName = "EmailTemplates";

            OrderId = 103;
        }

        public override void RegisterRoutes(IEndpointRouteBuilder endpoint)
        {
            //var routeNamespaces = new [] { "ThermalClub.Web.Modules.EmailTemplates.Controllers" };
        }
    }
}