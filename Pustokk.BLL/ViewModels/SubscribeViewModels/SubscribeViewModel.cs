using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.SubscribeViewModels
{
    public class SubscribeViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public DateTime SubscribedAt { get; set; }
    }

    public class SubscribeCreateViewModel : IViewModel
    {
        public required string Email { get; set; }
    }

    public class SubscribeUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public DateTime SubscribedAt { get; set; }//subscribe date
    }
}
