using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Repositories
{
    public class ImageGalleryRepository : RepositoryBase<ImageGallery>, IImageGalleryRepository
    {
        public ImageGalleryRepository(MainContext _context) : base(_context)
        {
        }
    }
}
