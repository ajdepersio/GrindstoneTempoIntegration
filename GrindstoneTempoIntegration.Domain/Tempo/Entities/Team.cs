using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo.Entities
{
    public class Team
    {
        public string self { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public Lead lead { get; set; }
        public object program { get; set; }
        public Links links { get; set; }
        public Members members { get; set; }
        public Permissions permissions { get; set; }
    }
}
