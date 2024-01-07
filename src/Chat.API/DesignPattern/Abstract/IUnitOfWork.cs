using Chat.API.Data.Entities;
using Chat.API.DesignPattern.Implementation;

namespace Chat.API.DesignPattern.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        GenericRepository<User> UserRepository { get; }
        Task CompleteAsync();
    }
}
