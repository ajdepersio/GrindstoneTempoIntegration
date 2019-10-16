using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Grindstone
{
    public class TimeEntry
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string GrindstoneId { get; set; }
        public string WorkItemId { get; set; }

        public TimeEntry(R r)
        {
            this.Start = r.s;
            this.End = r.e;
            this.Description = r.n;
            this.GrindstoneId = r.i;
            this.WorkItemId = r.t;
        }
    }
}
