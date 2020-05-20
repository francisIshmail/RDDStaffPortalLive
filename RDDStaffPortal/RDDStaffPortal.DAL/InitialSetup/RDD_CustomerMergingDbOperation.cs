using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RDDStaffPortal.DAL.InitialSetup
{
   public  class RDD_CustomerMergingDbOperation
    {

        CommonFunction Com = new CommonFunction();

        public  List<RDD_CustomerMerging> GetRDDCustMergList(int i)
        {
            List<RDD_CustomerMerging> Objlist = new List<RDD_CustomerMerging>();
            try
            {                
                SqlParameter[] parm = { };
                DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[i];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_CustomerMerging()
                        {
                            CardCode=dr["CardCode"].ToString(),
                            CardName=dr["CardName"].ToString(),
                            DBName=dr["DBNAME"].ToString(),
                            CustTyp=dr["typ"].ToString(),
                            bgcolor=dr["bgcolor"].ToString(),
                            IsAlreadyMapped=Convert.ToBoolean(dr["IsAlreadyMapped"].ToString())
                        });
                    }
                }
            }
            catch (Exception)
            {

                Objlist = null;
            }
           


            return Objlist;
        }

        public List<RDD_CustomerMerging> GetRDDCustMerParent(string ParentCode,string ParentDbname)
        {

            List<RDD_CustomerMerging> Objlist = new List<RDD_CustomerMerging>();
            try
            {
                SqlParameter[] parm = { new SqlParameter("@Parent_DBName",ParentDbname),
                    new SqlParameter("@Parent_CardCode",ParentCode) };
                DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping_ParentCode", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_CustomerMerging()
                        {
                            CardCode = dr["CardCode"].ToString(),
                            CardName = dr["CardName"].ToString(),
                            DBName = dr["DBNAME"].ToString(),
                           
                        });
                    }
                }
            }
            catch (Exception)
            {

                Objlist = null;
            }



            return Objlist;
        }

        public RDD_customermapping  save(RDD_customermapping Cust)
        {
            RDD_customermapping rdd = new RDD_customermapping();
            try
            {
                int i = Cust.ChildLists.Count;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (Cust.EditFlag == false)
                    {
                        SqlParameter[] sqlpar = { new SqlParameter("@Parent_CardCode", Cust.Parent_CardCode),
                            new SqlParameter("@Parent_DBName", Cust.Parent_DBName),
                            new SqlParameter("@CustomerName",Cust.CustomerName),
                            new SqlParameter("@CreatedBy", Cust.CreatedBy)

                        };
                        rdd.saveflag = Com.ExecuteNonQuery("Insert_RDD_CustomerMapping", sqlpar);
                    }
                   
                    while (i > 0)
                    {
                        SqlParameter[] sqlpar1 = { new SqlParameter("@Parent_CardCode", Cust.Parent_CardCode),
                            new SqlParameter("@Parent_DBName", Cust.Parent_DBName),
                            new SqlParameter("@CreatedBy", Cust.CreatedBy),
                             new SqlParameter("@Child_CardCode", Cust.ChildLists[i-1].Child_CardCode),
                              new SqlParameter("@CustomerName",Cust.CustomerName),
                            new SqlParameter("@Child_CardName", Cust.ChildLists[i-1].Child_CardName),
                             new SqlParameter("@Child_DBName", Cust.ChildLists[i-1].Child_DBName)

                        };
                        rdd.saveflag = Com.ExecuteNonQuery("Insert_RDD_CustomerMapping", sqlpar1);
                        i--;
                    }

                    rdd.errormsg = "Save Succesfully";
                    scope.Complete();
                }
               
            }
            catch (Exception ex)
            {

                rdd.errormsg = ex.Message;
                rdd.saveflag = false;
            }


            return rdd;
        }



        public bool DeleteActivity(string code,string dbname,string typ)
        {

            bool t = false;
            try
            {
                SqlParameter[] parm = { new SqlParameter("@code",code),
                new SqlParameter("@typ",typ),
                    new SqlParameter("@dbname",dbname) };
                t = Com.ExecuteNonQuery("Delete_RDD_Customermapping", parm);
            }
            catch (Exception)
            {

                t = false;
            }

           
            return t;
        }
    }
}
