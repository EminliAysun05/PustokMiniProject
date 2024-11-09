using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.DAL.DataContext.Entities
{
    public class ProductImage
    {
        public required string ImageUrl { get; set; } 
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public bool IsMain { get; set; } = false;
        public bool IsAdd { get; set; } = false;
    }
}
