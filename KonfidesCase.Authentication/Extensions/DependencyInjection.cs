using KonfidesCase.Authentication.DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KonfidesCase.Authentication.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddKonfidesAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KonfidesCaseAuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(KonfidesCaseAuthDbContext.ConnectionName)));

            services.AddHttpContextAccessor();

            services.AddAuthentication();
            services.AddIdentity<AuthUser, AuthRole>(options =>
            {
                options.User.RequireUniqueEmail = true;                
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<KonfidesCaseAuthDbContext>()
                .AddDefaultTokenProviders();
            //services.AddCors(options => options.AddPolicy("myCors", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddScoped<IAuthService, AuthService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
