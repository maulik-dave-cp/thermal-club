using ThermalClub.Modules.Core.Api;
using ThermalClub.Modules.Core.Authorization;
using ThermalClub.Modules.ErrorLogs.Data.Permissions;
using ThermalClub.Modules.ErrorLogs.Models.DTOs;
using ThermalClub.Modules.ErrorLogs.Services;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.ErrorLogs.Api
{
    [Route("api/admin/error-log")]
    [AuthorizeApiAdminUser]
    public class ErrorLogController : BaseApiController
    {
        private readonly Auth _auth;
        private readonly IErrorLogService _errorLogService;

        public ErrorLogController(Auth auth, IErrorLogService errorLogService)
        {
            _auth = auth;
            _errorLogService = errorLogService;
        }

        [HttpGet("")]
        [AuthorizeApiAdminUser(permissions: new[] { ErrorLogPermission.List })]
        public IActionResult Get([FromQuery] ErrorLogFilterDto dto)
        {
            return Result(_errorLogService.List(dto));
        }
    }
}