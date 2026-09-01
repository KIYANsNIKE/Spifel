using Spifel.Application.Services.Implementations.Features.ChangeAvatar;
using Spifel.Application.Services.Implementations.Features.ChangePassword;
using Spifel.Application.Services.Implementations.Features.Login;
using Spifel.Application.Services.Implementations.Features.Register;
using Spifel.Application.Services.Implementations.Features.Update;
using Spifel.Domain.Common.Result;
using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Result> RegisterAsync(RegisterDto dto);
        Task<Result<User>> LoginUserAsync(LoginDto dto);
        Task<bool> ActiveAccountAsync(string acctiveCode);
        Task<Result> ChangePasswordAsync(ChangePasswordDto dto);
        Task<Result<User>> GetUserByIdAsync(int userId);
        Task<Result> UpdateUserAsync(UpdateDto dto);
        Task<Result> ChangeAvatarAsync(ChangeAvatarDto dto);
        Task<Result> DeleteAvatarAsync(int userId);
        Task<Result> IsProfileCompletedAsync(int userId);
        Task<Result> ForcePasswordChange(string username,string password);
    }
}
