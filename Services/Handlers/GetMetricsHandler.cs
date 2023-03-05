using DataContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.DataAccess;
using Services.Queries.GetRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Handlers
{
    public class GetMetricsHandler : IRequestHandler<GetMetricsQuery, IEnumerable<MetricsModel>>
    {
        private MyDbContext _context;

        public GetMetricsHandler(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MetricsModel>> Handle(GetMetricsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Metrics.GroupBy(x => x.SystemName, t => t.ExecutionTime).Select(x => 
            new MetricsModel ()
            { 
                SystemName = x.Key,
                CountRequest = x.Count()
            }).ToListAsync();

        }
    }
}
