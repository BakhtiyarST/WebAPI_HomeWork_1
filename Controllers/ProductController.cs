using Microsoft.AspNetCore.Mvc;
using WebAPI_S1.Data;
using WebAPI_S1.Models;

namespace WebAPI_S1.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
	[HttpPost("Product")]
	public ActionResult<int> AddProduct(string name, string description, decimal price)
	{
		using (ProductContext productContext = new ProductContext())
		{
			if (productContext.Products.Any(p => p.Name == name))
				return StatusCode(409);
			var product = new Product() { Name = name, Description = description, Price = price };
			productContext.Products.Add(product);
			productContext.SaveChanges();
			return Ok(product.Id);
		}
	}

	[HttpGet("Product")]
	public ActionResult<IEnumerable<Product>> GetAllProducts()
	{
		using (ProductContext productContext = new ProductContext())
		{
			var list = productContext.Products.Select(p => new Product() { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price }).ToList();

			return Ok(list);
		}
	}

	[HttpDelete("Product")]
	public ActionResult<int> DeleteProduct(int id)
	{
		using (ProductContext productContext = new ProductContext())
		{
			//Product? product = new Product();
			var product=productContext.Products.FirstOrDefault(p => p.Id == id);
			if (product != null)
			{
				productContext.Products.Remove(product);
				productContext.SaveChanges();
			}
			return Ok();
		}
	}



	[HttpPatch("Product")]
	public ActionResult<Product> UpdatePrice(int id, decimal price)
	{
		using (ProductContext productContext = new ProductContext())
		{
			var product = productContext.Products.FirstOrDefault(p => p.Id == id);
			if (product != null)
			{
				product.Price = price;
				productContext.SaveChanges();
			}
			return Ok();
		}
	}
}
