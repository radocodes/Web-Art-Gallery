namespace WAG.Services.Interfaces
{
    public interface IBiographyService
    {
        string GetBiography();

        void EditBiography(string editedText);
    }
}
