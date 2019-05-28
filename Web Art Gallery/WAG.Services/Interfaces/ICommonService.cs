using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WAG.Services.Interfaces
{
    public interface ICommonService
    {
        Task<string> UploadImageAsync(string directoryPath, string fileName, IFormFile imgFile);

        Task<string> UploadTextToFileAsync(string directoryPath, string fileName, string text);

        string DownloadTextFromFile(string directoryPath, string fileName);
    }
}
