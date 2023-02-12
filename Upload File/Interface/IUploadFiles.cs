using Microsoft.AspNetCore.WebUtilities;

namespace Upload_File.Interface
{
    public interface IUploadFiles
    {
        Task<bool> UploadFileStream(MultipartReader reader, MultipartSection section);
        Task<bool> UploadFileBuffered(IFormFile file);
    }
}
