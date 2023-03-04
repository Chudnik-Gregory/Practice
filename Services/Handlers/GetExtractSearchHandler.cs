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
        private readonly ISearchDataAccess searchDataAccess;

        public GetExtractSearchHandler(ISearchDataAccess searchDataAccess)
        {
            this.searchDataAccess = searchDataAccess;
        }

        public Task<IEnumerable<ExtractSearchModel>> Handle(GetExtractSearchQuery request, CancellationToken cancellationToken)
        {
            searchDataAccess.SetExtractSearches(request.Wait, request.Min, request.Max);
            //InvalidOperationException: Collection was modified; enumeration operation may not execute.
            //Если метод GetExtractSearches возвращает List
            return Task.FromResult(searchDataAccess.GetExtractSearches());
        }
    }
}
