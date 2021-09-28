using ThermalClub.Modules.AdminRolePermissions.Data.Repositories;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminUsers.CacheManagers;
using ThermalClub.Modules.AdminUsers.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Filters;
using ThermalClub.Modules.AdminUsers.ListOrders;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.AdminUsers.Validators;
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

namespace ThermalClub.Modules.AdminUsers.Services
{
    public interface IAdminUserService
    {
        Result List(AdminUserFilterDto dto);
        Result Active(IList<int> ids);
        Result Inactive(IList<int> ids, int loggedInUserId);
        Result Delete(IList<int> ids, int loggedInUserId);

        Result Create(AdminUserCreateDto dto);
        AdminUserEditDto ById(int id);
        Result Edit(int id, AdminUserEditDto dto);

        IList<string> GetRoles(int userId);
        IList<string> GetPermissions(int userId);   
        IList<IdNameDto> GetAdminUsers();
    }

    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IAdminRoleRepository _adminRoleRepository;
        private readonly AdminUserCreateValidator _adminUserCreateValidator;
        private readonly AdminUserEditValidator _adminUserEditValidator;
        private readonly IAdminUsersAdminRolesRepository _adminUsersAdminRolesRepository;
        private readonly IAdminRolesAdminPermissionsRepository _adminRolesAdminPermissionsRepository;
          
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminUserService(
            IAdminUserRepository adminUserRepository,
            IAdminRoleRepository adminRoleRepository,
            AdminUserCreateValidator adminUserCreateValidator,
            AdminUserEditValidator adminUserEditValidator,
            IAdminUsersAdminRolesRepository adminUsersAdminRolesRepository,
            IAdminRolesAdminPermissionsRepository adminRolesAdminPermissionsRepository,
                 
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _adminUserRepository = adminUserRepository;
            _adminRoleRepository = adminRoleRepository;
            _adminUserCreateValidator = adminUserCreateValidator;
            _adminUserEditValidator = adminUserEditValidator;
            _adminUsersAdminRolesRepository = adminUsersAdminRolesRepository;
            _adminRolesAdminPermissionsRepository = adminRolesAdminPermissionsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result List(AdminUserFilterDto dto)
        {
            var filter = dto ?? new AdminUserFilterDto();
            var query = _adminUserRepository.AsNoTracking;

            query = new AdminUserFilter(query, filter).FilteredQuery();
            query = new AdminUserListOrder(query, filter).OrderByQuery();
            var result = new Result().SetPaging(filter, query.Count());

            result.Data = query.Select(s => new
	            {
		            s.Id,
		            s.Name,
		            s.Email,
		            s.IsActive,
		            s.LastLoginAt,
		            s.CreatedAt,
		            AdminRoles = s.AdminUsersAdminRoles.Select(x => new {x.AdminRole.Name}).ToList()
	            }).ToPaged(result.Paging.Page, result.Paging.Size)
                .ToList();

            return result;
        }

        public Result Active(IList<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return new Result().SetError(Messages.SelectAtLeastOneItemFromList);

            var query = _adminUserRepository.AsNoTracking.Where(q => ids.Contains(q.Id));
            var result = new Result().SetSuccess(Messages.RecordActivate, query.Count());

            query.Update(w => new AdminUser {IsActive = true});
            AdminUserCacheManager.ClearCache();

            return result;
        }

        public Result Inactive(IList<int> ids, int loggedInUserId)
        {
            if (ids == null || ids.Count == 0)
                return new Result().SetError(Messages.SelectAtLeastOneItemFromList);

            if (ids.Contains(loggedInUserId))
                return new Result().SetError(Messages.AdminTryOwnAccountInactive);

            var query = _adminUserRepository.AsNoTracking.Where(q => ids.Contains(q.Id));
            var result = new Result().SetSuccess(Messages.RecordInactivate, query.Count());

            query.Update(w => new AdminUser {IsActive = false});
            AdminUserCacheManager.ClearCache();

            return result;
        }

        public Result Delete(IList<int> ids, int loggedInUserId)
        {
            if (ids == null || ids.Count == 0)
                return new Result().SetError(Messages.SelectAtLeastOneItemFromList);

            if (ids.Contains(loggedInUserId))
                return new Result().SetError(Messages.AdminTryOwnAccountDelete);

            var query = _adminUserRepository.AsNoTracking.Where(q => ids.Contains(q.Id));
            var result = new Result().SetSuccess(Messages.RecordDelete, query.Count());

            query.Delete();
            AdminUserCacheManager.ClearCache();

            return result;
        }

        public Result Create(AdminUserCreateDto dto)
        {
            var result = _adminUserCreateValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _mapper.Map<AdminUser>(dto);
            _adminUserRepository.Insert(entity);

            foreach (var roleId in dto.Roles)
                entity.AdminUsersAdminRoles.Add(new AdminUsersAdminRoles
                {
                    AdminRole = _adminRoleRepository.Find(roleId)
                });

            _unitOfWork.Commit();
            AdminUserCacheManager.ClearCache();
            result.Id = entity.Id;

            return result.SetSuccess(Messages.RecordSaved);
        }

        public AdminUserEditDto ById(int id)
        {
            var entity = _adminUserRepository.AsNoTracking
                .Include(i => i.AdminUsersAdminRoles)
                .ThenInclude(i => i.AdminRole)
                .FirstOrDefault(s => s.Id == id);

            return _mapper.Map<AdminUserEditDto>(entity);
        }

        public Result Edit(int id, AdminUserEditDto dto)
        {
            dto.Id = id;

            var result = _adminUserEditValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _adminUserRepository.AsNoTracking
                .Include(i => i.AdminUsersAdminRoles)
                .ThenInclude(i => i.AdminRole)
                .FirstOrDefault(s => s.Id == dto.Id);

            if (entity == null)
                return null;

            _mapper.Map(dto, entity);
            _adminUserRepository.Update(entity);
            ChildRoleUpdate(entity, dto);

            _unitOfWork.Commit();
            AdminUserCacheManager.ClearCache();

            return result.SetSuccess(Messages.RecordSaved);
        }

        private void ChildRoleUpdate(AdminUser entity, AdminUserEditDto dto)
        {
            var currentRecords = entity.AdminUsersAdminRoles.Select(s => s.AdminRoleId).ToList();

            var addedRecords = dto.Roles.Except(currentRecords).ToList();
            foreach (var record in addedRecords)
                entity.AdminUsersAdminRoles.Add(
                    new AdminUsersAdminRoles()
                    {
                        AdminRole = _adminRoleRepository.Find(record)
                    });

            var deletedRecords = currentRecords.Except(dto.Roles).ToList();
            foreach (var record in deletedRecords)
                entity.AdminUsersAdminRoles.Remove(entity.AdminUsersAdminRoles.First(w => w.AdminRoleId == record));
        }

        public IList<string> GetRoles(int userId)
        {
            return _adminUsersAdminRolesRepository.AsNoTracking
                .Include(i => i.AdminRole)
                .Where(w => w.AdminUserId == userId)
                .Select(s => s.AdminRole.SystemName.ToLower())
                .ToList();
        }

        public IList<string> GetPermissions(int userId)
        {
            var roleIds = _adminUsersAdminRolesRepository.AsNoTracking
                .Where(w => w.AdminUserId == userId)
                .Select(s => s.AdminRoleId)
                .ToList();

            var permissions = _adminRolesAdminPermissionsRepository.AsNoTracking
                .Include(i => i.AdminPermission)
                .Where(w => roleIds.Contains(w.AdminRoleId))
                .OrderBy(o => o.AdminPermission.Left)
                .Select(s => s.AdminPermission.Name)
                .ToList();

            return permissions;
        }

        public IList<IdNameDto> GetAdminUsers()
        {
            return _adminUserRepository.AsNoTracking
                .OrderBy(w => w.Name)
                .Select(s => new IdNameDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .FromCache(AdminUserCacheManager.Name)
                .ToList();
        }
    }
}