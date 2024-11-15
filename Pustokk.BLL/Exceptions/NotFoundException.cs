using Pustokk.BLL.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustokk.BLL.Exceptions
{
    public class NotFoundException : Exception, IBaseException
    {
        public NotFoundException(string message = "Not found") : base(message)
        {

        }
    }
}
