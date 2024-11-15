using Microsoft.AspNetCore.Http;
using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.TagViewModels;
using Pustokk.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.ProductViewModels
{
    public class ProductViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal DisCountPrice { get; set; }
        //public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<string> TagNames { get; set; } = new();
        //  public List<int>? TagIds { get; set; }
        // public string? TagName { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<string> ImageUrls { get; set; } = new();
    }

    public class ProductCreateViewModel : IViewModel
    {
        public  string? Name { get; set; }
        public  decimal? Price { get; set; }
        public string? Description { get; set; }
        public  int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = new();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public decimal? DisCountPrice { get; set; }
        public  List<IFormFile>? ImageFiles { get; set; }
    }

    public class ProductUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        public required int CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();
        public List<IFormFile>? NewImageFiles { get; set; }
        //emin deyilem
        public List<string> ImageUrls { get; set; } = new List<string>();
        public bool RemoveOldImages { get; set; }
        public List<string>? ExistingImageUrls { get; set; }
        public decimal DisCountPrice { get; set; }
    }
    //public class ProductDetailsViewModel : IViewModel
    //{
    //    public int Id { get; set; }
    //    public required string Name { get; set; }
    //    public decimal Price { get; set; }
    //    public required string Description { get; set; }
    //    public List<string> ImageUrls { get; set; } = new();
    //    public required string CategoryName { get; set; }
    //}
}



