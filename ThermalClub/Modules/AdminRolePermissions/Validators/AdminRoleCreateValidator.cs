using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Data.Repositories;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using ThermalClub.Modules.Core.Validators;
using FluentValidation;

namespace ThermalClub.Modules.AdminRolePermissions.Validators
{
    public class AdminRoleCreateValidator : AbstractValidator<AdminRoleCreateDto>
    {
        private readonly IAdminPermissionRepository _adminPermissionRepository;

        public AdminRoleCreateValidator(
            IAdminPermissionRepository adminPermissionRepository)
        {
            _adminPermissionRepository = adminPermissionRepository;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(v => v.Permissions)
                .Cascade(CascadeMode.Stop)
                .AjNotNull()
                .Must(ValidPermissions).WithMessage("Invalid permission selected.");

           
        }

        private bool ValidPermissions(List<int> permissionIds)
        {
            return _adminPermissionRepository.AsNoTracking.Count(w =>
                       permissionIds.Contains(w.Id)) == permissionIds.Count;
        }
    }
}