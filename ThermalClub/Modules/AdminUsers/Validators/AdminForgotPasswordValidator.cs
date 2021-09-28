using ThermalClub.Modules.AdminUsers.Models.DTOs;
using FluentValidation;

namespace ThermalClub.Modules.AdminUsers.Validators
{
    public class AdminForgotPasswordValidator : AbstractValidator<AdminForgotPasswordDto>
    {
        public AdminForgotPasswordValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}