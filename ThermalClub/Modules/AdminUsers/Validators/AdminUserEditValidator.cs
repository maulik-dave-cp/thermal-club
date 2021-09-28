using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.Core.Validators;
using FluentValidation;
using FluentValidation.Validators;

namespace ThermalClub.Modules.AdminUsers.Validators
{
    public class AdminUserEditValidator : AbstractValidator<AdminUserEditDto>
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IAdminRoleRepository _adminRoleRepository;

        public AdminUserEditValidator(
            IAdminUserRepository adminUserRepository,
            IAdminRoleRepository adminRoleRepository)
        {
            _adminUserRepository = adminUserRepository;
            _adminRoleRepository = adminRoleRepository;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(v => v.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().EmailAddress()
                .MaximumLength(100)
                .Must(UniqueEmail).WithMessage("{PropertyName} already used with other user.");

            When(v => !string.IsNullOrEmpty(v.Password), () =>
            {
                RuleFor(v => v.Password)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .Length(6, 20);
            });

            RuleFor(v => v.Roles)
                .Cascade(CascadeMode.Stop)
                .AjNotNull()
                .Must(ValidRole).WithMessage("Invalid role selected.");
        }

        private bool UniqueEmail(AdminUserEditDto dto, string email)
        {
            return !_adminUserRepository.AsNoTracking.Any(w => w.Email == email && w.Id != dto.Id);
        }

        private bool ValidRole(List<int> roleIds)
        {
            return _adminRoleRepository.AsNoTracking.Count(w => roleIds.Contains(w.Id)) == roleIds.Count;
        }
    }
}