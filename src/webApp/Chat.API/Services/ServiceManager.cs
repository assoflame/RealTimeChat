using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IChatService> _chatService;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(IRepoManager repoManager, IConfiguration configuration)
        {
            _chatService = new Lazy<IChatService>(() => new ChatService(repoManager));
            _authService = new Lazy<IAuthService>(() => new AuthService(repoManager, configuration));
        }

        public IChatService ChatService => _chatService.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
