using System.Linq;
using ThermalClub.Modules.AdminUsers.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.AdminUsers.Validators;
using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Encryption;
using ThermalClub.Modules.Core.Validators;
using ThermalClub.Modules.Core.Content;

namespace ThermalClub.Modules.AdminUsers.Services.Auth
{
    public interface IAdminResetPasswordService
    {
        bool IsValidToken(string token);

        Result ResetPassword(string token, AdminResetPasswordDto dto);
    }

    public class AdminResetPasswordService : IAdminResetPasswordService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminResetPasswordService(
            IAdminUserRepository adminUserRepository,
            IUnitOfWork unitOfWork)
        {
            _adminUserRepository = adminUserRepository;
            _unitOfWork = unitOfWork;
        }

        public bool IsValidToken(string token)
        {
            return !string.IsNullOrEmpty(token) &&
                   _adminUserRepository.AsNoTracking.Any(w => w.ForgotPasswordToken == token && w.IsActive);
        }

        public Result ResetPassword(string token, AdminResetPasswordDto dto)
        {
            var validator = new AdminResetPasswordValidator();
            var result = validator.ValidateResult(dto);
            if (!result.Success) return result;

            var adminUser =
                _adminUserRepository.AsNoTracking.FirstOrDefault(w =>
                    w.Email == dto.Email && w.ForgotPasswordToken == token && w.IsActive);

            if (adminUser == null)
                return new Result().SetError(Messages.InvalidForgotPasswordToken).SetBlankRedirect();

            SetNewPassword(adminUser, dto.Password);

            return new Result().SetBlankRedirect()
                .SetSuccess(Messages.SuccessResetPassword);
        }

        private void SetNewPassword(AdminUser adminUser, string newPassword)
        {
            adminUser.Salt = SecurityHelper.GenerateSalt();
            adminUser.Password = SecurityHelper.GenerateHash(newPassword, adminUser.Salt);

            adminUser.ForgotPasswordToken = null;
            _adminUserRepository.Update(adminUser);

            _unitOfWork.Commit();
        }
    }
}