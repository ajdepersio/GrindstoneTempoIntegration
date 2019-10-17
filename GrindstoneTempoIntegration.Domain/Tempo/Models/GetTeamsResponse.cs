using GrindstoneTempoIntegration.Domain.Tempo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo.Models
{
    public class GetTeamsResponse
    {
        public string self { get; set; }
        public Metadata metadata { get; set; }
        public List<Team> results { get; set; }
    }
}
