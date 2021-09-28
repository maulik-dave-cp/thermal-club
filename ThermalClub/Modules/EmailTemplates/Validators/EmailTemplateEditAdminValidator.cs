using ThermalClub.Modules.EmailTemplates.Models;
using ThermalClub.Modules.EmailTemplates.Models.DTOs;
using FluentValidation;

namespace ThermalClub.Modules.EmailTemplates.Validators
{
    public class EmailTemplateEditAdminValidator : AbstractValidator<EmailTemplateEditAdminDto>
    {
        public EmailTemplateEditAdminValidator()
        {
            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().MaximumLength(100);
            RuleFor(v => v.Subject)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().MaximumLength(100);
            RuleFor(v => v.Content).NotEmpty();

            RuleFor(v => v.FromName).MaximumLength(100);
            RuleFor(v => v.FromEmail)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(256)
                .EmailAddress().When(w => w.FromEmail != "");

            When(w => w.TemplateType == TemplateType.Admin, () =>
            {
                RuleForEach(v => v.ToEmails).SetValidator(new EmailClassNotEmptyAdminValidator());
            }).Otherwise(() =>
            {
                RuleForEach(v => v.ToEmails).SetValidator(new EmailClassAdminValidator());
            });
            
            RuleForEach(v => v.CcEmails).SetValidator(new EmailClassAdminValidator());
            RuleForEach(v => v.BccEmails).SetValidator(new EmailClassAdminValidator());
        }

        //private bool ValidateEmails(string email)
        //{
        //    if (string.IsNullOrEmpty(email)) return true;
        //    var emails = email.Trim().TrimEnd(',').Split(',');
        //    return emails.All(item => Regex.IsMatch(item.Trim(),
        //        @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        //        RegexOptions.IgnoreCase));
        //}
    }

    public class EmailClassNotEmptyAdminValidator : AbstractValidator<EmailClass>
    {
        public EmailClassNotEmptyAdminValidator()
        {
            RuleFor(v => v.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();
        }
    }

    public class EmailClassAdminValidator : AbstractValidator<EmailClass>
    {
        public EmailClassAdminValidator()
        {
            RuleFor(v => v.Email).EmailAddress();
        }
    }
}