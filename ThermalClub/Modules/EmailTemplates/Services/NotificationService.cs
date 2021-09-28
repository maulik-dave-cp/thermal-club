using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Content;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Validators;
using ThermalClub.Modules.EmailTemplates.Data.Repositories;
using ThermalClub.Modules.EmailTemplates.Models;
using ThermalClub.Modules.ErrorLogs.CacheManagers;
using ThermalClub.Modules.ErrorLogs.Data.Repositories;
using ThermalClub.Modules.ErrorLogs.Models;
using ThermalClub.Modules.ErrorLogs.Models.DTOs;
using ThermalClub.Modules.ErrorLogs.Validators;
using AutoMapper;

namespace ThermalClub.Modules.EmailTemplates.Services
{
    public interface INotificationService
    {
        EmailTemplate ByEmailTemplateType(string emailTemplateType);
        Result CreateErrorLog(ErrorLogCreateDto dto);
    }

    public class NotificationService : INotificationService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IErrorLogRepository _errorLogRepository;
        private readonly ErrorLogCreateValidator _errorLogCreateValidator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IEmailTemplateRepository emailTemplateRepository, IErrorLogRepository errorLogRepository,
            ErrorLogCreateValidator errorLogCreateValidator, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _errorLogRepository = errorLogRepository;
            _errorLogCreateValidator = errorLogCreateValidator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public EmailTemplate ByEmailTemplateType(string emailTemplateType)
        {
            return _emailTemplateRepository.GetEmailTemplateByType(emailTemplateType);
        }

        public Result CreateErrorLog(ErrorLogCreateDto dto)
        {
            var result = _errorLogCreateValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var entity = _mapper.Map<ErrorLog>(dto);
            _errorLogRepository.Insert(entity);

            _unitOfWork.Commit();
            ErrorLogCacheManager.ClearCache();
            result.Id = entity.Id;

            return result.SetSuccess(Messages.RecordSaved);
        }

    }
}