using System.Collections.Generic;
using System.Web.Mvc;
using TestProjectSavchenko.Entities;
using System.Linq;

namespace TestProjectSavchenko.Controllers
{
    public class WizardController : Controller
    {
        public class ProvinceModel
        {
           public string id {get;set;}
           public string name {get;set;}
           public string countryId {get;set;}
        }

        public class CountryModel
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public ActionResult Validate()
        {
            return View();
        }

        public ActionResult Location()
        {
            return View(model: "123");
        }

        [HttpPost]
        public JsonResult ValidateUser(UserModel userModel)
        {
            if (userModel != null)
            {
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Location", "Wizard", new { userId = "7e96b930-a786-44dd-8576-052ce608e38f" });
                return Json(new { isValidUser = true, url = redirectUrl });
            }
            else
                return Json(new { isValidUser = false, validationResultMessage = "User does not exist" });
        }

        [HttpGet]
        public JsonResult GetProvinces(string id)
        {
            List<ProvinceModel> list = new List<ProvinceModel>(){             
                new ProvinceModel(){ countryId = "65000000", id = "1", name = "Kiev" },
                new ProvinceModel(){ countryId = "65000001", id = "2", name = "Kharkov" },
                new ProvinceModel(){ countryId = "65000002", id = "3", name = "Odessa" }};
          
          return this.Json(list.Where(x=> x.countryId == id) , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCountries()
        {
            List<CountryModel> list = new List<CountryModel>(){             
                new CountryModel(){id = "65000000", name = "Ukraine" },
                new CountryModel(){id = "65000001", name = "Russia" },
                new CountryModel(){id = "65000002", name = "Norway" }};

            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}