using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UserService.API.Data;
using UserService.API.Data.Persistence;
using UserService.API.Data.Persistence.Repositories;
using UserService.API.Interfaces.Persistence;
using UserService.API.Interfaces.Services;
using UserService.API.Services.Authentication;
using UserService.API.Services.Customers;
using UserService.API.Services.Notifications;

namespace UserService.API.Middlewares
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
                        Array.Empty<string>()
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
                        Array.Empty<string>()
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
