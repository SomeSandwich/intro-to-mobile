using Api.Context;
using Api.Context.Constants.Enums;
using Api.Context.Entities;
using API.Types.Mapping;
using API.Types.Objects;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public OrderService(MobileDbContext context)
    {
        _context = context;

        var config = new MapperConfiguration(opt => { opt.AddProfile<OrderProfile>(); });
        _mapper = config.CreateMapper();
    }

    public async Task<int> AddAsync(CreateOrderReq args)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var order = _mapper.Map<CreateOrderReq, Order>(args);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var listItems = args.Details;

            var listDetail = _mapper.Map<ICollection<Item>, ICollection<OrderDetail>>(listItems);

            if (listDetail is not null)
            {
                foreach (var detail in listDetail)
                {
                    detail.OrderId = order.Id;
                }

                await _context.OrderDetails.AddRangeAsync(listDetail);
                await _context.SaveChangesAsync();
            }

            transaction.Commit();

            return order.Id;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            Console.WriteLine(e);
            throw;
        }
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
            .FromSql(
                $"select sum(\"UnitPrice\") as totalPrice from \"OrderDetails\" where \"OrderId\" = {id} group by \"OrderId\"");

        // order.Total = total.totalPrice;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id) => throw new NotImplementedException();
}
