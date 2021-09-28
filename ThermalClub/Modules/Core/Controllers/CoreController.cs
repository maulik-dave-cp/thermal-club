using System.Net.Http;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using ThermalClub.Modules.Core.Helpers;
using ThermalClub.Modules.CurrentProject.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.Core.Controllers
{
    public class CoreController : Controller
    {
        private SqlContext _dataContext;
        protected IDatabaseFactory DatabaseFactory { get; }
        protected SqlContext DataContext => _dataContext ??= DatabaseFactory.Get();

        public CoreController(IDatabaseFactory databaseFactory
            )
        {
            DatabaseFactory = databaseFactory;
        }

        public ActionResult Seed()
        {
            ObjectHelper.GetEnumerableOfType<BaseSeed>(DataContext)
                .ForEach(seedClass => seedClass.Seed());

            return Content("Done");
        }
    }
}
