using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cleanArchitecture.Application.Interfaces;
using cleanArchitecture.Domain.Models;
using cleanArchitecture.Infra.Data.Interfaces;


namespace CRUD_cleanArchitecture_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        User user = new User();
        public HomeController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }


        #region Get Users
        public ActionResult Index()
        {
            var userList = _userService.GetUsers();
            return View(userList);
        }

        #endregion

        #region AddUpdateUser
        public ActionResult AddUpdateUser(int? Id)
        {
            try
            {
                //Get CountryList
                SelectList CountryList = new SelectList(_userService.GetCountryList(), "Value", "Text");
                //Get HobbyList and Bind in UserHobbyList
                SelectList HobbyList = new SelectList(_userService.GetHobbies(), "HobbyId", "HobbyName");
                //Added in ViewBag
                ViewBag.CountryList = CountryList;
                ViewBag.HobbyList = HobbyList;

                if (Id >= 0)
                {
                    user = _userRepository.GetUserById(Id);
                    SelectList StateList = new SelectList(_userRepository.GetStatesListbyCountryId(user.CountryId), "Value", "Text");
                    ViewBag.StateList = StateList;
                    // Bind CityList with Selected value
                    SelectList CityList = new SelectList(_userRepository.GetCitiesListbyStateId(user.StateId), "Value", "Text");
                    ViewBag.CityList = CityList;
                }

                return View(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region AddUpdateUser Post
        [HttpPost]
        public ActionResult AddUpdateUser(User model)
        {
            //Get CountryList
            SelectList CountryList = new SelectList(_userService.GetCountryList(), "Value", "Text");

            MultiSelectList HobbyList = new MultiSelectList(_userService.GetHobbies(), "HobbyId", "HobbyName");
            ViewBag.HobbyList = HobbyList;

            ViewBag.CountryList = CountryList;

            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        System.IO.Stream fs = file.InputStream;
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                        model.Profilepicture = base64String;

                    }
                    else if (file.ContentLength > 0 && model.UserId == 0)
                    {
                        ModelState.AddModelError("Image", "Please Select Image!!");
                        return View(model);
                    }
                    else
                    {
                        model.Profilepicture = "";
                    }

                    model.Hobbies = string.Join(",", model.HobbiesArray);
                    var result = _userService.AddUpdateUser(model);

                    if (result == true)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "There is an error in save Product!!";
                        return View(model);
                    }

                }
                else
                {
                    ModelState.AddModelError("Image", "Please Select Image!!");
                    return View(model);
                }
            }
            return View(model);
        } 
        #endregion

        #region GetStateListbyCountryId
        public JsonResult GetStateListbyCountryId(int countryId)
        {
            try
            {
                // Get StateList From CommonData By Passing CountryId 
                SelectList StateList = new SelectList(_userService.GetStateListbyCountryId(countryId), "Value", "Text");
                return Json(StateList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region GetCityListbyStateId
        public JsonResult GetCityListbyStateId(int stateId)
        {
            try
            {
                // Get cityList From CommonData By Passing StateId 
                SelectList CityList = new SelectList(_userService.GetCityListbyStateId(stateId), "Value", "Text");
                return Json(CityList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region DeleteUser
        public ActionResult DeleteUser(int? Id)
        {
            try
            {
                var result = _userService.DeleteUser(Id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }
}