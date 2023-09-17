namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IMineRepository MineRepo { get; }
        IRoleRepository RoleRepo { get; }
        IUserRepository UserRepo { get; }
    }
}
