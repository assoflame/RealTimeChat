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

        public ServiceManager(/*IMongoDatabase db*/)
        {
            _chatService = new Lazy<IChatService>(() => new ChatService(db));
            _authService = new Lazy<IAuthService>(() => new AuthService(db));
        }

        public IChatService ChatService => _chatService.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
