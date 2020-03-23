using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace RDDStaffPortal.Models
{
    public class Login
    {
            [Required(ErrorMessage = "*")]
            public string UserName { get; set; }
    
            [Required(ErrorMessage = "*")]
            public string UserPassword { get; set; }
            //public bool RememberMe { get; set; }
    }



}

