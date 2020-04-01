using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
  public  class RDD_Departments
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
    }
}
