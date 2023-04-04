using Api.Context;
using Api.Context.Constants.Enums;
using Api.Context.Entities;
using API.Types.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Services;

public interface IOrderService
{
    Task<int> AddAsync(CreateOrderReq args);

    Task<Order?> GetAsync(int id);
    Task<IEnumerable<Order>> GetByCustomerId(int customerId);
    Task<IEnumerable<Order>> GetBySellerId(int sellerId);

    Task<bool> UpdateAddressAsync(int id, string addr);
    Task<bool> UpdateStatusAsync(int id, OrderStatus status);
    Task<bool> UpdateTotalAsync(int id);

    Task<bool> DeleteAsync(int id);
}

public class OrderService : IOrderService
{
    private readonly MobileDbContext _context;

    public OrderService(MobileDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(CreateOrderReq args)
    {
        var checkUser = _context.Users.Any(e=>e.)
        await _context.Orders.AddAsync(args);
        await _context.SaveChangesAsync();

        var listDetail = args.OrderDetail;
        foreach (var detail in listDetail)
        {
            detail.OrderId = args.Id;
        }

        await _context.OrderDetails.AddRangeAsync(listDetail);
        await _context.SaveChangesAsync();

        return args.Id;
    }

    public async Task<Order?> GetAsync(int id)
    {
        var order = _context.Orders
            .Include(e => e.OrderDetail)
            .FirstOrDefault(e => e.Id == id);

        if (order is null)
            return null;

        return order;
    }

    public async Task<IEnumerable<Order>> GetByCustomerId(int customerId)
    {
        var listOrder = _context.Orders.Where(e => e.CustomerId == customerId).AsEnumerable();

        return listOrder;
    }

    public async Task<IEnumerable<Order>> GetBySellerId(int sellerId)
    {
        var listOrder = _context.Orders.Where(e => e.SellerId == sellerId).AsEnumerable();

        return listOrder;
    }

    public async Task<bool> UpdateAddressAsync(int id, string addr)
    {
        var order = _context.Orders.FirstOrDefault(e => e.Id == id);

        if (order is null)
            return false;

        order.DeliveryAddress = addr;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateStatusAsync(int id, OrderStatus status)
    {
        var order = _context.Orders.FirstOrDefault(e => e.Id == id);

        if (order is null)
            return false;

        order.Status = status;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateTotalAsync(int id)
    {
        var order = _context.Orders
            .Include(e => e.OrderDetail)
            .FirstOrDefault(e => e.Id == id);

        if (order is null)
            return false;

        var listDetail = order.OrderDetail
            .GroupBy(e => new { OrderId = e.OrderId })
            .Select(e => new { OrderId = e.Key.OrderId, TotolPrice = e.Select(a => a.UnitPrice) })
            .FirstOrDefault(e => e.OrderId == id);

        var total = _context.OrderDetails
            .FromSql($"select sum(\"UnitPrice\") as totalPrice from \"OrderDetails\" where \"OrderId\" = {id} group by \"OrderId\"");

        // order.Total = total.totalPrice;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
}
