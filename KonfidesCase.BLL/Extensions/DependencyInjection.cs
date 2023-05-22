using KonfidesCase.BLL.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KonfidesCase.BLL.Extensions
{
    public static class DependencyInjection
    {
        #region AddConfidesBLLServices method for Program.cs
        public static IServiceCollection AddKonfidesBllServices(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
        #endregion
    }
}
