using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WAG.Services.Interfaces
{
    public interface ICommonService
    {
        Task<string> UploadPictureAsync(IFormFile picture);

        Task<string> UploadTextToFileAsync(string text, string DirectoryPath);

        string DownloadTextFromFile(string fileName, string directoryPath);
    }
}
