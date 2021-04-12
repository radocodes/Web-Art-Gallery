using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WAG.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(string directoryPath, string fileName, IFormFile imgFile);

        Task<string> SaveTextToFileAsync(string directoryPath, string fileName, string text);

        string GetTextFromFile(string directoryPath, string fileName);
    }
}
