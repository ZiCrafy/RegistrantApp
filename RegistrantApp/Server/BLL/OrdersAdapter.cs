using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
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
}