using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.CategoryViewModels
{
    public class CategoryViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        //public List<ProductViewModel>? Products { get; set; }
    }

    public class CategoryCreateViewModel : IViewModel
    {
        public required string Name { get; set; }
        public required IFormFile ImageFile { get; set; }
    }

    public class CategoryUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ExistingImageUrl { get; set; }
    }
}
