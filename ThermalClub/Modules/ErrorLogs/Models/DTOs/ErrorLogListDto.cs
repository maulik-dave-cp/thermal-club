using System;

namespace ThermalClub.Modules.ErrorLogs.Models.DTOs
{
    public class ErrorLogListDto
    {
        public int Id { get; set; }
        public string ErrorType { get; set; }
        public string Description { get; set; }
        public string Stacktrace { get; set; }
        public bool IsEmailSent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}