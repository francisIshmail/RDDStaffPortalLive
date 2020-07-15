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
    }
}
