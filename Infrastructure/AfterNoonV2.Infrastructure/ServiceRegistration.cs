using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.Abstractions.Storage;
using AfterNoonV2.Application.Abstractions.Token;
using AfterNoonV2.Infrastructure.Services;
using AfterNoonV2.Infrastructure.Services.Storage;
using AfterNoonV2.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAppService, AppService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
