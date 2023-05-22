using KonfidesCase.Authentication.DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KonfidesCase.Authentication.Extensions
{
    public static class DependencyInjection
    {
        #region AddKonfidesAuthServices method for Program.cs
        public static IServiceCollection AddKonfidesAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KonfidesCaseAuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(KonfidesCaseAuthDbContext.ConnectionName)));

            services.AddAuthentication();                       
            
            services.AddIdentity<AuthUser, AuthRole>(options =>
            {                
                options.SignIn.RequireConfirmedAccount = true;                
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

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;                
                options.Cookie.IsEssential = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;                
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });            

            services.AddScoped<IAuthService, AuthService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
        #endregion
    }
}
