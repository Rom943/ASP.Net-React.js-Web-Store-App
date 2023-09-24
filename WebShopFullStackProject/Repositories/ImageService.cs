using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;
using Azure.Core;


namespace ShopApi.Repositories
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        
        
        public ImageService(IWebHostEnvironment _webHostEnvironment,IHttpContextAccessor _contextAccessor)
        {
            this._webHostEnvironment = _webHostEnvironment;
            this._contextAccessor = _contextAccessor?? throw new ArgumentNullException(nameof(_contextAccessor));
        }

        public string ImageSaveHandler(string RelatedTo, string objectName, bool multi, IFormFile? singelImg, IFormFileCollection multiImgs)
        {
            string imgPathName = "";
            string directoryName;
            

            switch (multi)
            {
                case false:
                    
                    directoryName = objectName + "TbnImgFolder";
                    string directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{RelatedTo}\\{objectName}\\{directoryName}";
                    if (System.IO.Directory.Exists(directoryPath))
                    {
                        System.IO.Directory.Delete(directoryPath, true);
                    }
                    System.IO.Directory.CreateDirectory(directoryPath);
                    string imgName = new string(Path.GetFileNameWithoutExtension(singelImg.FileName).ToArray()).Replace(' ', '-');
                    imgName += Path.GetExtension(singelImg.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, directoryPath, imgName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        singelImg.CopyTo(fileStream);
                    }
                    imgPathName = imgName;
                    break;
                    
                case true:
                     directoryName = objectName + "ImgGalleryFolder";
                     directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{RelatedTo}\\{objectName}\\{directoryName}";

                    if (System.IO.Directory.Exists(directoryPath))
                    {
                        System.IO.Directory.Delete(directoryPath, true);
                    }

                    System.IO.Directory.CreateDirectory(directoryPath);
                    foreach (var file in multiImgs)
                    {
                        imgName = new string(Path.GetFileNameWithoutExtension(file.FileName).ToArray()).Replace(' ', '-');
                        imgName += Path.GetExtension(file.FileName);
                        imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, directoryPath, imgName);
                        using (FileStream fileStream = System.IO.File.Create(imagePath))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                    imgPathName = directoryName;
                    break;
            }
                return imgPathName;
        }

        public string GetSingleImageSRC(string RealatedTo, string name, string imgName) 
        {
            HttpRequest requst = _contextAccessor.HttpContext.Request;
            var imgSrc = string.Empty;
            string filePath = _webHostEnvironment.ContentRootPath + $"Images\\{RealatedTo}\\{name}\\{name}TbnImgFolder\\{imgName}";
            if (File.Exists(filePath))
            {
                imgSrc = String.Format("{0}://{1}{2}/Images/{3}/{4}/{5}/{6}", requst.Scheme, requst.Host, requst.PathBase, RealatedTo, name, name + "TbnImgFolder", imgName);
            }
            return imgSrc;
        }

        public string [] GetMultiImageSRC(string RealatedTo, string name, string GalleryName)
        {
            HttpRequest request = _contextAccessor.HttpContext.Request;
            List<string> productGallery = new List<string>();
            string directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{RealatedTo}\\{name}\\{GalleryName}";
            if (Directory.Exists(directoryPath))
            {                
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    string imgSrc = String.Format("{0}://{1}{2}/Images/{3}/{4}/{5}/{6}", request.Scheme, request.Host, request.PathBase, RealatedTo, name, GalleryName, Path.GetFileName(file));
                    productGallery.Add(imgSrc);

                }
            }

            return productGallery.ToArray();
        }




    }
}
