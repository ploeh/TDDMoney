using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDDMoney
{
    public class ExpressionExamples
    {
        [Fact]
        public void AssociativityExample()
        {
            var x = Money.Dollar(1).Plus(Money.Franc(3)).Plus(Money.Dollar(5));
            var y = Money.Dollar(1).Plus(Money.Franc(3).Plus(Money.Dollar(5)));

            Assert.Equal(x, y, Compare.UsingBank);
        }

        [Fact]
        public void IdentityExample()
        {
            var leftIdentity = Plus.Identity.Plus(Money.Franc(42));
            var rightIdentity = Money.Dollar(1337).Plus(Plus.Identity);

            Assert.Equal(Money.Franc(42), leftIdentity, Compare.UsingBank);
            Assert.Equal(Money.Dollar(1337), rightIdentity, Compare.UsingBank);
        }
    }
}
