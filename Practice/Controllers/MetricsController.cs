using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Queries.GetRequests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice.Controllers
{
    public class MetricsController : BaseController
    {
        private readonly IMediator _mediator;

        public MetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<MetricsModel>> GetMetrics()
        {
            return await _mediator.Send(new GetMetricsQuery());
        }
    }
}
