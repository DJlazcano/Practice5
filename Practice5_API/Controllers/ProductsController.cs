﻿using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data;
using Practice5_Model.Models;

namespace Practice5_API.Controllers
{
	[ApiController]
	public class ProductsController : ControllerBase
	{
		//public IActionResult Index()
		//{
		//	return View();
		//}

		private readonly ApplicationDbContext _db;

		public ProductsController(ApplicationDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		[Route("/products")]
		public IActionResult GetProducts()
		{
			return Ok(_db.Products.ToList());

		}

		//[HttpGet("{id}")]
		[HttpGet]
		[Route("/product/{id}")]
		public IActionResult GetProductById(int id)
		{
			var product = _db.Products.Find(id);

			return Ok(product);
		}

		//Create Shirt

		[HttpPost]
		[Route("/product")]
		public async Task<IActionResult> CreateProduct([FromBody] Product product)
		{
			if (product == null) return BadRequest();

			await _db.Products.AddAsync(product);
			await _db.SaveChangesAsync();

			return CreatedAtAction(nameof(GetProductById), new { id = product.Product_Id }, product);
		}

		[HttpPut]
		[Route("/product/{id}")]

		public IActionResult UpdateProduct(int id, Product product)
		{
			if (id != product.Product_Id)
			{
				return BadRequest();
			}
			try
			{
				_db.Products.Update(product);
			}
			catch (Exception)
			{
				throw;
			}


			return NoContent();
		}

		[HttpDelete]
		[Route("/product/{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var product = _db.Products.First(p => p.Product_Id == id);
			if (product == null)
			{
				return NotFound();
			}
			_db.Products.Remove(product);
			_db.SaveChanges();

			return Ok(product);

		}
	}
}
