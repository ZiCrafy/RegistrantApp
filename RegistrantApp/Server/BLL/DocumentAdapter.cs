using Mapster;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Documents;
using RegistrantApp.Shared.PresentationLayer.Documents;

namespace RegistrantApp.Server.BLL;

public class DocumentAdapter : BaseAdapter
{
    public DocumentAdapter(RaContext ef) : base(ef)
    {
    }


    public async Task<ICollection<ViewDocument>?> GetAsync(long idAccount, bool showDeleted)
    {
        var listDocuments = await _ef.Documents
            .Include(x => x.Account)
            .Where(doc => doc.Account!.AccountID == idAccount && doc.IsDeleted == showDeleted)
            .ToListAsync();

        return listDocuments.Adapt<List<ViewDocument>>();
    }

    public async Task<ViewDocument?> CreateAsync(Token session, dtoDocumentCreate dto)
    {
        var document = new Document();
        dto.Adapt(document);

        document.Account = await _ef.Accounts
            .FirstOrDefaultAsync(account => account.AccountID == dto.idAccount);
        await _ef.AddAsync(document);
        await _ef.AuditChanges(session.OwnerToken);
        return document.Adapt<ViewDocument>();
    }

    public async Task<ViewDocument?> UpdateAsync(Token session, dtoDocumentUpdate dto)
    {
        var foundDocument = await _ef.Documents
            .FirstOrDefaultAsync(document => document.DocumentID == dto.DocumentID);

        if (foundDocument is null)
            return null;
        dto.Adapt(foundDocument);
        _ef.Update(foundDocument);
        await _ef.AuditChanges(session.OwnerToken);
        return foundDocument.Adapt<ViewDocument>();
    }

    public async Task DeleteAsync(Token session, IEnumerable<long> idsDocument)
    {
        foreach (var item in idsDocument)
        {
            var foundDocument = await _ef.Documents
                .FirstOrDefaultAsync(document => document.DocumentID == item);
            if (foundDocument is null)
                continue;

            foundDocument.IsDeleted = true;
            _ef.Update(foundDocument);
        }
        
        await _ef.AuditChanges(session.OwnerToken);
    }
}