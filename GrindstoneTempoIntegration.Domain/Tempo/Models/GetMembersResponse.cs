using GrindstoneTempoIntegration.Domain.Tempo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo.Models
{
    public class GetMembersResponse
    {
        public string self { get; set; }
        public Metadata metadata { get; set; }
        public List<MemberResult> results { get; set; }
    }
}
