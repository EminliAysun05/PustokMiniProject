using Pustokk.BLL.ViewModels.CategoryViewModels;
using Pustokk.BLL.ViewModels.ProductViewModels;
using Pustokk.BLL.ViewModels.ServiceViewModels;
using Pustokk.BLL.ViewModels.SliderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModel
{
    public class HomeViewModel
    {
        public List<ProductViewModel>? Products { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
        public List<SliderViewModel>? Sliders { get; set; }
        public List<ServiceViewModel>? Services { get; set; }
       // public Dictionary<string, string>? Settings { get; set; }
    }
}
