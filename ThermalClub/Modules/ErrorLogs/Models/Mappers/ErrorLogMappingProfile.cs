using ThermalClub.Modules.ErrorLogs.Models.DTOs;
using AutoMapper;

namespace ThermalClub.Modules.ErrorLogs.Models.Mappers
{
    public class ErrorLogMappingProfile : Profile
    {
        public ErrorLogMappingProfile()
        {
            // Create
            CreateMap<ErrorLogCreateDto, ErrorLog>();
        }
    }
}