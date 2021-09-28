using ThermalClub.Modules.CurrentProject.Helpers;
using Sentry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

/*
How to use code sample: 
=======================
    new MailHelper()
        .From("test@gmail.com")
        .To("ajmerainfo@gmail.com")
        .Subject("Hello Subject {%WebsiteUrl%}")
        .Body("Test Body {%Name%}")
        .Variables(new Dictionary<string, object>()
        {
            {"Name", "Keyur Ajmera"}
        }).Send();
*/

namespace ThermalClub.Modules.Core.Helpers
{
    public class MailHelper
    {
        private readonly MailSetting _mailSetting;
        private readonly IList<MailAddress> _toAddresses;
        private readonly IList<MailAddress> _ccAddresses;
        private readonly IList<MailAddress> _bccAddresses;
        private string _subject;
        private string _body;
        private IDictionary<string, object> _variables;
        private MailAddress _from;
        private readonly IList<Attachment> _attachment;

        public MailHelper(MailSetting mailSetting)
        {
            _mailSetting = mailSetting;
            _toAddresses = new List<MailAddress>();
            _ccAddresses = new List<MailAddress>();
            _bccAddresses = new List<MailAddress>();
            _variables = new Dictionary<string, object>();
            _attachment = new List<Attachment>();
        }

        public MailHelper To(string email)
        {
            _toAddresses.Add(new MailAddress(email));

            return this;
        }

        public MailHelper To(string name, string email)
        {
            _toAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Cc(string email)
        {
            _ccAddresses.Add(new MailAddress(email));

            return this;
        }

        public MailHelper Cc(string name, string email)
        {
            _ccAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Bcc(string email)
        {
            _bccAddresses.Add(new MailAddress(email));

            return this;
        }

        public MailHelper Bcc(string name, string email)
        {
            _bccAddresses.Add(new MailAddress(email, name));

            return this;
        }

        public MailHelper Subject(string subject)
        {
            _subject = subject;

            return this;
        }

        public MailHelper Body(string body)
        {
            _body = body;

            return this;
        }

        public MailHelper Variables(IDictionary<string, object> bodyValues)
        {
            _variables = bodyValues;

            return this;
        }

        public MailHelper AddVariables(string key, object value)
        {
            _variables.Add(key, value);

            return this;
        }

        public MailHelper From(string email)
        {
            _from = new MailAddress(email);

            return this;
        }

        public MailHelper From(string name, string email)
        {
            _from = new MailAddress(email, name);

            return this;
        }

        public MailHelper Attachment(Stream contentStream, string name)
        {
            _attachment.Add(new Attachment(contentStream, name));

            return this;
        }

        public Result Send()
        {
            Result result = new Result();

            try
            {
                var message = new MailMessage();

                foreach (var toAddress in _toAddresses)
                    message.To.Add(toAddress);

                foreach (var ccAddress in _ccAddresses)
                    message.CC.Add(ccAddress);

                foreach (var bccAddress in _bccAddresses)
                    message.Bcc.Add(bccAddress);

                message.Subject = PrepareSubjectWithVariables();
                message.From = PrepareFrom();
                message.Body = PrepareBodyWithVariables();
                message.IsBodyHtml = true;

                GetSmtpClient().Send(message);

                result.SetSuccess();
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);

                result.SetError(ex.Message);
                result.Data = ex.StackTrace;
            }
            return result;
        }

        public Result SendWithAttachment()
        {
            Result result = new Result();
            try
            {
                var message = new MailMessage();

                foreach (var toAddress in _toAddresses)
                    message.To.Add(toAddress);

                foreach (var ccAddress in _ccAddresses)
                    message.CC.Add(ccAddress);

                foreach (var bccAddress in _bccAddresses)
                    message.Bcc.Add(bccAddress);

                message.Subject = PrepareSubjectWithVariables();
                message.From = PrepareFrom();
                message.Body = PrepareBodyWithVariables();
                message.IsBodyHtml = true;

                foreach (var attachment in _attachment)
                    message.Attachments.Add(attachment);

                GetSmtpClient().Send(message);

                result.SetSuccess();
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);

                result.SetError(ex.Message);
                result.Data = ex.StackTrace;
            }
            return result;
        }

        private SmtpClient GetSmtpClient()
        {
            var smtpClient = new SmtpClient(_mailSetting.Host, _mailSetting.Port)
            {
                EnableSsl = _mailSetting.EnableSsl
            };

            if (_mailSetting.IsAuthentication)
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_mailSetting.Username, _mailSetting.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            }

            return smtpClient;
        }

        public MailAddress PrepareFrom()
        {
            if (_from != null)
                return _from;

            return !string.IsNullOrEmpty(_mailSetting.FromName)
                ? new MailAddress(_mailSetting.FromEmail, _mailSetting.FromName)
                : new MailAddress(_mailSetting.FromEmail);
        }

        public string PrepareSubjectWithVariables()
        {
            return !_variables.Any() ? _subject : ReplaceWithVariable(_subject);
        }

        public string PrepareBodyWithVariables()
        {
            return !_variables.Any() ? _body : ReplaceWithVariable(_body);
        }

        private string ReplaceWithVariable(string str)
        {
            return _variables.Aggregate(str,
                (current, value) => current.Replace("{%" + value.Key + "%}", Convert.ToString(value.Value)));
        }

        public MailMessage PrepareEmailContent()
        {
            try
            {
                var message = new MailMessage();

                foreach (var toAddress in _toAddresses)
                    message.To.Add(toAddress);

                foreach (var ccAddress in _ccAddresses)
                    message.CC.Add(ccAddress);

                foreach (var bccAddress in _bccAddresses)
                    message.Bcc.Add(bccAddress);

                message.Subject = PrepareSubjectWithVariables();
                message.From = PrepareFrom();
                message.Body = PrepareBodyWithVariables();
                message.IsBodyHtml = true;

                //GetSmtpClient()
                //    .Send(message);

                // return new Result().SetSuccess();
                return message;
            }
            catch (Exception ex)
            {
                //SentrySdk.CaptureException(ex);
                return null;
            }
        }

    }
}