using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

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
