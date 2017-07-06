using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMoney
{
    public static class Expression
    {
        public static IExpression Times(this IExpression exp, int multiplier)
        {
            return Enumerable
                .Repeat(exp, multiplier)
                .Aggregate((x, y) => x.Plus(y));
        }
    }
}
