using ThermalClub.Modules.ErrorLogs.Data.Repositories;
using ThermalClub.Modules.ErrorLogs.Models.DTOs;
using FluentValidation;

namespace ThermalClub.Modules.ErrorLogs.Validators
{
    public class ErrorLogCreateValidator : AbstractValidator<ErrorLogCreateDto>
    {
        public ErrorLogCreateValidator(IErrorLogRepository errorLogRepository)
        {
            RuleFor(v => v.ErrorType)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}