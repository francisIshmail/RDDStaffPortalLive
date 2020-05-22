using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;

namespace RDDStaffPortal.DAL.DataModels
{
    public class RDD_customermapping
    {
        public int MappingId { get; set; }
        public string Parent_DBName { get; set; }
        public string Parent_CardCode { get; set; }
        public string Child_DBName { get; set; }
        public string Child_CardCode { get; set; }
        public string Child_CardName { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public List<ChildList> ChildLists { get; set; }
        public ChildList ChildList { get; set; }
        public bool EditFlag { get; set; }
        public bool saveflag { get; set; }
        public string errormsg { get; set; }
    }
    public  class ChildList{
        public string Child_CardCode { get; set; }
        public string Child_CardName { get; set; }
        public string Child_DBName { get; set; }
    }
}
