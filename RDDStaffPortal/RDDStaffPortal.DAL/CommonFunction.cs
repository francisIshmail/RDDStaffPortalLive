using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Globalization;
using OfficeOpenXml;

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
        public partial class Outcls1
        {
            public bool Outtf { get; set; }

            public int Id { get; set; }
            public string Responsemsg { get; set; }
        }

        public bool SqlBulkCopyQuery(DataTable dt,string tblname, SqlParameter[] p)
        {
            bool t = false;
            try
            {
                using (SqlConnection con = new SqlConnection(Conn))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = tblname;

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        int k = p.Length;
                        int j = 0;
                        while (j < k )
                        {
                            sqlBulkCopy.ColumnMappings.Add(p[j].SourceColumn, p[j].ParameterName);
                            j = j + 1;
                        }
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
                t = true;
            }
            catch (Exception ex)
            {

                t = false;
            }
            
            return t;
        }
        public int ExecuteNonQuery(string SqlCommondText)
        {
            int rowaffected = 0;
            using (SqlConnection cn = new SqlConnection(Conn))
            {
                try
                {                   
                    cn.Open();                   
                    SqlCmd = new SqlCommand(SqlCommondText, cn);
                    rowaffected = SqlCmd.ExecuteNonQuery();                   
                    cn.Close();
                }
                catch (Exception ex)
                {
                    if (trans != null)
                        trans.Rollback();
                    throw;
                }
            }
            return rowaffected;
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
                        //if (i > 0)
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

        #region Sp_ForInsDelUpdDataOut
        public List<Outcls1> ExecuteNonQueryListID(string SqlCommondText, SqlParameter[] p)
        {
            errormsg = "";
            bool t = false;
            List<Outcls1> str1 = new List<Outcls1>();
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
                    while (j < k - 2)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    SqlCmd.Parameters.Add(p[k - 2].ParameterName, SqlDbType.Int).Direction = ParameterDirection.Output;
                    SqlCmd.Parameters.Add(p[k - 1].ParameterName, SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                    try
                    {
                        int i = SqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            trans.Commit();
                            t = true;
                            str1.Add(new Outcls1
                            {
                                Outtf = t,
                                Id= Convert.ToInt32(SqlCmd.Parameters[p[k - 2].ParameterName].Value.ToString()),
                                Responsemsg = SqlCmd.Parameters[p[k - 1].ParameterName].Value.ToString()
                            });
                        }
                        else
                        {
                            t = false;
                            str1.Add(new Outcls1
                            {
                                Outtf = t,
                                Id = Convert.ToInt32(SqlCmd.Parameters[p[k - 2].ParameterName].Value.ToString()),
                                Responsemsg = SqlCmd.Parameters[p[k - 1].ParameterName].Value.ToString()
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                        trans.Rollback();
                        t = false;
                        str1.Add(new Outcls1
                        {
                            Outtf = t,
                            Id=-1,
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
                        SqlCmd.CommandTimeout = 0;
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

        public string ExecuteScalar1(string sqlCommandText)
        {
            string numrows = "";
            using (SqlConnection cn = new SqlConnection(Conn))
            {
                try
                {
                    cn.Open();
                    SqlCmd = new SqlCommand(sqlCommandText, cn);
                    numrows = (string)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                }
            }
            return numrows;
        }
        public int ExecuteScalar(string sqlCommandText)
        {
            int numrows = 0;
            using (SqlConnection cn = new SqlConnection(Conn))
            {
                try
                {                   
                    cn.Open();
                    SqlCmd = new SqlCommand(sqlCommandText, cn);
                    numrows = (int)SqlCmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                }
            }
            return numrows;
        }
        #region Sp_RetriveSingleValueDecimal
        public int ExecuteScalar(string sqlCommandText, SqlParameter[] p)
        {
           int numrows = 0;
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
                            numrows = Convert.ToInt32(o.ToString());
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
          
        public object ExecuteScalar(string sql, SqlParameter[] p, CommandType _CommandType)
        {
            SqlConnection con = new SqlConnection(Conn);
            object retval = null;
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = _CommandType;
                cmd.CommandTimeout = 0;
                if (p != null)
                {
                    for (int i = 0; i <= p.Length - 1; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                retval = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return retval;
        }

        public DataTable ExcelToDataTable(ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            }

            for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                var newRow = table.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                table.Rows.Add(newRow);
            }
            return table;
        }
        public DataTable RemoveBlankRow(DataTable dtRomoveBlank)
        {

            for (int h = 0; h < dtRomoveBlank.Rows.Count; h++)
            {
                if (dtRomoveBlank.Rows[h].IsNull(0) == true)
                {

                    dtRomoveBlank.Rows.RemoveAt(h);
                    h = 0;
                }
            }
            return dtRomoveBlank;
        }
        public DataTable ChangeColumnDataType(DataTable table)
        {
            List<string> columnName = new List<string>();
            try
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    columnName.Add(table.Columns[i].ColumnName);
                }
                foreach (string ColName in columnName)
                {
                    DataColumn newcolumn = new DataColumn("temporary", typeof(string));
                    table.Columns.Add(newcolumn);
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            row["temporary"] = Convert.ChangeType(row[ColName], typeof(string));
                        }
                        catch
                        {
                        }
                    }
                    table.Columns.Remove(ColName);
                    newcolumn.ColumnName = ColName;
                }
            }
            catch (Exception)
            {
                return new DataTable();
            }

            return table;
        }
        public DataTable SettiingDataTableHeaderAsList<T>(DataTable dt, List<T> items)
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var i = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dc.ColumnName = Props[i].Name;
                i++;
            }
            return dt;
        }
        public List<T> ConvertDataTableToClassObjectList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, Convert.ToString(dr[column.ColumnName]), null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}


