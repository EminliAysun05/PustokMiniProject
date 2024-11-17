using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities;

public class BasketItem : BaseEntity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public int Quantity { get; set; }

}
