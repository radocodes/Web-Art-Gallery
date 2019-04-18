using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<WAGUser> GetAllUsers();

        WAGUser GetUserById(string id);

        IList<string> GetUserRolesNameById(string id);
    }
}