using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using AutoMapper;

namespace ThermalClub.Modules.AdminRolePermissions.Models.Mappers
{
    public class AdminRoleMappingProfile : Profile
    {
        public AdminRoleMappingProfile()
        {
            // Create
            CreateMap<AdminRoleCreateDto, AdminRole>();

            // Edit
            CreateMap<AdminRoleEditDto, AdminRole>();
            CreateMap<AdminRole, AdminRoleEditDto>()
                .ForMember(m => m.Permissions, opt => opt.MapFrom(v => v.AdminRolesAdminPermissionses.Select(s => s.AdminPermissionId).ToList()))
                ;
        }
    }
}