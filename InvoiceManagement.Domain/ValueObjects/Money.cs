namespace InvoiceManagement.Domain.ValueObjects;

public class Money
{
    public decimal Amount { get; private set; }

    public Money(decimal amount)
    {
        Amount = amount;
    }

    // Overload operators for money calculations
    public static Money operator +(Money a, Money b) => new Money(a.Amount + b.Amount);
    public static Money operator -(Money a, Money b) => new Money(a.Amount - b.Amount);
    public static Money operator *(Money a, int multiplier) => new Money(a.Amount * multiplier);
    public static bool operator ==(Money a, Money b) => a.Amount == b.Amount;
    public static bool operator !=(Money a, Money b) => a.Amount != b.Amount;
    public override bool Equals(object obj) => obj is Money money && Amount == money.Amount;
    public override int GetHashCode() => Amount.GetHashCode();
}
