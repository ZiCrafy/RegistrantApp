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
    protected OrdersAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<ViewOrderPagination> Get(DateOnly startDate, DateOnly startEnd, int index,
        int recordsByPage, bool showDeleted, string search)
    {
        var totalRecords = _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Count(order => order.DateTimePlannedArrive >= new DateTime(startDate.Year, startDate.Month, startDate.Day)
                            &&
                            order.DateTimePlannedArrive <= new DateTime(startEnd.Year, startEnd.Month, startEnd.Day)
                            &&
                            order.IsDeleted == showDeleted);

        var data = _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Where(order =>
                (order.DateTimePlannedArrive >= new DateTime(startDate.Year, startDate.Month, startDate.Day)
                 &&
                 order.DateTimePlannedArrive <= new DateTime(startEnd.Year, startEnd.Month, startEnd.Day)
                 &&
                 order.IsDeleted == showDeleted)
                &&
                (order.Account.LastName.ToUpper().Contains(search.ToUpper())
                 ||
                 order.Account.MiddleName.ToUpper().Contains(search.ToUpper())
                 ||
                 order.Account.FirstName.ToUpper().Contains(search.ToUpper())
                 ||
                 order.Auto.AutoNumber.ToUpper().Contains(search.ToUpper())
                )
            )
            .OrderBy(order => order.DateTimePlannedArrive)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToList()
            .Adapt<List<ViewOrder>>();

        var pagionation = new ViewOrderPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            Orders = data
        };

        return pagionation;
    }

    public async Task<ViewOrderPagination> Get(DateOnly startDate, DateOnly startEnd, int index,
        int recordsByPage, bool showDeleted)
    {
        var totalRecords = _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Count(order => order.DateTimePlannedArrive >= new DateTime(startDate.Year, startDate.Month, startDate.Day)
                            &&
                            order.DateTimePlannedArrive <= new DateTime(startEnd.Year, startEnd.Month, startEnd.Day)
                            &&
                            order.IsDeleted == showDeleted);

        var data = _ef.Orders
            .Include(x => x.Auto)
            .Include(x => x.Account)
            .Where(order =>
                order.DateTimePlannedArrive >= new DateTime(startDate.Year, startDate.Month, startDate.Day)
                &&
                order.DateTimePlannedArrive <= new DateTime(startEnd.Year, startEnd.Month, startEnd.Day)
                &&
                order.IsDeleted == showDeleted)
            .OrderBy(order => order.DateTimePlannedArrive)
            .Skip(index * recordsByPage)
            .Take(recordsByPage)
            .ToList()
            .Adapt<List<ViewOrder>>();


        var pagionation = new ViewOrderPagination()
        {
            TotalRecords = totalRecords,
            TotalPages = totalRecords / recordsByPage,
            Orders = data
        };

        return pagionation;
    }

    public async Task<ViewOrder> Create(dtoOrderCreate dto)
    {
        var order = new Order();
        dto.Adapt(order);

        order.Contragent =
            await _ef.Contragents.FirstOrDefaultAsync(contrant => contrant.ContragentID == dto.IdContragent);
        order.Auto = await _ef.Autos.FirstOrDefaultAsync(auto => auto.AutoID == dto.IdAuto);
        order.Account = await _ef.Accounts.FirstOrDefaultAsync(account => account.AccountID == dto.IdAccount);
        order.OrderDetail = new OrderDetail();

        await _ef.AddAsync(order);
        await _ef.SaveChangesAsync();

        return order.Adapt<ViewOrder>();
    }

    public async Task<ViewOrder> Update(dtoOrderUpdate dto)
    {

        var foundOrder = await _ef.Orders.FirstOrDefaultAsync(order => order.OrderID == dto.OrderID);
        if (foundOrder == null)
            return null;

        dto.Adapt(foundOrder);
        
        foundOrder.Contragent =
            await _ef.Contragents.FirstOrDefaultAsync(contrant => contrant.ContragentID == dto.IdContragent);
        foundOrder.Auto = await _ef.Autos.FirstOrDefaultAsync(auto => auto.AutoID == dto.IdAuto);
        foundOrder.Account = await _ef.Accounts.FirstOrDefaultAsync(account => account.AccountID == dto.IdAccount);

        _ef.Update(foundOrder);
        await _ef.SaveChangesAsync();
        
        return foundOrder.Adapt<ViewOrder>();
    }

    public async Task Delete(long[] idsOrders)
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