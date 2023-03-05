using MediatR;
using Models;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Queries.GetRequests
{
    public class GetMetricsQuery : IRequest<IEnumerable<MetricsModel>>
    {
        public int ID { get; set; }
        /// <summary>
        /// Название системы
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// Время выполнения
        /// </summary>
        public TimeSpan? ExecutionTime { get; set; }
        public int? CountRequest { get; set; }
    }
}
