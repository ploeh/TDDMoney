using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMoney
{
    public static class Compare
    {
        public static ExpressionEqualityComparer UsingBank =
            new ExpressionEqualityComparer();
    }
}
