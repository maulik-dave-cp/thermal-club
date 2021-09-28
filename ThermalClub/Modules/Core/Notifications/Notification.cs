using ThermalClub.Modules.Core.Helpers;
using ThermalClub.Modules.CurrentProject.Helpers;
using ThermalClub.Modules.EmailTemplates.Data.Emails;
using ThermalClub.Modules.EmailTemplates.Models;
using ThermalClub.Modules.EmailTemplates.Services;
using ThermalClub.Modules.ErrorLogs.Helpers;
using ThermalClub.Modules.ErrorLogs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using MailSetting = ThermalClub.Modules.CurrentProject.Helpers.MailSetting;

namespace ThermalClub.Modules.Core.Notifications
{
    public class Notification
    {
        private readonly INotificationService _notificationService;
        protected MailHelper MailHelper;
        protected Dictionary<string, object> Variables = new Dictionary<string, object>();
        protected EmailTemplate EmailTemplate;
        private readonly SiteSetting _siteSetting;
        private MailSetting _mailSetting;

        public Notification(
            ThermalConfiguration configuration,
            INotificationService notificationService)
        {
            _notificationService = notificationService;

            MailHelper = new MailHelper(configuration.MailSetting);

            _siteSetting = configuration.SiteSetting;
            _mailSetting = configuration.MailSetting;
        }

        private void SetSendEmailAddressesFromTemplate()
        {
            if (EmailTemplate.ToEmails != null)
                foreach (var email in EmailTemplate.ToEmails)
                    MailHelper.To(email.Email);

            if (EmailTemplate.CcEmails != null)
                foreach (var email in EmailTemplate.CcEmails)
                    MailHelper.Cc(email.Email);

            if (EmailTemplate.BccEmails != null)
                foreach (var email in EmailTemplate.BccEmails)
                    MailHelper.Bcc(email.Email);
        }

        public Result Send()
        {
            SetSendEmailAddressesFromTemplate();

            var layout = _notificationService.ByEmailTemplateType(EmailTemplateEmail.Layout).Content;
            layout = layout.Replace("{%Head%}", GetHeader());
            layout = layout.Replace("{%Footer%}", GetFooter());

            MailHelper.Subject(EmailTemplate.Subject);
            MailHelper.Body(layout.Replace("{%Body%}", EmailTemplate.Content));

            AddVariables();
            MailHelper.Variables(Variables);

            var result = MailHelper.Send();
            if (!result.Success)
            {
                var dto = new ErrorLogCreateDto();
                dto.ErrorType = ErrorTypeEnum.EmailSendError.ToString();
                dto.Description = result.Message;
                dto.Stacktrace = result.Data;
                _notificationService.CreateErrorLog(dto);
            }
            return result;
        }

        public Result SendWithAttachment(List<EmailAttachmentDto> attachmentDtos)
        {
            SetSendEmailAddressesFromTemplate();

            var layout = _notificationService.ByEmailTemplateType(EmailTemplateEmail.Layout).Content;
            layout = layout.Replace("{%Head%}", GetHeader());
            layout = layout.Replace("{%Footer%}", GetFooter());

            MailHelper.Subject(EmailTemplate.Subject);
            MailHelper.Body(layout.Replace("{%Body%}", EmailTemplate.Content));
            if (attachmentDtos != null)
            {
                foreach (var item in attachmentDtos)
                {
                    MailHelper.Attachment(item.FileContent, item.FileName);
                }
            }

            AddVariables();
            MailHelper.Variables(Variables);

            var result = MailHelper.SendWithAttachment();
            if (!result.Success)
            {
                var dto = new ErrorLogCreateDto();
                dto.ErrorType = ErrorTypeEnum.EmailSendError.ToString();
                dto.Description = result.Message;
                dto.Stacktrace = result.Data;
                _notificationService.CreateErrorLog(dto);
            }
            return result;
        }

        private string GetHeader()
        {
            return @"<table align='center' border='0' cellpadding='0' cellspacing='0' class='m-wrap' width='600'>
		          <tbody>
		            <tr>
		              <td align='left' height='80' valign='middle'><a href='{%WebsiteUrl%}' style='text-decoration:none;border:none;'><img alt='logo' height='' src='{%WebsiteUrl%}dist/client/img/email/logo.png' width='150' /></a>
		              </td>
		            </tr>
		          </tbody>
		        </table>";
        }

