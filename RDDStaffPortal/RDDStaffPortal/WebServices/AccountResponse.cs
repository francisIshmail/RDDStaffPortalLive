using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.WebServices
{
   
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }


}