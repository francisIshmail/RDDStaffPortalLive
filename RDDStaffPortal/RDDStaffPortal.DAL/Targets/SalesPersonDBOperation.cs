
using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.Targets
{
   public class SalesPersonDBOperation
    {
        CommonFunction Com = new CommonFunction();
        public List<RDD_CountryTarget> GetCountryList(string LoginUserName)
        {
            List<RDD_CountryTarget> _CountryList = new List<RDD_CountryTarget>();

            //  List<RDD_Menus> _MenusList = new List<RDD_Menus>();

            try
            {
                DataSet dscont1 = new DataSet();
                Db.constr = Global.getConnectionStringByName("tejSAP");

               // DataSet dscont = Db.myGetDS("RDD_GetSalesPersonwiseCountry");
                SqlParameter[] Para = {
                    new SqlParameter("@p_ShortName",LoginUserName),
                    
                };
                dscont1 = Com.ExecuteDataSet("RDD_GetSalesPersonwiseCountry", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");

                if (dscont1.Tables.Count > 0)
                {
                    DataTable dtCountry = dscont1.Tables[0];
                    for (int i = 0; i < dtCountry.Rows.Count; i++)
                    {
                        RDD_CountryTarget _Country = new RDD_CountryTarget();

                        if (dtCountry.Rows[i]["CountryCode"] != null && !DBNull.Value.Equals(dtCountry.Rows[i]["CountryCode"]))
                        {
                            _Country.CountryCode = dtCountry.Rows[i]["CountryCode"].ToString();
                        }
                        if (dtCountry.Rows[i]["Country"] != null && !DBNull.Value.Equals(dtCountry.Rows[i]["Country"]))
                        {
                            _Country.CountryName = dtCountry.Rows[i]["Country"].ToString();
                        }

                        _CountryList.Add(_Country);
                    }
                }
            }
            catch (Exception ex)
            {
                _CountryList = null;
            }
            return _CountryList;
           

        }
        public List<RDD_salesperson> GetSalesPersonList(string lognm, string ftype)
        {
            List<RDD_salesperson> _SalesPersonList = new List<RDD_salesperson>();

            //  List<RDD_Menus> _MenusList = new List<RDD_Menus>();
            var unm = "";
            try
            {
                

                Db.constr = Global.getConnectionStringByName("tejSAP");
                SqlParameter[] Para = {
                    new SqlParameter("@p_LoginUserName",lognm),
                   new SqlParameter("@p_FormType",ftype)

                };
                DataSet dscont =  Com.ExecuteDataSet("RDD_GetSalesPerson", CommandType.StoredProcedure, Para);
                if (dscont.Tables.Count > 0)
                {
                    DataTable dtCountry = dscont.Tables[0];
                    for (int i = 0; i < dtCountry.Rows.Count; i++)
                    {
                        RDD_salesperson _salesperson = new RDD_salesperson();
                            _salesperson.Id = dtCountry.Rows[i]["Id"].ToString();
                      
                            _salesperson.Name = dtCountry.Rows[i]["Name"].ToString();

                        if (ftype == "F")
                        {
                            _salesperson.LName = dtCountry.Rows[i]["LoginName"].ToString();
                        }
                        _SalesPersonList.Add(_salesperson);
                    }
                }
            }
            catch (Exception ex)
            {
                _SalesPersonList = null;
            }
            return _SalesPersonList;
            // List<RDD_Modules> _ModuleList = new List<RDD_Modules>();
            //try
            //{
            //    DataSet dsModules = Com.ExecuteDataSet("RDD_GetCountry");
            //    if (dsModules.Tables.Count > 0)
            //    {
            //        DataTable dtModule = dsModules.Tables[0];
            //        DataRowCollection drc = dtModule.Rows;
            //        foreach (DataRow dr in drc)
            //        {
            //            _CountryList.Add(new RDD_CountryTarget()
            //            {
            //                CountryCode = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString(): "",
            //                CountryName = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["country"].ToString() : "",

            //            });
            //        }

            //    }

            //}
            //catch (Exception)
            //{

            //    _CountryList = null;
            //}

            //return _CountryList;

        }

        public List<RDD_CountryBU> GetBUList(string Ccode, string frommonth, string Qvalue, string QYear, string type, string year,string salesperson)
        {
            List<RDD_CountryBU> _BUList = new List<RDD_CountryBU>();

            //  List<RDD_Menus> _MenusList = new List<RDD_Menus>();

            try
            {
                string curyr = DateTime.Now.Year.ToString();
                string curmnt = DateTime.Now.Month.ToString();
                string buenable = "";
                double revtot = 0;
               double gptot = 0;
               
                DataSet dscont1 = new DataSet();
                Db.constr = Global.getConnectionStringByName("tejSAP");
                if (type == "Y")
                {
                    // dscont1 = Db.myGetDS("RDD_MonthlyCountryBU");
                    SqlParameter[] Para = {
                    new SqlParameter("@country",Ccode),
                    new SqlParameter("@year",year),
                      new SqlParameter("@month",frommonth),
                      new SqlParameter("@salesperson",salesperson),
                };
                    dscont1 = Com.ExecuteDataSet("RDD_MonthlySalesPersonBU", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                    if (Convert.ToInt32(year) < Convert.ToInt32(curyr))
                    {
                        buenable = "Disable";
                    }
                    else if (Convert.ToInt32(year) == Convert.ToInt32(curyr) && (Convert.ToInt32(curmnt) > Convert.ToInt32(frommonth)))
                    {
                        buenable = "Disable";
                    }
                    else
                    {
                        buenable = "Enable";
                    }
                }
                else if (type == "Q")
                {
                    string fmonth = "";
                    string tomonth = "";
                    if (Qvalue == "Q1")
                    {
                        fmonth = "1";
                        tomonth = "3";
                    }
                    else if (Qvalue == "Q2")
                    {
                        fmonth = "4";
                        tomonth = "6";
                    }
                    else if (Qvalue == "Q3")
                    {
                        fmonth = "7";
                        tomonth = "9";
                    }
                    else if (Qvalue == "Q4")
                    {
                        fmonth = "10";
                        tomonth = "12";
                    }
                    SqlParameter[] Para = {
                    new SqlParameter("@country",Ccode),
                    new SqlParameter("@year",QYear),
                      new SqlParameter("@fmonth",fmonth),
                      new SqlParameter("@tmonth",tomonth),
                      new SqlParameter("@salesperson",salesperson),
                };
                    dscont1 = Com.ExecuteDataSet("RDD_QuarterlySalespersonBU", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                    if (Convert.ToInt32(QYear) < Convert.ToInt32(curyr))
                    {
                        buenable = "Disable";
                    }
                    else if (Convert.ToInt32(QYear) == Convert.ToInt32(curyr) && (Convert.ToInt32(curmnt) > Convert.ToInt32(fmonth)) && (Convert.ToInt32(curmnt) > Convert.ToInt32(tomonth)))
                    {
                        buenable = "Disable";
                    }
                    else
                    {
                        buenable = "Enable";
                    }
                    //  dscont1 = Db.myGetDS("RDD_QuarterCountryBU");
                }
                if (dscont1.Tables[0].Rows.Count > 0)
                {
                    if (dscont1.Tables.Count > 0)
                    {
                        DataTable dtBU = dscont1.Tables[0];
                        for (int i = 0; i < dtBU.Rows.Count; i++)
                        {
                            RDD_CountryBU _BU = new RDD_CountryBU();

                            if (dtBU.Rows[i]["BU"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["BU"]))
                            {
                                _BU.BU = dtBU.Rows[i]["BU"].ToString();
                            }
                            if (dtBU.Rows[i]["revenue"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["revenue"]))
                            {
                                _BU.revenue = dtBU.Rows[i]["revenue"].ToString();
                                revtot += Convert.ToDouble(dtBU.Rows[i]["revenue"].ToString());
                            }
                            if (dtBU.Rows[i]["gp"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["gp"]))
                            {
                                _BU.GPTarget = dtBU.Rows[i]["gp"].ToString();
                                gptot += Convert.ToDouble(dtBU.Rows[i]["gp"].ToString());
                            }
                            _BU.EnableProperty = buenable;
                            _BUList.Add(_BU);
                        }
                        RDD_CountryBU _BU1 = new RDD_CountryBU();
                            _BU1.BU = "Total";
                         _BU1.revenue = revtot.ToString();
                        
                            _BU1.GPTarget = gptot.ToString();
                      
                        _BUList.Add(_BU1);
                    }
                }
                else
                {
                    DataSet dscont = Db.myGetDS("RDD_GetBU");
                    if (dscont.Tables.Count > 0)
                    {
                        DataTable dtBU = dscont.Tables[0];
                        for (int i = 0; i < dtBU.Rows.Count; i++)
                        {
                            RDD_CountryBU _BU = new RDD_CountryBU();

                            if (dtBU.Rows[i]["BU"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["BU"]))
                            {
                                _BU.BU = dtBU.Rows[i]["BU"].ToString();
                            }
                            if (dtBU.Rows[i]["revenue_targets"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["revenue_targets"]))
                            {
                                _BU.revenue = dtBU.Rows[i]["revenue_targets"].ToString();
                                revtot += Convert.ToDouble(dtBU.Rows[i]["revenue_targets"].ToString());
                            }
                            if (dtBU.Rows[i]["GP_targets"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["GP_targets"]))
                            {
                                _BU.GPTarget = dtBU.Rows[i]["GP_targets"].ToString();
                                gptot += Convert.ToDouble(dtBU.Rows[i]["GP_targets"].ToString());
                            }
                            _BU.EnableProperty = "Enable";
                            _BUList.Add(_BU);
                        }
                        RDD_CountryBU _BU1 = new RDD_CountryBU();
                        _BU1.BU = "Total";
                        _BU1.revenue = revtot.ToString();

                        _BU1.GPTarget = gptot.ToString();

                        _BUList.Add(_BU1);
                    }
                }
            }
            catch (Exception ex)
            {
                _BUList = null;
            }
            return _BUList;

        }

        public List<RDD_CountryBU> GetCopyBUList(string Type, string fromyear, string frommonth, string fromQuarter, string fromsalesperson,
                        string fromcountry, string copyyear,string copymonth, string copyQyear, string copyQuarter , string copysalesperson , string copycountry)
        {
            List<RDD_CountryBU> _BUList = new List<RDD_CountryBU>();

            //  List<RDD_Menus> _MenusList = new List<RDD_Menus>();
            double revtot = 0;
            double gptot = 0;
            try
            {
                string curyr = DateTime.Now.Year.ToString();
                string curmnt = DateTime.Now.Month.ToString();
                string buenable = "";

                DataSet dscont1 = new DataSet();
                Db.constr = Global.getConnectionStringByName("tejSAP");
                if (Type == "Y")
                {
                    // dscont1 = Db.myGetDS("RDD_MonthlyCountryBU");
                    SqlParameter[] Para = {
                    new SqlParameter("@country",copycountry),
                    new SqlParameter("@year",fromyear),
                      new SqlParameter("@fmonth",frommonth),
                      new SqlParameter("@salesperson",fromsalesperson),
                      new SqlParameter("@fromsalesperson",copysalesperson) ,
new SqlParameter("@fromyear",copyyear),
new SqlParameter("@frommonth",copymonth),

                };
                    dscont1 = Com.ExecuteDataSet("RDD_copyfromsalespersonMonthlyBU", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                    if (Convert.ToInt32(fromyear) < Convert.ToInt32(curyr))
                    {
                        buenable = "Disable";
                    }
                    else if (Convert.ToInt32(fromyear) == Convert.ToInt32(curyr) && (Convert.ToInt32(curmnt) > Convert.ToInt32(frommonth)))
                    {
                        buenable = "Disable";
                    }
                    else
                    {
                        buenable = "Enable";
                    }
                }
                else if (Type == "Q")
                {
                    string fmonth = "";
                    string tomonth = "";
                    string copyfmonth = "";
                    string copytmonth = "";
                    if (fromQuarter == "Q1" )
                    {
                        fmonth = "1";
                        tomonth = "3";
                    }
                    else if (fromQuarter == "Q2")
                    {
                        fmonth = "4";
                        tomonth = "6";
                    }
                    else if (fromQuarter == "Q3")
                    {
                        fmonth = "7";
                        tomonth = "9";
                    }
                    else if (fromQuarter == "Q4")
                    {
                        fmonth = "10";
                        tomonth = "12";
                    }

                    if (copyQuarter == "Q1")
                    {
                       copyfmonth = "1";
                        copytmonth = "3";
                    }
                    else if (copyQuarter == "Q2")
                    {
                        copyfmonth = "4";
                        copytmonth = "6";
                    }
                    else if (copyQuarter == "Q3")
                    {
                        copyfmonth = "7";
                       copytmonth = "9";
                    }
                    else if (copyQuarter == "Q4")
                    {
                        copyfmonth = "10";
                        copytmonth = "12";
                    }
                    SqlParameter[] Para = {

 new SqlParameter("@country",fromcountry),   
 new SqlParameter("@year",fromyear),  
 new SqlParameter("@fmonth",fmonth),  
 new SqlParameter("@tmonth",tomonth),  
 new SqlParameter("@salesperson",fromsalesperson),  
 new SqlParameter("@copyyear",copyQyear),  
 new SqlParameter("@copyfmonth",copyfmonth),  
 new SqlParameter("@copytmonth",copytmonth),  
 new SqlParameter("@copysalesperson",copysalesperson),  
                };
                    dscont1 = Com.ExecuteDataSet("RDD_CopyQuarterlyCopyBU", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                    if (Convert.ToInt32(fromyear) < Convert.ToInt32(curyr))
                    {
                        buenable = "Disable";
                    }
                    else if (Convert.ToInt32(fromyear) == Convert.ToInt32(curyr) && (Convert.ToInt32(curmnt) > Convert.ToInt32(fmonth)) && (Convert.ToInt32(curmnt) > Convert.ToInt32(tomonth)))
                    {
                        buenable = "Disable";
                    }
                    else
                    {
                        buenable = "Enable";
                    }
                    //  dscont1 = Db.myGetDS("RDD_QuarterCountryBU");
                }
                if (dscont1.Tables[0].Rows.Count > 0)
                {
                    if (dscont1.Tables.Count > 0)
                    {
                        DataTable dtBU = dscont1.Tables[0];
                        for (int i = 0; i < dtBU.Rows.Count; i++)
                        {
                            RDD_CountryBU _BU = new RDD_CountryBU();

                            if (dtBU.Rows[i]["BU"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["BU"]))
                            {
                                _BU.BU = dtBU.Rows[i]["BU"].ToString();
                            }
                            if (dtBU.Rows[i]["revenue"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["revenue"]))
                            {
                                _BU.revenue = dtBU.Rows[i]["revenue"].ToString();
                                revtot += Convert.ToDouble(dtBU.Rows[i]["revenue"].ToString());
                            }
                            if (dtBU.Rows[i]["gp"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["gp"]))
                            {
                                _BU.GPTarget = dtBU.Rows[i]["gp"].ToString();
                                gptot += Convert.ToDouble(dtBU.Rows[i]["gp"].ToString());
                            }
                            _BU.EnableProperty = buenable;
                            _BUList.Add(_BU);
                        }
                        RDD_CountryBU _BU1 = new RDD_CountryBU();
                        _BU1.BU = "Total";
                        _BU1.revenue = revtot.ToString();

                        _BU1.GPTarget = gptot.ToString();

                        _BUList.Add(_BU1);
                    }
                }
                else
                {
                    DataSet dscont = Db.myGetDS("RDD_GetBU");
                    if (dscont.Tables.Count > 0)
                    {
                        DataTable dtBU = dscont.Tables[0];
                        for (int i = 0; i < dtBU.Rows.Count; i++)
                        {
                            RDD_CountryBU _BU = new RDD_CountryBU();

                            if (dtBU.Rows[i]["BU"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["BU"]))
                            {
                                _BU.BU = dtBU.Rows[i]["BU"].ToString();
                            }
                            if (dtBU.Rows[i]["revenue_targets"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["revenue_targets"]))
                            {
                                _BU.revenue = dtBU.Rows[i]["revenue_targets"].ToString();
                                revtot += Convert.ToDouble(dtBU.Rows[i]["revenue_targets"].ToString());
                            }
                            if (dtBU.Rows[i]["GP_targets"] != null && !DBNull.Value.Equals(dtBU.Rows[i]["GP_targets"]))
                            {
                                _BU.GPTarget = dtBU.Rows[i]["GP_targets"].ToString();
                                gptot += Convert.ToDouble(dtBU.Rows[i]["GP_targets"].ToString());
                            }
                            _BU.EnableProperty = "Enable";
                            _BUList.Add(_BU);
                        }
                        RDD_CountryBU _BU1 = new RDD_CountryBU();
                        _BU1.BU = "Total";
                        _BU1.revenue = revtot.ToString();

                        _BU1.GPTarget = gptot.ToString();

                        _BUList.Add(_BU1);
                    }
                }
            }
            catch (Exception ex)
            {
                _BUList = null;
            }
            return _BUList;
        }
        public string SaveBU(List<RDD_SalesPersonTarget> tar,string usernm)
        {
            string result = "";
            string result1 = "";
            //List<CountryTarget> tar = new List<CountryTarget>();
            string fmonth = "";
            string tomonth = "";
            string mailqval = "0";
            string mailyval = "0";
            string ismonth = "0";
            string saverslt = "False";
            if (tar[0].type == "Y")
            {
                mailyval = tar[0].month;
                ismonth = "1";
                for (int i = 0; i < tar.Count; i++)
                {
                    SqlParameter[] Para = {
                    new SqlParameter("@year", tar[0].year),
                    new SqlParameter("@month", tar[0].month),
                      new SqlParameter("@country", tar[0].country),
                         new SqlParameter("@Bu", tar[i].BU),
                           new SqlParameter("@revenue",tar[i].revenue),
                         new SqlParameter("@gp", tar[i].gp),
                         new SqlParameter("@salesperson",tar[0].salesperson),
                          new SqlParameter("@createdby",usernm),
 new SqlParameter("@LastUpdatedBy",usernm),
                         new SqlParameter("@p_Response",1),

                };
                    Para[9].Direction = ParameterDirection.Output;
                    // cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;


                    result = Com.ExecuteScalars("RDD_SaveUpdateSalespersonBU", Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                }
                saverslt = "True";
            }
            else if (tar[0].type == "Q")
            {

                if (tar[0].quarter == "Q1")
                {
                    fmonth = "1";
                    tomonth = "3";
                    mailqval = "1";
                }
                else if (tar[0].quarter == "Q2")
                {
                    fmonth = "4";
                    tomonth = "6";
                    mailqval = "2";
                }
                else if (tar[0].quarter == "Q3")
                {
                    fmonth = "7";
                    tomonth = "9";
                    mailqval = "3";
                }
                else if (tar[0].quarter == "Q4")
                {
                    fmonth = "10";
                    tomonth = "12";
                    mailqval = "4";
                }
                for (int j = Convert.ToInt32(fmonth); j <= Convert.ToInt32(tomonth); j++)
                {

                    for (int i = 0; i < tar.Count; i++)
                    {
                        double revval = Convert.ToDouble(tar[i].revenue) / 3;
                        double gpval = Convert.ToDouble(tar[i].gp) / 3;
                        SqlParameter[] Para = {
                    new SqlParameter("@year", tar[0].Qyear),
                    new SqlParameter("@month", j),
                      new SqlParameter("@country", tar[0].country),
                         new SqlParameter("@Bu", tar[i].BU),
                           new SqlParameter("@revenue",revval),
                         new SqlParameter("@gp", gpval),
                         new SqlParameter("@salesperson",tar[0].salesperson),
                           new SqlParameter("@createdby",usernm),
 new SqlParameter("@LastUpdatedBy",usernm),
                          new SqlParameter("@p_Response",1),
                };
                        Para[9].Direction = ParameterDirection.Output;
                        result = Com.ExecuteScalars("RDD_SaveUpdateSalespersonBU", Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                    }
                    saverslt = "True";
                }



            }

            if (saverslt == "True")
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_Year", tar[0].year),
                    new SqlParameter("@p_Month", mailyval),
                      new SqlParameter("@p_Country", tar[0].country),
                        new SqlParameter("p_salesperson", tar[0].salesperson),
                          new SqlParameter("p_salespersonFullName", tar[0].sfullname),
                         new SqlParameter("@p_TargetsAddedBy", usernm),
                           new SqlParameter("@p_IsMonthly",ismonth),
                         new SqlParameter("@p_Quarter", mailqval),
};
                result1 = Com.ExecuteScalars("SendMailForSalespersonTagretsAdd", Para);// Db.myGetDS("RDD_MonthlyCountryBU");
            }

            return result;
        }
    }
}
