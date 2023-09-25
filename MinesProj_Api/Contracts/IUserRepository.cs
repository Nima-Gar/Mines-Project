using Entities.Models;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> Get(String username, string password);
        bool usernameAlreadyExists(String username);
    }
}
