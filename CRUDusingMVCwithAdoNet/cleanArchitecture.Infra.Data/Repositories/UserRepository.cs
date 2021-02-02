using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using cleanArchitecture.Domain.Models;
using cleanArchitecture.Infra.Data.Interfaces;
using System.Linq;
using System.Web;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cleanArchitecture.Infra.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        string connection = ConfigurationManager.ConnectionStrings["UserConn"].ConnectionString;
        private SqlConnection con;
        private SqlCommand command;
        private SqlDataReader dataReader;


        public IEnumerable<User> GetUsers()
        {
                List<User> userlist = new List<User>();
                using (con = new SqlConnection(connection))
                {
                    con.Open();
                    command = new SqlCommand("SP_GetUserList", con);
                    command.CommandType = CommandType.StoredProcedure;
                    dataReader = command.ExecuteReader();
                        while (dataReader.Read())
                        {
                            User user = new User();
                            user.UserId = Convert.ToInt32(dataReader["UserId"]);
                            user.Firstname = dataReader["Firstname"].ToString();
                            user.Lastname = dataReader["Lastname"].ToString();
                            user.Email = dataReader["Email"].ToString();
                            user.Profilepicture = "data:image/png;base64," + dataReader["Profilepicture"].ToString();

                    userlist.Add(user);
                        }
                        con.Close();
                }
                return userlist;
            }

        public List<SelectListItem> GetCountryList() {

            List<Country> countrylistobj = new List<Country>();
            List<SelectListItem> countrylist = new List<SelectListItem>();
            using (con = new SqlConnection(connection))
            {
                con.Open();                 
                command = new SqlCommand("select * from Country", con);
                dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Country country = new Country();
                        country.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                        country.CountryName = dataReader["CountryName"].ToString();

                    countrylistobj.Add(country);
                    }
                    con.Close();
                
            }

            countrylist = (from data in countrylistobj
                           select data).ToList().Select(x =>
                            new SelectListItem()
                            {
                                Text = x.CountryName.ToString(),
                                Value = x.CountryId.ToString()
                            }).ToList();
            return countrylist;

        }

        public List<SelectListItem> GetStatesListbyCountryId(int? countryId)
        {

            List<State> statelistobj = new List<State>();
            List<SelectListItem> statelist = new List<SelectListItem>();
            using (con = new SqlConnection(connection))
            {
                con.Open();
                // 1. declare command object with parameter
                command = new SqlCommand("select * from State where CountryId = @CountryId", con);
                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CountryId";
                param.Value = countryId;

                // 3. add new parameter to command object
                command.Parameters.Add(param);

                // get data stream
                dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                    State state = new State();
                    state.StateId = Convert.ToInt32(dataReader["StateId"]);
                    state.StateName = dataReader["StateName"].ToString();

                    statelistobj.Add(state);
                    }
                    con.Close();
            }

            statelist = (from data in statelistobj
                           select data).ToList().Select(x =>
                            new SelectListItem()
                            {
                                Text = x.StateName.ToString(),
                                Value = x.StateId.ToString()
                            }).ToList();
            return statelist;

        }

        public List<SelectListItem> GetCitiesListbyStateId(int? stateId)
        {

            List<City> citylistobj = new List<City>();
            List<SelectListItem> citylist = new List<SelectListItem>();
            using (con = new SqlConnection(connection))
            {
                con.Open();
                // 1. declare command object with parameter
                command = new SqlCommand("select * from City where StateId = @StateId", con);
                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@StateId";
                param.Value = stateId;

                // 3. add new parameter to command object
                command.Parameters.Add(param);

                // get data stream
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    City city = new City();
                    city.CityId = Convert.ToInt32(dataReader["CityId"]);
                    city.CityName = dataReader["CityName"].ToString();

                    citylistobj.Add(city);
                }
                con.Close();
            }

            citylist = (from data in citylistobj
                         select data).ToList().Select(x =>
                          new SelectListItem()
                          {
                              Text = x.CityName.ToString(),
                              Value = x.CityId.ToString()
                          }).ToList();
            return citylist;

        }

        public List<Hobby> GetHobbies() {

            List<Hobby> hobbylist = new List<Hobby>();
       
            using (con = new SqlConnection(connection))
            {
                con.Open();
                // 1. declare command object with parameter
                command = new SqlCommand("select * from Hobby", con);
              
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Hobby hobby = new Hobby();
                    hobby.HobbyId = Convert.ToInt32(dataReader["HobbyId"]);
                    hobby.HobbyName = dataReader["HobbyName"].ToString();

                    hobbylist.Add(hobby);
                }
                con.Close();
            }
          
            return hobbylist;

        }

        public bool AddUpdateUser(User model)
        {
            using (con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_InsertUpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@Firstname", model.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", model.Lastname);
                cmd.Parameters.AddWithValue("@Dateofbirth", model.Dateofbirth);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Profilepicture", model.Profilepicture);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@CityId", model.CityId);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@Phoneno", model.Phoneno);
                cmd.Parameters.AddWithValue("@Hobbies", model.Hobbies);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                    return true;
                else
                    return false;
            }
        }

        public User GetUserById(int? Id)
        {
            User user = new User();
            using (con = new SqlConnection(connection))
            {
                con.Open();
                command = new SqlCommand("SP_Get_UserById", con);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = Id;
                command.Parameters.Add(param);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    user.UserId = Convert.ToInt32(dataReader["UserId"]);
                    user.Firstname = dataReader["Firstname"].ToString();
                    user.Lastname = dataReader["Lastname"].ToString();
                    user.Dateofbirth = Convert.ToDateTime(dataReader["Dateofbirth"]);
                    user.Email = dataReader["Email"].ToString();
                    user.Profilepicture = "data:image/png;base64," + dataReader["Profilepicture"].ToString();
                    user.Gender = Convert.ToInt32(dataReader["Gender"]);
                    user.CityId = Convert.ToInt32(dataReader["CityId"]);
                    user.Address = dataReader["Address"].ToString();
                    user.Phoneno = dataReader["Phoneno"].ToString();
                    user.Hobbies = dataReader["Hobbies"].ToString();

                    var selectedHobbies = user.Hobbies.Split(',').Select(int.Parse).ToArray();
                    user.HobbiesArray = selectedHobbies;

                    user.StateId = Convert.ToInt32(dataReader["StateId"]);
                    user.CountryId = Convert.ToInt32(dataReader["CountryId"]);
                }
                con.Close();
            }
            return user;
        }

        public bool DeleteUser(int? Id)
        {
            using (con = new SqlConnection(connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_Delete_User", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
              
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                    return true;
                else
                    return false;

            }
        }

    }
}
