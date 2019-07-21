using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WAG.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(string directoryPath, string fileName, IFormFile imgFile);

        Task<string> UploadTextToFileAsync(string directoryPath, string fileName, string text);

        string DownloadTextFromFile(string directoryPath, string fileName);
    }
}
