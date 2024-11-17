using Pustokk.BLL.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.PaginateViewModels
{
    public class ProductPaginateViewModel : BasePageViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
        public string? SortBy { get; set; }
        public Dictionary<string, string> SortOptions { get; set; } = new();
    }
}
