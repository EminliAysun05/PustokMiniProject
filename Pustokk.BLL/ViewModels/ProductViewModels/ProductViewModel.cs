using Microsoft.AspNetCore.Http;
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
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public decimal DisCountPrice { get; set; }
    }

    public class ProductCreateViewModel : IViewModel
    {
        public  string? Name { get; set; }
        public  decimal? Price { get; set; }
        public string? Description { get; set; }
        public  int CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
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
        public List<IFormFile>? NewImageFiles { get; set; }
        //emin deyilem
        public bool RemoveOldImages { get; set; }
        public List<string>? ExistingImageUrls { get; set; }
        public decimal DisCountPrice { get; set; }
    }
}



