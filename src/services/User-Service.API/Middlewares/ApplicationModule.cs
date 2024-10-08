using Microsoft.EntityFrameworkCore;
using User_Service.API.Data.Persistence.Repositories;
using User_Service.API.Interfaces.Services;
using User_Service.API.Interfaces.Persistence;
using User_Service.API.Data;
using User_Service.API.Data.Persistence;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using User_Service.API.Services.Authentication;
using User_Service.API.Security;
using User_Service.API.Services.Notifications;
using User_Service.API.Services.Customers;

namespace User_Service.API.Middlewares
{
    public static class ApplicationModule
    {
        public static void AddMiddlewares(this WebApplicationBuilder builder)
        {
            builder.DbContextMiddleware();
            builder.DependencyInjectionMiddleware();
            builder.SecurityMiddleware();
            builder.SwaggerMiddleware();
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
            builder.Services.AddScoped<INotifier, Notifier>();
            builder.Services.Configure<Security.SecurityKey>(builder.Configuration.GetSection
                                                                 (nameof(Security.SecurityKey)));
        }

        public static void SwaggerMiddleware(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Lerning Code Enterprise API",
                    Contact = new OpenApiContact() { Name = "Guilherme Nascimento", Email = "guirafaelrn@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/MIT") }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta forma: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                c.AddSecurityDefinition("AcessSecretKey", new OpenApiSecurityScheme
                {
                    Description = "Chave de acesso necessária para acessar o sistema",
                    Name = "AcessSecretKey",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "AcessSecretKey"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static void SecurityMiddleware(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
                };
            });
        }
    }
}
