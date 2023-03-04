using MediatR;
using Services.DataAccess;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries.GetRequests
{
    public class GetExtractSearchQuery : IRequest<IEnumerable<ExtractSearchModel>>
    {
        public GetExtractSearchQuery(int wait, int min, int max)
        {
            Wait = wait;
            Min = min;
            Max = max;
        }

        public int Wait { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
