using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.CategoryViewModels;

public class CategoryViewModel : IViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!; 
    public int? ParentCategoryId { get; set; }
    public string? ParentCategoryName { get; set; }
    public List<CategoryViewModel>? SubCategories { get; set; }

    //public List<ProductViewModel>? Products { get; set; }
}

public class CategoryCreateViewModel : IViewModel
{
    public required string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public List<CategoryViewModel> Categories { get; set; } = new();


}

public class CategoryUpdateViewModel : IViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentCategoryId { get; set; }

}
