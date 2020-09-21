using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceWebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    adminId = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    passwordHash = table.Column<byte[]>(nullable: false),
                    passwordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.adminId);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    cartId = table.Column<Guid>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.cartId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryId = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "OrderSession",
                columns: table => new
                {
                    orderSessionId = table.Column<Guid>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSession", x => x.orderSessionId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<Guid>(nullable: false),
                    username = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    passwordHash = table.Column<byte[]>(nullable: false),
                    passwordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<Guid>(nullable: false),
                    categoryId = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(unicode: false, nullable: false),
                    price = table.Column<double>(nullable: false),
                    image = table.Column<string>(unicode: false, nullable: false),
                    stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.categoryId,
                        principalTable: "Category",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderId = table.Column<Guid>(nullable: false),
                    userId = table.Column<Guid>(nullable: false),
                    orderSessionId = table.Column<Guid>(nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<int>(nullable: false),
                    total = table.Column<decimal>(type: "decimal(7, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_Order_OrderSession",
                        column: x => x.orderSessionId,
                        principalTable: "OrderSession",
                        principalColumn: "orderSessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerId = table.Column<Guid>(nullable: false),
                    fullName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    address = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    city = table.Column<string>(unicode: false, maxLength: 80, nullable: false),
                    zipcode = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    personalIdentityNumber = table.Column<string>(unicode: false, maxLength: 15, nullable: false),
                    userId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerId);
                    table.ForeignKey(
                        name: "FK_Customer_User",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    cartProductId = table.Column<Guid>(nullable: false),
                    cartId = table.Column<Guid>(nullable: false),
                    productId = table.Column<Guid>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => x.cartProductId);
                    table.ForeignKey(
                        name: "FK_CartProduct_Cart",
                        column: x => x.cartId,
                        principalTable: "Cart",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartProduct_Product",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSessionProduct",
                columns: table => new
                {
                    orderSessionProductId = table.Column<Guid>(nullable: false),
                    orderSessionId = table.Column<Guid>(nullable: false),
                    productId = table.Column<Guid>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSessionProduct", x => x.orderSessionProductId);
                    table.ForeignKey(
                        name: "FK_OrderSessionProduct_OrderSession",
                        column: x => x.orderSessionId,
                        principalTable: "OrderSession",
                        principalColumn: "orderSessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSessionProduct_Product",
                        column: x => x.productId,
                        principalTable: "Product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_cartId",
                table: "CartProduct",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_productId",
                table: "CartProduct",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer",
                table: "Customer",
                column: "personalIdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_userId",
                table: "Customer",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_orderSessionId",
                table: "Order",
                column: "orderSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSessionProduct_orderSessionId",
                table: "OrderSessionProduct",
                column: "orderSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSessionProduct_productId",
                table: "OrderSessionProduct",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_categoryId",
                table: "Product",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User",
                table: "User",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderSessionProduct");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "OrderSession");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
