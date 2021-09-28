using ThermalClub.Modules.AdminUsers.Data.Emails;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.Core.Notifications;
using ThermalClub.Modules.CurrentProject.Helpers;
using ThermalClub.Modules.EmailTemplates.Services;

namespace ThermalClub.Modules.AdminUsers.Notifications
{
    public class AdminUserForgotPasswordNotification : Notification
    {
        private readonly INotificationService _notificationService;

        public AdminUserForgotPasswordNotification(
            ThermalConfiguration configuration,
            INotificationService notificationService) : base(configuration, notificationService)
        {
            _notificationService = notificationService;
        }

        public AdminUserForgotPasswordNotification Prepare(AdminUser adminUser)
        {
            EmailTemplate = _notificationService.ByEmailTemplateType(AdminUserEmail.AdminUserResetPassword);

            MailHelper.To(adminUser.Name, adminUser.Email);

            Variables.Add("ResetPasswordLink", "reset-password/" + adminUser.ForgotPasswordToken);

            return this;
        }
    }
}