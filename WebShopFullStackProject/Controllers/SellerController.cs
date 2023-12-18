using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopApi.DTO;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    /// <summary>
    /// Controller for managing sellers and related operations.
    /// </summary>
    //[Authorize]
    //[Authorize(Policy = "SellerOnly")]
    [ApiController]
    [Route("api/[controller]")]
    public class SellerController : Controller
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageService _imageService;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageGalleryRepository _imageGalleryRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SellerController"/> class.
        /// </summary>
        public SellerController(
            ISellerRepository _sellerRepository,
            IProductRepository _productRepository,
            ICategoryRepository _categoryRepository,
            IImageService _imgService,
            IPurchaseRepository _purchaseRepository,
            IReviewRepository _reviewRepository,
            IImageRepository _imageRepository,
            IImageGalleryRepository _imageGalleryRepository,
            IUserRepository _userRepository)
        {
            this._sellerRepository = _sellerRepository;
            this._productRepository = _productRepository;
            this._categoryRepository = _categoryRepository;
            this._imageService = _imgService;
            this._purchaseRepository = _purchaseRepository;
            this._reviewRepository = _reviewRepository;
            this._imageRepository = _imageRepository;
            this._imageGalleryRepository = _imageGalleryRepository;
            this._userRepository = _userRepository;
        }

        /// <summary>
        /// Creates a product for a seller.
        /// </summary>
        /// <param name="sellerId">The ID of the seller.</param>
        /// <param name="product">The product details.</param>
        /// <returns>Returns the created product.</returns>
        [HttpPost("{sellerId:int}/create/product")]
        public async Task<ActionResult> CreateProduct(int sellerId, [FromForm] CreateProductDTO product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (product.ProductGallery?.Count > 5)
            {
                return BadRequest("You can upload only 5 images to the product gallery.");
            }

            var category = await _categoryRepository.FindByCondition(c => c.ID == product.CategoryId).Include(c => c.Products).FirstOrDefaultAsync();
            var seller = await _sellerRepository.FindByCondition(s => s.ID == sellerId).Include(s => s.Products).FirstOrDefaultAsync();

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Stock = product.Stock,
                Category = category,
                Seller = seller
            };
            var result = await _productRepository.Create(newProduct);
            await _imageService.ImageSaveHandler("products", result.ID, product.ThumbnailImgFile);
            await _imageService.GallerySaveHandler("products", result.ID, product.ProductGallery);

            return Ok(result);
        }

        /// <summary>
        /// Updates a product for a seller.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="productDTO">The updated product details.</param>
        /// <returns>Returns Ok if the operation is successful.</returns>
        [HttpPut("update/product/{productId:int}")]
        public async Task<ActionResult> UpdateProduct(int productId, [FromForm] UpdateProductDTO productDTO)
        {
            var product = await _productRepository.FindByCondition(p => p.ID == productId).Include(p => p.Category).Include(i => i.TbnImg).FirstOrDefaultAsync();
            var category = await _categoryRepository.FindByCondition(c => c.ID == product.Category.ID).Include(c => c.Products).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            };
            if (productDTO.ProductGallery?.Count > 5)
            {
                return BadRequest("You can upload only 5 images to the product gallery.");
            }

            product.ProductName = productDTO.ProductName;
            product.ProductDescription = productDTO.ProductDescription;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;

            if (productDTO.ThumbnailImgFile != null)
            {
                await _imageService.ImageSaveHandler("products", product.ID, productDTO.ThumbnailImgFile);
            }

            if (productDTO.ProductGallery != null)
            {
                await _imageService.GallerySaveHandler("products", productId, productDTO.ProductGallery);
            }

            _productRepository.Update(product);

            return Ok();
        }

        /// <summary>
        /// Gets product details by product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve.</param>
        /// <returns>Returns details of the specified product.</returns>
        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult> GetProductByID(int productId)
        {
            var product = await _productRepository.FindByCondition(p => p.ID == productId)
                .Include(p => p.Category)
                .Include(g => g.Gallery)
                .Include(i => i.TbnImg).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            var getProduct = new GetProductDTO
            {
                ID = product.ID,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Rank = product.Rank,
                Stock = product.Stock,
                Category = product.Category?.CategoryName,
                ThumbnailImgSRC = product.TbnImg != null ? _imageService.GetSingleImageSRC(product.TbnImg, product.ID) : null,
                ProductGallerySRC = product.Gallery != null ? _imageService.GetMultiImageSRC(product.Gallery, product.ID) : null,
            };
            return Ok(getProduct);
        }

        /// <summary>
        /// Gets products associated with a seller.
        /// </summary>
        /// <param name="sellerId">The ID of the seller.</param>
        /// <returns>Returns a list of products associated with the seller.</returns>
        [HttpGet("{sellerId:int}/products")]
        public async Task<ActionResult<GetProductDTO>> GetSellersProducts(int sellerId)
        {
            var products = await _productRepository.FindByCondition(p => p.Seller.ID == sellerId)
                .Include(p => p.Seller)
                .Include(i => i.TbnImg)
                .Include(g => g.Gallery).ToListAsync();
            List<GetProductDTO> productList = new List<GetProductDTO>();
            foreach (var product in products)
            {
                var productDto = new GetProductDTO
                {
                    ID = product.ID,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = product.Price,
                    Rank = product.Rank,
                    Stock = product.Stock,
                    Category = product.Category?.CategoryName,
                    ThumbnailImgSRC = product.TbnImg != null ? _imageService.GetSingleImageSRC(product.TbnImg, product.ID) : null,
                    ProductGallerySRC = product.Gallery != null ? _imageService.GetMultiImageSRC(product.Gallery, product.ID) : null,
                };
                productList.Add(productDto);
            }
            return Ok(productList.ToArray());
        }

        /// <summary>
        /// Gets purchases associated with a seller.
        /// </summary>
        /// <param name="sellerId">The ID of the seller.</param>
        /// <returns>Returns a list of sales associated with the seller.</returns>
        [HttpGet("{sellerId:int}/purchases")]
        public async Task<ActionResult> GetSellersPurchases(int sellerId)
        {
            var purchases = await _purchaseRepository
                .FindByCondition(p => p.Product.Seller.ID == sellerId)
                .Include(p => p.Product)
                .Include(P => P.Customer)
                .Include(p => p.Customer.User)
                .Include(ip => ip.Product.TbnImg)
                .Include(ic => ic.Customer.User.TbnImg)
                .ToListAsync();

            List<GetSellerSalesDTO> sales = new List<GetSellerSalesDTO>();
            if (purchases != null)
            {
                foreach (var purchase in purchases)
                {
                    var sale = new GetSellerSalesDTO
                    {
                        ProductId = purchase.Product.ID,
                        ProductName = purchase.Product.ProductName,
                        Date = purchase.Date,
                        TotalPrice = purchase.TotalPrice,
                        CustomerName = purchase.Customer.User.FirstName,
                        ShipingAddress = purchase.Customer.ShipingAddress,
                        ProductTbnImgSRC = purchase.Product.TbnImg != null ? _imageService.GetSingleImageSRC(purchase.Product.TbnImg, purchase.Product.ID) : string.Empty,
                        CustomerImgSRC = purchase.Customer.User.TbnImg != null ? _imageService.GetSingleImageSRC(purchase.Customer.User.TbnImg, purchase.Customer.User.ID) : string.Empty,
                    };
                    sales.Add(sale);
                }
            }

            return Ok(sales.ToArray());
        }

        /// <summary>
        /// Gets product reviews associated with a seller's product.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>Returns a list of product reviews.</returns>
        [HttpGet("/product/{productId:int}/review")]
        public async Task<ActionResult> GetSellersProductReviews(int productId)
        {
            var reviews = await _reviewRepository
                .FindByCondition(r => r.Product.ID == productId)
                .Include(r => r.Customer)
                .Include(r => r.Customer.User)
                .Include(r => r.Product)
                .ToListAsync();

            List<GetSellersProductReviewDTO> productReviews = new List<GetSellersProductReviewDTO>();
            if (reviews != null)
            {
                foreach (var review in reviews)
                {
                    var reviewDTO = new GetSellersProductReviewDTO
                    {
                        ID = review.ID,
                        ReviewText = review.ReviewText,
                        Rank = review.Rank,
                        Date = review.Date,
                        CustomerName = review.Customer.User.FirstName,
                        ProductName = review.Product.ProductName
                    };
                    productReviews.Add(reviewDTO);
                }
            }
            return Ok(productReviews);
        }

        /// <summary>
        /// Updates details of a seller.
        /// </summary>
        /// <param name="sellerId">The ID of the seller.</param>
        /// <param name="sellerDTO">The updated seller details.</param>
        /// <returns>Returns Ok if the operation is successful.</returns>
        [HttpPut("{sellerId:int}/update")]
        public async Task<ActionResult> UpdateSellerDetails(int sellerId, [FromForm] UpdateSellerDTO sellerDTO)
        {
            var seller = await _sellerRepository.FindByCondition(s => s.ID == sellerId).FirstOrDefaultAsync();
            if (seller == null)
            {
                return NotFound();
            }
            if (sellerDTO.StoreName == null)
            {
                return BadRequest();
            }
            seller.StoreName = sellerDTO.StoreName;
            if (sellerDTO.StoreImage != null)
            {
                await _imageService.ImageSaveHandler("sellers", seller.ID, sellerDTO.StoreImage);
            }
            _sellerRepository.Update(seller);

            return Ok();
        }

        [HttpDelete("product/delete/{productId:int}/{sellerId:int}")]
        public async Task<ActionResult> DeleteProduct(int productId,int sellerId)
        {
            var product =await _productRepository.FindByCondition(p => p.ID == productId)
                .Include(r=>r.Reviews)
                .Include(p=>p.Purchases)
                .Include(i=>i.TbnImg)
                .Include(g=>g.Gallery)
                .FirstOrDefaultAsync();
            var seller = await _sellerRepository.FindByCondition(s => s.ID == sellerId).Include(p=>p.Products).FirstOrDefaultAsync();
            if(seller == null)
            {
                return NotFound("seller not found");
            }
            if (seller.Products==null)
            {
                return NotFound("seller has no products");
            }
            if(product == null)
            {
                return NotFound("product not found");
            }

            if (seller.Products.Contains(product))
            {
                
                List<Review> reviews =await _reviewRepository.FindByCondition(r => r.Product.ID == product.ID).Include(p => p.Product).ToListAsync();
                if (reviews.Count > 0)
                {
                    foreach (Review review in reviews)
                    {
                        _reviewRepository.Delete(review);
                    }
                }
                List<Purchase> purchases =await _purchaseRepository.FindByCondition(p=>p.Product.ID == product.ID).Include(P=>P.Product).ToListAsync();
                if (purchases.Count > 0)
                {
                    foreach(Purchase purchase in purchases)
                    {
                        purchase.Product = null;
                        _purchaseRepository.Update(purchase);
                    }
                }
                _productRepository.Delete(product);
                if(product.TbnImg != null)
                {
                    _imageService.DeleteImage(product.TbnImg.ID, product.ID);
                }
                if(product.Gallery != null)
                {
                    _imageService.DeleteGallery(product.Gallery.ID, product.ID);
                }
            }
            else
            {
                return NotFound("seller do not contains this product");
            }

            
                return Ok();
        }

    }
}
