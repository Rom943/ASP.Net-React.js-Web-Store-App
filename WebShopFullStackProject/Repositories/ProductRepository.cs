
using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Repositories
{
    public class ProductRepository: RepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(MainContext _context) : base(_context) { }
    }

}
