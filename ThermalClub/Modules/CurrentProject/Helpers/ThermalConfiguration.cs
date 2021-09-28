using System;

namespace ThermalClub.Modules.CurrentProject.Helpers
{
    public class ThermalConfiguration
    {
        public BackendAjConfiguration Backend { get; set; }
        public MailSetting MailSetting { get; set; }

        public SiteSetting SiteSetting { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }
        public ThermalClub ThermalClub { get; set; }
       
    }

    public class SiteSetting
    {
        public string SiteTitle { get; set; }
        public string WebsiteUrl { get; set; }
    }

    public class BackendAjConfiguration
    {
        public string JwtSecret { get; set; }
    }

    public class MailSetting
    {
        public bool Enabled { get; set; }

        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string ContactEmail { get; set; }

        public string Host { get; set; }
        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public bool IsAuthentication { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnectionString { get; set; }       
    }

    public class ThermalClub
    {
        public string FuelLevelGasStationIp { get; set; }
    }

 
}