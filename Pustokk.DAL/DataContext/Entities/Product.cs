using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DisCountPrice { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; } 

        public ICollection<ProductTag>? ProductTags { get; set; }
    }
}
