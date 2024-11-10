using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.ViewModels.SetttingViewModels
{
    public class SettingViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }

    public class SettingCreateViewModel : IViewModel
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }

    public class SettingUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}

