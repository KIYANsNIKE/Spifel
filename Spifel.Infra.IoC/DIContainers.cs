using Microsoft.Extensions.DependencyInjection;
using Spifel.Domain.Contracts;
using Spifel.Infra.Data.Repositories;
using Spifel.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Spifel.Application.Services.Implementations;
using Spifel.Domain.Contracts.Services;
using Spifel.Infra.Data.Services;

namespace Spifel.Infra.IoC
{
    public static class DIContainers
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IFileService, FileService>();
            #endregion
        }
    }
}