        private string GetFooter()
        {
            return @"<table align='center' border='0' cellpadding='0' cellspacing='0' class='m-wrap foot-wrap' width='600'>
                <tbody>
                    <tr>
                        <td align='left' height='80' valign='middle'><font color='#ffffff' face='Arial, Helvetica, sans-serif' size='2'> 
                            ©{%Year%} &nbsp; {%WebsiteName%} All rights reserved.</font>
                        </td>
                        <td align='center' height='80' valign='middle' width='35'><a href='https://www.facebook.com/WeightsMeasures' style='height: 18px;display: block;vertical-align: middle;' target='_blank'><img alt='Facebook' class='CToWUd' src='{%WebsiteUrl%}dist/client/img/email/icn_facebook.png' style='height:18px' /> </a>
                        </td>
                        <td align='center' height='80' valign='middle' width='35'><a href='https://twitter.com/WeightsMeasures' style='height: 18px;display: block;vertical-align: middle;' target='_blank'><img alt='Twitter' class='CToWUd' src='{%WebsiteUrl%}dist/client/img/email/icn_twitter.png' style='height:18px' /> </a>
                        </td>
                        <td align='center' height='80' valign='middle' width='35'><a href='https://www.youtube.com/channel/UCLg_rvut3OXbZn94rIILiMA' style='height: 18px;display: block;vertical-align: middle;' target='_blank'><img alt='Youtube' class='CToWUd' src='{%WebsiteUrl%}dist/client/img/email/icn_youtube.png' style='height:18px' /> </a>
                        </td>
                        <td align='center' height='80' valign='middle' width='35'><a href='http://www.linkedin.com/in/weightsmeasures' style='height: 18px;display: block;vertical-align: middle;' target='_blank'><img alt='linkedin' class='CToWUd' src='{%WebsiteUrl%}dist/client/img/email/icn_linkedin.png' style='height:18px' /> </a>
                        </td>
                    </tr>
                 </tbody>
               </table>";
        }

        private void AddVariables()
        {
            Variables.AddOrUpdate("WebsiteUrl", _siteSetting.WebsiteUrl);
            Variables.AddOrUpdate("WebsiteName", _siteSetting.SiteTitle);
            Variables.AddOrUpdate("ContactEmail", _mailSetting.ContactEmail);
            // Variables.AddOrUpdate("SocialLinks", BuildSocialLinks());

            Variables.AddOrUpdate("Year", DateTime.Today.Year);
        }

        //private string BuildSocialLinks()
        //{
        //    var socialLinks = _notificationService.ByConfiguration<SocialLinks>(ConfigurationType.SocialLinks);

        //    var html = "";
        //    html += GenerateSocialLink(socialLinks.Facebook, "Facebook");
        //    html += GenerateSocialLink(socialLinks.Twitter, "Twitter");
        //    html += GenerateSocialLink(socialLinks.LinkedIn, "LinkedIn");
        //    html += GenerateSocialLink(socialLinks.Youtube, "Youtube");
        //    //html += GenerateSocialLink(socialLinks.Instagram, "Instagram");
        //    //html += GenerateSocialLink(socialLinks.Pinterest, "Pinterest");

        //    return !string.IsNullOrEmpty(html)
        //        ? $@"{html}"
        //        : "";
        //}

        private string GenerateSocialLink(string url, string name)
        {
            return !string.IsNullOrEmpty(url)
                ? $@"<td height='80' width='35'  valign='middle' align='center'>
                        <a style='display:inline-block;text-align:center;' target='_blank' href='{url}'>
                            <img src='{_siteSetting.WebsiteUrl}dist/client/img/email/icn_{name.ToLower()}.png' style='width:16px;' alt='{name}' />
                        </a> </td>"
                : "";
        }

        public MailMessage PrepareEmailContent()
        {
            SetSendEmailAddressesFromTemplate();

            var layout = _notificationService.ByEmailTemplateType(EmailTemplateEmail.Layout).Content;
            layout = layout.Replace("{%Head%}", GetHeader());
            layout = layout.Replace("{%Footer%}", GetFooter());

            MailHelper.Subject(EmailTemplate.Subject);
            MailHelper.Body(layout.Replace("{%Body%}", EmailTemplate.Content));

            AddVariables();
            MailHelper.Variables(Variables);

            return MailHelper.PrepareEmailContent();
        }
        public enum ErrorTypeEnum : int
        {
            HangfireJobFail = 1,
            EmailSendError = 2,
            StorePing = 3,
        }
    }
}