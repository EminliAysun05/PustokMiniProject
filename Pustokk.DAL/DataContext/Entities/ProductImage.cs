using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities;

public class ProductImage : BaseEntity
{
    public required string ImageUrl { get; set; } 
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public bool IsMain { get; set; } = false;
    public bool IsAdd { get; set; } = false;
}
