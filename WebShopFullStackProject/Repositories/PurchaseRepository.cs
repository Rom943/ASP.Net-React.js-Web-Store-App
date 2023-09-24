using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Repositories
{
    public class PurchaseRepository : RepositoryBase<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MainContext _context) : base(_context)
        {
        }
    }
}
