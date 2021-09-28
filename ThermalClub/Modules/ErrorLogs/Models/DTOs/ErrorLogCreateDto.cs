namespace ThermalClub.Modules.ErrorLogs.Models.DTOs
{
    public class ErrorLogCreateDto
    {
        public string ErrorType { get; set; }
        public string Description { get; set; }
        public string Stacktrace { get; set; }
        public bool IsEmailSent { get; set; }
    }
}