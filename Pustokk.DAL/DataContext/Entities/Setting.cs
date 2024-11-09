using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities
{
    public class Setting : BaseEntity
    {
        public required string Adress {  get; set; }
        public required string Phone { get; set; }
        public required string Mail { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
