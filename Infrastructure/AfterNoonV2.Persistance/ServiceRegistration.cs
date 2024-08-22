using AfterNoonV2.Application.Abstractions.Services;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Application.Services;
using AfterNoonV2.Domain.Entities.Identity;
using AfterNoonV2.Persistance.Repositeries;
using AfterNoonV2.Persistance.Services;
using AfterNoonV2.Persistance.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterNoonV2.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddServiceRegestration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AfterNoonV2DbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PSql")));
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AfterNoonV2DbContext>().AddDefaultTokenProviders();

            services.AddScoped<ICustomerReadRepo, CustomerReadRepo>();
            services.AddScoped<ICustomerWriteRepo, CustomerWriteRepo>();
            services.AddScoped<IOrderReadRepo, OrderReadRepo>();
            services.AddScoped<IOrderWriteRepo, OrderWriteRepo>();
            services.AddScoped<IProductReadRepo, ProductReadRepo>();
            services.AddScoped<IProductWriteRepo, ProductWriteRepo>();
            services.AddScoped<IFileReadRepo, FileReadRepo>();
            services.AddScoped<IFileWriteRepo, FileWriteRepo>();
            services.AddScoped<IInvoiceFileReadRepo, InvoiceFileReadRepo>();
            services.AddScoped<IInvoiceFileWriteRepo, InvoiceFileWriteRepo>();
            services.AddScoped<IProductImageFileReadRepo, ProductImageFileReadRepo>();
            services.AddScoped<IProductImageFileWriteRepo, ProductImageFileWriteRepo>();
            services.AddScoped<IBasketReadRepo, BasketReadRepo>();
            services.AddScoped<IBasketWriteRepo, BasketWriteRepo>();
            services.AddScoped<IBasketItemReadRepo, BasketItemReadRepo>();
            services.AddScoped<IBasketItemWriteRepo, BasketItemWriteRepo>();
            services.AddScoped<IMenuReadRepo, MenuReadRepo>();
            services.AddScoped<IMenuWriteRepo, MenuWriteRepo>();
            services.AddScoped<IEndpointReadRepo, EndpointReadRepo>();
            services.AddScoped<IEndpointWriteRepo, EndpointWriteRepo>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();

            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
