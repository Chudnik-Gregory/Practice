using DataContext;
using MediatR;
using Models;
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
        private MyDbContext _context;

        public GetExtractSearchHandler(
            ISearchDataAccess searchDataAccess,
            MyDbContext myDbContext)
        {
            _searchDataAccess = searchDataAccess;
            _context = myDbContext;

        }

        public async Task<IEnumerable<ExtractSearchModel>> Handle(GetExtractSearchQuery request, CancellationToken cancellationToken)
        {
            await _searchDataAccess.SetExtractSearches(request.Wait, request.Min, request.Max);
            await SaveMetrics();
            return await _searchDataAccess.GetExtractSearches();
        }

        private async Task SaveMetrics()
        {
            var metrics = await _searchDataAccess.GetExtractSearches();
            foreach (var metric in metrics)
            {
                _context.Metrics.Add(new MetricsModel()
                {
                    SystemName = metric.Name,
                    ExecutionTime = metric.WorkTime,
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
