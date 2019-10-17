using System;
using System.Collections.Generic;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo.Entities
{
    public class MemberResult
    {
        public string self { get; set; }
        public Team team { get; set; }
        public Member member { get; set; }
        public Memberships memberships { get; set; }
    }
}
