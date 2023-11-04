using Mapster;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Accounts;
using RegistrantApp.Shared.Dto.Auto;
using RegistrantApp.Shared.Dto.Contragents;
using RegistrantApp.Shared.Dto.Documents;
using RegistrantApp.Shared.Dto.Security;
using RegistrantApp.Shared.PresentationLayer.Accounts;
using RegistrantApp.Shared.PresentationLayer.Contragents;
using RegistrantApp.Shared.PresentationLayer.Documents;
using RegistrantApp.Shared.PresentationLayer.Files;
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

    }
}