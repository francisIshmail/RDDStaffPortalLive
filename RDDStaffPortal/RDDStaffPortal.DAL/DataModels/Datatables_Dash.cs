using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.DataModels
{
   public  partial class Datatables_Dash
    {
        public string CompanyName { get; set; }
        public DateTime Date1 { get; set; }
        public decimal Amount { get; set; }

    }
    public partial  class Pichart_Dash
    {
        public string  bgcolrs { get; set; }
        public decimal points { get; set; }
        public string lblname { get; set; }
    }


    public partial class BarChart_Dash {
      

        public List<decimal> points { get; set; }

        public List<string> lbls { get; set; }


    }
    public partial class Card_Dash
    {
        public decimal RevTarget{get;set;}
        public decimal RevForecast { get;set;}
        public decimal ActualRev { get;set;}
        public int RevTrgetAcheivedPercent { get;set;}
        public int RevForecastAcheivedPercent { get;set;}
        public decimal GPTarget { get;set; }
        public decimal GPForecast { get;set;}
        public decimal ActualGP { get;set;}
        public int GPTrgetAcheivedPercent { get;set;}
       public int GPForecastAcheivedPercent { get; set; }
    }



    public   partial class Sales_BU
    {
        public string CompanyName { get; set; }
        public decimal Points1 { get; set; }
        public decimal Points2 { get; set; }
        public decimal Points3 { get; set; }
        public decimal Points4 { get; set; }
        public decimal Points5 { get; set; }
        public decimal Points6 { get; set; }
    }
}
