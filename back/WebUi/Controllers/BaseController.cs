
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace webUI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        public IMediator _mediatr { get; set; }

        protected IMediator Mediatr => _mediatr ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}