using System;

namespace DDDOrders.Domain.Basic;
public class Quantity
{
    private Quantity() { }
    public Quantity(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Quantity can't be less than 0");
        }

        Amount = amount;
    }

    public int Amount { get; private set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        else if (obj is Quantity)
        {
            return ((Quantity)obj).Amount == this.Amount;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return this.Amount.GetHashCode();
    }

    public static implicit operator Quantity(int x)
    {
        return new Quantity(x);
    }

    public static explicit operator int(Quantity money)
    {
        return money.Amount;
    }
}
