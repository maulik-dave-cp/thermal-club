using ThermalClub.Modules.Core.Filters;
using System;

namespace ThermalClub.Modules.ErrorLogs.Models.DTOs
{
    public class ErrorLogFilterDto : BaseFilterDto
    {
        public ErrorLogFilterDto()
        {
            SortColumn = "CreatedAt";
            SortType = "DESC";
        }
        public string ErrorType { get; set; }
        public string Description { get; set; }
        public string Stacktrace { get; set; }
        public bool IsEmailSent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}