using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(MainContext _context) : base(_context)
        {
        }
    }
}
