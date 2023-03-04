using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Model
{
    public class ExtractSearchModel
    {
        public ExtractSearchModel(ExtractSearchBase search, Status status)
        {
            Name = search.Name;
            WorkTime = search.WorkTime;
            Status = status;
            StatusName = status.GetStatusName();
        }

        public string Name { get; set; }
        public Status Status { get; set; }
        public string StatusName { get; set; }
        public TimeSpan WorkTime { get; set; }
    }
}
