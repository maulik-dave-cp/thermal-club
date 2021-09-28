using ThermalClub.Modules.CurrentProject.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.Backend.Controllers
{
    public class AdminController : Controller
    {
        private readonly ThermalConfiguration _configuration;

        public AdminController(ThermalConfiguration configuration)
        {
            _configuration = configuration;

            //ViewBag.Environment = configuration.Environment;
            //ViewBag.AssetVersion = System.Web.HttpContext.Current.Application["Version"];
        }

        public ActionResult Index()
        {
            ViewData["Title"] = _configuration.SiteSetting.SiteTitle;

            return View();
        }
    }
}
