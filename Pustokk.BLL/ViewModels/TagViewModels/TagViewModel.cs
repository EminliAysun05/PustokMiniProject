using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.TagViewModels
{
    public class TagViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class TagCreateViewModel : IViewModel
    {
        public  string? Name { get; set; }
    }

    public class TagUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
