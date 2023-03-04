using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.DataAccess;
using Services.Model;
using Services.Queries.GetRequests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practice.Controllers
{
    public class SearchController : BaseController
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<ExtractSearchModel>> GetSearch(
            [FromQuery] int wait, 
            [FromQuery] int randomMin,
            [FromQuery] int randomMax)
        {
            var result  = await _mediator.Send(new GetExtractSearchQuery(wait, randomMin, randomMax));
            return result;
        }
    }
}
