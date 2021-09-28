using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Extensions;
using ThermalClub.Modules.Core.Validators;
using ThermalClub.Modules.EmailTemplates.CacheManagers;
using ThermalClub.Modules.EmailTemplates.Data.Repositories;
using ThermalClub.Modules.EmailTemplates.Filters;
using ThermalClub.Modules.EmailTemplates.ListOrders;
using ThermalClub.Modules.EmailTemplates.Models;
using ThermalClub.Modules.EmailTemplates.Models.DTOs;
using ThermalClub.Modules.EmailTemplates.Validators;
using AutoMapper;
using ThermalClub.Modules.Core.Content;

namespace ThermalClub.Modules.EmailTemplates.Services
{
    public interface IEmailTemplateAdminService
    {
        Result List(EmailTemplateAdminFilterDto dto);

        EmailTemplateEditAdminDto ById(int id);
        Result Edit(int id, EmailTemplateEditAdminDto dto);
    }

    public class EmailTemplateAdminService : IEmailTemplateAdminService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly EmailTemplateEditAdminValidator _validatorEdit;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmailTemplateAdminService(
            IEmailTemplateRepository emailTemplateRepository,
            EmailTemplateEditAdminValidator validatorEdit,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _validatorEdit = validatorEdit;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result List(EmailTemplateAdminFilterDto dto)
        {
            var filter = dto ?? new EmailTemplateAdminFilterDto();
            var query = _emailTemplateRepository.AsNoTracking;

            query = new EmailTemplateAdminFilter(query, filter).FilteredQuery();
            query = new EmailTemplateAdminListOrder(query, filter).OrderByQuery();
            var result = new Result().SetPaging(filter, query.Count());

            result.Data = query.Select(s => new
            {
                s.Id,
                s.Name,
                s.CreatedAt,
                s.UpdatedAt,
            })
                .ToPaged(result.Paging.Page, result.Paging.Size)
                .ToList();

            return result;
        }

        public EmailTemplateEditAdminDto ById(int id)
        {
            var entity = _emailTemplateRepository.Find(id);

            return _mapper.Map<EmailTemplateEditAdminDto>(entity);
        }

        public Result Edit(int id, EmailTemplateEditAdminDto dto)
        {
            dto.Id = id;

            var result = _validatorEdit.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _emailTemplateRepository.Find(id);

            if (entity == null)
                return null;

            _mapper.Map(dto, entity);

            _emailTemplateRepository.Update(entity);

            _unitOfWork.Commit();
            EmailTemplateCacheManager.ClearCache();

            return result.SetSuccess(Messages.RecordSaved);
        }
    }
}