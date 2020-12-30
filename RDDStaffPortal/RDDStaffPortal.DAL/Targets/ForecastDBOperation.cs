using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.Targets
{
   public class ForecastDBOperation
    {
        CommonFunction Com = new CommonFunction();
      


        public List<RDD_ForecastBU> GetBUList(string year, string Month, string salesperson, string country)
        {
            List<RDD_ForecastBU> _BUList = new List<RDD_ForecastBU>();

            //  List<RDD_Menus> _MenusList = new List<RDD_Menus>();

            try
            {
                int DisableForecastAfterDays = 30;
                DisableForecastAfterDays = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DisableForecastEntryAfterDays"].ToString());

                string curyr = DateTime.Now.Year.ToString();
                string curmnt = DateTime.Now.Month.ToString();
                string cuday = DateTime.Now.Day.ToString();
                string buenable = "";
                DataSet dscont1 = new DataSet();
                Db.constr = Global.getConnectionStringByName("tejSAP");

                // dscont1 = Db.myGetDS("RDD_MonthlyCountryBU");
                SqlParameter[] Para = {
                     new SqlParameter("@p_Month",Month),
 new SqlParameter("@p_Year",year),
 new SqlParameter("@p_Country",country),
 new SqlParameter("@p_salesperson",salesperson),

                };
                dscont1 = Com.ExecuteDataSet("RDD_GetSalespersonForecast", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                if (Convert.ToInt32(year) < Convert.ToInt32(curyr))
                {
                    buenable = "Disable";
                }
                else if (Convert.ToInt32(year) == Convert.ToInt32(curyr) && (Convert.ToInt32(curmnt) > Convert.ToInt32(Month)))
                {
                    buenable = "Disable";
                }
                else if (Convert.ToInt32(year) == Convert.ToInt32(curyr) && (Convert.ToInt32(curmnt) == Convert.ToInt32(Month))  && (Convert.ToInt32(cuday)> DisableForecastAfterDays))
                {
                    buenable = "Disable";
                }
                else
                {
                    buenable = "Enable";
                }
                double revtot = 0;
                double revrrtot = 0;
                double revbtbtot = 0;
                double gptot = 0;
                double gprrtot = 0;
                double gpbtbtot = 0;

                if (dscont1.Tables[0].Rows.Count > 0)
                {
                    if (dscont1.Tables.Count > 0)
                    {
                        DataTable dtBU = dscont1.Tables[0];
                       
                        for (int i = 0; i < dtBU.Rows.Count; i++)
                        {
                            RDD_ForecastBU _BU = new RDD_ForecastBU();

                            if (dtBU.Rows[i]["BU"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["BU"]))
                            {
                                _BU.BU = dtBU.Rows[i]["BU"].ToString();
                            }
                            if (dtBU.Rows[i]["Revenue"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["Revenue"]))
                            {
                                _BU.Revenue = dtBU.Rows[i]["Revenue"].ToString();
                                revtot += Convert.ToDouble(dtBU.Rows[i]["Revenue"].ToString());
                            }
                            if (dtBU.Rows[i]["RevenueRR"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["RevenueRR"]))
                            {
                                _BU.RevenueRR = dtBU.Rows[i]["RevenueRR"].ToString();
                                revrrtot += Convert.ToDouble(dtBU.Rows[i]["RevenueRR"].ToString());
                            }
                            if (dtBU.Rows[i]["RevenueBTB"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["RevenueBTB"]))
                            {
                                _BU.RevenueBTB = dtBU.Rows[i]["RevenueBTB"].ToString();
                                revbtbtot += Convert.ToDouble(dtBU.Rows[i]["RevenueBTB"].ToString());
                            }
                            if (dtBU.Rows[i]["gp"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["gp"]))
                            {
                                _BU.gp = dtBU.Rows[i]["gp"].ToString();
                                gptot += Convert.ToDouble(dtBU.Rows[i]["gp"].ToString());
                            }
                            if (dtBU.Rows[i]["GPRR"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["GPRR"]))
                            {
                                _BU.GPRR = dtBU.Rows[i]["GPRR"].ToString();
                                gprrtot += Convert.ToDouble(dtBU.Rows[i]["GPRR"].ToString());
                            }
                            if (dtBU.Rows[i]["GPBTB"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["GPBTB"]))
                            {
                                _BU.GPBTB = dtBU.Rows[i]["GPBTB"].ToString();
                                gpbtbtot += Convert.ToDouble(dtBU.Rows[i]["GPBTB"].ToString());
                            }
                            if (dtBU.Rows[i]["revenuepercent"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["revenuepercent"]))
                            {
                                _BU.revenuepercent = dtBU.Rows[i]["revenuepercent"].ToString();
                            }
                            if (dtBU.Rows[i]["gppercent"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["gppercent"]))
                            {
                                _BU.gppercent = dtBU.Rows[i]["gppercent"].ToString();
                            }
                            _BU.EnableProperty = buenable;
                            _BUList.Add(_BU);
                        }
                        RDD_ForecastBU _BU1 = new RDD_ForecastBU();
_BU1.BU = "Total";

                        _BU1.Revenue = revtot.ToString();

                        _BU1.RevenueRR = revrrtot.ToString();

                        _BU1.RevenueBTB = revbtbtot.ToString();

                        _BU1.gp = gptot.ToString();
                        
                            _BU1.GPRR = gprrtot.ToString();
                        
                            _BU1.GPBTB =gpbtbtot.ToString();
                        
                            _BU1.revenuepercent = "0.00";
                       
                            _BU1.gppercent = "0.00";
                       
                        _BUList.Add(_BU1);
                    }
                }
                else
                {
                    // DataSet dscont = Db.myGetDS("RDD_getForcastBU");
                    SqlParameter[] Para1 = {
                     new SqlParameter("@p_Month",Month),
 new SqlParameter("@p_Year",year),
 new SqlParameter("@p_Country",country),
 new SqlParameter("@p_salesperson",salesperson),

                };
                    dscont1 = Com.ExecuteDataSet("RDD_getForcastBU", CommandType.StoredProcedure, Para1);// Db.myGetDS("RDD_MonthlyCountryBU");
                   


                    if (dscont1.Tables[0].Rows.Count > 0)
                    {
                        if (dscont1.Tables.Count > 0)
                        {
                            DataTable dtBU = dscont1.Tables[0];
                            for (int i = 0; i < dtBU.Rows.Count; i++)
                            {
                                RDD_ForecastBU _BU = new RDD_ForecastBU();

                                if (dtBU.Rows[i]["BU"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["BU"]))
                                {
                                    _BU.BU = dtBU.Rows[i]["BU"].ToString();
                                }
                                if (dtBU.Rows[i]["Revenue"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["Revenue"]))
                                {
                                    _BU.Revenue = dtBU.Rows[i]["Revenue"].ToString();
                                    revtot += Convert.ToDouble(dtBU.Rows[i]["Revenue"].ToString());
                                }
                                if (dtBU.Rows[i]["RevenueRR"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["RevenueRR"]))
                                {
                                    _BU.RevenueRR = dtBU.Rows[i]["RevenueRR"].ToString();
                                    revrrtot += Convert.ToDouble(dtBU.Rows[i]["RevenueRR"].ToString());
                                }
                                if (dtBU.Rows[i]["RevenueBTB"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["RevenueBTB"]))
                                {
                                    _BU.RevenueBTB = dtBU.Rows[i]["RevenueBTB"].ToString();
                                    revbtbtot += Convert.ToDouble(dtBU.Rows[i]["RevenueBTB"].ToString());
                                }
                                if (dtBU.Rows[i]["gp"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["gp"]))
                                {
                                    _BU.gp = dtBU.Rows[i]["gp"].ToString();
                                    gptot += Convert.ToDouble(dtBU.Rows[i]["gp"].ToString());
                                }
                                if (dtBU.Rows[i]["GPRR"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["GPRR"]))
                                {
                                    _BU.GPRR = dtBU.Rows[i]["GPRR"].ToString();
                                    gprrtot += Convert.ToDouble(dtBU.Rows[i]["GPRR"].ToString());
                                }
                                if (dtBU.Rows[i]["GPBTB"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["GPBTB"]))
                                {
                                    _BU.GPBTB = dtBU.Rows[i]["GPBTB"].ToString();
                                    gpbtbtot += Convert.ToDouble(dtBU.Rows[i]["GPBTB"].ToString());
                                }
                                if (dtBU.Rows[i]["revenuepercent"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["revenuepercent"]))
                                {
                                    _BU.revenuepercent = dtBU.Rows[i]["revenuepercent"].ToString();
                                }
                                if (dtBU.Rows[i]["gppercent"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["gppercent"]))
                                {
                                    _BU.gppercent = dtBU.Rows[i]["gppercent"].ToString();
                                }
                                _BU.EnableProperty = buenable;
                                _BUList.Add(_BU);
                            }
                            RDD_ForecastBU _BU1 = new RDD_ForecastBU();
                            _BU1.BU = "Total";

                            _BU1.Revenue = revtot.ToString();

                            _BU1.RevenueRR = revrrtot.ToString();

                            _BU1.RevenueBTB = revbtbtot.ToString();

                            _BU1.gp = gptot.ToString();

                            _BU1.GPRR = gprrtot.ToString();

                            _BU1.GPBTB = gpbtbtot.ToString();

                            _BU1.revenuepercent = "0.00";

                            _BU1.gppercent = "0.00";

                            _BUList.Add(_BU1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _BUList = null;
            }
            return _BUList;

        }


        public string SaveBU(List<RDD_ForecastTarget> tar,string usernm)
        {
            string result = "";
            //List<CountryTarget> tar = new List<CountryTarget>();
            string fmonth = "";
            string tomonth = "";
            string saverslt = "False";
            string result1 = "";
            DateTime createdon = DateTime.Now;


            for (int i = 0; i < tar.Count; i++)
                {
                   

                          SqlParameter[] Para = {
                        new SqlParameter("@country",tar[0].country),
    new SqlParameter("@salesperson",tar[0].salesperson),
    new SqlParameter("@bu",tar[i].BU),
    new SqlParameter("@year",tar[0].year),
    new SqlParameter("@month",tar[0].month),
    new SqlParameter("@revenuebtb",tar[i].revenuebtb),
    new SqlParameter("@revenuerr",tar[i].revenurrr),
    new SqlParameter("@gpbtb",tar[i].gpbtb),
    new SqlParameter("@gprr",tar[i].gprr),
    new SqlParameter("@revenueper",tar[i].revper),
new SqlParameter("@gpbper",tar[i].gpper),
new SqlParameter("@createdby",usernm),
new SqlParameter("@LastUpdatedBy",usernm),
new SqlParameter("@p_Response",1),

                };
                    Para[13].Direction = ParameterDirection.Output;
                    // cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;


                    result = Com.ExecuteScalars("RDD_SAveUpdateForecast", Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                saverslt = "True";
                }

            if (saverslt == "True")
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_Year", tar[0].year),
                    new SqlParameter("@p_Month", tar[0].month),
                      new SqlParameter("@p_Country", tar[0].country),
                        new SqlParameter("p_salesperson", tar[0].salesperson),
                          new SqlParameter("p_salespersonFullName", tar[0].sfullname),
                         new SqlParameter("@p_ForecastAddedBy", usernm),
                          
};
               
                result1 = Com.ExecuteScalars("getForecastDataToSendEmail", Para);// Db.myGetDS("RDD_MonthlyCountryBU");
            }

            return result;
        }
    }
}
