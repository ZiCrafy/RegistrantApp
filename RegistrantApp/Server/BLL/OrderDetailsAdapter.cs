using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Orders;
using RegistrantApp.Shared.PresentationLayer.Orders;

namespace RegistrantApp.Server.BLL;

public class OrderDetailsAdapter : BaseAdapter
{
    public OrderDetailsAdapter(RaContext ef) : base(ef)
    {
    }

    public async Task<ViewOrderDetail?> GetAsync(long idOrder)
    {
        var found = await _ef
            .OrderDetails
            .FirstOrDefaultAsync(x => x.OrderDetailID == idOrder);

        return found?.Adapt<ViewOrderDetail>();
    }

    public async Task<ViewOrderDetail?> UpdateAsync(Token session, dtoOrderDetailsUpdate dto)
    {
        var foundOrderDetail = await _ef.OrderDetails.FirstOrDefaultAsync(order =>
            order.OrderDetailID == dto.OrderDetailID
        );

        if (foundOrderDetail is null)
            return null;

        dto.Adapt(foundOrderDetail);

        foundOrderDetail.StoreKeeperAccount = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == dto.IdAccountStoreKeeper);

        if (foundOrderDetail.StoreKeeperAccount is null)
            return null;

        _ef.Update(foundOrderDetail);
        await _ef.AuditChanges(session.OwnerToken);

        return foundOrderDetail.Adapt<ViewOrderDetail>();
    }
}