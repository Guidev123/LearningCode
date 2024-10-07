using Microsoft.EntityFrameworkCore;
using User.API.Data;
using User.API.Data.Persistence.Repositories;
using User.API.Events.Notify;
using User.API.Interfaces.Events;
using User.API.Interfaces.Persistence;
using User.API.Interfaces.Services;
using User.API.Security;
using User.API.Services;

namespace User.API.Middlewares
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
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddScoped<INotifyer, Notifyer>();
        }
    }
}
