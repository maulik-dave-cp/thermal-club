using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Content;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Extensions;
using ThermalClub.Modules.Core.Validators;
using ThermalClub.Modules.ErrorLogs.CacheManagers;
using ThermalClub.Modules.ErrorLogs.Data.Repositories;
using ThermalClub.Modules.ErrorLogs.Filters;
using ThermalClub.Modules.ErrorLogs.Helpers;
using ThermalClub.Modules.ErrorLogs.ListOrders;
using ThermalClub.Modules.ErrorLogs.Models;
using ThermalClub.Modules.ErrorLogs.Models.DTOs;
using ThermalClub.Modules.ErrorLogs.Notifications;
using ThermalClub.Modules.ErrorLogs.Validators;
using AutoMapper;
using System.Linq;
using System.Threading;

namespace ThermalClub.Modules.ErrorLogs.Services
{
    public interface IErrorLogService
    {
        Result List(ErrorLogFilterDto dto);
        Result Create(ErrorLogCreateDto dto);
          void SendErrorLogEmail();
    }

    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _errorLogRepository;
 
        private readonly ErrorLogCreateValidator _errorLogCreateValidator;
        private readonly ErrorLogSendEmailNotification _errorLogSendEmailNotification;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ErrorLogService(IErrorLogRepository errorLogRepository, 
            ErrorLogCreateValidator errorLogCreateValidator, ErrorLogSendEmailNotification errorLogSendEmailNotification, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _errorLogRepository = errorLogRepository;
            _errorLogCreateValidator = errorLogCreateValidator;
            _errorLogSendEmailNotification = errorLogSendEmailNotification;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Result List(ErrorLogFilterDto dto)
        {
            var filter = dto ?? new ErrorLogFilterDto();
            var query = _errorLogRepository.AsNoTracking;

            query = new ErrorLogFilter(query, filter).FilteredQuery();
            query = new ErrorLogListOrder(query, filter).OrderByQuery();
            var result = new Result().SetPaging(filter, query.Count());

            result.Data = query.Select(s => new ErrorLogListDto
            {
                Id = s.Id,
                ErrorType = s.ErrorType,
                Description = s.Description,
                Stacktrace = s.Stacktrace,
                IsEmailSent = s.IsEmailSent,
                CreatedAt = s.CreatedAt
            }).ToPaged(result.Paging.Page, result.Paging.Size).ToList();

            return result;
        }

        public Result Create(ErrorLogCreateDto dto)
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
                
        public void SendErrorLogEmail()
        {
            var getErrorLogEmailList = _errorLogRepository.AsNoTracking.Where(x => x.IsEmailSent == false).Take(10).ToList();

            if (getErrorLogEmailList.Count == 0)
            {
                return;
            }

            foreach (var item in getErrorLogEmailList)
            {
                var result = _errorLogSendEmailNotification.SendErrorMail(ErrorNotificationHelper.GenerateTable(item.Description, item.Stacktrace)).Send();
                if (result.Success)
                {
                    item.IsEmailSent = true;
                    _errorLogRepository.Update(item);
                }
            }
            _unitOfWork.Commit();
        }
    }
}