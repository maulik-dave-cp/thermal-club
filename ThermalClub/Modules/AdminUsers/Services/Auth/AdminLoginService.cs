using System;
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
    public interface IAdminLoginService
    {
        Result Login(AdminLoginDto dto, out AdminUser adminUser);
    }

    public class AdminLoginService : IAdminLoginService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminLoginService(
            IAdminUserRepository adminUserRepository,
            IUnitOfWork unitOfWork
            )
        {
            _adminUserRepository = adminUserRepository;
            _unitOfWork = unitOfWork;
        }

        public Result Login(AdminLoginDto dto, out AdminUser adminUser)
        {
            adminUser = null;

            var validator = new AdminLoginValidator();
            var result = validator.ValidateResult(dto);
            if (!result.Success) return result;

            adminUser = _adminUserRepository.AsNoTracking
                .FirstOrDefault(w => w.IsActive && w.Email == dto.Email);

            return adminUser == null
                ? SendFailedLoginResponse()
                : VerifyPassword(dto.Password, adminUser);
        }

        private Result VerifyPassword(string password, AdminUser adminUser)
        {
            if (!SecurityHelper.VerifyHash(password, adminUser.Password, adminUser.Salt))
                return SendFailedLoginResponse();

            SetLastLoginAt(adminUser);

            return new Result().SetSuccess();
        }

        private static Result SendFailedLoginResponse()
        {
            return new Result()
                .SetError(Messages.InvalidLogin);
        }

        private void SetLastLoginAt(AdminUser adminUser)
        {
            adminUser.LastLoginAt = DateTime.Now;

            _adminUserRepository.Update(adminUser);
            _unitOfWork.Commit();
        }
    }
}