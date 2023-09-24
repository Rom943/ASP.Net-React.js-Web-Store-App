using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;

namespace ShopApi.Repositories.IRepositories
{
    public interface IImageService
    {
        string GetSingleImageSRC(string RealatedTo, string name, string imgName);
        string [] GetMultiImageSRC(string RealatedTo, string name, string GalleryName);
        string ImageSaveHandler(string RelatedTo, string objectName, bool multi, IFormFile? singelImg, IFormFileCollection? multiImgs);
    }
}
