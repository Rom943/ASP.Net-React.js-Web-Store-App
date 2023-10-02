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
            var objectNameFix=objectName.Replace(" ", "-");
            string directoryName;
            string imgName;
            string checkFile;
            

            switch (multi)
            {
                case false:
                     checkFile = Path.GetExtension(singelImg.FileName);
                    if (checkFile != ".jpg" && checkFile != ".png" && checkFile != ".jpeg")
                    {
                        break;
                    }

                    directoryName = objectNameFix + "TbnImgFolder";
                    string directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{RelatedTo}\\{objectNameFix}\\{directoryName}";
                    if (System.IO.Directory.Exists(directoryPath))
                    {
                        System.IO.Directory.Delete(directoryPath, true);
                    }
                    System.IO.Directory.CreateDirectory(directoryPath);
                    imgName = new string(Path.GetFileNameWithoutExtension(singelImg.FileName).ToArray()).Replace(' ', '-');
                    imgName += Path.GetExtension(singelImg.FileName);
                    var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, directoryPath, imgName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        singelImg.CopyTo(fileStream);
                    }
                    imgPathName = imgName;
                    break;
                    
                case true:
                     directoryName = objectNameFix + "ImgGalleryFolder";
                     directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{RelatedTo}\\{objectNameFix}\\{directoryName}";
                    foreach(var file in multiImgs)
                    {
                        checkFile = Path.GetExtension(file.FileName);
                        if (checkFile != ".jpg" && checkFile != ".png" && checkFile != ".jpeg")
                        {
                            break;
                        }
                    }

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
            var FixedName = name.Replace(" ", "-");
            HttpRequest requst = _contextAccessor.HttpContext.Request;
            var imgSrc = string.Empty;
            string filePath = _webHostEnvironment.ContentRootPath + $"Images\\{RealatedTo}\\{FixedName}\\{FixedName}TbnImgFolder\\{imgName}";
            if (File.Exists(filePath))
            {
                imgSrc = String.Format("{0}://{1}{2}/Images/{3}/{4}/{5}/{6}", requst.Scheme, requst.Host, requst.PathBase, RealatedTo, FixedName, FixedName + "TbnImgFolder", imgName);
            }
            return imgSrc;
        }

        public string [] GetMultiImageSRC(string RealatedTo, string name, string GalleryName)
        {
            var FixedName = name.Replace(" ", "-");
            HttpRequest request = _contextAccessor.HttpContext.Request;
            List<string> productGallery = new List<string>();
            string directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{RealatedTo}\\{FixedName}\\{GalleryName}";
            if (Directory.Exists(directoryPath))
            {                
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    string imgSrc = String.Format("{0}://{1}{2}/Images/{3}/{4}/{5}/{6}", request.Scheme, request.Host, request.PathBase, RealatedTo, FixedName, GalleryName, Path.GetFileName(file));
                    productGallery.Add(imgSrc);

                }
            }

            return productGallery.ToArray();
        }




    }
}
