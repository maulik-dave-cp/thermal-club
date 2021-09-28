using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.Core.Encryption;
using AutoMapper;

namespace ThermalClub.Modules.AdminUsers.Models.Mappers
{
    public class AdminUserMappingProfile : Profile
    {
        public AdminUserMappingProfile()
        {
            // Create
            CreateMap<AdminUserCreateDto, AdminUser>()
                .AfterMap((dto, entity) =>
                {
                    entity.Salt = SecurityHelper.GenerateSalt();
                    entity.Password = SecurityHelper.GenerateHash(dto.Password, entity.Salt);
                    entity.AdminUsersAdminRoles = new List<AdminUsersAdminRoles>();
                });

            // Edit
            CreateMap<AdminUserEditDto, AdminUser>()
                .ForMember(m => m.Password, opt => opt.Ignore())
                .AfterMap((dto, entity) =>
                {
                    if (string.IsNullOrEmpty(dto.Password)) return;

                    entity.Salt = SecurityHelper.GenerateSalt();
                    entity.Password = SecurityHelper.GenerateHash(dto.Password, entity.Salt);
                });

            CreateMap<AdminUser, AdminUserEditDto>()
                .ForMember(m => m.Password, opt => opt.Ignore())
                .ForMember(m => m.Roles, opt => opt.MapFrom(v => v.AdminUsersAdminRoles.Select(s => s.AdminRoleId).ToList()));
        }
    }
}