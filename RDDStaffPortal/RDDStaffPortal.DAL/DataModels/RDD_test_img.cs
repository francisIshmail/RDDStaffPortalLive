using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  class RDD_test_img
    {
        public byte[]  image1  { get; set; }
        public string imgtyp { get; set; }
        public  string LogoPath { get; set; }


        public bool EditFlag { get; set; }
        public bool SaveFlag { get; set; }
    }
}
