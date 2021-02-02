using System;
using System.Collections.Generic;
using System.Text;
using cleanArchitecture.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cleanArchitecture.Infra.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        List<SelectListItem> GetCountryList();
        List<SelectListItem> GetStatesListbyCountryId(int? countryId);
        List<SelectListItem> GetCitiesListbyStateId(int? stateId);
        List<Hobby> GetHobbies();
        bool AddUpdateUser(User model);
        User GetUserById(int? Id);
        bool DeleteUser(int? Id);
    }
}
