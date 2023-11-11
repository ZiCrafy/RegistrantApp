using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Orders;
using RegistrantApp.Shared.PresentationLayer.Orders;

namespace RegistrantApp.Server.BLL;

public class OrdersAdapter : BaseAdapter
{
    public OrdersAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<ViewOrderPagination?> GetAsync(DateOnly startDate, DateOnly startEnd, int index,
        int recordsByPage, bool showDeleted, string search)
    {
        var totalRecords = _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Count(order => order.DateTimePlannedArrive >= new DateTime(startDate.Year, startDate.Month, startDate.Day)
                            &&
                            order.DateTimePlannedArrive <= new DateTime(startEnd.Year, startEnd.Month, startEnd.Day)
                                .AddDays(1).Date
                            &&
                            order.IsDeleted == showDeleted);

        var data = await _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Where(order =>
                (order.DateTimePlannedArrive >= new DateTime(startDate.Year, startDate.Month, startDate.Day)
                 &&
                 order.DateTimePlannedArrive <=
                 new DateTime(startEnd.Year, startEnd.Month, startEnd.Day).AddDays(1).Date
                 &&
                 order.IsDeleted == showDeleted)
                &&
                (order!.Account!.LastName!.ToUpper().Contains(search.ToUpper())
                 ||
                 order.Account.MiddleName.ToUpper().Contains(search.ToUpper())
                 ||
                 order.Account.FirstName.ToUpper().Contains(search.ToUpper())
                 ||
                 order!.Auto!.AutoNumber.ToUpper().Contains(search.ToUpper())
                )
            )
            .OrderBy(order => order.DateTimePlannedArrive)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToListAsync();

        var pagination = new ViewOrderPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            Orders = data.Adapt<List<ViewOrder>>()
        };

        return pagination;
    }

    public async Task<ViewOrderPagination?> GetAsync(DateOnly startDate, DateOnly startEnd, int index,
        int recordsByPage, bool showDeleted)
    {
        var totalRecords = _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Count(order =>
                order.DateTimePlannedArrive.Date >= new DateTime(startDate.Year, startDate.Month, startDate.Day).Date
                &&
                order.DateTimePlannedArrive.Date <=
                new DateTime(startEnd.Year, startEnd.Month, startEnd.Day).AddDays(1).Date
                &&
                order.IsDeleted == showDeleted);

        var data = await _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Where(order =>
                order.DateTimePlannedArrive.Date >= new DateTime(startDate.Year, startDate.Month, startDate.Day).Date
                &&
                order.DateTimePlannedArrive.Date <=
                new DateTime(startEnd.Year, startEnd.Month, startEnd.Day).AddDays(1).Date
                &&
                order.IsDeleted == showDeleted)
            .OrderBy(order => order.DateTimePlannedArrive)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToListAsync();

        var pagination = new ViewOrderPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            Orders = data.Adapt<List<ViewOrder>>()
        };

        return pagination;
    }

    public async Task<ViewOrder?> CreateAsync(dtoOrderCreate dto)
    {
        var order = new Order();
        dto.Adapt(order);

        order.Contragent =
            await _ef.Contragents
                .FirstOrDefaultAsync(contragent => contragent.ContragentID == dto.IdContragent);

        order.Auto = await _ef.Autos
            .FirstOrDefaultAsync(auto => auto.AutoID == dto.IdAuto);

        order.Account = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == dto.IdAccount);

        if (order.Contragent == null || order.Auto == null || order.Account == null)
            return null;

        order.OrderDetail = new OrderDetail();

        await _ef.AddAsync(order);
        await _ef.SaveChangesAsync();

        return order.Adapt<ViewOrder>();
    }

    public async Task<ViewOrder?> UpdateAsync(dtoOrderUpdate dto)
    {
        var foundOrder = await _ef.Orders
            .FirstOrDefaultAsync(order => order.OrderID == dto.OrderID);

        if (foundOrder is null)
            return null;

        dto.Adapt(foundOrder);

        foundOrder.Contragent =
            await _ef.Contragents
                .FirstOrDefaultAsync(contragent => contragent.ContragentID == dto.IdContragent);

        foundOrder.Auto = await _ef.Autos
            .FirstOrDefaultAsync(auto => auto.AutoID == dto.IdAuto);

        foundOrder.Account = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == dto.IdAccount);

        if (foundOrder.Contragent == null || foundOrder.Auto == null || foundOrder.Account == null)
            return null;

        _ef.Update(foundOrder);
        await _ef.SaveChangesAsync();

        return foundOrder.Adapt<ViewOrder>();
    }

    public async Task DeleteAsync(IEnumerable<long> idsOrders)
    {
        foreach (var item in idsOrders)
        {
            var found = await _ef.Orders
                .FirstOrDefaultAsync(order => order.OrderID == item);

            if (found == null)
                continue;

            found.IsDeleted = true;

            _ef.Update(found);
        }

        await _ef.SaveChangesAsync();
    }
}