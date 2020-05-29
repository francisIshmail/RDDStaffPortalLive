using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.InitialSetup
{
   public class RDD_Log
    {
       public int Id { get; set;}
       public string TableName { get; set; }
       public int KeyId { get; set; }
       public string ColName { get; set; }
       public string ColDescription { get; set; }
       public string OldValue { get; set; }
       public string NewValue { get; set; }
       public string ChangedBy { get; set; }
       public DateTime ChangedOn { get; set; }
    }
}
