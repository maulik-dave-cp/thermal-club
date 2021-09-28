using ThermalClub.Modules.Core.Api;
using ThermalClub.Modules.Core.Filters;
using ThermalClub.Modules.EmailTemplates.Models;
using ThermalClub.Modules.EmailTemplates.Models.DTOs;
using ThermalClub.Modules.EmailTemplates.Services;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.EmailTemplates.Api
{
    [Route("api/admin/email-templates")]
    public class EmailTemplatesController : BaseApiController
    {
        private readonly IEmailTemplateAdminService _emailTemplateAdminService;

        public EmailTemplatesController(
            IEmailTemplateAdminService emailTemplateAdminService)
        {
            _emailTemplateAdminService = emailTemplateAdminService;
        }

        [HttpGet("")]
        public IActionResult Get([FromQuery]EmailTemplateAdminFilterDto dto)
        {
            return Result(_emailTemplateAdminService.List(dto));
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Result(_emailTemplateAdminService.ById(id));
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody]EmailTemplateEditAdminDto dto)
        {
            return Result(_emailTemplateAdminService.Edit(id, dto));
        }

        [HttpGet("get-template-type")]
        public IActionResult GetEmailTemplateType()
        {
            return Result(Enumeration.GetDescriptionAll<TemplateType>());
        }
    }
}