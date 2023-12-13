using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Repositories
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(MainContext _context) : base(_context)
        {
        }
    }
}
