using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.System.Command
{
    public class SeedSampleDataCommand : IRequest
    {
        public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
        {
            private readonly IKayCafetDbContext _context;
            private readonly RoleManager<Role> _roleManager;
            private readonly UserManager<User> _userManager;

            public SeedSampleDataCommandHandler(IKayCafetDbContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
            {
                _context = context;
                _roleManager = roleManager;
                _userManager = userManager;
            }
            public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
            {
                var sampleDataSeeder = new SampleDataSeeder(_context, /*_roleManager,*/ _userManager);
                await sampleDataSeeder.SeedAllDataAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
