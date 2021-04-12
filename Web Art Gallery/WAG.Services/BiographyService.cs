using WAG.Services.Constants;
using WAG.Services.Interfaces;

namespace WAG.Services
{
    public class BiographyService : IBiographyService
    {
        private readonly IFileService FileService;
        public BiographyService(IFileService fileService)
        {
            this.FileService = fileService;
        }

        public string GetBiography()
        {
            return this.FileService.GetTextFromFile(GlobalConstants.BioDirectoryPath, GlobalConstants.BioFileName);
        }

        public void EditBiography(string editedText)
        {
            this.FileService.SaveTextToFileAsync(GlobalConstants.BioDirectoryPath, GlobalConstants.BioFileName, editedText);
        }
    }
}
