using System;

namespace DDDOrders.Domain.Basic;
public class Money
{
    private Money() { }
    public Money(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Money can't be less than 0");
        }

        Amount = amount;
    }

    public decimal Amount { get; private set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        else if (obj is Money)
        {
            return ((Money)obj).Amount == this.Amount;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return this.Amount.GetHashCode();
    }

    public static implicit operator Money(decimal x)
    {
        return new Money(x);
    }

    public static explicit operator decimal(Money money)
    {
        return money.Amount;
    }

    public static Money operator +(Money a) => a.Amount + 1;
    public static Money operator -(Money a) => a.Amount - 1;

    public static Money operator +(Money a, Money b) => a.Amount + b.Amount;

    public static Money operator -(Money a, Money b) => a.Amount - b.Amount;

    public static Money operator *(Money a, Money b) => a.Amount * b.Amount;

    public static Money operator /(Money a, Money b)
    {
        if (b.Amount == 0)
        {
            throw new DivideByZeroException();
        }
        return a.Amount / b.Amount;
    }
}
