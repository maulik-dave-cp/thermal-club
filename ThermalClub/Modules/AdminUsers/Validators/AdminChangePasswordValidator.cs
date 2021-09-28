using ThermalClub.Modules.AdminUsers.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.Core.Encryption;
using FluentValidation;

namespace ThermalClub.Modules.AdminUsers.Validators
{
    public class ChangePasswordValidator : AbstractValidator<AdminChangePasswordDto>
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public ChangePasswordValidator(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;

            RuleFor(v => v.CurrentPassword)
                .NotEmpty()
                .Must(ValidateCurrentPassword).WithMessage("Invalid 'Current Password'.");

            RuleFor(v => v.NewPassword)
                .NotEmpty().Length(6, 20)
                .Equal(v => v.ConfirmPassword).WithMessage("'New Password' should match to 'Confirm Password'.");

            RuleFor(v => v.ConfirmPassword).NotEmpty();
        }

        private bool ValidateCurrentPassword(AdminChangePasswordDto dto, string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            var adminUser = _adminUserRepository.Find(dto.Id);

            return adminUser != null &&
                   SecurityHelper.VerifyHash(dto.CurrentPassword, adminUser.Password, adminUser.Salt);
        }
    }
}