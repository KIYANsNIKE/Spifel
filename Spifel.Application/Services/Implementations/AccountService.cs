using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Spifel.Application.Generator;
using Spifel.Application.Mapper;
using Spifel.Application.Security;
using Spifel.Application.Services.Implementations.Features.ChangeAvatar;
using Spifel.Application.Services.Implementations.Features.ChangePassword;
using Spifel.Application.Services.Implementations.Features.Login;
using Spifel.Application.Services.Implementations.Features.Register;
using Spifel.Application.Services.Implementations.Features.Update;
using Spifel.Application.Services.Interfaces;
using Spifel.Application.Shared.Validations;
using Spifel.Domain.Common.Result;
using Spifel.Domain.Contracts;
using Spifel.Domain.Contracts.Services;
using Spifel.Domain.Models.User;
using Spifel.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations
{
    public class AccountService(IUserRepository _userRepository,IFileService _fileService) : IAccountService
    {
        public async Task<bool> ActiveAccountAsync(string acctiveCode)
        {
            var user = await _userRepository.GetUserByActiveCodeAsync(acctiveCode);

            if (user == null) return false;

            user.IsEmailActive = true;
            user.EmailActiveCode = UniqName.Generate();

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();

            return true;
        }

        public async Task<Result> ChangeAvatarAsync(ChangeAvatarDto dto)
        {
            // Validation
            var validation = await new ChangeAvatarDtoValidator().ValidateAsync(dto);
            var validationResult = validation.ToResult();

            if(validationResult.IsFailure)
                return validationResult;


            // Saving file 
            var newFileName = await _fileService.SaveAsync(dto.AvatarFile, "Profiles");

            // update avatar 
            var user = await _userRepository.GetUserByIdAsync(dto.UserId);
            var oldFileName = user.Avatar;
            user.Avatar = newFileName;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();

            //Deleting old file
            if (!string.IsNullOrWhiteSpace(oldFileName) && !oldFileName.StartsWith("NoPhoto"))
            {
                await _fileService.DeleteAsync(oldFileName, "Profiles");
            }



            return Result.Success();    
        }

        public async Task<Result> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(dto.UserId);

            if (!PasswordHelper.VerifyPassword(dto.OldPassword, user.Password))
                return Result<User>.Failure(Error.Forbidden(ErrorCode.PasswordMismatch.ToString(), "رمز عبور صحیح نیست"));

            user.Password = PasswordHelper.HashPassword(dto.NewPassword);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();

            return Result.Success();

        }

        public async Task<Result> DeleteAvatarAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            await _fileService.DeleteAsync(user.Avatar, "Profiles");

            user.Avatar = "NoPhoto.jpg";
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();


            return Result.Success();
        }

        public async Task<Result<User>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return Result<User>.Success(user!);
        }

        public async Task<Result<User>> LoginUserAsync(LoginDto dto)
        {
            var user = await _userRepository.GetUserByEmailOrUserNameAsync(dto.UserNameOrEmail);
            if (user == null || user.IsDeleted)
                return Result<User>.Failure(Error.NotFound(ErrorCode.UserNotFound.ToString(), "کاربر یافت نشد"));

            if (!PasswordHelper.VerifyPassword(dto.Password, user.Password))
                return Result<User>.Failure(Error.NotFound(ErrorCode.UserNotFound.ToString(), "کاربر یافت نشد"));

            if (!user.IsEmailActive)
                return Result<User>.Failure(Error.NotActive(ErrorCode.UserNotActive.ToString(), "اکانت فعال نیست"));

            return Result<User>.Success(user);
        }

        public async Task<Result> RegisterAsync(RegisterDto dto)
        {
            var validation =await new RegisterDtoValidator(_userRepository).ValidateAsync(dto);
            var validationResult = validation.ToResult();

            if (validationResult.IsFailure)
                return validationResult;

            // Register User
            var user = dto.ToUser();
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveAsync();

            // Email activation
            return Result.Success();
        }

        public async Task<Result> UpdateUserAsync(UpdateDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(dto.Id);
            //mapper later
            // Username , Email AUTHENTICATION
            user.UserName = dto.UserName;
            user.FirstName = dto.Name;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Mobile = dto.PhoneNumber;
            //mapper later
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveAsync();
            return Result.Success();
        }
    }
}
