using Microsoft.EntityFrameworkCore;
using User_Service.API.Data.Persistence.Repositories;
using User_Service.API.Interfaces.Events;
using User_Service.API.Interfaces.Services;
using User_Service.API.Interfaces.Persistence;
using User_Service.API.Services;
using User_Service.API.Events.Notify;
using User_Service.API.Data;
using User_Service.API.Security;
using User_Service.API.Data.Persistence;

namespace User_Service.API.Middlewares
{
    public static class ApplicationModule
    {
        public static void AddMiddlewares(this WebApplicationBuilder builder)
        {
            builder.DbContextMiddleware();
            builder.DependencyInjectionMiddleware();
        }
        public static void DbContextMiddleware(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));
        }

        public static void DependencyInjectionMiddleware(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddScoped<INotifyer, Notifyer>();
        }
    }
}
