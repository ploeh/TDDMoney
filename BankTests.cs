using Xunit;

namespace TDDMoney
{
    public class BankTests
    {
        [Fact]
        public void IdentityRateShouldBe1()
        {
            Assert.Equal(1, new Bank().Rate("USD", "USD"));
        }
    }
}
