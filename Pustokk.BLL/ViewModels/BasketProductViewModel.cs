using Pustokk.DAL.DataContext.Entities;

namespace Pustokk.BLL.ViewModels;

public class BasketProductViewModel
{
    public Product Product { get; set; } = null!;
    public required string ProductName { get; set; } 
    public decimal Price { get; set; }
    public int Count { get; set; }
    public decimal Total { get; set; }

}
