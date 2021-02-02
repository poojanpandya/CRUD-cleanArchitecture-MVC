using System;
using System.Collections.Generic;
using System.Text;
using cleanArchitecture.Application.Interfaces;
using cleanArchitecture.Infra.Data.Interfaces;
using cleanArchitecture.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cleanArchitecture.Application.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SelectList GetCountryList()
        {
            try
            {
                SelectList CountryList = new SelectList(_userRepository.GetCountryList(), "Value", "Text");
                return CountryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SelectList GetStateListbyCountryId(int countryId)
        {
            try
            {
                SelectList StateList = new SelectList(_userRepository.GetStatesListbyCountryId(countryId), "Value", "Text");
                return StateList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SelectList GetCityListbyStateId(int stateId)
        {
            try
            {
                SelectList CityList = new SelectList(_userRepository.GetCitiesListbyStateId(stateId), "Value", "Text");
                return CityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Hobby> GetHobbies()
        {
            try
            {
                List<Hobby> hobbiesList = new List<Hobby>();
                hobbiesList = _userRepository.GetHobbies();
                return hobbiesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddUpdateUser(User model) {
            try
            {
                var result = _userRepository.AddUpdateUser(model);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserById(int? Id)
        {
            try
            {
                var result = _userRepository.GetUserById(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteUser(int? Id)
        {
            try
            {
                var result = _userRepository.DeleteUser(Id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
