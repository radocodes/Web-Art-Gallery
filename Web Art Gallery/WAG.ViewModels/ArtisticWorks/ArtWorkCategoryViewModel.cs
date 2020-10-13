using WAG.Data.Models;

namespace WAG.ViewModels.ArtisticWorks
{
    public class ArtWorkCategoryViewModel : BaseModel<int>
    {
        public string Name { get; set; }

        public string MainPictureFileName { get; set; }
    }
}
