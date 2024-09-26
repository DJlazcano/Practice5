using Microsoft.AspNetCore.Mvc;
using Moq;
using Practice5_DataAccess.Data;
using Practice5_Model.Models;
using Practice5_WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Practice5.Tests.WebApp.Tests
{
	public class PurchaseFixture
	{
		public Mock<ApplicationDbContext> MockDbContext { get; private set; }
		public PurchaseController PurchaseController { get; private set; }

		public PurchaseController purchase => new PurchaseController(MockDbContext.Object);

		public PurchaseFixture()
		{
			MockDbContext = new Mock<ApplicationDbContext>();
			PurchaseController = new PurchaseController(MockDbContext.Object);
		}
	}
	public class PurchaseTest : IClassFixture<PurchaseFixture>
	{
		private readonly ITestOutputHelper _TestOutPutHelper;
		private readonly PurchaseFixture _purchaseFixture;

		public PurchaseTest(ITestOutputHelper testOutputHelper, PurchaseFixture purchaseFixture)
		{
			_TestOutPutHelper = testOutputHelper;
			_purchaseFixture = purchaseFixture;
		}

		[Fact]
		public void FetchProducts_ReturnsListOfPurchase()
		{
			var purchases = new List<Purchase> {
				new Purchase { Purchase_Id = 1, PurchaseDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),
					PurchasePrice = 20, QuantityPurchased = 50, TotalCost = 1000, Product_Id =1},
				new Purchase { Purchase_Id = 2, PurchaseDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),
					PurchasePrice = 20, QuantityPurchased = 250, TotalCost = 100000, Product_Id =2}
			}.AsQueryable();

			_purchaseFixture.MockDbContext.Setup(db => db.Purchases).Returns((Microsoft.EntityFrameworkCore.DbSet<Purchase>)purchases);

			var result = _purchaseFixture.PurchaseController.Index();
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);

			Assert.Equal(3, model.Count);
		}


		[Fact]
		public void Upsert_Get_ReturnsViewResult_WithPurchase()
		{

			var mockPurchase = new Purchase
			{
				Purchase_Id = 1,
				PurchaseDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),
				PurchasePrice = 20,
				QuantityPurchased = 50,
				TotalCost = 1000,
				Product_Id = 1
			};
			_purchaseFixture.MockDbContext.Setup(db => db.Purchases.First(It.IsAny<Func<Purchase, bool>>())).Returns(mockPurchase);


			var result = _purchaseFixture.PurchaseController.Upsert(1);


			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<Purchase>(viewResult.ViewData.Model);
			Assert.Equal(1, model.Purchase_Id);
		}

		[Fact]
		public async Task Upsert_Update_ReturnsRedirectToActionResult()
		{

			var mockPurchase = new Purchase
			{
				Purchase_Id = 0,
				PurchaseDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),
				PurchasePrice = 20,
				QuantityPurchased = 50,
				TotalCost = 1000,
				Product_Id = 1
			};


			var result = _purchaseFixture.PurchaseController.Upsert(mockPurchase);


			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirectToActionResult.ActionName);
		}

		[Fact]
		public async Task Upsert_Create_ReturnsRedirectToActionResult()
		{

			var mockPurchase = new Purchase
			{
				Purchase_Id = 1,
				PurchaseDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),
				PurchasePrice = 20,
				QuantityPurchased = 50,
				TotalCost = 1000,
				Product_Id = 1
			};


			var result = _purchaseFixture.PurchaseController.Upsert(mockPurchase);


			var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Index", redirectToActionResult.ActionName);
		}

		[Fact]
		public async Task Upsert_Delete_ReturnsRedirectToActionResult()
		{

			var purchases = new List<Purchase> {
				new Purchase { Purchase_Id = 1, PurchaseDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),
					PurchasePrice = 20, QuantityPurchased = 50, TotalCost = 1000, Product_Id =1},
				new Purchase { Purchase_Id = 2, PurchaseDate = Convert.ToDateTime("2024-09-16 8:23:00 AM"),
					PurchasePrice = 20, QuantityPurchased = 250, TotalCost = 100000, Product_Id =2}
			}.AsQueryable();

			_purchaseFixture.MockDbContext.Setup(db => db.Purchases).Returns((Microsoft.EntityFrameworkCore.DbSet<Purchase>)purchases);

			var result = _purchaseFixture.PurchaseController.Delete(2);
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<List<Purchase>>(viewResult.ViewData.Model);

			Assert.Equal(1, model.Count);
		}
	}
}