using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cognizant.CodeChallenge.Application.Features.Participants;

namespace Cognizant.CodeChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParticipantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<Get.Response> GetAll(int? top, CancellationToken cancellationToken)
        {
            return _mediator.Send(new Get.Query(top), cancellationToken);
        }
    }
}
