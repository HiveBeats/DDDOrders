using DDDOrders.Domain.Basic;
using DDDOrders.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDOrders.Domain.Models_ValueObjects;
public class Product
{
    public int Id { get; set; }
    public int ProductTypeId { get; set; }
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
    public int ProductTypeId { get; set; }
    public Money Price { get; set; }
    public Quantity Quantity { get; set; }
}

public class Coupon
{
    private IList<Order> _orders;
    public string Code { get; private set; }
    public Discount Discount { get; private set; }
    public int ProductTypeId { get; private set; }
    public IReadOnlyCollection<Order> Orders { get => (IReadOnlyCollection<Order>)_orders; } //todo: Order Id (as we ensure that aggregates are referenced by Id only).
}

public class Order
{
    private DistinctByCollection<Coupon, int> _coupons = new DistinctByCollection<Coupon, int>((c) => c.ProductTypeId);
    private IList<OrderLine> _orderLines;
    public int Id { get; private set; }
    public Address Address { get; private set; }
    public Money Total => GetOrderTotal();
    public IReadOnlyCollection<Coupon> Coupons { get => (IReadOnlyCollection<Coupon>)_coupons; }
    public IReadOnlyCollection<OrderLine> OrderLines { get => (IReadOnlyCollection<OrderLine>)_orderLines; }

    public void AddCoupon(Coupon coupon)
    {
        try
        {
            _coupons.Add(coupon);
        }
        catch (ArgumentException)
        {
            throw new Exception("Can't add coupon for same product type twice");
        }
        catch (Exception) { throw; }
    }

    private Money GetOrderTotal()
    {
        Money orderLinesTotal = OrderLines.Sum(x => (decimal)x.Price);
        var discountedOrderLines = _orderLines.ToListDictionary((ol) => ol.ProductTypeId);

        Money total = 0;

        foreach (var category in discountedOrderLines)
        {
            Money categorySum = category.Value.Sum(x => (decimal)x.Price);
            var categoryCoupon = Coupons.FirstOrDefault(x => x.ProductTypeId == category.Key);
            if (categoryCoupon != null)
            {
                total += categorySum - (categorySum * (decimal)categoryCoupon.Discount);
            }
            else
            {
                total += categorySum;
            }
        }

        return total;
    }
}


