using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ICareApplication.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }

        [Required]
        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Pincode { get; set; }

        public Nullable<int> InsurerId { get; set; }

        public virtual Insurer Insurer { get; set; }
        [Required]
        public string InsurerName { get; set; }
    }
}