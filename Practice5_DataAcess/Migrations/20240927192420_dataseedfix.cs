using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Practice5_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class dataseedfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Inventory_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Inventory_Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Purchase_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityPurchased = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Purchase_Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Sale_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantitySold = table.Column<double>(type: "float", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Sale_Id);
                    table.ForeignKey(
                        name: "FK_Sales_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_Id", "Description", "Price", "ProductName", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, "Theyw are small but sweet", 15.0, "Lemon", 100 },
                    { 2, "Large and no seed", 32.5, "Mango", 50 },
                    { 3, "Some came damaged", 85.5, "Avocado", 230 },
                    { 4, "Very sweet and juicy", 12.4, "Orange", 400 },
                    { 5, "Not very good batch", 50.5, "Pineapple", 20 },
                    { 6, "Colorful and sweet", 25.699999999999999, "Cherry", 600 },
                    { 7, "Not many", 70.5, "Pomegranate", 8 },
                    { 8, "Very sweet and plenty of them", 36.299999999999997, "Blueberry", 200 },
                    { 9, "Large and no seed, also very sweet", 29.300000000000001, "Cantaloupe", 36 },
                    { 10, "Large and Full of water", 43.5, "Coconut", 136 }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Inventory_Id", "Product_Id", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 100 },
                    { 2, 2, 200 },
                    { 3, 3, 30 },
                    { 4, 4, 234 },
                    { 5, 5, 531 },
                    { 6, 6, 345 },
                    { 7, 7, 322 },
                    { 8, 8, 345 },
                    { 9, 9, 232 },
                    { 10, 10, 40 }
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Purchase_Id", "Product_Id", "PurchaseDate", "PurchasePrice", "QuantityPurchased", "TotalCost" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), 10.699999999999999, 100, 107.0 },
                    { 2, 3, new DateTime(2024, 9, 17, 7, 14, 0, 0, DateTimeKind.Unspecified), 40.5, 50, 2025.0 },
                    { 3, 2, new DateTime(2024, 9, 15, 5, 14, 0, 0, DateTimeKind.Unspecified), 15.300000000000001, 50, 765.0 },
                    { 4, 4, new DateTime(2024, 9, 25, 10, 14, 0, 0, DateTimeKind.Unspecified), 6.5, 200, 1300.0 },
                    { 5, 5, new DateTime(2024, 9, 1, 5, 25, 0, 0, DateTimeKind.Unspecified), 25.0, 10, 250.0 },
                    { 6, 6, new DateTime(2024, 9, 30, 18, 56, 0, 0, DateTimeKind.Unspecified), 12.699999999999999, 500, 6350.0 },
                    { 7, 7, new DateTime(2024, 9, 13, 8, 45, 0, 0, DateTimeKind.Unspecified), 60.899999999999999, 8, 487.89999999999998 },
                    { 8, 3, new DateTime(2024, 9, 17, 7, 14, 0, 0, DateTimeKind.Unspecified), 48.5, 200, 9700.0 },
                    { 9, 1, new DateTime(2024, 9, 6, 8, 36, 0, 0, DateTimeKind.Unspecified), 27.199999999999999, 100, 272.0 },
                    { 10, 8, new DateTime(2024, 9, 30, 8, 56, 0, 0, DateTimeKind.Unspecified), 24.399999999999999, 200, 4880.0 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Sale_Id", "CustomerName", "Product_Id", "QuantitySold", "SaleDate", "SalePrice", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "John Smith", 1, 20.0, new DateTime(2024, 9, 16, 8, 23, 0, 0, DateTimeKind.Unspecified), 15.0, 300.0 },
                    { 2, "Juan Manuel", 5, 2.0, new DateTime(2024, 9, 20, 16, 23, 0, 0, DateTimeKind.Unspecified), 50.5, 101.0 },
                    { 3, "Dany Michel", 10, 10.0, new DateTime(2024, 9, 5, 9, 23, 0, 0, DateTimeKind.Unspecified), 43.5, 435.0 },
                    { 4, "Sofia", 7, 1.0, new DateTime(2024, 9, 5, 17, 23, 0, 0, DateTimeKind.Unspecified), 70.5, 70.5 },
                    { 5, "Lily", 6, 40.0, new DateTime(2024, 9, 10, 11, 11, 0, 0, DateTimeKind.Unspecified), 25.699999999999999, 1028.0 },
                    { 6, "Zac", 6, 50.0, new DateTime(2024, 9, 12, 18, 23, 0, 0, DateTimeKind.Unspecified), 25.699999999999999, 1285.0 },
                    { 7, "Jhonny", 6, 20.0, new DateTime(2024, 9, 15, 10, 23, 0, 0, DateTimeKind.Unspecified), 25.699999999999999, 514.0 },
                    { 8, "Harold", 9, 62.0, new DateTime(2024, 9, 23, 21, 34, 0, 0, DateTimeKind.Unspecified), 36.299999999999997, 2250.5999999999999 },
                    { 9, "Sebastian", 4, 82.0, new DateTime(2024, 9, 15, 20, 23, 0, 0, DateTimeKind.Unspecified), 12.4, 1016.8 },
                    { 10, "Alonso", 4, 2.0, new DateTime(2024, 9, 17, 7, 14, 0, 0, DateTimeKind.Unspecified), 12.4, 24.800000000000001 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Product_Id",
                table: "Inventories",
                column: "Product_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Product_Id",
                table: "Purchases",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Product_Id",
                table: "Sales",
                column: "Product_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
