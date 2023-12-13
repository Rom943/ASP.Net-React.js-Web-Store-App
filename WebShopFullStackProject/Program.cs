using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopApi.Context;
using ShopApi.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShopApi.Models.Enums;
using System.Text.Json.Serialization;
using ShopApi.Repositories.IRepositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MainContext>(o => { o.UseSqlServer(builder.Configuration.GetConnectionString("MainDB")); });

        // Add services to the container.
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        builder.Services.AddScoped<ISellerRepository, SellerRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IImageService, ImageService>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped<IImageGalleryRepository, ImageGalleryRepository>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopApi", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddAuthentication("Bearer").AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidAudience = builder.Configuration["Authentication:Audience"] ?? throw new ArgumentException("Authentication:Audience"),
                ValidIssuer = builder.Configuration["Authentication:Issuer"] ?? throw new ArgumentException("Authentication:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:Secret"] ?? throw new ArgumentException("Authentication:Secret"))
                )
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("userType", "Admin");
            });
            options.AddPolicy("SellerOnly", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("userType", "Seller");
            });
            options.AddPolicy("CutomerOnly", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("userType", "Customer");
            });
        });

        builder.Services.AddCors((o) =>
        {

            o.AddPolicy("open", b =>
            b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod()
        );
        });


        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<MainContext>();
            //context.Database.Migrate();
            //context.Initialize();
        }


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
            RequestPath = "/Images"
        });

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCors("open");

        app.MapControllers();

        app.Run();
    }
}