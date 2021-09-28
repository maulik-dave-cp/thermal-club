using System.Linq;
using ThermalClub.Modules.AdminUsers.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.AdminUsers.Notifications;
using ThermalClub.Modules.AdminUsers.Validators;
using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Helpers;
using ThermalClub.Modules.Core.Validators;

namespace ThermalClub.Modules.AdminUsers.Services.Auth
{
    public interface IAdminForgotPasswordService
    {
        Result ForgotPassword(AdminForgotPasswordDto dto);
    }

    public class AdminForgotPasswordService : IAdminForgotPasswordService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly AdminUserForgotPasswordNotification _adminUserForgotPasswordNotification;
        private readonly IUnitOfWork _unitOfWork;

        public AdminForgotPasswordService(
            IAdminUserRepository adminUserRepository,
            AdminUserForgotPasswordNotification adminUserForgotPasswordNotification,
            IUnitOfWork unitOfWork)
        {
            _adminUserRepository = adminUserRepository;
            _adminUserForgotPasswordNotification = adminUserForgotPasswordNotification;
            _unitOfWork = unitOfWork;
        }

        public Result ForgotPassword(AdminForgotPasswordDto dto)
        {
            var validator = new AdminForgotPasswordValidator();
            var result = validator.ValidateResult(dto);
            if (!result.Success) return result;

            var adminUser = _adminUserRepository.AsNoTracking
                .FirstOrDefault(w => w.IsActive && w.Email == dto.Email);

            if (adminUser != null)
            {
                GenerateAndSaveForgotPasswordToken(adminUser);
                result = _adminUserForgotPasswordNotification.Prepare(adminUser).Send();
            }

            if (!result.Success) return result;
            result = ForgotPasswordResponse(dto.Email);

            return result;
        }

        private string GenerateAndSaveForgotPasswordToken(AdminUser adminUser)
        {
            var passwordResetToken = StringHelper.RandomString(12);

            adminUser.ForgotPasswordToken = passwordResetToken;
            _adminUserRepository.Update(adminUser);
            _unitOfWork.Commit();

            return passwordResetToken;
        }

        private static Result ForgotPasswordResponse(string email)
        {
            return new Result().SetSuccess(
                $"If there is an account associated with <b>{email}</b> you will receive an email with a link to reset your password.");
        }
    }
}