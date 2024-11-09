using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities;

public class Setting : BaseEntity
{
    public required string Key { get; set; }
    public required string Value { get; set; }

}
