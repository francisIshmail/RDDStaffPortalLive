using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.SAP
{
    public class SalesOrder_DBOperation
    {
        public DataSet Get_BindDDLList(string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("EXEC RDD_SOR_Get_DDL_Lists '" + dbname + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader GetCustomers(string prefix, string dbname, string field)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("RDD_SOR_GetList_Customers '" + prefix + "','" + dbname + "','" + field + "'");

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader Get_CustomersDue_Info(string dbname, string cardcode)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("RDD_SOR_Get_CustomerDue '" + cardcode + "','" + dbname + "'");

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader Get_PayTerms_Days(string dbname, string groupnum)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("Select ExtraDays From [" + dbname + "].[dbo].[OCTG] Where GroupNum= " + groupnum);

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader GetItemList(string prefix, string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("Exec RDD_SOR_GetList_ItemCode '" + prefix + "','" + dbname + "'");

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_ActiveOPGSelloutList(string basedb, string rebatedb, string itemcode)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("EXEC RDD_SOR_Get_ActiveOPGSelloutList '" + basedb + "','" + rebatedb + "','" + itemcode + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_WarehouseQty(string itemcode, string whscode, string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Select Convert(Numeric(10,2),T1.OnHand) OnHand,Convert(Numeric(10,2),T1.OnHand-(T1.IsCommited+T1.OnOrder)) ActalQty  From [" + dbname + "].[dbo].[OITW] T1 Where T1.ItemCode='" + itemcode + "' And T1.WhsCode='" + whscode + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_GPAndGPPer(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Execute RDD_SOR_Get_GPAndGPPer '" + dbname + "', '" + itemcode + "', '" + warehouse + "', " + qtysell + ", " + pricesell + ", '" + curr + "', '" + opgrebateid + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_TaxCodeRate(string taxcode, string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS(" Select Convert(Numeric(10,2),Rate) Rate  From [" + dbname + "].[dbo].[OVTG] T1 Where T1.Code='" + taxcode + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
