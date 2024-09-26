using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data;
using Practice5_Model.Models;

namespace Practice5_API.Controllers
{
	[ApiController]
	public class InventoryController : Controller
	{
		private readonly ApplicationDbContext _db;

		public InventoryController(ApplicationDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		[Route("/inventories")]
		public IActionResult Getinventorys()
		{
			return Ok(_db.Inventories.ToList());

		}

		//[HttpGet("{id}")]
		[HttpGet]
		[Route("/inventory/{id}")]
		public IActionResult GetinventoryById(int id)
		{
			var inventory = _db.Inventories.Find(id);

			return Ok(inventory);
		}

		//Create Shirt

		[HttpPost]
		[Route("/inventory")]
		public async Task<IActionResult> Createinventory([FromBody] Inventory inventory)
		{
			if (inventory == null) return BadRequest();

			var product = _db.Products.Find(inventory.Product_Id);

			if (product == null) return BadRequest();

			inventory.Product = product;

			await _db.Inventories.AddAsync(inventory);
			await _db.SaveChangesAsync();

			return CreatedAtAction(nameof(GetinventoryById), new { id = inventory.Inventory_Id }, inventory);
		}

		[HttpPut]
		[Route("/inventory/{id}")]

		public IActionResult Updateinventory(int id, Inventory inventory)
		{
			if (id != inventory.Inventory_Id)
			{
				return BadRequest();
			}
			try
			{
				_db.Inventories.Update(inventory);
			}
			catch (Exception)
			{
				throw;
			}


			return NoContent();
		}

		[HttpDelete]
		[Route("/inventory/{id}")]
		public IActionResult Deleteinventory(int id)
		{
			var inventory = _db.Inventories.First(p => p.Inventory_Id == id);
			if (inventory == null)
			{
				return NotFound();
			}
			_db.Inventories.Remove(inventory);
			_db.SaveChanges();

			return Ok(inventory);

		}
	}
}
