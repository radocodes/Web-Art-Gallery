using CloudinaryDotNet;

namespace WAG.Services.Interfaces
{
    public interface ICloudinaryService
    {
        Cloudinary GetCloudinaryInstance();

        Account CloudinaryAccount();

        string GenerateSignature(string timestamp, string source);
    }
}
