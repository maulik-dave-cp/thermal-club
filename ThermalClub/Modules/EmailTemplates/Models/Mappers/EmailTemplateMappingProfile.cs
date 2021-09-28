using System.Linq;
using ThermalClub.Modules.EmailTemplates.Models.DTOs;
using AutoMapper;

namespace ThermalClub.Modules.EmailTemplates.Models.Mappers
{
    public class EmailTemplateMappingProfile : Profile
    {
        public EmailTemplateMappingProfile()
        {
            // Edit
            CreateMap<EmailTemplateEditAdminDto, EmailTemplate>();
            CreateMap<EmailTemplate, EmailTemplateEditAdminDto>()
                .ForMember(m => m.ToEmail,
                    opt => opt.MapFrom(m =>
                        m.ToEmails.Count <= 0
                            ? string.Empty
                            : m.ToEmails.Select(s => s.Email.ToString()).StringJoin(',')))

            .ForMember(m => m.CcEmail,
                opt => opt.MapFrom(m =>
                        m.CcEmails.Count <= 0
                            ? string.Empty
                            : m.CcEmails.Select(s => s.Email.ToString()).StringJoin(',')))
                    .ForMember(m => m.BccEmail,
                        opt => opt.MapFrom(m =>
                            m.BccEmails.Count <= 0
                                ? string.Empty
                                : m.BccEmails.Select(s => s.Email.ToString()).StringJoin(',')));
        }
    }
}
