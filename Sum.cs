using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMoney
{
    public class Sum : IExpression
    {
        public Sum(IExpression augend, IExpression addend)
        {
            Augend = augend;
            Addend = addend;
        }

        public Money Reduce(Bank bank, string to)
        {
            int amount = Augend.Reduce(bank, to).Amount + Addend.Reduce(bank, to).Amount;
            return new Money(amount, to);
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public IExpression Times(int multiplier)
        {
            return new Sum(Augend.Times(multiplier), Addend.Times(multiplier));
        }

        public IExpression Augend { get; set; }
        public IExpression Addend { get; set; }
    }
}
