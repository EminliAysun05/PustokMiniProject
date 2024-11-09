using Pustokk.DAL.DataContext.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.DAL.Repositories.Contracts;

public interface IQuery<T> where T : BaseEntity
{
    IQueryable<T> Query();
}

