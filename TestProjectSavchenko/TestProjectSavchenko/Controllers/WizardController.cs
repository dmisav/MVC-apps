using System;
using System.Web.Mvc;
using TestProject.DataAccessLayer;
using TestProject.DataAccessLayer.Entities;
using TestProject.DataAccessLayer.Mappers;

namespace TestProjectSavchenko.Controllers
{
    public class WizardController : Controller
    {
        private DataFacade dataFacade;

        public WizardController(DataFacade dataFacade)
        {
            this.dataFacade = dataFacade;
        }

        public ActionResult Validate()
        {
            return View();
        }

        public ActionResult Location(string userId)
        {
            var user = dataFacade.GetUserByUserID(Convert.ToInt32(userId));
            return View(user);
        }

        [HttpPost]
        public JsonResult ValidateUser(UserModel userModel)
        {
            dataFacade.SignUpUser(userModel.email, userModel.password);
            var foundUser = dataFacade.GetUserByEmail(userModel.email);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Location", "Wizard", new { userId = foundUser.id});
            return Json(new { url = redirectUrl });
        }

        [HttpPost]
        public JsonResult SaveLocation(UserModel userModel)
        {
            dataFacade.SaveUserProvince(Convert.ToInt32(userModel.id), Convert.ToInt32(userModel.provinceId));
            var message = "User information has been saved";
            return Json(new { resultMessage = message });
        }

        [HttpGet]
        public JsonResult GetProvinces(string id)
        {
            var provinces = dataFacade.GetProvinces(Convert.ToInt32(id));
            return this.Json(provinces, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCountries()
        {
            var countries = dataFacade.GetCountries();
            return this.Json(countries, JsonRequestBehavior.AllowGet);
        }
    }
}