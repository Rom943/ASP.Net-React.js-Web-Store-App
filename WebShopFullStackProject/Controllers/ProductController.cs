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
        private readonly IImageService _imageService;
        private readonly IReviewRepository _reviewRepository;


        public ProductController (IProductRepository _productRepository,
            IWebHostEnvironment _hostEnvironment,
            ICategoryRepository _categoryRepository,
            ISellerRepository _sellerRepository,
            IImageService _imageService,
            IReviewRepository _reviewRepository)
        {
            this._productRepository = _productRepository;
            this._hostEnvironment = _hostEnvironment;
            this._categoryRepository = _categoryRepository;
            this._sellerRepository = _sellerRepository;
            this._imageService = _imageService;
            this._reviewRepository = _reviewRepository;
        }

        [HttpGet("categories/products")]
        public async Task<ActionResult> GetAllProductsByCategories()
        {
            var categories = _categoryRepository.FindAll().Include(c => c.Products).ToList();
            List<GetCategoryListDTO> categoryList = new List<GetCategoryListDTO>();
            foreach (var category in categories)
            {
                var products =await _productRepository.FindByCondition(p => p.Category.ID == category.ID).Include(p => p.Category).ToListAsync();
                List<GetProductDTO> productsList = new List<GetProductDTO>();
                foreach(var product in products)
                {
                    var productDTO = new GetProductDTO 
                    {
                        ID = product.ID,
                        ProductName= product.ProductName,
                        ProductDescription= product.ProductDescription,
                        Price= product.Price,
                        Rank= product.Rank,
                        Stock= product.Stock,
                        Category=product.Category.CategoryName,
                        ThumbnailImgSRC=_imageService.GetSingleImageSRC("products",product.ProductName,product.ThumbnailImgName),
                        ProductGallerySRC=_imageService.GetMultiImageSRC("products",product.ProductName,product.ProductGalleryName)
                    };
                    productsList.Add(productDTO);
                }
                var categoryListDTO = new GetCategoryListDTO
                {
                    ID = category.ID,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                    ImageSRC = _imageService.GetSingleImageSRC("categories", category.CategoryName, category.ImageName),
                    ProductCunt = productsList.Count(),
                    ProductList = productsList.ToArray()                    
                };
                categoryList.Add(categoryListDTO);
            }
            return Ok(categoryList.ToArray());
        }

        [HttpGet("id/{productId:int}")]
        public async Task<ActionResult> GetProductById(int productId)
        {
            var product =await _productRepository.FindByCondition(p => p.ID == productId).Include(p => p.Reviews).Include(p => p.Category).Include(p => p.Seller).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            var relatedProducts =await _productRepository.FindByCondition(r => r.Category.ID == product.Category.ID).Include(r => r.Category).ToListAsync();

            List<GetProductDTO> RelatedList = new List<GetProductDTO>();
            foreach (var item in relatedProducts)
            {
                var relatedProduct = new GetProductDTO
                {
                    ID= item.ID,
                    ProductName= item.ProductName,
                    ProductDescription = item.ProductDescription,
                    Price = item.Price,
                    Rank= item.Rank,
                    Stock= item.Stock,
                    Category=item.Category.CategoryName,
                    ThumbnailImgSRC=_imageService.GetSingleImageSRC("products",item.ProductName,item.ThumbnailImgName),
                    ProductGallerySRC=_imageService.GetMultiImageSRC("products",item.ProductName,item.ProductGalleryName)
                };
                RelatedList.Add(relatedProduct);
            };
            var reviews = await _reviewRepository.FindByCondition(r => r.Product.ID == product.ID).Include(r => r.Customer.User).ToListAsync();
            List<GetCustomerReviewsDTO> reviewList = new List<GetCustomerReviewsDTO>();
            foreach (var item in reviews)
            {
                string formattedDate= item.Date.ToString("dd.MM.yyyy"); 

                var review = new GetCustomerReviewsDTO
                {
                    ProductId=item.ID,
                    ProductName=item.Product.ProductName,
                    ReviewText=item.ReviewText,
                    Rank=item.Rank,
                    Date= formattedDate,
                    ReviewerImgSRC=_imageService.GetSingleImageSRC("Users",item.Customer.User.FirstName,item.Customer.User.ProfileImageName),
                    ReviewerName = item.Customer.User.FirstName,
                    ShipingAddress = item.Customer.ShipingAddress,
                };

                reviewList.Add(review);
            }
            var productDTO = new GetProductByIdDTO
            {
                ProductId = productId,
                ProductName = product.ProductName,
                ProductDescription= product.ProductDescription, 
                Price= product.Price,
                Rank= product.Rank,
                Stock= product.Stock,
                CategoryName=product.Category.CategoryName,
                ThumbnailImgSRC=_imageService.GetSingleImageSRC("products",product.ProductName,product.ThumbnailImgName),
                ProductGallerySRC=_imageService.GetMultiImageSRC("products",product.ProductName,product.ProductGalleryName),
                RelatedProducts=RelatedList.ToArray(),
                ReviewList=reviewList.ToArray(),
                StoreName=product.Seller.StoreName,
                StoreImgSRC=_imageService.GetSingleImageSRC("sellers",product.Seller.StoreName,product.Seller.StoresImgName)
            };


            return Ok(productDTO);
        }
    }
}
