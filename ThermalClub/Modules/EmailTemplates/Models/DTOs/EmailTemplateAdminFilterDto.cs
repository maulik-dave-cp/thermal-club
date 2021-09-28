using ThermalClub.Modules.Core.Filters;

namespace ThermalClub.Modules.EmailTemplates.Models.DTOs
{
    public class EmailTemplateAdminFilterDto : BaseFilterDto
    {
        public EmailTemplateAdminFilterDto()
        {
            SortColumn = "Name";
            SortType = "ASC";
        }

        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
