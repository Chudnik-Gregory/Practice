using MediatR;
using Services.DataAccess;
using Services.Model;
using Services.Queries.GetRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Handlers
{
    public class GetExtractSearchHandler : IRequestHandler<GetExtractSearchQuery, IEnumerable<ExtractSearchModel>>
    {
        private readonly ISearchDataAccess _searchDataAccess;

        public GetExtractSearchHandler(ISearchDataAccess searchDataAccess)
        {
            _searchDataAccess = searchDataAccess;
        }

        public async Task<IEnumerable<ExtractSearchModel>> Handle(GetExtractSearchQuery request, CancellationToken cancellationToken)
        {
            await _searchDataAccess.SetExtractSearches(request.Wait, request.Min, request.Max);
            return await _searchDataAccess.GetExtractSearches();
        }
    }
}
