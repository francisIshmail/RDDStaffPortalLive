using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RDDStaffPortal.Areas.Admin.Models
{
    public class Menus
    {
        public int MenuId { get; set; }
        public string ObjType { get; set; }
        public string MenuName { get; set; }
        public int ModuleId { get; set; }
        public int Levels { get; set; }
        public string ModuleName { get; set; }
        public string MenuCssClass { get; set; }
        public string ModuleCssClass { get; set; }
        public string URL { get; set; }
        public int DisplaySeq { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}