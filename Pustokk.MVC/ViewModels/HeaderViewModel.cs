using Pustokk.DAL.DataContext.Entities;

namespace Pustokk.MVC.ViewModels
{
    public class HeaderViewModel
    {

        public Setting Setting { get; set; } // Saytın ümumi məlumatları
        public List<Category> Categories { get; set; }
    }
}
