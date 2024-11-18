using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.DAL.DataContext.AppSettingModels
{
    public class CloudinaryOptions
    {
        public string APIKey { get; set; } = null!;
        public string APISecret { get; set; } = null!;
        public string CloudName { get; set; } = null!;
    }
}
