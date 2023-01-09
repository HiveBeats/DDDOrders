namespace SimpleOrders.Models;

public class Models
{
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
    public string Code { get; set; }
    public decimal Discount { get; set; }
    public int ProductTypeId { get; set; }
}

public class OrderCoupon
{
    public int OrderId { get; set; }
    public int CouponId { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string AddressCity { get; set; }
    public string AddressStreet { get; set; }
    public decimal Total { get; set; }
}