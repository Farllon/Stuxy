using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Stuxy.Identity.Core.Models;
using Stuxy.Identity.Infrastructure.Data;
using Stuxy.Identity.Infrastructure.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddStuxy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomIdentity(configuration);
            services.AddCustomSwagger();
            services.AddCustomVersioning();
            services.AddCustomCors();
            services.AddLocalization();
            services.AddControllers();
        }

        private static void AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            services.AddIdentityCore<ApplicationUser>(options =>
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 1;

                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                        options.Lockout.MaxFailedAccessAttempts = 5;
                        options.Lockout.AllowedForNewUsers = true;

                        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                        options.User.RequireUniqueEmail = true;

                        options.SignIn.RequireConfirmedAccount = true;
                        options.SignIn.RequireConfirmedEmail = true;
                        options.SignIn.RequireConfirmedPhoneNumber = false;

                    })
                .AddRoles<ApplicationRole>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<MultiLanguageIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        private static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Identity API",
                    Description = "Stuxy Identity API",
                    Contact = new OpenApiContact
                    {
                        Name = "Farllon Costa",
                        Url = new Uri("https://www.linkedin.com/in/farllon")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // Add GenerateDocumentationFile in csproj and suppress
                //var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token: Bearer {your token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });
        }

        private static void AddCustomVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver")
                );
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        private static void AddCustomCors(this IServiceCollection services)
        {

        }
    }
}
