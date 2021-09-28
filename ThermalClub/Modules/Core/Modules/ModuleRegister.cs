using System;
using System.Collections.Generic;
using ThermalClub.Modules.Core.Helpers;
using Microsoft.AspNetCore.Routing;

namespace ThermalClub.Modules.Core.Modules
{
    public static class ModuleRegister
    {
        private static readonly IEnumerable<BaseModule> Modules;

        static ModuleRegister()
        {
            Modules = ObjectHelper.GetEnumerableOfType<BaseModule>(null);
        }

        public static void RegisterBackgroundJobs()
        {
            foreach (var module in Modules)
                module.RegisterBackgroundJobs();
        }

        public static void RegisterModuleRoutes(IEndpointRouteBuilder endpoint)
        {
            foreach (var module in Modules)
                module.RegisterRoutes(endpoint);
        }
    }
}