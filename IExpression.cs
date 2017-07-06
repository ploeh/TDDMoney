namespace TDDMoney
{
    public interface IExpression
    {
        Money Reduce(Bank bank, string to);
        IExpression Plus(IExpression addend);
    }
}
