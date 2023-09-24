using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Repositories
{
    public class AdminRepository : RepositoryBase<SiteManager>, IAdminRepository
    {
        public AdminRepository(MainContext _context) : base(_context)
        {
        }
    }
}
