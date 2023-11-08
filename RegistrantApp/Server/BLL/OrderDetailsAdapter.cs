using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Dto.Orders;
using RegistrantApp.Shared.PresentationLayer.Orders;

namespace RegistrantApp.Server.BLL;

public class OrderDetailsAdapter : BaseAdapter
{
    protected OrderDetailsAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<ViewOrderDetail> Get(long idOrder)
    {
        var found = await _ef
            .OrderDetails
            .FirstOrDefaultAsync(x => x.OrderDetailID == idOrder);

        return found.Adapt<ViewOrderDetail>();
    }

    public async Task<ViewOrderDetail> Update(dtoOrderDetailsUpdate dto)
    {
        var foundOrderDetail = await _ef.OrderDetails.FirstOrDefaultAsync(order =>
            order.OrderDetailID == dto.OrderDetailID
        );

        dto.Adapt(foundOrderDetail);

        foundOrderDetail!.StoreKeeperAccount = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == dto.IdAccountStoreKeeper);


        _ef.Update(foundOrderDetail);
        await _ef.SaveChangesAsync();

        return foundOrderDetail.Adapt<ViewOrderDetail>();
    }
}