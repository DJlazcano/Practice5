using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data;
using Practice5_Model.Models;

namespace Practice5_API.Controllers
{
	[ApiController]
	public class SalesController : Controller
	{

		private readonly ApplicationDbContext _db;

		public SalesController(ApplicationDbContext db)
		{
			this._db = db;
		}

		[HttpGet]
		[Route("/sales")]
		public IActionResult Getsales()
		{
			return Ok(_db.Sales.ToList());

		}

		//[HttpGet("{id}")]
		[HttpGet]
		[Route("/sale/{id}")]
		public IActionResult GetsaleById(int id)
		{
			var sale = _db.Sales.Find(id);

			return Ok(sale);
		}

		//Create Shirt

		[HttpPost]
		[Route("/sale")]
		public async Task<IActionResult> Createsale([FromBody] Sale sale)
		{
			Console.WriteLine(sale.Sale_Id + " " + sale.Product_Id + " " + sale.SaleDate);
			if (sale == null) return BadRequest();

			var product = await _db.Products.FindAsync(sale.Product_Id);

			sale.Product = product;

			await _db.Sales.AddAsync(sale);
			await _db.SaveChangesAsync();

			return CreatedAtAction(nameof(GetsaleById), new { id = sale.Sale_Id }, sale);
		}

		[HttpPut]
		[Route("/sale/{id}")]

		public IActionResult Updatesale(int id, Sale sale)
		{
			if (id != sale.Sale_Id)
			{
				return BadRequest();
			}
			try
			{
				_db.Sales.Update(sale);
			}
			catch (Exception)
			{
				throw;
			}


			return NoContent();
		}

		[HttpDelete]
		[Route("/sale/{id}")]
		public IActionResult Deletesale(int id)
		{
			var sale = _db.Sales.First(p => p.Sale_Id == id);
			if (sale == null)
			{
				return NotFound();
			}
			_db.Sales.Remove(sale);
			_db.SaveChanges();

			return Ok(sale);

		}
	}
}
