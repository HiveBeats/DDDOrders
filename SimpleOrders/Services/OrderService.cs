using SimpleOrders.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SimpleOrders.Services;

public class OrderService
{
    public Coupon GetCoupon(string code)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderLine> GetOrderLines(int orderId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Coupon> GetOrderCoupons(int orderId)
    {
        throw new NotImplementedException();
    }

    public IDbTransaction OpenTransaction()
    {
        throw new NotImplementedException();
    }

    public Order GetOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public void InsertOrderCoupon(OrderCoupon coupon)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrder(Order order)
    {
        throw new Exception();
    }


    public bool AddCoupon(int orderId, string code)
    {
        var coupon = GetCoupon(code);
        if (coupon == null)
        {
            return false;
        }

        var orderLines = GetOrderLines(orderId);
        var orderCoupons = GetOrderCoupons(orderId);

        // # rule: one coupon per product type in the order
        if (orderCoupons.Any(x => x.ProductTypeId == coupon.ProductTypeId))
        {
            return false;
        }

        using (var tx = OpenTransaction())
        {
            try
            {
                var order = GetOrder(orderId);

                InsertOrderCoupon(new OrderCoupon
                {
                    OrderId = orderId
                });

                // # rule: order total is equal to order lines total - discount
                order.Total = order.Total - (order.Total * coupon.Discount);

                UpdateOrder(order);

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                return false;
            }
        }

        return true;
    }
}
