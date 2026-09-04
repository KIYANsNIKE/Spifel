using Microsoft.AspNetCore.Http;
using Spifel.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Infra.Data.Services
{
    public class FileService() : IFileService
    {

        public async Task<string>   SaveAsync(
            IFormFile file,
            string folder)
        {

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                folder);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var extension = Path.GetExtension(file.FileName);

            var fileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{extension}";

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            await using var stream = new FileStream(
                filePath,
                FileMode.Create);

            await file.CopyToAsync(stream);

            return fileName;
        }

        public Task DeleteAsync(
            string fileName,
            string folder)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return Task.CompletedTask;

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                folder,
                fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }
    }
}
