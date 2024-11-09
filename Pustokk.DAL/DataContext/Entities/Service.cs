using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities
{
    public class Service : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string IconUrl { get; set; }
    }
}
