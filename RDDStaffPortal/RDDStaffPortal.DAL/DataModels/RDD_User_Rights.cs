using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.DataModels
{
    public class RDD_User_Rights
    {
        public string MenuId { get; set; }
        public string UserId { get; set; }

       // public List<SelectListItem> UserList { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string AuthoTyp { get; set; }

        public string Flag { get; set; }
       

        public List<MenuDetail> MenuDetails { get; set; }

        public MenuDetail MenuDetail { get; set; }

        public bool Saveflag { get; set; }




    }
    public class MenuDetail
    {
        public string MenuId { get; set; }
        public string AuthoTyp { get; set; }
    }

    }
