using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities;

public class Subscribe : BaseEntity
{
    public string? Email { get; set; }
    public bool IsActive { get; set; }

}
