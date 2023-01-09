using DDDOrders.Domain.ModelsInvariants;
using System;
using System.Data;

namespace DDDOrders.Services.FakePersistence;

public class OrderService
{
    public Coupon GetCoupon(string code)
    {
        throw new NotImplementedException();
    }

    public Order GetOrderAggregate(int orderId)
    {
        throw new NotImplementedException();
    }

    public IDbTransaction OpenTransaction()
    {
        throw new NotImplementedException();
    }

    public void SaveOrderAggregate(Order order) { }

    public bool AddCoupon(int orderId, string code)
    {
        using var tx = OpenTransaction();
        try
        {
            //ignore implementation for now, as persistence detail
            var order = GetOrderAggregate(orderId);

            var coupon = GetCoupon(code);

            order.AddCoupon(coupon);

            //ignore implementation for now, as persistence detail
            SaveOrderAggregate(order);

            tx.Commit();
        }
        catch
        {
            tx.Rollback();
            return false;
        }
        return true;
    }
}
