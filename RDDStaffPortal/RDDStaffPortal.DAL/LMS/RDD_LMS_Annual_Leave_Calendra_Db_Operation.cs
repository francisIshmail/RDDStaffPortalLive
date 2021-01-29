using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels.LMS;
using static RDDStaffPortal.DAL.CommonFunction;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.Mvc;
using SAPbobsCOM;

namespace RDDStaffPortal.DAL.LMS
{
    
    public class RDD_LMS_Annual_Leave_Calendra_Db_Operation
    {
        CommonFunction Com = new CommonFunction();
        //RDD_LMS_Annual_Leave_Calendar_Dropdown_Fill
        public DataSet Get_LMS_Leave_Planner(string UserName,int? page,string currentFilter,int pagesize,DateTime fromdate,DateTime todate,string country,int deptid,int empid)
        {
            DataSet ds = null;
            try
            {
                if (page > 0)
                {
                    page = page;
                }
                else
                {
                    page = 1;
                }
                // DataSet DS = Db.myGetDS("EXEC RDD_DisplayEmployeeList "+ currentFilter);
                SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_search",currentFilter),
                     new SqlParameter("@p_pagesize",pagesize),
                     new SqlParameter("@p_pageno",page),
                     new SqlParameter("@p_SortColumn","EmployeeId"),
                     new SqlParameter("@p_SortOrder","ASC"),
                     new SqlParameter("@p_fromdate",fromdate),
                     new SqlParameter("@p_todate",todate),
                     new SqlParameter("@p_country",country),
                     new SqlParameter("@p_deptid",deptid),
                     new SqlParameter("@p_empid",empid)



                };
                ds = Com.ExecuteDataSet("RDD_LMS_Annual_Leave_Calendar", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                ds = null;
            }
           
            

            return ds;
        }


        public DataSet Get_LMS_Leave_Planner_DropDown_Fill(string UserName, string  ptype,string CountryCode,int deptid)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_username",UserName),
                    new SqlParameter("@p_type",ptype),
                     new SqlParameter("@p_countrycode",CountryCode),
                     new SqlParameter("@p_depid",deptid)
                };
                ds = Com.ExecuteDataSet("RDD_LMS_Annual_Leave_Calendar_Dropdown_Fill", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
           {

                ds = null;
            }
            return ds;
        }
    }
}
