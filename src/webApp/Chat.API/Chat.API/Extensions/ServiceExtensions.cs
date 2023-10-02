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
            => services.AddSingleton<IDictionary<string, UserConnection>, Dictionary<string, UserConnection>>();

        public static void ConfigureDatabase(this IServiceCollection services)
            => services.AddSingleton<IMongoDatabase>(
                new MongoClient("mongodb://localhost:27017").GetDatabase("chat")
                );

        public static void ConfigureServiceManager(this IServiceCollection services)
            => services.AddSingleton<IServiceManager, ServiceManager>();

        public static void ConfigureRepoManager(this IServiceCollection services)
            => services.AddSingleton<IRepoManager, RepoManager>();

        public static void ConfigureJWT(this IServiceCollection services)
            => services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "validIssuer",
                        ValidAudience = "validAudience",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key"))
                    };
                });
    }
}
