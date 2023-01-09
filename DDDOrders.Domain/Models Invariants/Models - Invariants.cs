using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDOrders.Domain.ModelsInvariants;

public class Product
{
    public int Id { get; set; }
    public int ProductType { get; set; }
}

public struct Address //value object
{
    public string AddressCity { get; set; }
    public string AddressStreet { get; set; }
}

public class OrderLine
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class Coupon
{
    private IList<Order> _orders;
    public string Code { get; private set; }
    public decimal Discount { get; private set; }
    public int ProductTypeId { get; private set; }
    public IReadOnlyCollection<Order> Orders { get => (IReadOnlyCollection<Order>)_orders; } //todo: Order Id (as we ensure that aggregates are referenced by Id only).
}

public class Order
{
    private IList<Coupon> _coupons;
    private IList<OrderLine> _orderLines;
    public int Id { get; private set; }
    public Address Address { get; private set; }
    public decimal Total => GetOrderTotal();
    public IReadOnlyCollection<Coupon> Coupons { get => (IReadOnlyCollection<Coupon>)_coupons; }
    public IReadOnlyCollection<OrderLine> OrderLines { get => (IReadOnlyCollection<OrderLine>)_orderLines; }

    public void AddCoupon(Coupon coupon)
    {
        if (_coupons.Any(x => x.ProductTypeId == coupon.ProductTypeId))
        {
            throw new Exception("Can't add coupon for same product type twice");
        }

        _coupons.Add(coupon);
    }

    private decimal GetOrderTotal()
    {
        var orderLinesTotal = OrderLines.Sum(x => x.Price);
        var total = orderLinesTotal - (orderLinesTotal * Coupons.Sum(x => x.Discount));
        //add that rule to part 1 article 
        if (total < 0)
        {
            throw new Exception("Total can't be less than 0");
        }

        return total;
    }
}

