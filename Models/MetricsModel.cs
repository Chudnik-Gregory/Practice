using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class MetricsModel
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
