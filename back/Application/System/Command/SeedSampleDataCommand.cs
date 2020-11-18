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
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IUserManagerService _userManagerService;

            public SeedSampleDataCommandHandler(IKayCafetDbContext context, RoleManager<IdentityRole> roleManager, IUserManagerService userManagerService)
            {
                _context = context;
                _roleManager = roleManager;
                _userManagerService = userManagerService;
            }
            public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
            {
                var sampleDataSeeder = new SampleDataSeeder(_context, _roleManager, _userManagerService);
                await sampleDataSeeder.SeedAllDataAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
