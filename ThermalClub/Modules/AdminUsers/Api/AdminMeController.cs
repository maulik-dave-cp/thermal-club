using ThermalClub.Modules.AdminUsers.Services;
using ThermalClub.Modules.Core.Api;
using ThermalClub.Modules.Core.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.AdminUsers.Api
{
    [Route("api/admin/me")]
    public class AdminMeController : BaseApiController
    {
	    private readonly Auth _auth;
	    private readonly IAdminUserService _adminUserService;

        public AdminMeController(
            Auth auth,
            IAdminUserService adminUserService)
        {
	        _auth = auth;
	        _adminUserService = adminUserService;
        }

        [HttpGet("permissions")]
        public IActionResult GetPermissions()
        {
            var user = _auth.LoggedInUser();
            if (user != null)
                return Result(_adminUserService.GetPermissions(user.Id));

            return Unauthorized();
        }
               
    }
}
