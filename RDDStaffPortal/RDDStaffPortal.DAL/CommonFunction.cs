﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace RDDStaffPortal.DAL
{
    public class CommonFunction
    {
        SqlConnection SqlConn = null;
        SqlCommand SqlCmd = null;
        SqlDataAdapter da = null;
        SqlTransaction trans = null;
        string errormsg;
        DataSet ds = null;
        string Conn;
        public CommonFunction()
        {
            Conn = ConfigurationManager.ConnectionStrings["tejSAP"].ToString();
        }     
        public  partial class Outcls{
            public bool Outtf { get; set; }
            public string Responsemsg { get; set; }
        }
        /*Last Parameter As Out Parameter*/
        #region Sp_ForInsDelUpdDataOut
        public List<Outcls> ExecuteNonQueryList(string SqlCommondText, SqlParameter[] p)
        {
            errormsg = "";
            bool t = false;
            List<Outcls> str1 = new List<Outcls>();
            str1.Clear();
            using (SqlConn = new SqlConnection(Conn))
            {
                SqlConn.Open();
                trans = SqlConn.BeginTransaction();
                using (SqlCmd = new SqlCommand(SqlCommondText, SqlConn, trans))
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    int k = p.Length;
                    int j = 0;
                    while (j < k-1)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    SqlCmd.Parameters.Add(p[k - 1].ParameterName, SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;                   
                    try
                    {
                        int i = SqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            trans.Commit(); 
                            t = true;
                            str1.Add(new Outcls
                            {
                                Outtf = t,
                                Responsemsg = SqlCmd.Parameters[p[k-1].ParameterName].Value.ToString()
                            });
                        };
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                        trans.Rollback();
                        t = false;
                        str1.Add(new Outcls
                        {
                            Outtf = t,
                            Responsemsg = errormsg
                        });
                    }
                    finally
                    {
                        trans.Dispose();
                    }
                }

            }
            return str1;
        }
        #endregion

        #region Sp_ForInsDelUpdData
        public bool ExecuteNonQuery(string SqlCommondText, SqlParameter[] p)
        {
            errormsg = "";
            bool t = false;
            using (SqlConn = new SqlConnection(Conn))
            {
                SqlConn.Open();
                trans = SqlConn.BeginTransaction();
                using (SqlCmd = new SqlCommand(SqlCommondText, SqlConn, trans))
                {                  
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    int k = p.Length;
                    int j = 0;
                    while (j < k)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    try
                    {                                              
                        int i = SqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            trans.Commit();
                            t = true;
                        };                       
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                        trans.Rollback();
                        t = false;
                    }
                    finally
                    { 
                        trans.Dispose();
                    }
                }

            }
            return t;
        }
        #endregion
        #region Sp_RetriveDataset
        public DataSet ExecuteDataSet(string SqlCommandText, CommandType cmdd, SqlParameter[] p)
        {
            using (SqlConn = new SqlConnection(Conn))
            {
                try
                {
                    SqlConn.Open();
                    da = new SqlDataAdapter(SqlCommandText, SqlConn);
                    da.SelectCommand.CommandTimeout = 0;
                    da.SelectCommand.CommandType = cmdd;
                    da.SelectCommand.Parameters.AddRange(p);
                    ds = new DataSet();
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                }
            }

            return ds;
        }
        #endregion

        #region RetriveDataset
        public DataSet ExecuteDataSet(string SqlCommandText)
        {
            using (SqlConn = new SqlConnection(Conn))
            {
                try
                {
                    SqlConn.Open();
                    da = new SqlDataAdapter(SqlCommandText, SqlConn);
                    da.SelectCommand.CommandTimeout = 0;                  
                    ds = new DataSet();
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                }
            }

            return ds;
        }
        #endregion


        #region Sp_RetriveSingleValueDecimal
        public decimal ExecuteScalarDec(string sqlCommandText, SqlParameter[] p)
        {
            decimal numrows = 0;
            using (SqlConn = new SqlConnection(Conn))
            {
                SqlConn.Open();
                using (SqlCmd = new SqlCommand(sqlCommandText, SqlConn))
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    int k = p.Length;
                    int j = 0;
                    while (j < k)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    try
                    {
                        object o = SqlCmd.ExecuteScalar();
                        if (o != null)
                        {
                            numrows = Convert.ToDecimal(o.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                    }
                }
            }
            return numrows;
        }
        #endregion
        #region Sp_RetriveSingleValueString
        public string ExecuteScalars(string sqlCommandText, SqlParameter[] p)
        {
            string numrows = "";
            using (SqlConn = new SqlConnection(Conn))
            {
                SqlConn.Open();
                using (SqlCmd = new SqlCommand(sqlCommandText, SqlConn))
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    int k = p.Length;
                    int j = 0;
                    while (j < k)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    try
                    {
                        object o = SqlCmd.ExecuteScalar();
                        if (o != null)
                        {
                            numrows = o.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                    }
                }
            }
            return numrows;
        }
        #endregion
        #region Sp_RetriveSingleValueDate
        public DateTime ExecuteScalarDte(string sqlCommandText, SqlParameter[] p)
        {
            DateTime? numrows = null;
            using (SqlConn = new SqlConnection(Conn))
            {
                SqlConn.Open();
                using (SqlCmd = new SqlCommand(sqlCommandText, SqlConn))
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    int k = p.Length;
                    int j = 0;
                    while (j < k)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    try
                    {
                        object o = SqlCmd.ExecuteScalar();
                        if (o != null)
                        {
                            numrows =Convert.ToDateTime(o.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                    }
                }
            }
            return numrows.Value;
        }
        #endregion
    }


}

