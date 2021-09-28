using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.AdminUsers.Services.Auth;
using ThermalClub.Modules.Core.Api;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ThermalClub.Modules.AdminUsers.Api
{
    [Route("api/admin/auth")]
    public class AdminAuthController : BaseApiController
    {
        private readonly IAdminLoginService _adminLoginService;
        private readonly IAdminForgotPasswordService _adminForgotPasswordService;
        private readonly IAdminResetPasswordService _adminResetPasswordService;

        public AdminAuthController(
            IAdminLoginService adminLoginService,
            IAdminForgotPasswordService adminForgotPasswordService,
            IAdminResetPasswordService adminResetPasswordService
            )
        {
	        _adminLoginService = adminLoginService;
	        _adminForgotPasswordService = adminForgotPasswordService;
	        _adminResetPasswordService = adminResetPasswordService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] AdminLoginDto dto)
        {
            var result = _adminLoginService.Login(dto, out var adminUser);

            if (result.Success)
            {
                var rememberDays = dto.IsRememberMe ? 30 : 1;

                var payload = new Dictionary<string, object>
                {
                    {"id", adminUser.Id},
                    {"name", adminUser.Name},
                    {"email", adminUser.Email},
                    {"iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds()},
                    {"exp", DateTimeOffset.UtcNow.AddDays(rememberDays).ToUnixTimeSeconds()}
                };
                const string secret = "iHXSFfZOZjS5ojQkG0pQqxD29h7KRuhp";

                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

                var token = encoder.Encode(payload, secret);

                result.Data = token;

                // UserAuth.SignInAdmin(adminUser.Id, adminUser.Name, adminUser.Email, remember);
            }

            return Ok(result);
        }

        [HttpPost, Route("forgot-password")]
        public IActionResult ForgotPassword([FromBody] AdminForgotPasswordDto dto)
        {
            return Result(_adminForgotPasswordService.ForgotPassword(dto));
        }

        [HttpPost, Route("reset-password/{token}")]
        public IActionResult ResetPassword(string token, [FromBody] AdminResetPasswordDto dto)
        {
            return Result(_adminResetPasswordService.ResetPassword(token, dto));
        }
    }
}
