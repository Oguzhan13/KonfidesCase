using KonfidesCase.BLL.Services.Concretes;
using KonfidesCase.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KonfidesCase.BLL.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddKonfidesBllServices(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
