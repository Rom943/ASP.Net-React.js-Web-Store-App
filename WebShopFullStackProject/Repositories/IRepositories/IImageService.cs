using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;

namespace ShopApi.Repositories.IRepositories
{
    public interface IImageService
    {
        string GetSingleImageSRC(Image img,int relatedId);
        string[] GetMultiImageSRC(ImageGallery gallery, int relatedId);
        Task ImageSaveHandler(string relatedTo, int relatedId, IFormFile imgFile);
        Task GallerySaveHandler(string relatedTo, int relatedId, IFormFileCollection imgFile);
        void DeleteImage(int imgID, int relatedId);
        void DeleteGallery(int galleryID, int relatedId);

    }
}
