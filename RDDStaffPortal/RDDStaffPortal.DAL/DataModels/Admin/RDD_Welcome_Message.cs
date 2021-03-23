using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Profile;
using System.Windows.Media.Animation;

namespace RDDStaffPortal.DAL.DataModels.Admin
{
   public partial class RDD_Welcome_Message
    {
        public string Welcome_title { get; set; }
        public bool imgbool { get; set; }
        public bool IsActive { get; set; }
        public int Welcome_id { get; set; }
        public string Welcome_Message { get; set; }
        public byte[] Welcome_image { get; set; }
        public string Welcome_image1 { get; set; }
        public string Errormsg { get; set; }
        public string ActionType { get; set; }
        public bool Saveflag { get; set; }
        public bool EditFlag { get; set; }
        public string  Loginid { get; set; }
        public DateTime?  Loginon { get; set; }
    }


    public partial class RDD_Welcome_Message_User {
        public int Welcome_Id { get; set; }
        public string UserName { get; set; }
        public string Reminder { get; set; }
        public string  Read { get; set; }
        public bool Saveflag { get; set; }
        public string Errormsg { get; set; }
        

    }
}
