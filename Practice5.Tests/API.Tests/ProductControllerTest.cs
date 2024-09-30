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
using Newtonsoft.Json;
using System.Text.Json;


namespace Practice5.Tests.API.Tests
{
	public class ProductControllerTest : ControllerBase
	{
		private readonly HttpClient _httpClient;
		public ProductsController _controller { get; private set; }

		public Mock<ApplicationDbContext>? _mockDbContext { get; private set; }

		public ProductControllerTest()
		{
			_mockDbContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
			_httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://localhost:7052/")
			};
			_controller = new ProductsController(_mockDbContext.Object);
		}

		[Fact]

		public async Task GetProducts_ReturnsOkResult()
		{
			var response = await _httpClient.GetAsync("products");
			Assert.True(response.IsSuccessStatusCode);
		}

		[Fact]
		public async Task PostProduct_AddsProduct_Returnscreated()
		{
			var product = new Product { Product_Id = 1, ProductName = "Grape" };
			var url = "\"https://localhost:7052/product";
			var jsonBody = JsonConvert.SerializeObject(product);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");


			var response = await _httpClient.PostAsync(url, content);


			Assert.True(response.IsSuccessStatusCode);
		}

		[Fact]
		public async Task PutProduct_UpdatesProduct()
		{
			var product = new Product { Product_Id = 2, ProductName = "Grape" };
			var url = "\"https://localhost:7052/product/2";
			var jsonBody = JsonConvert.SerializeObject(product);
			var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

			var response = await _httpClient.PutAsync(url, content);


			Assert.True(response.IsSuccessStatusCode);
		}

		[Fact]
		public void DeleteProduct_ReturnsDeletedProduct()
		{
			var url = "\"https://localhost:7052/product/2";

			var response = _httpClient.DeleteAsync(url);

			Assert.True(response.IsCompletedSuccessfully);
		}
	}
}
