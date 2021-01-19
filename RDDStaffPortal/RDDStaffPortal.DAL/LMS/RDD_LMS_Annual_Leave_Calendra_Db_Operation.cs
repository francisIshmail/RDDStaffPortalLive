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

namespace RDDStaffPortal.DAL.LMS
{
    
    public class RDD_LMS_Annual_Leave_Calendra_Db_Operation
    {
        CommonFunction Com = new CommonFunction();

        public DataSet Get_LMS_Leave_Planner(int? page,string currentFilter,int pagesize,DateTime fromdate,DateTime todate)
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

                    new SqlParameter("@p_search",currentFilter),
                     new SqlParameter("@p_pagesize",pagesize),
                     new SqlParameter("@p_pageno",page),
                     new SqlParameter("@p_SortColumn","EmployeeId"),
                     new SqlParameter("@p_SortOrder","ASC"),
                     new SqlParameter("@p_fromdate",fromdate),
                     new SqlParameter("@p_todate",todate),



                };
                ds = Com.ExecuteDataSet("RDD_LMS_Annual_Leave_Calendar", CommandType.StoredProcedure, Para);
            }
            catch (Exception)
            {

                ds = null;
            }
           
            

            return ds;
        }
    }
}
