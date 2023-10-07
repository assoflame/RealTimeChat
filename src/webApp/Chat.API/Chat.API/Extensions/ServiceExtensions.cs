using Chat.API.Hubs.ChatHelpers;
using DataAccess;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using Services;
using Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chat.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureConnectionsCollection(this IServiceCollection services)
            => services.AddSingleton<IDictionary<string, UserConnection>>(new Dictionary<string, UserConnection>());

        public static void ConfigureDatabase(this IServiceCollection services)
            => services.AddSingleton<IMongoDatabase>(
                new MongoClient("mongodb://localhost:27017").GetDatabase("chat")
                );

        public static void ConfigureServiceManager(this IServiceCollection services)
            => services.AddSingleton<IServiceManager, ServiceManager>();

        public static void ConfigureRepoManager(this IServiceCollection services)
            => services.AddSingleton<IRepoManager, RepoManager>();

        public static void ConfigureJWT(this IServiceCollection services)
            => services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = "validIssuer",
                        ValidAudience = "validAudience",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SECRETKEY1234567SECRETKEY1234567"))
                    };

                    opts.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs/chat")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

        public static void ConfigureCors(this IServiceCollection services)
            => services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
    }
}
