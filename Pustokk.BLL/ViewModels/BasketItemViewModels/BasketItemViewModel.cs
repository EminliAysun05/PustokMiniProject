using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.BasketItemViewModels
{
    public class BasketItemViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }

    public class BasketItemCreateViewModel : IViewModel
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }

    public class BasketItemUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
    }
}

