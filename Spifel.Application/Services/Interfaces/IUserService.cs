using Spifel.Domain.Common.Result;
using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<IEnumerable<User>>> GetAllUsersAsync();
    }
}
