namespace DataAccess.Interfaces
{
    public interface IRepoManager
    {
        IMessageRepo Messages { get; }
        IUserRepo Users { get; }
        IRoomRepo Rooms { get; }
    }
}
