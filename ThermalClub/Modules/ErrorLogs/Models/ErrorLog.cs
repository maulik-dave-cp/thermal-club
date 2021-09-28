using ThermalClub.Modules.Core.Data;
using System;

namespace ThermalClub.Modules.ErrorLogs.Models
{
    public class ErrorLog : ITrackable
    {
        public int Id { get; set; }
        public string ErrorType { get; set; }
        public string Description { get; set; }
        public string Stacktrace { get; set; }
        public bool IsEmailSent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}