using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QEnergy.Core.Actions;
using QEnergy.Core.Domain.Configuration;
using QEnergy.Services.Persistence.EntityFramework;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace QEnergy.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddControllersWithViews();

            JwtAuthorization jwtAuthorization = new JwtAuthorization();
            configuration.GetSection(nameof(JwtAuthorization)).Bind(jwtAuthorization);
            services.Configure<DatabaseConfiguration>(configuration.GetSection(nameof(DatabaseConfiguration)));
            services.Configure<JwtAuthorization>(configuration.GetSection(nameof(JwtAuthorization)));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient(provider =>
            {
                return ResolveDbContext(configuration);
            });

            services.AddMediatR(Assembly.GetAssembly(typeof(QEnergyActions)));
            services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
            services.AddSession();
            services.AddMvc();

            var key = Encoding.ASCII.GetBytes(jwtAuthorization.SecretKey);
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "smart";
            })
                .AddPolicyScheme("smart", "Bearer or Jwt", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        var bearerAuth = context.Request.Headers["Authorization"].FirstOrDefault()?.StartsWith("Bearer ") ?? false;
                        // You could also check for the actual path here if that's your requirement:
                        // eg: if (context.HttpContext.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCulture))
                        if (bearerAuth)
                            return JwtBearerDefaults.AuthenticationScheme;
                        else
                            return CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                })
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login/login");
                    options.AccessDeniedPath = new PathString("/Login/login");
                    options.Cookie.Name = "access_token";
                    options.ReturnUrlParameter = "/Projects/List";
                })
                .AddJwtBearer(options =>
                {
                    options.Audience = jwtAuthorization.Audience;
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        RequireAudience = true,
                        ValidateAudience = true,
                        ValidAudience = jwtAuthorization.Audience
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            // Add the access_token as a claim, as we may actually need it
                            var accessToken = context.SecurityToken as JwtSecurityToken;
                            if (accessToken != null)
                            {
                                ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                                if (identity != null)
                                {
                                    identity.AddClaim(new Claim("access_token", accessToken.RawData));
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSwaggerGen(swagger =>
            {
                ConfigureSwagger(swagger);
            });

            return services;
        }

        public static QEnergyDbContext ResolveDbContext(IConfiguration configuration)
        {
            var databaseConfiguration = new DatabaseConfiguration();
            configuration.GetSection(nameof(DatabaseConfiguration)).Bind(databaseConfiguration);
            var connectionString = string.Format(databaseConfiguration.ConnectionString, databaseConfiguration.User, databaseConfiguration.Password);
            var optionsBuilder = new DbContextOptionsBuilder<QEnergyDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var context = new QEnergyDbContext(optionsBuilder.Options);
            context.Database.SetCommandTimeout(120);
            return context;
        }

        private static void ConfigureSwagger(SwaggerGenOptions swagger)
        {
            var contact = new OpenApiContact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
            swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                               new OpenApiInfo
                               {
                                   Title = SwaggerConfiguration.DocInfoTitle,
                                   Version = SwaggerConfiguration.DocInfoVersion,
                                   Description = SwaggerConfiguration.DocInfoDescription,
                                   Contact = contact
                               }
                                );
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swagger.IncludeXmlComments(xmlPath);
            var xmlPathModels = Path.Combine(AppContext.BaseDirectory, $"{typeof(JwtAuthorization).Assembly.GetName().Name}.xml");
            swagger.IncludeXmlComments(xmlPathModels);

            // Add filters to fix enums
            swagger.AddEnumsWithValuesFixFilters();

            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
        }
    }
}
