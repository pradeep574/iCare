using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace ICareApplication.Controllers
{
    public class InsurerController : Controller
    {
        ICareRepository repo = new ICareRepository();

        [HttpGet]
        // GET: Insurer
        public ActionResult Index()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<DataAccess.Insurer> entityInsurer = repo.GetInsurer();    
            List<Models.Insurer> insurer = new List<Models.Insurer>();

            foreach (var eH in entityInsurer)
            {
                Models.Insurer temp = new Models.Insurer();
                temp.InsurerId = eH.InsurerId;
                temp.InsurerName = eH.InsurerName;
                temp.RegistrationNo = eH.RegistrationNo;
                temp.headOffice = eH.headOffice;

                insurer.Add(temp);
            }
            return View(insurer);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["EmailId"] == null)
                return RedirectToAction("Index","Home");
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Insurer insurer)
        {
            if (Session["EmailId"] == null)
                return RedirectToAction("Index", "Home");
            
            if (ModelState.IsValid)
            {
                Insurer insurerInfo = new Insurer()
                {

                    InsurerName = insurer.InsurerName,
                    RegistrationNo= insurer.RegistrationNo,
                    headOffice = insurer.headOffice
                    
                };
                bool result = repo.AddInsurer(insurerInfo);
                if (!result)
                    return View("Error");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Models.Insurer insurer)
        {
            if (Session["EmailId"] == null )
            {
                RedirectToAction("Index", "Home");
            }

            Insurer insurerInfo = new Insurer()
            {   InsurerId = insurer.InsurerId,
                InsurerName = insurer.InsurerName,
                RegistrationNo = insurer.RegistrationNo,
                headOffice = insurer.headOffice    
            };

            bool success = repo.UpdateInsurer(insurerInfo);
            if (!success)
                return View("Error");

            return RedirectToAction("Index","Insurer");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["EmailId"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            var businessEntity = repo.GetInsurer(id);

            Models.Insurer insurer1 = new Models.Insurer()
            {
                InsurerId = businessEntity.InsurerId,
                InsurerName = businessEntity.InsurerName,
                RegistrationNo = businessEntity.RegistrationNo,
                headOffice = businessEntity.headOffice
            };
            return View(insurer1);
        }
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["EmailId"] == null)
                return RedirectToAction("Index", "Home");
            var businessEntity = repo.GetInsurer(id);
            
            Models.Insurer insurer1 = new Models.Insurer()
            {
                InsurerId = businessEntity.InsurerId,
                InsurerName = businessEntity.InsurerName,
                RegistrationNo = businessEntity.RegistrationNo,
                headOffice = businessEntity.headOffice
            };
            return View(insurer1);
        }
    }
}