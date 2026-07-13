using Spifel.Application.Services.Implementations.Features.ChangePassword;
using Spifel.Application.Services.Implementations.Features.Login;
using Spifel.Application.Services.Implementations.Features.Register;
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
    }
}
