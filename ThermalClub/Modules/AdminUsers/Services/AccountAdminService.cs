using ThermalClub.Modules.AdminUsers.Data.Repositories;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.AdminUsers.Validators;
using ThermalClub.Modules.Core;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Encryption;
using ThermalClub.Modules.Core.Validators;
using AutoMapper;
using ThermalClub.Modules.Core.Content;

namespace ThermalClub.Modules.AdminUsers.Services
{
    public interface IAccountAdminService
    {
        AdminEditProfileDto GetEditProfile(int userId);
        Result SaveEditProfile(AdminEditProfileDto model);

        Result ChangePassword(AdminChangePasswordDto model);
    }

    public class AccountAdminService : IAccountAdminService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EditProfileValidator _editProfileValidator;
        private readonly ChangePasswordValidator _changePasswordValidator;
        private readonly IMapper _mapper;

        public AccountAdminService(
            IAdminUserRepository adminUserRepository,
            IUnitOfWork unitOfWork,
            EditProfileValidator editProfileValidator,
            ChangePasswordValidator changePasswordValidator,
            IMapper mapper)
        {
            _adminUserRepository = adminUserRepository;
            _editProfileValidator = editProfileValidator;
            _changePasswordValidator = changePasswordValidator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public AdminEditProfileDto GetEditProfile(int userId)
        {
            var adminUser = _adminUserRepository.Find(userId);

            var dto = new AdminEditProfileDto { Id = adminUser.Id, Name = adminUser.Name, Email = adminUser.Email };

            return _mapper.Map<AdminEditProfileDto>(dto);
        }

        public Result SaveEditProfile(AdminEditProfileDto dto)
        {
            var result = _editProfileValidator.ValidateResult(dto);
            if (!result.Success) return result;

            var dbAdminUser = _adminUserRepository.Find(dto.Id);
            if (dbAdminUser == null) return null;

            EditProfileSave(dto, dbAdminUser);
            return new Result().SetSuccess(Messages.ProfileUpdated);
        }

        private void EditProfileSave(AdminEditProfileDto model, AdminUser adminUser)
        {
            adminUser.Name = model.Name;
            adminUser.Email = model.Email;

            _adminUserRepository.Update(adminUser);
            _unitOfWork.Commit();
        }

        public Result ChangePassword(AdminChangePasswordDto dto)
        {
            var result = _changePasswordValidator.ValidateResult(dto);

            if (!result.Success)
                return result;

            var adminUser = _adminUserRepository.Find(dto.Id);
            ChangePasswordSave(dto.NewPassword, adminUser);

            return new Result().SetSuccess(Messages.PasswordUpdated).Clear();
        }

        private void ChangePasswordSave(string newPassword, AdminUser adminUser)
        {
            var salt = SecurityHelper.GenerateSalt();
            var encryptedPassword = SecurityHelper.GenerateHash(newPassword, salt);

            adminUser.Salt = salt;
            adminUser.Password = encryptedPassword;

            _adminUserRepository.Update(adminUser);
            _unitOfWork.Commit();
        }
    }
}