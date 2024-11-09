using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string? UserId {  get; set; } //log in olan userlar ucun

        public int Quantity { get; set; }

    }
}
