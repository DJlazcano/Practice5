using Microsoft.AspNetCore.Mvc;
using Practice5_DataAccess.Data;
using Practice5_Model.Models;

namespace Practice5_WebApp.Controllers
{
	public class ProductController : Controller
	{

		private readonly ApplicationDbContext _db;

		public ProductController(ApplicationDbContext db)
		{

			_db = db;
		}
		public IActionResult Index()
		{
			List<Product> objList = _db.Products.ToList();

			return View(objList);
		}

		public IActionResult Upsert(int? id)
		{
			Product obj = new Product();
			if (id == null || id == 0)
			{
				//Create
				return View(obj);
			}
			//Edit
			obj = _db.Products.First(p => p.Product_Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upsert(Product obj)
		{
			if (obj.Product_Id == 0)
			{
				//Create
				await _db.Products.AddAsync(obj);
			}
			else
			{
				//Update
				_db.Products.Update(obj);
			}
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			Product obj = new Product();
			//Edit
			obj = _db.Products.First(p => p.Product_Id == id);
			if (obj == null)
			{
				return NotFound();
			}

			_db.Products.Remove(obj);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
