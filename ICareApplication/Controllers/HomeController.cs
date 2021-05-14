using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace ICareApplication.Controllers
{
    public class HomeController : Controller
    {
        ICareRepository repo = new ICareRepository();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection loginForm)
        {
            string emailId = loginForm["txtEmailId"];
            string password = loginForm["txtPassword"];
            User user = repo.ValidateUser(emailId, password);
            if(user == null)
            {
                ViewBag.ErrorMsg = "Invalid Credentials, Please try again";
                return View("Index");
            }
            else
            {
                Session["EmailId"] = emailId;
                return RedirectToAction("Index", "Hospital");
            }
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Models.User user)
        {
            
            if(ModelState.IsValid)
            {
                DataAccess.User userInfo = new DataAccess.User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailId = user.EmailId,
                    Password = user.Password
                };
                bool result = repo.AddUser(userInfo);
                if (!result)
                {
                    return View("Error");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SearchHospitalByPincode(FormCollection searchForm)
        {
            string hospitalPincode= searchForm["txtPincode"];
            List<DataAccess.Get_hospitals_Result> entityHospital = repo.SearchHospitalByPincode(563113);
            List<Models.Hospital> hospitals = new List<Models.Hospital>();
            foreach (var eH in entityHospital)
            {
                Models.Hospital temp = new Models.Hospital();
                temp.HospitalName = eH.HospitalName;
                temp.City = eH.City;
                temp.Address = eH.Address;
                temp.State = eH.State;
                temp.Pincode = eH.Pincode;

                hospitals.Add(temp);
            }
            return View(hospitals);
            //return RedirectToAction("SearchHospitalByPincode", "Home");   
        }

        [HttpGet]
        public ActionResult SearchHospitalByName()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchHospitalByName(FormCollection searchForm)
        {
            string hospitalName = searchForm["txtHospital"];
            List<DataAccess.Hospital> entityHospital = repo.SearchHospitals(hospitalName);
            List<Models.Hospital> hospitals = new List<Models.Hospital>();
            foreach (var eH in entityHospital)
            {
                Models.Hospital temp = new Models.Hospital();
                temp.HospitalName = eH.HospitalName;
                temp.City = eH.City;
                temp.Address = eH.Address;
                temp.State = eH.State;
                temp.Pincode = eH.Pincode;
                hospitals.Add(temp);
            }
            return View("SearchHospitalByNameResult",hospitals);
        }
        
    }
}