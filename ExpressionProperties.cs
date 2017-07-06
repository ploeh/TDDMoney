using FsCheck;
using FsCheck.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDDMoney
{
    public class ExpressionProperties
    {
        [Property(QuietOnSuccess = true)]
        public Property PlusIsAssociative()
        {
            return Prop.ForAll(
                GenerateExpression().Three().ToArbitrary(),
                t => PlusIsAssociative(t.Item1, t.Item2, t.Item3));
        }

        private void PlusIsAssociative(
            IExpression x,
            IExpression y,
            IExpression z)
        {
            Assert.Equal(
                x.Plus(y).Plus(z),
                x.Plus(y.Plus(z)),
                new ExpressionEqualityComparer());
        }

        [Property(QuietOnSuccess = true)]
        public Property PlusHasIdentity()
        {
            return Prop.ForAll(
                GenerateExpression().ToArbitrary(),
                x => PlusHasIdentity(x));
        }

        private void PlusHasIdentity(IExpression x)
        {
            var comparer = new ExpressionEqualityComparer();
            Assert.Equal(x, x.Plus(Plus.Identity), comparer);
            Assert.Equal(x, Plus.Identity.Plus(x), comparer);
        }

        private class ExpressionEqualityComparer : IEqualityComparer<IExpression>
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

        private static Gen<Money> GenerateMoney()
        {
            return from amount in Arb.Generate<int>()
                   from factory in Gen.Elements<Func<int, Money>>(Money.Dollar, Money.Franc)
                   select factory(amount);
        }

        private static Gen<IExpression> GenerateExpression(int size)
        {
            if (size <= 0)
                return GenerateMoney().Select(x => (IExpression)x);

            var subTree = GenerateExpression(size / 2);
            return Gen.OneOf(
                GenerateMoney().Select(x => (IExpression)x),
                subTree.Two().Select(t => (IExpression)new Sum(t.Item1, t.Item2)));
        }

        private static Gen<IExpression> GenerateExpression()
        {
            return Gen.Sized(GenerateExpression);
        }
    }
}
