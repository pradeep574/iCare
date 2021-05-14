using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace ICareApplication.Controllers
{
    public class HospitalController : Controller
    {
        ICareRepository repo = new ICareRepository();
        
        [HttpGet]
        // GET: Hospital
        public ActionResult Index()
        {
            if(Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<DataAccess.Hospital> entityHospital = repo.GetHospital();
            List<Models.Hospital> hospitals = new List<Models.Hospital>();

            foreach (var eH in entityHospital)
            {
                Models.Hospital temp = new Models.Hospital();
                temp.HospitalId = eH.HospitalId;
                temp.HospitalName = eH.HospitalName;
                temp.City = eH.City;
                temp.Address = eH.Address;
                temp.State = eH.State;
                temp.Pincode = eH.Pincode;
                temp.InsurerId = eH.InsurerId;
                temp.InsurerName = repo.GetInsurer(eH.InsurerId.GetValueOrDefault()).InsurerName;
                hospitals.Add(temp);
            }
            return View(hospitals);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<string> res = repo.GetAllInsurerNames();
            ViewBag.list = res;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Hospital hospital)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                Hospital hospitalInfo = new Hospital()
                {
                    HospitalName = hospital.HospitalName,
                    City = hospital.City,
                    Address = hospital.Address,
                    State = hospital.State,
                    Pincode = hospital.Pincode,
                    InsurerId = repo.GetInsurerId(hospital.InsurerName)
                };
                bool result = repo.AddHospital(hospitalInfo);
                if (!result)
                    return View("Error");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["EmailId"] == null)
                return RedirectToAction("Index", "Home");
            var businessEntity = repo.GetHospital(id);
            Models.Hospital hospitalInfo = new Models.Hospital()
            {
                HospitalId = businessEntity.HospitalId,
                HospitalName = businessEntity.HospitalName,
                City = businessEntity.City,
                Address = businessEntity.Address,
                State = businessEntity.State,
                Pincode = businessEntity.Pincode,
                InsurerName = repo.GetInsurer(businessEntity.InsurerId.GetValueOrDefault()).InsurerName
            };
            return View(hospitalInfo);
        }
        [HttpPost]
        public ActionResult Edit(Models.Hospital hospital)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Hospital hospitalInfo = new Hospital()
            {
                HospitalName = hospital.HospitalName,
                HospitalId = hospital.HospitalId,
                Address = hospital.Address,
                City = hospital.City,
                State = hospital.State,
                Pincode = hospital.Pincode,
                InsurerId = hospital.InsurerId
            };

            bool success = repo.UpdateHospital(hospitalInfo);
            if (!success)
            {
                return View("Error");
            }
            return RedirectToAction("Index","Hospital");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var businessEntity = repo.GetHospital(id);

            Models.Hospital hospitalInfo = new Models.Hospital()
                {
                    HospitalId = businessEntity.HospitalId,
                    HospitalName = businessEntity.HospitalName,
                    City = businessEntity.City,
                    Address = businessEntity.Address,
                    State = businessEntity.State,
                    Pincode = businessEntity.Pincode,
                    InsurerId = businessEntity.InsurerId
                };
            return View(hospitalInfo);
        }
        [HttpGet]
        public ActionResult SearchHospitalByPincode()
        {
            return View("SearchHospitalByPincode","_Layout");
        }
        [HttpPost]
        public ActionResult SearchHospitalByPincode(FormCollection searchForm)
        {
            string hospitalPincode = searchForm["txtPincode"];
            List<DataAccess.Get_hospitals_Result> entityHospital = repo.SearchHospitalByPincode(Convert.ToInt32(hospitalPincode));
            //List<Insurer> insurer = repo.GetInsurer();
            List<Models.Hospital> hospitals = new List<Models.Hospital>();
            foreach (var eH in entityHospital)
            {
                Models.Hospital temp = new Models.Hospital();
                temp.HospitalName = eH.HospitalName;
                temp.City = eH.City;
                temp.Address = eH.Address;
                temp.State = eH.State;
                temp.InsurerName = repo.GetInsurer(eH.InsurerId).InsurerName;
                //temp.InsurerName = repo.GetInsurer(eH.InsurerId);
                hospitals.Add(temp);
            }
            return View("SearchHospitalByPincodeResult","_Layout",hospitals);
            //return RedirectToAction("SearchHospitalByPincode", "Home");   
        }
        [HttpGet]
        public ActionResult SearchHospitalByName()
        {
            return View("SearchHospitalByName", "_Layout");
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
            return View("SearchHospitalByNameResult", "_Layout", hospitals);
        }
    }
}