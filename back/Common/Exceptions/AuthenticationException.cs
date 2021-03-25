using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class AuthenticationException : Exception
    {
        public List<string> Errors { get; set; }

        public AuthenticationException(List<string> errors) : base($"An error occured during the authentication process")
        {
            Errors = errors;
        }
    }
}
