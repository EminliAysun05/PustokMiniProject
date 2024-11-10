using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.ProductImageViewModels
{
    public class ProductImageViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int ProductId { get; set; }
    }

   
    public class ProductImageCreateViewModel : IViewModel
    {
        public required int ProductId { get; set; }
        public required IFormFile ImageFile { get; set; }
    }

  
    public class ProductImageUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public IFormFile? NewImageFile { get; set; }
        public string? ExistingImageUrl { get; set; }  // Mövcud şəkil saxlanır
    }
}
}
