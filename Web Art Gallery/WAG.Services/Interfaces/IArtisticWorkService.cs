using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAG.ViewModels.InputViewModels;

namespace WAG.Services.Interfaces
{
    public interface IArtisticWorkService
    {
        void AddArtWork(ArtWorkInputViewModel inputViewModel);
    }
}
