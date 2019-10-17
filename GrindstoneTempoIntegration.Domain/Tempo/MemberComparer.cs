using GrindstoneTempoIntegration.Domain.Tempo.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GrindstoneTempoIntegration.Domain.Tempo
{
    public class MemberComparer : IEqualityComparer<Member>
    {
        public bool Equals([AllowNull] Member x, [AllowNull] Member y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else if (x.accountId == y.accountId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode([DisallowNull] Member obj)
        {
            return obj.accountId.GetHashCode();
        }
    }
}
