using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities;

public class Slider : BaseEntity
{
    public required string ImageUrl { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? ButtonText { get; set; }
    //public string Link { get; set; } //buttona basanda getdiyi sehife ucun
    public bool IsActive { get; set; }
}
