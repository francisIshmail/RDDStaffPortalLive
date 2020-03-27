using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace RDDStaffPortal.DAL
{
    public static class Db
    {
        public static String constr;

        //public Db(String conn)
        //{
        //    constr = conn; 
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        public static SqlDataReader myGetReader(String Sql)
        {
            SqlDataReader drd;
            SqlConnection dbconn = new SqlConnection(constr);
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(Sql, dbconn);
            drd = cmd.ExecuteReader();
            return drd;
        }

        public static DataSet myGetDS(String Sql)
        {
            DataSet ds = new DataSet();
            SqlConnection dbconn = new SqlConnection(constr);
            dbconn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Sql, dbconn);
            da.Fill(ds, "Table");
            dbconn.Close();

            return ds;
        }

        public static void myExecuteSQL(String Sql)
        {

            SqlConnection dbconn = new SqlConnection(constr);
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(Sql, dbconn);
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                throw (ex);
            }
        }

        public static long myExecuteSQLReturnLatestAutoID(String Sql)
        {
            int AutoID = -1;
            SqlConnection dbconn = new SqlConnection(constr);
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(Sql + "; SELECT CAST(scope_identity() as int) AS LastID", dbconn);
            try
            {
                AutoID = (int)cmd.ExecuteScalar();
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                return AutoID;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                throw (ex);
            }
        }

        public static int myExecuteScalar(String Sql)
        {
            int val = -1;
            SqlConnection dbconn = new SqlConnection(constr);
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(Sql, dbconn);
            try
            {
                val = (int)cmd.ExecuteScalar();
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                return val;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                throw (ex);
            }
        }

        public static decimal myExecuteScalar3(String Sql)
        {
            decimal val = -1;
            SqlConnection dbconn = new SqlConnection(constr);
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(Sql, dbconn);
            try
            {
                val = (decimal)cmd.ExecuteScalar();
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                return val;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                throw (ex);
            }
        }


        public static string myExecuteScalar2(String Sql)
        {
            string val = "-1";
            SqlConnection dbconn = new SqlConnection(constr);
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(Sql, dbconn);
            try
            {
                val = (string)cmd.ExecuteScalar();
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                return val;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                dbconn.Close();
                dbconn.Dispose();
                throw (ex);
            }
        }


    }
}
