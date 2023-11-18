using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MinesDbContext _minesContext;
        private AccountsDbContext _accountsContext;
        private IMineRepository? _mineRepo;
        private IRoleRepository? _roleRepo;
        private IUserRepository? _userRepo;
        // + all of repositories needed to be wrapped and used...

        public IMineRepository MineRepo
        {
            get
            {
                // assign if it is null
                _mineRepo ??= new MineRepository(_minesContext);
                return _mineRepo;
            }
        }

        public IRoleRepository RoleRepo
        {
            get
            {
                _roleRepo ??= new RoleRepository(_accountsContext);
                return _roleRepo;
            }
        }

        public IUserRepository UserRepo
        {
            get
            {
                _userRepo ??= new UserRepository(_accountsContext);
                return _userRepo;
            }
        }

        public RepositoryWrapper(MinesDbContext minesContext, AccountsDbContext accountsContext)
        {
            _minesContext = minesContext;
            _accountsContext = accountsContext;
        }
    }
}
