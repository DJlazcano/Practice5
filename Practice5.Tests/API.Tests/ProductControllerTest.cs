using Microsoft.AspNetCore.Mvc;
using Moq;
using Practice5_DataAccess.Data;
using Practice5_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice5_Model.Models;

namespace Practice5.Tests.API.Tests
{
	public class ProductControllerTest : ControllerBase
	{
		public Mock<ApplicationDbContext> _mockDbContext { get; private set; }
		public ProductsController _controller { get; private set; }

		public ProductControllerTest()
		{
			_mockDbContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
			_controller = new ProductsController(_mockDbContext.Object);
		}

		[Fact]

		public async Task GetProducts_ReturnsOkResult()
		{
			var product_id = 1;

			var product = new Product { Product_Id = 1, ProductName = "Grape" };

			var products = new List<Product> { product };

			var mockSet = new Mock<DbSet<Product>>();

			mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.AsQueryable().Provider);
			mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.AsQueryable().Expression);
			mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.AsQueryable().ElementType);
			mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.AsQueryable().GetEnumerator());

			_mockDbContext.Setup(c => c.Products).Returns(mockSet.Object);

			var result = _controller.GetProductById(product_id);
			var okResult = Assert.IsType<OkObjectResult>(result);
			var productResponse = Assert.IsType<Product>(okResult.Value);
			Assert.Equal(product_id, productResponse.Product_Id);
		}

		//[Fact]
		//public async Task PostProduct_AddsProduct_Returnscreated()
		//{
		//	var product = new Product { Product_Id = 1, ProductName = "Grape" };

		//	var mockSet = new Mock<DbSet<Product>>();
		//	_mockDbContext.Setup(c => c.Products).Returns(mockSet.Object);

		//	var result = _controller.CreateProduct(product);

		//	var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
		//	var returnValue = Assert.IsType<Product>(CreatedAtActionResult.Value);
		//	Assert.Equal("Grape", returnValue.ProductName);
		//}
	}
}
