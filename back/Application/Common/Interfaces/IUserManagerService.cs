using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserManagerService
    {
        public Task CreateUserAsync(CancellationToken cancelationToken);
    }
}
