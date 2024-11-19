using Microsoft.AspNetCore.Mvc;
using WebAPI_S1.Data;
using WebAPI_S1.Models;

namespace WebAPI_S1.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductGroupController : ControllerBase
{
	[HttpPost("ProductGroup")]
	public ActionResult<int> AddProductGroup(string name, string description)
	{
		using (ProductContext productContext = new ProductContext())
		{
			if (productContext.Groups.Any(pg => pg.Name == name))
				return StatusCode(409);
			var productGroup = new ProductGroup() { Name = name, Description = description };
			productContext.Groups.Add(productGroup);
			productContext.SaveChanges();
			return Ok(productGroup.Id);
		}
	}

	[HttpGet("ProductGroup")]
	public ActionResult<IEnumerable<ProductGroup>> GetAllProductGroups()
	{
		using (ProductContext productContext = new ProductContext())
		{
			var list = productContext.Groups.Select(p => new ProductGroup() { Id = p.Id, Name = p.Name, Description = p.Description }).ToList();

			return Ok(list);
		}
	}

	[HttpDelete("ProductGroup")]
	public ActionResult<int> DeleteProductGroup(int id)
	{
		using (ProductContext productContext = new ProductContext())
		{
			var productGroup = productContext.Groups.FirstOrDefault(pg => pg.Id == id);
			if (productGroup != null)
			{
				productContext.Groups.Remove(productGroup);
				productContext.SaveChanges();
			}
			return Ok();
		}
	}
}
