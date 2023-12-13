using Microsoft.AspNetCore.Mvc;
using ShopApi.Models;
using System.IO;
using ShopApi.Repositories.IRepositories;
using ShopApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace ShopApi.Controllers
{
    /// <summary>
    /// Controller for managing products, categories, and related operations.
    /// </summary>
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
        private readonly IImageRepository _imageRepository;
        private readonly IImageGalleryRepository _imageGalleryRepository;


        public ProductController(
            IProductRepository _productRepository,
            IWebHostEnvironment _hostEnvironment,
            ICategoryRepository _categoryRepository,
            ISellerRepository _sellerRepository,
            IImageService _imageService,
            IReviewRepository _reviewRepository,
            IImageRepository _imageRepository,
            IImageGalleryRepository _imageGalleryRepository)
        {
            this._productRepository = _productRepository;
            this._hostEnvironment = _hostEnvironment;
            this._categoryRepository = _categoryRepository;
            this._sellerRepository = _sellerRepository;
            this._imageService = _imageService;
            this._reviewRepository = _reviewRepository;
            this._imageRepository = _imageRepository;
            this._imageGalleryRepository = _imageGalleryRepository;
        }

        /// <summary>
        /// Gets all products grouped by categories, including product details.
        /// </summary>
        /// <returns>Returns a list of categories with associated products.</returns>
        [HttpGet("categories/products")]
        public async Task<ActionResult> GetAllProductsByCategories()
        {
            // Retrieve categories with associated products and category images
            var categories = _categoryRepository.FindAll().Include(c => c.Products).Include(i => i.TbnImg).ToList();
            List<GetCategoryListDTO> categoryList = new List<GetCategoryListDTO>();

            // Populate DTOs with category and product details
            foreach (var category in categories)
            {
                var products = await _productRepository.FindByCondition(p => p.Category.ID == category.ID)
                    .Include(p => p.Category)
                    .Include(i => i.TbnImg)
                    .Include(g => g.Gallery).ToListAsync();

                List<GetProductDTO> productsList = new List<GetProductDTO>();

                foreach (var product in products)
                {
                    var productDTO = new GetProductDTO
                    {
                        ID = product.ID,
                        ProductName = product.ProductName,
                        ProductDescription = product.ProductDescription,
                        Price = product.Price,
                        Rank = product.Rank,
                        Stock = product.Stock,
                        Category = product.Category.CategoryName != null ? product.Category.CategoryName : string.Empty,
                        ThumbnailImgSRC = product.TbnImg != null ? _imageService.GetSingleImageSRC(product.TbnImg, product.ID) : null,
                        ProductGallerySRC = product.Gallery != null ? _imageService.GetMultiImageSRC(product.Gallery, product.ID) : null
                    };
                    productsList.Add(productDTO);
                }

                var categoryListDTO = new GetCategoryListDTO
                {
                    ID = category.ID,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                    ProductCunt = productsList.Count(),
                    ProductList = productsList.ToArray(),
                    ImageSRC = category.TbnImg != null ? _imageService.GetSingleImageSRC(category.TbnImg, category.ID) : null
                };
                categoryList.Add(categoryListDTO);
            }

            return Ok(categoryList.ToArray());
        }

        /// <summary>
        /// Gets product details by product ID, including related products and reviews.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve.</param>
        /// <returns>Returns details of the specified product.</returns>
        [HttpGet("id/{productId:int}")]
        public async Task<ActionResult> GetProductById(int productId)
        {
            // Retrieve product details including related products, reviews, and images
            var product = await _productRepository.FindByCondition(p => p.ID == productId)
                .Include(si => si.TbnImg)
                .Include(sg => sg.Gallery)
                .Include(p => p.Reviews)
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(i => i.Seller.TbnImg).FirstOrDefaultAsync();

            // Return NotFound if product is not found
            if (product == null)
            {
                return NotFound();
            }

            // Retrieve related products
            var relatedProducts = await _productRepository.FindByCondition(r => r.Category.ID == product.Category.ID)
                .Include(p => p.TbnImg)
                .Include(g => g.Gallery)
                .Include(r => r.Category).ToListAsync();

            List<GetProductDTO> RelatedList = new List<GetProductDTO>();

            // Populate DTOs with related product details
            foreach (var item in relatedProducts)
            {
                var img = await _imageRepository.FindByCondition(i => i.ID == item.TbnImg.ID).FirstOrDefaultAsync();
                var galery = await _imageGalleryRepository.FindByCondition(g => g.ID == item.Gallery.ID).FirstOrDefaultAsync();
                var relatedProduct = new GetProductDTO
                {
                    ID = item.ID,
                    ProductName = item.ProductName,
                    ProductDescription = item.ProductDescription,
                    Price = item.Price,
                    Rank = item.Rank,
                    Stock = item.Stock,
                    Category = item.Category.CategoryName != null ? item.Category.CategoryName : string.Empty,
                    ThumbnailImgSRC = img != null ? _imageService.GetSingleImageSRC(img, item.ID) : null,
                    ProductGallerySRC = galery != null ? _imageService.GetMultiImageSRC(galery, item.ID) : null
                };
                RelatedList.Add(relatedProduct);
            }

            // Retrieve reviews associated with the product
            var reviews = await _reviewRepository.FindByCondition(r => r.Product.ID == product.ID)
                .Include(r => r.Customer.User)
                .Include(i => i.Customer.User.TbnImg).Include(c => c.Customer).ToListAsync();

            List<GetCustomerReviewsDTO> reviewList = new List<GetCustomerReviewsDTO>();

            // Populate DTOs with review details
            if (reviews.Count > 0)
            {
                foreach (var item in reviews)
                {
                    if (item.Customer != null)
                    {
                        string formattedDate = item.Date.ToString("dd.MM.yyyy");

                        var review = new GetCustomerReviewsDTO
                        {
                            ProductId = item.ID,
                            ProductName = item.Product.ProductName != null ? item.Product.ProductName : string.Empty,
                            ReviewText = item.ReviewText,
                            Rank = item.Rank,
                            Date = formattedDate,
                            ReviewerName = item.Customer.User.FirstName != null ? item.Customer.User.FirstName : string.Empty,
                            ShipingAddress = item.Customer.ShipingAddress != null ? item.Customer.ShipingAddress : string.Empty,
                            ReviewerImgSRC = item.Customer.User.TbnImg != null ? _imageService.GetSingleImageSRC(item.Customer.User.TbnImg, item.Customer.User.ID) : string.Empty,
                        };

                        reviewList.Add(review);
                    }
                }
            }

            // Populate DTO with product details
            var productDTO = new GetProductByIdDTO
            {
                ProductId = productId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Rank = Math.Round(product.Rank, 2),
                Stock = product.Stock,
                CategoryName = product.Category.CategoryName != null ? product.Category.CategoryName : string.Empty,
                RelatedProducts = RelatedList.ToArray(),
                ReviewList = reviewList.ToArray(),
                StoreName = product.Seller.ID != null ? product.Seller.StoreName : string.Empty,
                ThumbnailImgSRC = product.TbnImg != null ? _imageService.GetSingleImageSRC(product.TbnImg, product.ID) : string.Empty,
                ProductGallerySRC = product.Gallery != null ? _imageService.GetMultiImageSRC(product.Gallery, product.ID) : null,
                StoreImgSRC = product.Seller.TbnImg != null ? _imageService.GetSingleImageSRC(product.Seller.TbnImg, product.Seller.ID) : string.Empty,

            };

            return Ok(productDTO);
        }

        /// <summary>
        /// Gets the product gallery for the most purchased product.
        /// </summary>
        /// <returns>Returns the product gallery for the most purchased product.</returns>
        [HttpGet("getmostranked")]
        public async Task<IActionResult> GetMostPurchasedProductGallery()
        {
            // Retrieve the product with the maximum number of related purchases
            var entityWithMaxRelatedChildren = await _productRepository.FindEntityWithMaxRelatedChildrenAsync<Purchase>(
                entity => entity.Purchases);

            List<ProductImgSliderDTO> productImgSliderList = new List<ProductImgSliderDTO>();

            // Populate DTOs with product details for the most purchased product
            foreach (var entity in entityWithMaxRelatedChildren)
            {
                var product = await _productRepository.FindByCondition(p => p.ID == entity.ID).Include(i => i.TbnImg).FirstOrDefaultAsync();
                var productDTO = new ProductImgSliderDTO
                {
                    ProductId = entity.ID,
                    ProductName = entity.ProductName,
                    ProductDescription = entity.ProductDescription,
                    ProductImgSRC = product.TbnImg != null ? _imageService.GetSingleImageSRC(product.TbnImg, product.ID) : string.Empty
                };
                productImgSliderList.Add(productDTO);
            }

            // Return NotFound if no products are found
            if (entityWithMaxRelatedChildren == null)
            {
                return NotFound();
            }

            return Ok(productImgSliderList.ToArray());
        }

        /// <summary>
        /// Deletes the specified image by its ID from the product's thumbnail.
        /// </summary>
        /// <param name="imgID">The ID of the image to delete.</param>
        /// <returns>Returns Ok if the operation is successful.</returns>
        [HttpDelete("delete/image/{imgID:int}")]
        public async Task<ActionResult> DeleteImg(int imgID)
        {
            // Retrieve the image and the product associated with it
            var img = await _imageRepository.FindByCondition(i => i.ID == imgID).FirstOrDefaultAsync();
            var product = await _productRepository.FindByCondition(p => p.TbnImgId == imgID).Include(i => i.TbnImg).FirstOrDefaultAsync();

            // Update the product to remove the reference to the thumbnail image
            product.TbnImg = null;
            product.TbnImgId = null;
            _productRepository.Update(product);

            // Delete the specified image
            _imageRepository.Delete(img);

            return Ok();
        }

        /// <summary>
        /// Deletes all images from the specified image gallery by its ID.
        /// </summary>
        /// <param name="glryID">The ID of the image gallery to delete images from.</param>
        /// <returns>Returns Ok if the operation is successful.</returns>
        [HttpDelete("delete/gallery/{glryID:int}")]
        public async Task<ActionResult> DeleteImgFromGallery(int glryID)
        {
            // Retrieve all images associated with the specified image gallery
            List<Image> imgs = await _imageRepository.FindByCondition(i => i.Gallery.ID == glryID).ToListAsync();

            // Delete all images
            foreach (var image in imgs)
            {
                _imageRepository.Delete(image);
            }

            return Ok();
        }
    }}

