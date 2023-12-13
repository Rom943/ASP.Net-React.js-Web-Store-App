using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Context;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;
using Azure.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace ShopApi.Repositories
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IImageRepository _imageRepository;
        private readonly IImageGalleryRepository _imageGalleryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ICategoryRepository _categoryRepository;



        public ImageService(IWebHostEnvironment _webHostEnvironment,
            IHttpContextAccessor _contextAccessor,
            IImageGalleryRepository _imageGalleryRepository,
            IImageRepository _imageRepository,
            IUserRepository _userRepository,
            ISellerRepository _sellerRepository,
            IProductRepository _productRepository,
            ICategoryRepository _categoryRepository)
        {
            this._webHostEnvironment = _webHostEnvironment;
            this._contextAccessor = _contextAccessor ?? throw new ArgumentNullException(nameof(_contextAccessor));
            this._imageRepository = _imageRepository;
            this._imageGalleryRepository = _imageGalleryRepository;
            this._userRepository = _userRepository;
            this._sellerRepository = _sellerRepository;
            this._productRepository = _productRepository;
            this._categoryRepository = _categoryRepository;
        }

        public async Task ImageSaveHandler(string relatedTo, int relatedId, IFormFile imgFile)
        {
            var checkFile = Path.GetExtension(imgFile.FileName);
            Image isImgExist = null;
            if (checkFile != ".jpg" && checkFile != ".png" && checkFile != ".jpeg")
            {
                return;
            }

            Image img = new Image();
            img.RelatedTo = relatedTo;

            switch (relatedTo)
            {
                case "products":
                    var product = _productRepository.FindByCondition(p => p.ID == relatedId).FirstOrDefault();
                    img.Product = product;
                    isImgExist = await _imageRepository.FindByCondition(i => i.ID == product.TbnImgId).FirstOrDefaultAsync();

                    break;
                case "users":
                    var user = _userRepository.FindByCondition(u => u.ID == relatedId).FirstOrDefault();
                    img.User = user;
                    isImgExist = await _imageRepository.FindByCondition(i => i.ID == user.TbnImgId).FirstOrDefaultAsync();

                    break;
                case "sellers":
                    var seller = _sellerRepository.FindByCondition(s => s.ID == relatedId).FirstOrDefault();
                    img.Seller = seller;
                    isImgExist = await _imageRepository.FindByCondition(i => i.ID == seller.TbnImgId).FirstOrDefaultAsync();

                    break;
                case "categories":
                    var category = _categoryRepository.FindByCondition(c => c.ID == relatedId).FirstOrDefault();
                    img.Category = category;
                    isImgExist = await _imageRepository.FindByCondition(i => i.ID == category.TbnImgId).FirstOrDefaultAsync();

                    break;
            }


            string directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{relatedTo}\\{relatedId}\\TbnImage";
            if (System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.Delete(directoryPath, true);
            }
            System.IO.Directory.CreateDirectory(directoryPath);
            var imgName = new string(Path.GetFileNameWithoutExtension(imgFile.FileName).ToArray()).Replace(' ', '-');
            imgName += Path.GetExtension(imgFile.FileName);
            img.ImageName = imgName;
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, directoryPath, imgName);
            using var fileStream = new FileStream(imagePath, FileMode.Create);

            imgFile.CopyTo(fileStream);
            if (isImgExist != null)
            {
                isImgExist.ImageName = img.ImageName;
                _imageRepository.Update(isImgExist);
            }
            if(isImgExist == null)
            {
                 _imageRepository.Create(img);
            }
        }





        public async Task GallerySaveHandler(string relatedTo, int relatedId, IFormFileCollection imgFiles)
        {
            ImageGallery gallery = new ImageGallery();
            gallery.Images = new List<Image>();
            var product = await _productRepository.FindByCondition(p => p.ID == relatedId).Include(p => p.Gallery).FirstOrDefaultAsync();
            var isGalleryExist = await _imageGalleryRepository.FindByCondition(g => g.ID == product.GalleryId).Include(i=>i.Images).FirstOrDefaultAsync();
            gallery.Product = product;
            gallery.RelatedTo = relatedTo;
            var directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", relatedTo, relatedId.ToString(), "gallery");
            foreach (var file in imgFiles)
            {
                var checkFile = Path.GetExtension(file.FileName);
                if (checkFile != ".jpg" && checkFile != ".png" && checkFile != ".jpeg")
                {
                    return;
                }
            }

            if (System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.Delete(directoryPath, true);
            }

            System.IO.Directory.CreateDirectory(directoryPath);

            if (isGalleryExist == null)
            {
                foreach (var file in imgFiles)
                {
                    Image img = new Image();
                    img.RelatedTo = relatedTo;
                    var imgName = new string(Path.GetFileNameWithoutExtension(file.FileName).ToArray()).Replace(' ', '-');
                    imgName += Path.GetExtension(file.FileName);
                    img.ImageName = imgName;
                    var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, directoryPath, imgName);
                    using (FileStream fileStream = System.IO.File.Create(imagePath))
                    {
                        file.CopyTo(fileStream);
                    }

                    var result = await _imageRepository.Create(img);
                    gallery.Images.Add(result);
                }


                await _imageGalleryRepository.Create(gallery);
            }

            if (isGalleryExist != null)
            {
                List<Image> imgs = await _imageRepository.FindByCondition(i => i.Gallery.ID == isGalleryExist.ID).ToListAsync();
                foreach (var img in imgs)
                {
                    _imageRepository.Delete(img);
                }
                foreach (var file in imgFiles)
                {
                    Image img = new Image();
                    img.RelatedTo = relatedTo;
                    var imgName = new string(Path.GetFileNameWithoutExtension(file.FileName).ToArray()).Replace(' ', '-');
                    imgName += Path.GetExtension(file.FileName);
                    img.ImageName = imgName;
                    var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, directoryPath, imgName);
                    using (FileStream fileStream = System.IO.File.Create(imagePath))
                    {
                        file.CopyTo(fileStream);
                    }

                    var result = await _imageRepository.Create(img);
                    isGalleryExist.Images.Add(result);
                }


                 _imageGalleryRepository.Update(isGalleryExist);
            }
        }









        public string GetSingleImageSRC(Image img, int relatedId)
        {


            HttpRequest requst = _contextAccessor.HttpContext.Request;
            var imgSrc = string.Empty;
            string filePath = _webHostEnvironment.ContentRootPath + $"Images\\{img.RelatedTo}\\{relatedId}\\TbnImage\\{img.ImageName}";
            if (File.Exists(filePath))
            {
                imgSrc = String.Format("{0}://{1}{2}/Images/{3}/{4}/{5}/{6}", requst.Scheme, requst.Host, requst.PathBase, img.RelatedTo, relatedId, "TbnImage", img.ImageName);
                return imgSrc;
            }
            else
            {
                return string.Empty;
            }

        }

        public string[] GetMultiImageSRC(ImageGallery gallery, int relatedId)
        {

            HttpRequest request = _contextAccessor.HttpContext.Request;
            List<string> productGallery = new List<string>();
            string directoryPath = _webHostEnvironment.ContentRootPath + $"Images\\{gallery.RelatedTo}\\{relatedId}\\gallery";
            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    string imgSrc = String.Format("{0}://{1}{2}/Images/{3}/{4}/gallery/{5}", request.Scheme, request.Host, request.PathBase, gallery.RelatedTo, relatedId, Path.GetFileName(file));
                    productGallery.Add(imgSrc);

                }
            }

            return productGallery.ToArray();
        }

        public void DeleteImage (int imgID,int relatedId)
        {
            var img = _imageRepository.FindByCondition(i => i.ID == imgID).FirstOrDefault();
            string filePath = string.Empty;
            if (img != null)
            {
                filePath = _webHostEnvironment.ContentRootPath + $"Images\\{img.RelatedTo}\\{relatedId}\\TbnImage\\{img.ImageName}";
                if (System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.Delete(filePath, true);
                }
                _imageRepository.Delete(img);
            }

        }

        public void DeleteGallery (int galleryID , int relatedId)
        {
            var gallery =  _imageGalleryRepository.FindByCondition(g => g.ID == galleryID).Include(i => i.Images).FirstOrDefault();
            string galleryPath = string.Empty;
            if (gallery != null)
            {
                List<Image> imgs = _imageRepository.FindByCondition(i => i.Gallery.ID == gallery.ID).ToList();
                if(imgs.Count > 0)
                {
                    foreach (Image img in imgs)
                    {
                        string filePath = string.Empty;
                        if (img != null)
                        {
                            filePath = _webHostEnvironment.ContentRootPath + $"Images\\{img.RelatedTo}\\{relatedId}\\gallery\\{img.ImageName}";
                            if (System.IO.Directory.Exists(filePath))
                            {
                                System.IO.Directory.Delete(filePath, true);
                            }
                            _imageRepository.Delete(img);
                        }
                    }
                }

                galleryPath = _webHostEnvironment.ContentRootPath + $"Images\\{gallery.RelatedTo}\\{relatedId}\\gallery";
                if (System.IO.Directory.Exists(galleryPath))
                {
                    System.IO.Directory.Delete(galleryPath, true);
                }
                _imageGalleryRepository.Delete(gallery);

            }
        }
    }
}

