using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Spifel.Application.Generator;
using Spifel.Application.Mapper;
using Spifel.Application.Security;
using Spifel.Application.Services.Implementations.Features.Login;
using Spifel.Application.Services.Implementations.Features.Register;
using Spifel.Application.Services.Interfaces;
using Spifel.Application.Shared.Validations;
using Spifel.Domain.Common.Result;
using Spifel.Domain.Contracts;
using Spifel.Domain.Models.User;
using Spifel.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations
{
    public class AccountService(IUserRepository _userRepository) : IAccountService
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

        public async Task<Result<User>> LoginUserAsync(LoginDto dto)
        {
            var user = await _userRepository.GetUserByEmailOrUserName(dto.UserNameOrEmail);
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
            var validation = new RegisterDtoValidator(_userRepository).ValidateAsync(dto);
            var validationResult = validation.Result.ToResult();

            if (validationResult.IsFailure)
                return validationResult;

            // Register User
            var user = dto.ToUser();
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveAsync();

            // Email activation
            return Result.Success();
        }
    }
}
