using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities
{
    public class Tag : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<ProductTag>? ProductTags { get; set; }

    }
}
