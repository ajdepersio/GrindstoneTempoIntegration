using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Grindstone
{
    public class WorkItem
    {
        public string Name { get; set; }
        public string TicketId { get => this.Name.Split(' ')[0]; }
        public string GrindstoneId { get; set; }

        public WorkItem(T t)
        {
            this.Name = t.n;
            this.GrindstoneId = t.i;
        }
    }
}
