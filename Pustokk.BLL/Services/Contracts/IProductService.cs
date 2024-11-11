using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.DAL.DataContext.Entities;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pustokk.BLL.Services.Contracts;

public interface IProductService 
{
    Task<ProductViewModel> AddProductWithImagesAsync(ProductCreateViewModel createViewModel);
}
