namespace TDDMoney
{
    public class Money : IExpression
    {
        public Money(int amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public override string ToString()
        {
            return Currency + " " + Amount;
        }

        public override bool Equals(object o)
        {
            Money money = (Money)o;

            return Amount == money.Amount &&
              Currency == money.Currency;
        }

        public override int GetHashCode()
        {
            return
                Amount.GetHashCode() ^
                Currency.GetHashCode();
        }

        public IExpression Times(int multiplier)
        {
            return new Money(Amount * multiplier, Currency);
        }

        public static Money Dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public IExpression Plus(IExpression addend)
        {
            return new Sum(this, addend);
        }

        public Money Reduce(Bank bank, string to)
        {
            int rate = bank.Rate(Currency, to);
            return new Money(Amount / rate, to);
        }

        public int Amount { get; }
        public string Currency { get; }
    }
}
