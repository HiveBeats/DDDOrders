using System.Collections.Generic;

namespace DDDOrders.Domain.Models;

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
    public Order Order { get; set; }//todo: Order Id (as we ensure that aggregates are referenced by Id only).
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class Coupon
{
    public string Code { get; set; }
    public decimal Discount { get; set; }
    public int ProductTypeId { get; set; }
    public IEnumerable<Order> Orders { get; set; } //todo: Order Id (as we ensure that aggregates are referenced by Id only).
}

public class Order
{
    public int Id { get; set; }
    public Address Address { get; set; }
    public decimal Total { get; set; }
    public IEnumerable<Coupon> Coupons { get; set; }
    public IEnumerable<OrderLine> OrderLines { get; set; }
}
