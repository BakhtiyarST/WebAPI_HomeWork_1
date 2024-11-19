using System.Diagnostics;

namespace WebAPI_S1.Models;

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public string Description { get; set; }
	public int? ProductGroupId { get; set; }
	public virtual List<Storage> Storages { get; set; }
	public virtual ProductGroup? ProductGroup { get; set; }
}
