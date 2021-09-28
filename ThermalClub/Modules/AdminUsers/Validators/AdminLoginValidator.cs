using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.Core.Validators;
using FluentValidation;

namespace ThermalClub.Modules.AdminUsers.Validators
{
    public class AdminLoginValidator : AjAbstractValidator<AdminLoginDto>
    {
        public AdminLoginValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(v => v.Password)
                .NotEmpty();
        }
    }
}