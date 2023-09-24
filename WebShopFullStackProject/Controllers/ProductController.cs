using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using System.IO;
using ShopApi.Repositories.IRepositories;
using ShopApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISellerRepository _sellerRepository;

        public ProductController (IProductRepository _productRepository,
            IWebHostEnvironment _hostEnvironment,
            ICategoryRepository _categoryRepository,
            ISellerRepository _sellerRepository)
        {
            this._productRepository = _productRepository;
            this._hostEnvironment = _hostEnvironment;
            this._categoryRepository = _categoryRepository;
            this._sellerRepository = _sellerRepository;
        }

        [HttpPost("seller/{sellerId:int}")]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductDTO product,int sellerId)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var category = await _categoryRepository.FindByCondition(c => c.ID == product.CategoryId).Include(c=>c.Products).FirstOrDefaultAsync();
            var seller = await _sellerRepository.FindByCondition(s => s.ID == sellerId).Include(s=>s.Products).FirstOrDefaultAsync();

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Stock = product.Stock,
                ThumbnailImgName = product.ThumbnailImgFile != null && product.ProductName != null ? await SaveImage(product.ThumbnailImgFile, product.ProductName) : null,
                ProductGalleryName = product.ProductGallery != null && product.ProductName != null ? await SaveImages(product.ProductGallery, product.ProductName) : null,
                Category = category,
                Seller = seller
                
            };


            var result = await _productRepository.Create(newProduct);

            return Created("product",product);
        } 

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile, string productName)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            System.IO.Directory.CreateDirectory(_hostEnvironment.ContentRootPath +$"\\Images\\products\\{productName}\\thumbnail");
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath,"Images\\products",productName,"thumbnail", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public async Task<string> SaveImages(IFormFileCollection fileCollection, string productName)
        {
            string galleryName = productName + DateTime.Now.ToString("yymmssfff")+"Gallery";
            System.IO.Directory.CreateDirectory(_hostEnvironment.ContentRootPath + $"\\Images\\products\\{productName}\\{galleryName}");
            foreach (var file in fileCollection)
            {
                var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, $"Images\\products\\{productName}\\{galleryName}", file.FileName);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return galleryName;
        }
    }
}
