using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WAG.ViewModels.InputViewModels;

namespace WAG.Services.Interfaces
{
    public interface IUserAccountService
    {
        SignInResult LoginUserSuccessfully(LoginInputViewModel loginInputViewModel);

        Task<SignInResult> RegisterUserSuccessfully(RegisterInputViewModel registerInputViewModel);

        void Logout();
    }
}
