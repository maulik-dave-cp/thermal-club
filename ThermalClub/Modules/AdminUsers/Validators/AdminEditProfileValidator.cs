using System.Linq;
using ThermalClub.Modules.AdminUsers.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using FluentValidation;

namespace ThermalClub.Modules.AdminUsers.Validators
{
    public class EditProfileValidator : AbstractValidator<AdminEditProfileDto>
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public EditProfileValidator(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);
            
            RuleFor(v => v.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .Must(UniqueEmail).WithMessage("{PropertyName} already used with other user.");
        }

        private bool UniqueEmail(AdminEditProfileDto dto, string email)
        {
            return !_adminUserRepository.AsNoTracking.Any(w => w.Id != dto.Id && w.Email == email);
        }
    }
}