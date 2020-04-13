﻿using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.InitialSetup
{
    public class RDD_User_RightsDBOperation
    {
        CommonFunction Com = new CommonFunction();


        public RDD_User_Rights GetNew()
        {
            return new RDD_User_Rights
            {

            };
        }

        public bool save1(RDD_User_Rights UserRights)
        {


            bool t = false;
            string response = string.Empty;
            try
            {
                DataTable dte=new DataTable();
                int k = UserRights.MenuDetails.Count;
                int i = 0;
                dte.Columns.Add(new DataColumn("MenuId", typeof(int)));
                dte.Columns.Add(new DataColumn("UserId", typeof(string)));
                dte.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dte.Columns.Add(new DataColumn("CreatedOn", typeof(DateTime)));
                dte.Columns.Add(new DataColumn("Auth_Type", typeof(string)));
                while (i<k)
                {
                    
                    dte.Rows.Add(UserRights.MenuDetails[i].MenuId,UserRights.UserId,UserRights.CreatedBy,DateTime.Now,UserRights.MenuDetails[i].AuthoTyp);
                    i++;
                }
                SqlParameter[] Para = {
                    
                    new SqlParameter("@tblRights",dte),
                    new SqlParameter("@flag",UserRights.Flag)
                    
                    
                };
                t = Com.ExecuteNonQuery("RDD_User_Rights_InsertUpdate", Para);
                
            }
            catch (Exception ex)
            {
                t = false;
            }
            
            return t;
        }
    }
}
