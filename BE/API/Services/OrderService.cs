using Api.Context;
using Api.Context.Constants.Enums;
using Api.Context.Entities;
using API.Types.Mapping;
using API.Types.Objects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IOrderService
{
    Task<int?> AddAsync(CreateOrderReq args);

    Task<Order?> GetAsync(int userId, int orderId);
    Task<Order?> GetByPostId(int postId);
    Task<IEnumerable<Order>> GetByCustomerId(int customerId);
    Task<IEnumerable<Order>> GetBySellerId(int sellerId);

    Task<bool> UpdateAddressAsync(int id, UpdateOrderAddressReq args);
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

    public async Task<int?> AddAsync(CreateOrderReq args)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var order = _mapper.Map<CreateOrderReq, Order>(args);

            var firstDetail = args.Details.FirstOrDefault()?.PostId;
            var sellerId = await _context.Posts.Where(e => e.Id == firstDetail).Select(e => e.UserId)
                .FirstOrDefaultAsync();

            order.SellerId = sellerId;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var listDetail = _mapper.Map<ICollection<OrderItemReq>, List<OrderDetail>>(args.Details, opt =>
            {
                opt.AfterMap((src, des) =>
                {
                    foreach (var desDetail in des)
                    {
                        desDetail.OrderId = order.Id;
                    }
                });
            });

            if (listDetail is not null)
            {
                await _context.OrderDetails.AddRangeAsync(listDetail);
                await _context.SaveChangesAsync();
            }

            foreach (var detail in listDetail)
            {
                var post = await _context.Posts.FirstOrDefaultAsync(e => e.Id == detail.PostId);

                if (post is null)
                {
                    continue;
                }

                post.IsSold = true;
                post.UpdatedDate = DateTime.Now.ToUniversalTime();

                await _context.SaveChangesAsync();
            }

            var money = listDetail.Sum(e => e.UnitPrice);
            var seller = await _context.Users.FirstOrDefaultAsync(e => e.Id == sellerId);
            var customer = await _context.Users.FirstOrDefaultAsync(e => e.Id == args.CustomerId);

            seller.Money += money;
            customer.Money -= money;

            Console.WriteLine(order.Id);

            await transaction.CommitAsync();

            return order.Id;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Order?> GetAsync(int userId, int orderId)
    {
        var order = _context.Orders
            .Include(e => e.OrderDetail)
            .FirstOrDefault(e => e.Id == orderId);

        if (order is null)
            return null;

        if (order.CustomerId != userId && order.SellerId != userId)
            throw new InvalidOperationException("Không đủ quyền truy cập");

        return order;
    }

    public async Task<Order?> GetByPostId(int postId)
    {
        var od = await _context.OrderDetails
            .Where(e => e.PostId == postId)
            .FirstOrDefaultAsync();

        if (od is null)
            return null;

        return await _context.Orders
            .Include(e => e.OrderDetail)
            .Where(e => e.Id == od.OrderId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetByCustomerId(int customerId)
    {
        var listOrder = _context.Orders.Where(e => e.CustomerId == customerId)
            .AsEnumerable();

        return listOrder;
    }

    public async Task<IEnumerable<Order>> GetBySellerId(int sellerId)
    {
        var listOrder = _context.Orders.Where(e => e.SellerId == sellerId).AsEnumerable();

        return listOrder;
    }

    public async Task<bool> UpdateAddressAsync(int id, UpdateOrderAddressReq args)
    {
        var order = _context.Orders.FirstOrDefault(e => e.Id == id);

        if (order is null)
            return false;

        if (order.CustomerId != args.CustomerId)
            throw new InvalidOperationException("Không đủ quyền truy cập");

        order.DeliveryAddress = args.DeliveryAddress;

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
