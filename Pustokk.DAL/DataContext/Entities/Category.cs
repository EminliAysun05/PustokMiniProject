using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Pustokk.DAL.DataContext.Entities.Common;

namespace Pustokk.DAL.DataContext.Entities
{
    public class Category : BaseEntity
    {
        public int Name { get; set; }
        public int ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category>? SubCategories { get; set; }
    }
}
