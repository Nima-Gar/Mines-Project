using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class RoleRepository : RepositoryBase<Role, AccountsDbContext>, IRoleRepository
    {
        private readonly AccountsDbContext _context;

        public RoleRepository(AccountsDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
