using Practice5_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Practice5_DataAccess.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Purchase> Purchases { get; set; }
		public DbSet<Inventory> Inventories { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=USQROJLAZCANOA1;Database=Practice5;TrustServerCertificate=True;Trusted_Connection=True;");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Sale>()
				.HasIndex(s => s.Product_Id)
				.IsUnique(false);

			modelBuilder.Entity<Purchase>()
				.HasIndex(p => p.Product_Id)
				.IsUnique(false);

			modelBuilder.Entity<Sale>()
				.HasOne(s => s.Product)
				.WithMany()
				.HasForeignKey(s => s.Product_Id)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Purchase>()
				.HasOne(p => p.Product)
				.WithMany()
				.HasForeignKey(p => p.Product_Id)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Product>()
				.HasOne(p => p.Inventory)
				.WithOne(i => i.Product)
				.HasForeignKey<Inventory>(i => i.Product_Id)
				.IsRequired(false);

			modelBuilder.Entity<Inventory>()
				.HasOne(i => i.Product)
				.WithOne(p => p.Inventory)
				.HasForeignKey<Inventory>(i => i.Product_Id)
				.OnDelete(DeleteBehavior.Cascade);

			// Seed Products data first
			var productsList = new Product[]
			{
				new Product{Product_Id = 1, ProductName ="Lemon",Description ="Theyw are small but sweet", Price = 15, QuantityInStock = 100 },
				new Product{Product_Id = 2,ProductName = "Mango",Description = "Large and no seed",Price = 32.5,QuantityInStock = 50 },
				new Product{Product_Id = 3,ProductName = "Avocado",Description = "Some came damaged",Price = 85.5,QuantityInStock = 230},
				new Product{Product_Id = 4,ProductName = "Orange",Description = "Very sweet and juicy",Price = 12.4,QuantityInStock = 400},
				new Product{Product_Id = 5,ProductName = "Pineapple",Description = "Not very good batch",Price = 50.5,QuantityInStock = 20},
				new Product{Product_Id = 6,ProductName = "Cherry",Description = "Colorful and sweet",Price = 25.7,QuantityInStock = 600},
				new Product{Product_Id = 7,ProductName = "Pomegranate",Description = "Not many",Price = 70.5,QuantityInStock = 8},
				new Product{Product_Id = 8,ProductName = "Blueberry",Description = "Very sweet and plenty of them",Price = 36.3,QuantityInStock = 200},
				new Product{Product_Id = 9,ProductName = "Cantaloupe",Description = "Large and no seed, also very sweet",Price = 29.3,QuantityInStock = 36},
				new Product{Product_Id = 10,ProductName = "Coconut",Description = "Large and Full of water",Price = 43.5,QuantityInStock = 136}
			};
			modelBuilder.Entity<Product>().HasData(productsList);


			var inventoryList = new Inventory[]
			{
				new Inventory{Inventory_Id = 1, Stock = 100, Product_Id = 1},
				new Inventory{Inventory_Id = 2, Stock = 200, Product_Id = 2},
				new Inventory{Inventory_Id = 3, Stock = 30, Product_Id = 3},
				new Inventory{Inventory_Id = 4, Stock = 234, Product_Id = 4},
				new Inventory{Inventory_Id = 5, Stock = 531, Product_Id = 5},
				new Inventory{Inventory_Id = 6, Stock = 345, Product_Id = 6},
				new Inventory{Inventory_Id = 7, Stock = 322, Product_Id = 7},
				new Inventory{Inventory_Id = 8, Stock = 345, Product_Id = 8},
				new Inventory{Inventory_Id = 9, Stock = 232, Product_Id = 9},
				new Inventory{Inventory_Id = 10, Stock = 40, Product_Id = 10}
			};
			modelBuilder.Entity<Inventory>().HasData(inventoryList);

			// Seed Sales data
			var salesList = new Sale[]
			{
				new Sale{Sale_Id = 1, QuantitySold = 20,SaleDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),SalePrice = 15,TotalAmount = 300,CustomerName = "John Smith",Product_Id = 1},
				new Sale{Sale_Id = 2, QuantitySold = 2,SaleDate = Convert.ToDateTime("2024-09-20 4:23:00 PM"),SalePrice = 50.5,TotalAmount = 101,CustomerName = "Juan Manuel",Product_Id = 5},
				new Sale{Sale_Id = 3, QuantitySold = 10,SaleDate = Convert.ToDateTime("2024-09-5 9:23:00 AM" ),SalePrice = 43.5,TotalAmount = 435,CustomerName = "Dany Michel",Product_Id = 10},
				new Sale{Sale_Id = 4, QuantitySold = 1,SaleDate = Convert.ToDateTime("2024-09-5 5:23:00 PM"),SalePrice = 70.5,TotalAmount = 70.5,CustomerName = "Sofia",Product_Id = 7},
				new Sale{Sale_Id = 5, QuantitySold = 40,SaleDate = Convert.ToDateTime("2024-09-10 11:11:00 AM"),SalePrice = 25.7,TotalAmount = 1028,CustomerName = "Lily",Product_Id = 6},
				new Sale{Sale_Id = 6, QuantitySold = 50,SaleDate =Convert.ToDateTime("2024-09-12 6:23:00 PM"),SalePrice = 25.7, TotalAmount = 1285 ,CustomerName = "Zac",Product_Id = 6},
				new Sale{Sale_Id = 7, QuantitySold = 20,SaleDate =Convert.ToDateTime("2024-09-15 10:23:00 AM"),SalePrice = 25.7,TotalAmount = 514,CustomerName = "Jhonny",Product_Id = 6},
				new Sale{Sale_Id = 8, QuantitySold = 62,SaleDate =Convert.ToDateTime("2024-09-23 9:34:00 PM"),SalePrice = 36.3,TotalAmount = 2250.6,CustomerName = "Harold",Product_Id = 9},
				new Sale{Sale_Id = 9, QuantitySold = 82,SaleDate =Convert.ToDateTime("2024-09-15 8:23:00 PM"),SalePrice = 12.4,TotalAmount = 1016.8,CustomerName = "Sebastian",Product_Id = 4},
				new Sale{Sale_Id = 10, QuantitySold = 2,SaleDate =Convert.ToDateTime("2024-09-17 7:14:00 AM"),SalePrice = 12.4,TotalAmount = 24.8,CustomerName = "Alonso",Product_Id = 4}
			};
			modelBuilder.Entity<Sale>().HasData(salesList);

			// Seed Purchases data
			var purchaseList = new Purchase[]
			{
				new Purchase{ Purchase_Id = 1, QuantityPurchased = 100,PurchasePrice = 10.7, PurchaseDate = Convert.ToDateTime("2024-09-1 7:00:00 PM"),TotalCost = 107,Product_Id =1 },
				new Purchase{ Purchase_Id = 2, QuantityPurchased = 50,PurchasePrice = 40.5, PurchaseDate = Convert.ToDateTime("2024-09-17 7:14:00 AM"),TotalCost = 2025,Product_Id =3},
				new Purchase{ Purchase_Id = 3, QuantityPurchased = 50,PurchasePrice = 15.3, PurchaseDate = Convert.ToDateTime("2024-09-15 5:14:00 AM"),TotalCost = 765,Product_Id =2},
				new Purchase{ Purchase_Id = 4, QuantityPurchased = 200,PurchasePrice = 6.5, PurchaseDate = Convert.ToDateTime("2024-09-25 10:14:00 AM"),TotalCost = 1300,Product_Id =4},
				new Purchase{ Purchase_Id = 5, QuantityPurchased = 10,PurchasePrice = 25, PurchaseDate = Convert.ToDateTime("2024-09-01 5:25:00 AM"),TotalCost = 250,Product_Id =5},
				new Purchase{ Purchase_Id = 6, QuantityPurchased = 500,PurchasePrice = 12.7, PurchaseDate = Convert.ToDateTime("2024-09-30 6:56:00 PM"),TotalCost = 6350,Product_Id =6},
				new Purchase{ Purchase_Id = 7, QuantityPurchased = 8,PurchasePrice = 60.9, PurchaseDate = Convert.ToDateTime("2024-09-13 8:45:00 AM"),TotalCost = 487.9,Product_Id =7},
				new Purchase{ Purchase_Id = 8, QuantityPurchased = 200,PurchasePrice = 48.5, PurchaseDate = Convert.ToDateTime("2024-09-17 7:14:00 AM"),TotalCost = 9700,Product_Id =3},
				new Purchase{ Purchase_Id = 9, QuantityPurchased = 100,PurchasePrice = 27.2, PurchaseDate = Convert.ToDateTime("2024-09-06 8:36:00 AM"),TotalCost = 272,Product_Id =1},
				new Purchase{ Purchase_Id = 10, QuantityPurchased = 200,PurchasePrice = 24.4, PurchaseDate = Convert.ToDateTime("2024-09-30 8:56:00 AM"),TotalCost = 4880,Product_Id =8}
			};
			modelBuilder.Entity<Purchase>().HasData(purchaseList);
		}
	}
}
