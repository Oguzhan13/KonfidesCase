using KonfidesCase.DAL.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KonfidesCase.DAL.Extensions
{
    public static class DependencyInjection
    {
        #region AddKonfidesDalServices method for Program.cs
        public static IServiceCollection AddKonfidesDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KonfidesCaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(KonfidesCaseDbContext.ConnectionName)));

            return services;
        }
        #endregion
    }
}
