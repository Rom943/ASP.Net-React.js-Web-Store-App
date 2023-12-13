using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopApi.Migrations
{
    /// <inheritdoc />
    public partial class initialSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageGalleries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelatedTo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGalleries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SeedingInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSeedingDone = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedingInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GalleryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_ImageGalleries_GalleryID",
                        column: x => x.GalleryID,
                        principalTable: "ImageGalleries",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    TbnImgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Images_TbnImgId",
                        column: x => x.TbnImgId,
                        principalTable: "Images",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TbnImgId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sellers_Images_TbnImgId",
                        column: x => x.TbnImgId,
                        principalTable: "Images",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Sellers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteManagers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteManagers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SiteManagers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteManagerID = table.Column<int>(type: "int", nullable: true),
                    TbnImgId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Categories_Images_TbnImgId",
                        column: x => x.TbnImgId,
                        principalTable: "Images",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Categories_SiteManagers_SiteManagerID",
                        column: x => x.SiteManagerID,
                        principalTable: "SiteManagers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Rank = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    TbnImgId = table.Column<int>(type: "int", nullable: true),
                    GalleryId = table.Column<int>(type: "int", nullable: true),
                    SellerID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_ImageGalleries_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "ImageGalleries",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Images_TbnImgId",
                        column: x => x.TbnImgId,
                        principalTable: "Images",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Sellers_SellerID",
                        column: x => x.SellerID,
                        principalTable: "Sellers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    CartsID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.CartsID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_CartProduct_Carts_CartsID",
                        column: x => x.CartsID,
                        principalTable: "Carts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchases_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Purchases_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Reveiws",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reveiws", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reveiws_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Reveiws_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductsID",
                table: "CartProduct",
                column: "ProductsID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SiteManagerID",
                table: "Categories",
                column: "SiteManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TbnImgId",
                table: "Categories",
                column: "TbnImgId",
                unique: true,
                filter: "[TbnImgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_GalleryID",
                table: "Images",
                column: "GalleryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GalleryId",
                table: "Products",
                column: "GalleryId",
                unique: true,
                filter: "[GalleryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerID",
                table: "Products",
                column: "SellerID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TbnImgId",
                table: "Products",
                column: "TbnImgId",
                unique: true,
                filter: "[TbnImgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CustomerID",
                table: "Purchases",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProductID",
                table: "Purchases",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Reveiws_CustomerID",
                table: "Reveiws",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reveiws_ProductID",
                table: "Reveiws",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_TbnImgId",
                table: "Sellers",
                column: "TbnImgId",
                unique: true,
                filter: "[TbnImgId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteManagers_UserId",
                table: "SiteManagers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_TbnImgId",
                table: "User",
                column: "TbnImgId",
                unique: true,
                filter: "[TbnImgId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Reveiws");

            migrationBuilder.DropTable(
                name: "SeedingInfo");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "SiteManagers");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "ImageGalleries");
        }
    }
}
