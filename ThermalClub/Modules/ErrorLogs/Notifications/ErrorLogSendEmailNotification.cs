using ThermalClub.Modules.Core.Helpers;
using ThermalClub.Modules.Core.Notifications;
using ThermalClub.Modules.CurrentProject.Helpers;
using ThermalClub.Modules.EmailTemplates.Services;
using ThermalClub.Modules.ErrorLogs.Data.Emails;
using System;

namespace ThermalClub.Modules.ErrorLogs.Notifications
{
    public class ErrorLogSendEmailNotification : Notification
    {
        private readonly INotificationService _notificationService;
        private readonly ThermalConfiguration _configuration;

        public ErrorLogSendEmailNotification(ThermalConfiguration configuration, INotificationService notificationService) : base(configuration, notificationService)
        {
            _notificationService = notificationService;
            _configuration = configuration;
        }

        public ErrorLogSendEmailNotification SendErrorMail(string errorDetail)
        {
            MailHelper = new MailHelper(_configuration.MailSetting);
            EmailTemplate = _notificationService.ByEmailTemplateType(ErrorLogEmail.ErrorLogNotificationEmail);
            Variables.RemoveIfContainsKey("TodayDate");
            Variables.RemoveIfContainsKey("ErrorDetail");
            Variables.Add("TodayDate", DateTime.Today.ToString("d"));
            Variables.Add("ErrorDetail", errorDetail);
            return this;
        }
    }
}