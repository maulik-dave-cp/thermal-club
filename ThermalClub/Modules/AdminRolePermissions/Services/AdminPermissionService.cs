using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.CacheManagers;
using ThermalClub.Modules.AdminRolePermissions.Data.Repositories;
using ThermalClub.Modules.AdminRolePermissions.Filters;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using ThermalClub.Modules.AdminRolePermissions.Validators;
using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Extensions;
using ThermalClub.Modules.Core.Validators;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThermalClub.Modules.Core.Content;
using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.AdminRolePermissions.Services
{
    public interface IAdminPermissionService
    {
        Result List(AdminPermissionFilterDto dto);
        Result Delete(IList<int> ids);

        Result Create(AdminPermissionDto dto);
        AdminPermissionDto ById(int id);
        Result Edit(int id, AdminPermissionDto dto);

        IEnumerable<AdminPermissionDto> GetSequenceData();
        void SaveSequenceData(IList<AdminPermissionSequenceDto> data);

        IEnumerable<AdminPermissionDropDownDto> GetAdminPermissions();
    }

    public class AdminPermissionService : IAdminPermissionService
    {
        private readonly IAdminPermissionRepository _adminPermissionRepository;
        private readonly AdminPermissionValidator _adminPermissionValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminPermissionService(
            IAdminPermissionRepository adminPermissionRepository,
            AdminPermissionValidator adminPermissionValidator,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _adminPermissionRepository = adminPermissionRepository;
            _adminPermissionValidator = adminPermissionValidator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result List(AdminPermissionFilterDto dto)
        {
            var filter = dto ?? new AdminPermissionFilterDto();
            var query = _adminPermissionRepository.AsNoTracking;

            query = new AdminPermissionFilter(query, filter).FilteredQuery();
            query = query.OrderBy(o => o.Left);
            var result = new Result().SetPaging(filter, query.Count());

            result.Data = query.Select(x => new
            {
                x.Id,
                x.Name,
                x.DisplayName,
                Depth = _adminPermissionRepository.AsNoTracking.Count(w => w.Left < x.Left && w.Right > x.Right),
            })
                .ToPaged(result.Paging.Page, result.Paging.Size)
                .ToList();

            return result;
        }

        public Result Delete(IList<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return new Result().SetError(Messages.SelectAtLeastOneItemFromList);

            var query = _adminPermissionRepository.AsNoTracking
                .Where(q => ids.Contains(q.Id));

            var result = new Result().SetSuccess(Messages.RecordDelete, query.Count());

            query.Delete();
            AdminRoleCacheManager.ClearCache();

            return result;
        }

        public Result Create(AdminPermissionDto dto)
        {
            var result = _adminPermissionValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _mapper.Map<AdminPermission>(dto);

            _adminPermissionRepository.Insert(entity);

            _unitOfWork.Commit();
            AdminRoleCacheManager.ClearCache();

            result.Id = entity.Id;

            return result.SetSuccess(Messages.RecordSaved);
        }

        public AdminPermissionDto ById(int id)
        {
            var entity = _adminPermissionRepository.Find(id);

            return _mapper.Map<AdminPermissionDto>(entity);
        }

        public Result Edit(int id, [FromBody] AdminPermissionDto dto)
        {
            var result = _adminPermissionValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _adminPermissionRepository.Find(dto.Id);

            if (entity == null)
                return null;

            _mapper.Map(dto, entity);
            _adminPermissionRepository.Update(entity);

            _unitOfWork.Commit();

            _adminPermissionRepository.MoveToParentNode("AdminPermissions", dto.Id,
                dto.IsParentSelected == true ? dto.ParentId : null);

            AdminRoleCacheManager.ClearCache();

            return result.SetSuccess(Messages.RecordSaved);
        }

        public IEnumerable<AdminPermissionDto> GetSequenceData()
        {
            return _adminPermissionRepository.AsNoTracking
                .OrderBy(o => o.Left)
                .Select(s => new AdminPermissionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DisplayName = s.DisplayName,
                    Depth = _adminPermissionRepository.AsNoTracking.Count(w => w.Left < s.Left && w.Right > s.Right),
                    Left = s.Left,
                    Right = s.Right,
                    ParentId = s.ParentId ?? 0
                }).ToList();
        }

        public void SaveSequenceData(IList<AdminPermissionSequenceDto> data)
        {
            var adminPermissions = _adminPermissionRepository.AsNoTracking.ToList();

            var sequence = 1;
            SetPermissionTree(ref adminPermissions, data, null, ref sequence);
            _unitOfWork.Commit();

            AdminRoleCacheManager.ClearCache();
        }

        private void SetPermissionTree(ref List<AdminPermission> adminPermissions, IEnumerable<AdminPermissionSequenceDto> data, int? parentId, ref int sequence)
        {
            foreach (var perms in data)
            {
                var adminPermission = adminPermissions.First(w => w.Id == (int)perms.Item.Id);

                adminPermission.Left = sequence++;
                adminPermission.ParentId = parentId;

                if (perms.Children != null && perms.Children.Any())
                {
                    SetPermissionTree(ref adminPermissions, perms.Children, adminPermission.Id, ref sequence);
                }

                adminPermission.Right = sequence++;
                _adminPermissionRepository.Update(adminPermission);
            }
        }

        public IEnumerable<AdminPermissionDropDownDto> GetAdminPermissions()
        {
            return _adminPermissionRepository.AsNoTracking
                .OrderBy(o => o.Left)
                .Select(s => new AdminPermissionDropDownDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DisplayName = s.DisplayName,
                    Depth = _adminPermissionRepository.AsNoTracking.Count(w => w.Left < s.Left && w.Right > s.Right),
                    Left = s.Left,
                    Right = s.Right,
                    ParentId = s.ParentId ?? 0
                }).ToList();
        }
    }
}