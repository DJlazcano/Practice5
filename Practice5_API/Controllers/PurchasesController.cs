using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Practice5_DataAccess.Data;
using Practice5_Model.Models;

namespace Practice5_API.Controllers
{
	[ApiController]
	public class PurchasesController : ControllerBase
	{
		private readonly ApplicationDbContext _db;

		public PurchasesController(ApplicationDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		[Route("/purchases")]
		public IActionResult GetPurchases()
		{
			return Ok(_db.Purchases.ToList());

		}

		//[HttpGet("{id}")]
		[HttpGet]
		[Route("/purchase/{id}")]
		public IActionResult GetpurchaseById(int id)
		{
			var purchase = _db.Purchases.Find(id);

			return Ok(purchase);
		}



		[HttpPost]
		[Route("/purchase")]
		public async Task<IActionResult> Createpurchase([FromBody] Purchase purchase)
		{
			if (purchase == null) return BadRequest();


			var product = await _db.Products.FindAsync(purchase.Product_Id);

			if (product == null || purchase.Product_Id == 0) return BadRequest("Product Not Found");

			purchase.Product = product;

			await _db.Purchases.AddAsync(purchase);
			await _db.SaveChangesAsync();

			return CreatedAtAction(nameof(GetpurchaseById), new { id = purchase.Purchase_Id }, purchase);
		}

		[HttpPut]
		[Route("/purchase/{id}")]

		public IActionResult Updatepurchase(int id, Purchase purchase)
		{
			if (id != purchase.Purchase_Id)
			{
				return BadRequest();
			}
			try
			{
				_db.Purchases.Update(purchase);
			}
			catch (Exception)
			{
				throw;
			}


			return NoContent();
		}

		[HttpDelete]
		[Route("/purchase/{id}")]
		public IActionResult Deletepurchase(int id)
		{
			var purchase = _db.Purchases.First(p => p.Purchase_Id == id);
			if (purchase == null)
			{
				return NotFound();
			}
			_db.Purchases.Remove(purchase);
			_db.SaveChanges();

			return Ok(purchase);

		}
	}
}
