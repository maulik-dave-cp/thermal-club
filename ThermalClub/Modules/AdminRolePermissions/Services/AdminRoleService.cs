using ThermalClub.Modules.AdminRolePermissions.CacheManagers;
using ThermalClub.Modules.AdminRolePermissions.Data.Repositories;
using ThermalClub.Modules.AdminRolePermissions.Filters;
using ThermalClub.Modules.AdminRolePermissions.ListOrders;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using ThermalClub.Modules.AdminRolePermissions.Validators;
using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Content;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.DTOs;
using ThermalClub.Modules.Core.Extensions;
using ThermalClub.Modules.Core.Validators;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.AdminRolePermissions.Services
{
    public interface IAdminRoleService
    {
        Result List(AdminRoleFilterDto dto);
        Result Delete(IList<int> ids);

        Result Create(AdminRoleCreateDto dto);
        AdminRoleEditDto ById(int id);
        Result Edit(int id, AdminRoleEditDto dto);

        IList<IdNameDto> GetRoles();
    }

    public class AdminRoleService : IAdminRoleService
    {
        private readonly IAdminRoleRepository _adminRoleRepository;
        private readonly AdminRoleCreateValidator _adminRoleCreateValidator;
        private readonly AdminRoleEditValidator _adminRoleEditValidator;
        private readonly IAdminPermissionRepository _adminPermissionRepository;
        private readonly IAdminRolesAdminPermissionsRepository _adminRolesAdminPermissionsRepository;
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminRoleService(
            IAdminRoleRepository adminRoleRepository,
            AdminRoleCreateValidator adminRoleCreateValidator,
            AdminRoleEditValidator adminRoleEditValidator,
            IAdminPermissionRepository adminPermissionRepository,
            IAdminRolesAdminPermissionsRepository adminRolesAdminPermissionsRepository,
         
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _adminRoleRepository = adminRoleRepository;
            _adminRoleCreateValidator = adminRoleCreateValidator;
            _adminRoleEditValidator = adminRoleEditValidator;
            _adminPermissionRepository = adminPermissionRepository;
            _adminRolesAdminPermissionsRepository = adminRolesAdminPermissionsRepository;
             
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result List(AdminRoleFilterDto dto)
        {
            var filter = dto ?? new AdminRoleFilterDto();
            var query = _adminRoleRepository.AsNoTracking;

            query = new AdminRoleFilter(query, filter).FilteredQuery();
            query = new AdminRoleListOrder(query, filter).OrderByQuery();
            var result = new Result().SetPaging(filter, query.Count());

            result.Data = query.Select(s => new
            {
                s.Id,
                s.Name,
               
            })
                .ToPaged(result.Paging.Page, result.Paging.Size)
                .ToList();

            return result;
        }

        public Result Delete(IList<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return new Result().SetError(Messages.SelectAtLeastOneItemFromList);

            var query = _adminRoleRepository.AsNoTracking.Include(i => i.AdminUsersAdminRoles)
                .Where(q => ids.Contains(q.Id));

            if (query.Any(w => w.AdminUsersAdminRoles.Any()))
                return new Result().SetError("You can't delete any record(s) which are assigned to any other record(s).");

            var result = new Result().SetSuccess(Messages.RecordDelete, query.Count());
            _adminRolesAdminPermissionsRepository.AsNoTracking.Where(w => ids.Contains(w.AdminRoleId)).Delete();
          
            query.Delete();
            AdminRoleCacheManager.ClearCache();

            return result;
        }

        public Result Create(AdminRoleCreateDto dto)
        {
            var result = _adminRoleCreateValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _mapper.Map<AdminRole>(dto);

            entity.SystemName = _adminRoleRepository.GenerateUniqueSlug(entity.Name, slugFieldName: "SystemName");
            entity.AdminRolesAdminPermissionses = new List<AdminRolesAdminPermissions>();
        
            _adminRoleRepository.Insert(entity);

            foreach (var roleId in dto.Permissions)
                entity.AdminRolesAdminPermissionses.Add(new AdminRolesAdminPermissions()
                {
                    AdminPermission = _adminPermissionRepository.Find(roleId)
                });

         

            _unitOfWork.Commit();
            AdminRoleCacheManager.ClearCache();

            result.Id = entity.Id;
            return result.SetSuccess(Messages.RecordSaved);
        }

        public AdminRoleEditDto ById(int id)
        {
            var entity = _adminRoleRepository.AsNoTracking
                .Include(i => i.AdminRolesAdminPermissionses)
                .FirstOrDefault(s => s.Id == id);

            return _mapper.Map<AdminRoleEditDto>(entity);
        }

        public Result Edit(int id, AdminRoleEditDto dto)
        {
            dto.Id = id;

            var result = _adminRoleEditValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _adminRoleRepository.AsNoTracking
                .Include(i => i.AdminRolesAdminPermissionses)
                .FirstOrDefault(s => s.Id == dto.Id);

            if (entity == null)
                return null;

            _mapper.Map(dto, entity);
            _adminRoleRepository.Update(entity);
            ChildPermissionUpdate(entity, dto);
        
            _unitOfWork.Commit();
            AdminRoleCacheManager.ClearCache();

            return result.SetSuccess(Messages.RecordSaved);
        }

        private void ChildPermissionUpdate(AdminRole entity, AdminRoleEditDto dto)
        {
            var currentRecords = entity.AdminRolesAdminPermissionses.Select(s => s.AdminPermissionId).ToList();

            var addedRecords = dto.Permissions.Except(currentRecords).ToList();

            foreach (var record in addedRecords)
                entity.AdminRolesAdminPermissionses.Add(
                    new AdminRolesAdminPermissions()
                    {
                        AdminPermission = _adminPermissionRepository.Find(record)
                    });

            var deletedRecords = currentRecords.Except(dto.Permissions).ToList();

            foreach (var record in deletedRecords)
                entity.AdminRolesAdminPermissionses.Remove(entity.AdminRolesAdminPermissionses.First(w => w.AdminPermissionId == record));
        }

        public IList<IdNameDto> GetRoles()
        {
            return _adminRoleRepository.Cached();
        }
    }
}