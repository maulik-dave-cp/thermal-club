using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.AdminUsers.Services;
using ThermalClub.Modules.Core.Api;
using ThermalClub.Modules.Core.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.AdminUsers.Api
{
    [Route("api/admin/account")]
    [AuthorizeApiAdminUser]
    public class AdminAccountController : BaseApiController
    {
	    private readonly AuthUser _authUser;
	    private readonly IAccountAdminService _accountAdminService;

        public AdminAccountController(
	        Auth auth,
            IAccountAdminService accountAdminService)
        {
	        _authUser = auth.LoggedInUser();
            _accountAdminService = accountAdminService;
        }

        [HttpGet, Route("edit-profile")]
        public IActionResult EditProfile()
        {
            return Result(_accountAdminService.GetEditProfile(_authUser.Id));
        }

        [HttpPost, Route("edit-profile")]
        public IActionResult EditProfile([FromBody] AdminEditProfileDto dto)
        {
            if (_authUser == null) return Ok();

            dto.Id = _authUser.Id;
            return Result(_accountAdminService.SaveEditProfile(dto));
        }

        [HttpPost, Route("change-password")]
        public IActionResult ChangePassword([FromBody] AdminChangePasswordDto dto)
        {
            if (_authUser == null) return Ok();

            dto.Id = _authUser.Id;
            return Result(_accountAdminService.ChangePassword(dto));
        }
    }
}
