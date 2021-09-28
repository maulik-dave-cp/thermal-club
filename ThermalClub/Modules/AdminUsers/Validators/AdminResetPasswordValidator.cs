using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.Core.Content;
using FluentValidation;

namespace ThermalClub.Modules.AdminUsers.Validators
{
    public class AdminResetPasswordValidator : AbstractValidator<AdminResetPasswordDto>
    {
        public AdminResetPasswordValidator()
        {
            RuleFor(v => v.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();

            RuleFor(v => v.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(6, 20)
                .Equal(v => v.ConfirmPassword).WithMessage(Messages.BothPasswordMatch);

            RuleFor(v => v.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(6, 20);
        }
    }
}