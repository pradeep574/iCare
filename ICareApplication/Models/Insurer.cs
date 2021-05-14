using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ICareApplication.Models
{
    public class Insurer
    {
        public int InsurerId { get; set; }

        [Required]
        [Display(Name = "Insurer Name")]
        public string InsurerName { get; set; }

        public Nullable<int> RegistrationNo { get; set; }
        public string headOffice { get; set; }
    }
}