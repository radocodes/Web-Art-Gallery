using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WAG.Services.Interfaces;

namespace WAG.Services
{
    public class CommonService : ICommonService
    {
        public async Task<string> UploadPictureAsync(IFormFile picture)
        {
            var fileName = $"{Guid.NewGuid()}.jpg";

            var filePath = $@"D:\RADO\IT\Projects\Web Art Gallery\Web Art Gallery\WAG WebApp\wwwroot\images\{fileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            };

            return fileName;
        }

        public async Task<string> UploadTextToFileAsync(string text, string directoryPath)
        {
            var fileName = $"{Guid.NewGuid()}.txt";

            var filePathFull = $@"{directoryPath}{fileName}";

            using (var stream = new StreamWriter(filePathFull))
            {
                await stream.WriteAsync(text);
            };

            return fileName;
        }

        public string DownloadTextFromFile(string fileName, string directoryPath)
        {
            var filePathFull = $@"{directoryPath}{fileName}";

            string text = null;

            using (var stream = new StreamReader(filePathFull))
            {
                text = stream.ReadToEnd();
            };

            return text;
        }
    }
}
