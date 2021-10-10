using System.Threading;
using System.Threading.Tasks;
using Cognizant.CodeChallenge.Application.Features.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Cognizant.CodeChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<Get.Response> GetAll(int? skip, int? take, CancellationToken cancellationToken)
        {
            return _mediator.Send(new Get.Query(skip, take), cancellationToken);
        }
    }
}
