using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopApi.DTO;
using ShopApi.Models;
using ShopApi.Repositories.IRepositories;

namespace ShopApi.Controllers
{
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


        public SellerController(
            ISellerRepository _sellerRepository,
            IProductRepository _productRepository,
            ICategoryRepository _categoryRepository,
            IImageService _imgService,
            IPurchaseRepository _purchaseRepository,
            IReviewRepository _reviewRepository)
        {
            this._sellerRepository = _sellerRepository;
            this._productRepository = _productRepository;
            this._categoryRepository = _categoryRepository;
            this._imageService = _imgService;
            this._purchaseRepository = _purchaseRepository;
            this._reviewRepository = _reviewRepository;
        }


        /// <summary>
        /// Creates a product by sellerId and CreateProductDTO
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="product"></param>
        /// <returns>Product Created</returns>
        [HttpPost("{sellerId:int}/create/product")]
        public async Task<ActionResult> CreateProduct(int sellerId, [FromForm] CreateProductDTO product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (product.ProductGallery?.Count > 5)
            {
                return BadRequest("you can upload only 5 images to the product gallery");
            }

            var category = await _categoryRepository.FindByCondition(c => c.ID == product.CategoryId).Include(c => c.Products).FirstOrDefaultAsync();
            var seller = await _sellerRepository.FindByCondition(s => s.ID == sellerId).Include(s => s.Products).FirstOrDefaultAsync();

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Stock = product.Stock,

                ThumbnailImgName =
                product.ThumbnailImgFile != null
                && product.ProductName != null ?
                 _imageService.ImageSaveHandler
                ("products", product.ProductName, false, product.ThumbnailImgFile, null): null,

                ProductGalleryName = product.ProductGallery != null
                && product.ProductName != null ?
                _imageService.ImageSaveHandler("products", product.ProductName, true, null, product.ProductGallery):null,

                Category = category,
                Seller = seller

            };


            var result = await _productRepository.Create(newProduct);

            return Ok();
        }



        [HttpPut("update/product/{productId:int}")]
        public async Task<ActionResult> UpdateProduct ([FromForm]CreateProductDTO productDTO,int productId)
        {
            var product = await _productRepository.FindByCondition(p => p.ID == productId).Include(p=>p.Category).FirstOrDefaultAsync();
            var category = await _categoryRepository.FindByCondition(c => c.ID == productDTO.CategoryId).Include(c => c.Products).FirstOrDefaultAsync();
            if (product == null) 
            {
                return NotFound(); 
            };
            if (productDTO.ProductGallery?.Count > 5)
            {
                return BadRequest("you can upload only 5 images to the product gallery");
            }

                product.ProductName = productDTO.ProductName;
                product.ProductDescription = productDTO.ProductDescription;
                product.Price = productDTO.Price;
                product.Stock = productDTO.Stock;

                product.ThumbnailImgName =
                productDTO.ThumbnailImgFile != null
                && productDTO.ProductName != null ?
                 _imageService.ImageSaveHandler
                ("product", productDTO.ProductName, false, productDTO.ThumbnailImgFile, null) : null;

                product.ProductGalleryName = productDTO.ProductGallery != null
                && productDTO.ProductName != null ?
                _imageService.ImageSaveHandler("product", productDTO.ProductName, true, null, productDTO.ProductGallery) : null;

                product.Category = category;


            _productRepository.Update(product);

            return Ok();
        }



        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult> GetProductByID(int productId)
        {
            var product = await _productRepository.FindByCondition(p => p.ID == productId).Include(p => p.Category).FirstOrDefaultAsync();
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
                ThumbnailImgSRC = _imageService.GetSingleImageSRC("products",product.ProductName,product.ThumbnailImgName),
                ProductGallerySRC = _imageService.GetMultiImageSRC("products",product.ProductName,product.ProductGalleryName)
            };
            return Ok(getProduct);
        }



        [HttpGet ("{sellerId:int}/products")]
        public async Task<ActionResult<GetProductDTO>> GetSellersProducts(int sellerId)
        {
            var products =await _productRepository.FindByCondition(p=>p.Seller.ID==sellerId).Include(p=>p.Seller).ToListAsync();
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
                    ThumbnailImgSRC = _imageService.GetSingleImageSRC("products",product.ProductName,product.ThumbnailImgName),
                    ProductGallerySRC = _imageService.GetMultiImageSRC("products",product.ProductName,product.ProductGalleryName)
                };
                productList.Add(productDto);
            }
            return Ok(productList.ToArray());
        }



        [HttpGet("{sellerId:int}/purchases")]
        public async Task<ActionResult> GetSellersPurchases(int sellerId)
        {
            var purchases = await _purchaseRepository
                .FindByCondition(p => p.Product.Seller.ID == sellerId)
                .Include(p => p.Product)              
                .Include(P=>P.Customer)
                .Include(p=>p.Customer.User)
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
                        ShipingAddress = purchase.Customer.ShipingAddress
                    };
                    sales.Add(sale);
                }
            }

            return Ok(sales.ToArray());
        }



        [HttpGet("/product/{productId:int}/review")]
        public async Task<ActionResult> GetSellersProductReviews (int productId)
        {
            var reviews =await _reviewRepository
                .FindByCondition(r=>r.Product.ID==productId)
                .Include(r=>r.Customer)
                .Include(r=>r.Customer.User)
                .Include(r=>r.Product)
                .ToListAsync();

            List<GetSellersProductReviewDTO> productReviews = new List<GetSellersProductReviewDTO>();
            if(reviews!=null)
            {
                foreach(var review in reviews)
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



        [HttpPut("{sellerId:int}/update")]
        public async Task<ActionResult> UpdateSellerDetails (int sellerId, [FromForm]UpdateSellerDTO sellerDTO)
        {
            var seller =await _sellerRepository.FindByCondition(s => s.ID == sellerId).FirstOrDefaultAsync();
            if (seller == null)
            {
                return NotFound();
            }
            if (sellerDTO.StoreName == null) 
            {
                return BadRequest();
            }
            seller.StoreName = sellerDTO.StoreName;
            seller.StoresImgName = _imageService.ImageSaveHandler("sellers",sellerDTO.StoreName,false,sellerDTO.StoreImage,null);

            _sellerRepository.Update(seller);

            return Ok();
        }
    }
}
