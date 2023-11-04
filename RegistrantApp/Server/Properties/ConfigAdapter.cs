using Mapster;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Accounts;
using RegistrantApp.Shared.PresentationLayer.Accounts;

namespace RegistrantApp.Server.Properties;

public class ConfigAdapter : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Account, dtoAccountCreate>()
            .Map(z => z.FirstName, x => x.FirstName)
            .Map(z => z.Password, z => z.Password)
            .Map(z => z.IsEmployee, z => z.IsEmployee)
            .Map(z => z.PhoneNumber, x => x.PhoneNumber)
            .Map(z => z.LastName, x => x.LastName)
            .Map(z => z.MiddleName, z => z.MiddleName);
        
        config.NewConfig<Account, dtoAccountUpdate>()
            .Map(z=> z.FirstName, x=> x.FirstName)
            .Map(z => z.Password, z => z.Password)
            .Map(z => z.IsEmployee, z => z.IsEmployee)
            .Map(z => z.PhoneNumber, x => x.PhoneNumber)
            .Map(z => z.LastName, x => x.LastName)
            .Map(z => z.MiddleName, z => z.MiddleName);
        
        config.NewConfig<Account, ViewAccount>()
            .Map(z => z.FirstName, x => x.FirstName)
            .Map(z => z.PhoneNumber, x => x.PhoneNumber)
            .Map(z => z.LastName, x => x.LastName)
            .Map(z => z.MiddleName, z => z.MiddleName);
            
        
    }
}