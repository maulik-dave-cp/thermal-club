using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using AutoMapper;

namespace ThermalClub.Modules.AdminRolePermissions.Models.Mappers
{
    public class AdminPermissionMappingProfile : Profile
    {
        public AdminPermissionMappingProfile()
        {
            // Create
            CreateMap<AdminPermissionDto, AdminPermission>()
                .AfterMap((dto, entity) => entity.ParentId = dto.IsParentSelected == true ? dto.ParentId : null);

            // Edit
            CreateMap<AdminPermission, AdminPermissionDto>()
                .AfterMap((dto, entity) => entity.IsParentSelected = dto.ParentId > 0);
        }
    }
}