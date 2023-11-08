using Mapster;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Accounts;
using RegistrantApp.Shared.Dto.Auto;
using RegistrantApp.Shared.Dto.Contragents;
using RegistrantApp.Shared.Dto.Documents;
using RegistrantApp.Shared.Dto.Orders;
using RegistrantApp.Shared.Dto.Security;
using RegistrantApp.Shared.PresentationLayer.Accounts;
using RegistrantApp.Shared.PresentationLayer.Contragents;
using RegistrantApp.Shared.PresentationLayer.Documents;
using RegistrantApp.Shared.PresentationLayer.Files;
using RegistrantApp.Shared.PresentationLayer.Orders;
using RegistrantApp.Shared.PresentationLayer.Security;
using File = RegistrantApp.Shared.Database.File;

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
            .Map(z => z.IsEmployee, z => z.IsEmployee)
            .Map(z => z.PhoneNumber, x => x.PhoneNumber)
            .Map(z => z.LastName, x => x.LastName)
            .Map(z => z.MiddleName, z => z.MiddleName);
        
        config.NewConfig<Account, ViewAccount>()
            .Map(z => z.FirstName, x => x.FirstName)
            .Map(z => z.PhoneNumber, x => x.PhoneNumber)
            .Map(z => z.LastName, x => x.LastName)
            .Map(z => z.MiddleName, z => z.MiddleName);

        config.NewConfig<Token, AccessToken>()
            .Map(z => z.Token, x => x.TokenID)
            .Map(z => z.DateTimeSessionExpired, x => x.DateTimeSessionExpired)
            .Map(z => z.DateTimeSessionStarted, x => x.DateTimeSessionStarted);

        config.NewConfig<Token, dtoAccessTokenFinished>()
            .Map(z => z.Token, x => x.TokenID)
            .Map(z => z.DateTimeSessionFinished, x => x.DateTimeSessionExpired);

        config.NewConfig<Auto, dtoAutoCreate>()
            .Map(z => z.Title, x => x.Title)
            .Map(z => z.AutoNumber, x => x.Title);
        
        config.NewConfig<Auto, dtoAutoUpdate>()
            .Map(z => z.Title, x => x.Title)
            .Map(z => z.AutoNumber, x => x.Title);

        config.NewConfig<Contragent, dtoContragentCreate>()
            .Map(z => z.Title, x => x.Title);
        
        config.NewConfig<Contragent, dtoContragentUpdate>()
            .Map(z => z.Title, x => x.Title)
            .Map(z=> z.ContragentID, x=> x.ContragentID);

        config.NewConfig<Contragent, ViewContragent>()
            .Map(z => z.Title, x => x.Title)
            .Map(z=> z.ContragentID, x=> x.ContragentID);

        config.NewConfig<Document, dtoDocumentCreate>()
            .Map(z => z.Title, x => x.Title)
            .Map(z => z.Authority, x => x.Authority)
            .Map(z => z.Number, x => x.Number)
            .Map(z => z.Seria, x => x.Seria)
            .Map(z => z.DateOfIssue, x => x.DateOfIssue);
        
        config.NewConfig<Document, dtoDocumentUpdate>()
            .Map(z => z.Title, x => x.Title)
            .Map(z => z.Authority, x => x.Authority)
            .Map(z => z.Number, x => x.Number)
            .Map(z => z.Seria, x => x.Seria)
            .Map(z => z.DateOfIssue, x => x.DateOfIssue)
            .Map(z=> z.DocumentID, x=> x.DocumentID);
        
        config.NewConfig<Document, ViewDocument>()
            .Map(z => z.Title, x => x.Title)
            .Map(z => z.Authority, x => x.Authority)
            .Map(z => z.Number, x => x.Number)
            .Map(z => z.Seria, x => x.Seria)
            .Map(z => z.DateOfIssue, x => x.DateOfIssue)
            .Map(z=> z.DocumentID, x=> x.DocumentID);

        config.NewConfig<File, ViewFile>()
            .Map(z => z.FileName, x => x.FileName)
            .Map(z => z.DateTimeUpload, x => x.DateTimeUpload)
            .Map(z => z.FileID, x => x.FileID);


        config.NewConfig<Order, dtoOrderCreate>()
            .Map(z => z.DateTimePlannedArrive, x => x.DateTimePlannedArrive);

        config.NewConfig<Order, dtoOrderUpdate>()
            .Map(z => z.DateTimeRegistration, x => x.DateTimeRegistration)
            .Map(z => z.DateTimeArrived, x => x.DateTimeArrived)
            .Map(z => z.DateTimeLeft, x => x.DateTimeLeft)
            .Map(z => z.DateTimeEndOrder, x => x.DateTimeEndOrder)
            .Map(z => z.DateTimePlannedArrive, x => x.DateTimePlannedArrive)
            .Map(z => z.DateTimeStartOrder, x => x.DateTimeStartOrder)
            .Map(z => z.IdAccount, x => x.Account!.AccountID)
            .Map(z => z.IdContragent, x => x.Contragent!.ContragentID)
            .Map(z => z.IdAuto, x => x.Auto!.AutoID)
            .Map(z=> z.OrderID, x=> x.OrderID);
        
        config.NewConfig<Order, ViewOrder>()
            .Map(z => z.AutoNumber, x => x.Auto!.AutoNumber)
            .Map(z => z.AutoTitle, x => x.Auto!.Title)
            .Map(z => z.AccountFirstName, x => x.Account!.FirstName)
            .Map(z => z.AccountLastName, x => x.Account!.LastName)
            .Map(z => z.AccountMiddleName, x => x.Account!.MiddleName)
            .Map(z => z.DateTimePlannedArrive, x => x.DateTimePlannedArrive)
            .Map(z => z.DateTimeArrived, x => x.DateTimeArrived)
            .Map(z => z.DateTimeLeft, x => x.DateTimeLeft)
            .Map(z => z.DateTimeRegistration, x => x.DateTimeRegistration)
            .Map(z => z.DateTimeEndOrder, x => x.DateTimeEndOrder)
            .Map(z => z.DateTimeStartOrder, x => x.DateTimeStartOrder);
    }
}