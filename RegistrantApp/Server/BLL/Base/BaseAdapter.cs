using System.Globalization;
using System.Reflection;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;

namespace RegistrantApp.Server.BLL.Base;

public class BaseAdapter
{
    protected readonly RaContext _ef;

    protected BaseAdapter(RaContext ef)
        => _ef = ef;


    /*protected async void AuditAdd(Account account, object entity)
    {
        var ownerEvent = $"{account.FirstName} {account.MiddleName.First()}. {account.LastName?.First()}.";

        foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
        {
            var curValue1 = _ef.Entry(entity).CurrentValues.TryGetValue<string>(propertyInfo.Name,out var curValue);
            
            var log = new Audit()
            {
                DateTimeEvent = DateTime.Now,
                OwnerEvent = ownerEvent,
                Object = entity!.ToString()!,
                Action = "Создание объекта",
                Property = propertyInfo.Name,
                ValueBefore = "",
                ValueAfter = curValue
                
            };
            
            await _ef.AddAsync(log);
            await _ef.SaveChangesAsync();
        }
    }

    protected async void RegisterEvent(Account account, string obj, string? action, string? property,
        string? valueafter, string? valuebefore)
    {
        var ownerEvent = $"{account.FirstName} {account.MiddleName.First()}. {account.LastName?.First()}.";

        var @event = new Audit()
        {
            DateTimeEvent = DateTime.Now,
            OwnerEvent = ownerEvent,
            Object = obj,
            Action = action,
            Property = property,
            ValueBefore = valuebefore,
            ValueAfter = valueafter
        };

        await _ef.AddAsync(@event);
        await _ef.SaveChangesAsync();
    }*/
}