namespace Services.Interfaces
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }
        IChatService ChatService { get; }
    }
}
