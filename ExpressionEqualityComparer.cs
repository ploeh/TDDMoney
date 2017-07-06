using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMoney
{
    public class ExpressionEqualityComparer : IEqualityComparer<IExpression>
    {
        private readonly Bank bank;

        public ExpressionEqualityComparer()
        {
            bank = new Bank();
            bank.AddRate("CHF", "USD", 2);
        }

        public bool Equals(IExpression x, IExpression y)
        {
            var xm = bank.Reduce(x, "USD");
            var ym = bank.Reduce(y, "USD");
            return object.Equals(xm, ym);
        }

        public int GetHashCode(IExpression obj)
        {
            return bank.Reduce(obj, "USD").GetHashCode();
        }
    }
}
