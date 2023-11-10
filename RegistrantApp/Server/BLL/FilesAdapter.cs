using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrantApp.Server.BLL.Base;
using RegistrantApp.Server.Database.Base;
using RegistrantApp.Shared.Database;
using RegistrantApp.Shared.Dto.Files;
using RegistrantApp.Shared.PresentationLayer.Files;
using File = RegistrantApp.Shared.Database.File;

namespace RegistrantApp.Server.BLL;

public class FilesAdapter : BaseAdapter
{
    public FilesAdapter(RaContext ef) : base(ef)
    {
    }


    public async Task<ICollection<ViewFile>?> GetFromDocumentsAsync(long idDocument, bool showDeleted)
    {
        var files = await _ef.Files
            .Include(x => x.Document)
            .Where(file => file.Document!.DocumentID == idDocument && file.IsDeleted == showDeleted)
            .ToListAsync();

        return files.Adapt<List<ViewFile>>();
    }

    public async Task<ICollection<ViewFile>?> GetFromOrderAsync(long idOrder, bool showDeleted)
    {
        var files = await _ef.Files
            .Include(x => x.Order)
            .Where(order => order.Order!.OrderID == idOrder && order.IsDeleted == showDeleted)
            .ToListAsync();

        return files.Adapt<List<ViewFile>>();
    }

    public async Task<FileContentResult?> DownloadAsync(string idFile)
    {
        var document = await _ef.Files
            .FirstOrDefaultAsync(file => file.FileID.ToString() == idFile.ToUpper());

        if (document is null)
            return null;

        var file = new FileContentResult(document.Bytes, "application/octet-stream")
        {
            FileDownloadName = document.FileName
        };

        return file;
    }

    public async Task<ViewFile?> UploadAsync(IFormFile file)
    {
        File newFile;

        await using (var fileStream = file.OpenReadStream())
        {
            newFile = new File()
            {
                FileName = file.FileName,
                Bytes = new byte[file.Length],
                DateTimeUpload = DateTime.Now,
                IsDeleted = false
            };

            await fileStream.ReadAsync(newFile.Bytes, 0, (int)file.Length);
        }
        
        await _ef.AddAsync(newFile);
        await _ef.SaveChangesAsync();

        return newFile.Adapt<ViewFile>();
    }

    public async Task<ViewFile?> AttachFileAsync(dtoFileAttach dto)
    {
        var foundFile = await _ef.Files
            .FirstOrDefaultAsync(file => file.FileID.ToString() == dto.IdFile);
        
        if (foundFile is null)
            return null;
        
        foundFile.Order = string.IsNullOrEmpty(dto.IdOrder.ToString())
            ? null
            : await _ef.Orders.FirstOrDefaultAsync(o => o.OrderID == dto.IdOrder);
        
        foundFile.Document = string.IsNullOrEmpty(dto.IdDocument.ToString())
            ? null
            : await _ef.Documents.FirstOrDefaultAsync(o => o.DocumentID == dto.IdDocument);

        _ef.Update(foundFile);
        await _ef.SaveChangesAsync();
        return foundFile.Adapt<ViewFile>();
    }
}