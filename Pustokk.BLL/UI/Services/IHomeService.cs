using Pustokk.BLL.ViewModel;
using Pustokk.BLL.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.UI.Services
{
    public interface IHomeService
    {
        Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId);
     //   Task<ProductDetailsViewModel> GetProductDetailsAsync(int productId);
        Task<List<ProductViewModel>> GetBestSellingProductAsync();
    }
}
