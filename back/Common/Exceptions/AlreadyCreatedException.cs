using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class AlreadyCreatedException : Exception
    {
        public AlreadyCreatedException(string name, object key) : base($"An entity {name} has already been added with the value {key}")
        {
        }
    }
}
