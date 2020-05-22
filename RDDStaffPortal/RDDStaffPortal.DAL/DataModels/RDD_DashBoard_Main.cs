using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  partial class RDD_DashBoard_Main
    {
        public string DashId { get; set; }
        public string DashName { get; set; }
        public string TypeOfChart { get; set; }
        public string Url { get; set; }
        public int NoOfColumn { get; set; }
        public string ColumNames { get; set; }
        public string FieldName { get; set; }
        public int IsActive { get; set; }

        public string colcss { get; set; }

        public string cssclass { get; set; }

        public string UserId { get; set; }

        public  List<UserDashWidget> UserDashWidgets { get; set; }
        public string UserDashWidget { get; set; }
    }
    public partial class UserDashWidget
    {
        public string DashId { get; set; }
        public bool IsActive { get; set; }
    }
}
