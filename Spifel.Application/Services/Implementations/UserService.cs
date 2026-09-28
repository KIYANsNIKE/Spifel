using Spifel.Application.Services.Interfaces;
using Spifel.Domain.Common.Result;
using Spifel.Domain.Contracts;
using Spifel.Domain.Contracts.Services;
using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations
{
    public class UserService(IUserRepository _userRepository, IFileService _fileService) : IUserService
    {
        public async Task<Result<IEnumerable<User>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            return Result<IEnumerable<User>>.Success(users);
        }
    }
}
