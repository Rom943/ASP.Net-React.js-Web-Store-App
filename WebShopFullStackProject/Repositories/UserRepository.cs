using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Repositories
{
    public class UserRepository: RepositoryBase<User>,IUserRepository
    {
        public UserRepository(MainContext _context) : base(_context) { }
    }
}
