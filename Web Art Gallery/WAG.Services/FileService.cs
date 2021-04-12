using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using WAG.Services.Interfaces;

namespace WAG.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveImageAsync(string directoryPath, string fileName, IFormFile imgFile)
        {
            var fileFullPath = $"{directoryPath}{fileName}";

            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await imgFile.CopyToAsync(stream);
            };

            return fileName;
        }

        public async Task<string> SaveTextToFileAsync(string directoryPath, string fileName, string text)
        {
            var fileFullPath = $"{directoryPath}{fileName}";

            using (var stream = new StreamWriter(fileFullPath, false))
            {
                await stream.WriteAsync(text);
            };

            return fileName;
        }

        public string GetTextFromFile(string directoryPath, string fileName)
        {
            var filePathFull = $"{directoryPath}{fileName}";

            string text = null;

            using (var stream = new StreamReader(filePathFull))
            {
                text = stream.ReadToEnd();
            };

            return text;
        }
    }
}
