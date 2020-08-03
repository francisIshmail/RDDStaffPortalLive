using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  class Rdd_comonDrop
    {
        public string Code { get; set; }
        public string CodeName { get; set; }

        public string imagepath { get; set; }
    }

    public partial class Employee_Configure
    {
        public string UserRole { get; set; }
        public List<Employee_ConfigureList> Employee_Configs { get; set; }

        public Employee_ConfigureList Employee_Config { get; set; }

    }

    public partial class Employee_ConfigureList
    {          
        public string ColumnName { get; set; }
        public string Description { get; set; }
        public string Types { get; set; }
    }
}
