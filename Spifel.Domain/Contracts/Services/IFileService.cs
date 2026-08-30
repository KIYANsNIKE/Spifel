using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Contracts.Services
{
    public interface IFileService
    {
        Task<string> SaveAsync(IFormFile file, string folder);

        Task DeleteAsync(string fileName, string folder);
    }
}
