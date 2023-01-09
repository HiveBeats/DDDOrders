using System;

namespace DDDOrders.Domain.Basic;
public class Discount
{
    private Discount() { }
    public Discount(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Discount can't be less than 0");
        }

        if (amount > 1)
        {
            throw new ArgumentException("Discount can't be more than 100%");
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
        else if (obj is Discount)
        {
            return ((Discount)obj).Amount == this.Amount;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return this.Amount.GetHashCode();
    }

    public static implicit operator Discount(decimal x)
    {
        return new Discount(x);
    }

    public static explicit operator decimal(Discount Discount)
    {
        return Discount.Amount;
    }

    public override string ToString()
    {
        return $"{Amount * 100}%";
    }
}
