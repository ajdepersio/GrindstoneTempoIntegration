using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo.Entities
{
    public class Active
    {
        public string self { get; set; }
        public int id { get; set; }
        public int commitmentPercent { get; set; }
        public object from { get; set; }
        public object to { get; set; }
        public Role role { get; set; }
    }
}
