using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMoney
{
    public static class Plus
    {
        public readonly static IExpression Identity = new PlusIdentity();

        private class PlusIdentity : IExpression
        {
            public IExpression Plus(IExpression addend)
            {
                return addend;
            }

            public Money Reduce(Bank bank, string to)
            {
                return new Money(0, to);
            }

            public IExpression Times(int multiplier)
            {
                return this;
            }
        }
    }
}
