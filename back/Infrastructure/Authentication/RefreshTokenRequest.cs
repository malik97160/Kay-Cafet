using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class RefreshTokenRequest
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
