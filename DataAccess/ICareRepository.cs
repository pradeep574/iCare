using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccess
{
    public class ICareRepository
    {
        private ICareContext db;

        public ICareRepository()
        {
            db = new ICareContext();
        }
        public User ValidateUser(string email,string password)
        {
            User user = null;
            //int success;
            try
            {
                user = db.Users.Where(v => v.EmailId == email && v.Password == password).FirstOrDefault();
            }
            catch
            {
                user = null;
                //success = 0;
            }
            return user; 
        }
        public bool AddUser(User user)
        {
            if (user == null)
                return false;
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool AddHospital(Hospital hospital)
        {
            if (hospital == null)
                return false;
            try
            {
                Console.WriteLine("In Add");
                db.Hospitals.Add(hospital);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool UpdateHospital(Hospital hospitalInfo)
        {
            if (hospitalInfo == null)
                return false;
            try
            {
                db.Entry(hospitalInfo).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public List<Hospital> SearchHospitals(string name)
        {
            List<Hospital> hos;
            List<Hospital> result = new List<Hospital>();
            try
            {
                Console.WriteLine("In Search");
                hos = this.GetHospital();
                foreach (var hosp in hos)
                {
                    if(hosp.HospitalName.Contains(name))
                    {
                        result.Add(hosp);
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        public List<Get_hospitals_Result> SearchHospitalByPincode(int pincode)
        {
            List<Get_hospitals_Result> result = new List<Get_hospitals_Result>();
            try
            {
                result = db.Get_hospitals(pincode).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        public List<Hospital> GetHospital()
        {
            try
            {
                return db.Hospitals.ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Hospital GetHospital(int id)
        {
            Hospital hosp = null;
            try
            {
                hosp = db.Hospitals.Find(id);
            }
            catch (Exception e)
            {
                hosp = null;
                throw new Exception(e.Message);
            }
            return hosp;
        }
        public bool AddInsurer(Insurer insurer)
        {
            if (insurer == null)
                return false;
            
            try
            {
                db.Insurers.Add(insurer);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public List<Insurer> GetInsurer()
        {
            List<Insurer> result = new List<Insurer>();
            try
            {
                result = db.Insurers.ToList();
            }
            catch(Exception e) 
            {
                throw new Exception(e.Message);
            }
            return result; 
        }
        public Insurer GetInsurer(int id)
        {
            Insurer insurer = null;
            try
            {
                insurer = db.Insurers.Find(id);
            }
            catch (Exception e)
            {
                insurer = null;
                throw new Exception(e.Message);
            }
            return insurer;
        }
        public bool UpdateInsurer(Insurer insurer)
        {
            if (insurer == null)
                return false;
            try
            {
                db.Entry(insurer).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
                return false;
            }
            return true;
        }
        public List<String> GetAllInsurerNames()
        {
            List<Insurer> insurers;
            List<String> result = new List<String>();
            try
            {
                Console.WriteLine("In Search");
                insurers = this.GetInsurer();
                foreach (var insurer in insurers)
                {
                        result.Add(insurer.InsurerName);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        public int GetInsurerId(string name)
        {
            List<Insurer> insurers;
            int id=0;
            try
            {
                insurers = this.GetInsurer();
                foreach (var insurer in insurers)
                {
                    if (insurer.InsurerName == name)
                    {
                        id = insurer.InsurerId;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return id;
        }
        public string GetInsurerName(int id)
        {
            return " ";
        }
    }
}
