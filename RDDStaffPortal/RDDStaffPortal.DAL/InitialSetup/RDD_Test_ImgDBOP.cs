using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.InitialSetup
{
    public class RDD_Test_ImgDBOP
    {
        CommonFunction Com = new CommonFunction();

        public RDD_test_img save1(RDD_test_img test)
        {
            StringBuilder sb = new StringBuilder();
            if (!test.EditFlag)
            {
                string response = string.Empty;
                try
                {
                    using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        SqlTransaction transaction;
                        using (transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                byte[] file;
                                using (var stream = new FileStream(test.LogoPath, FileMode.Open, FileAccess.Read))
                                {
                                    using (var reader = new BinaryReader(stream))
                                    {
                                        file = reader.ReadBytes((int)stream.Length);
                                    }
                                }
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = "insert into RDD_test_img(image1,imgtyp) values(@image1,@imgtyp)";
                                cmd.Connection = connection;
                                cmd.Transaction = transaction;
                                cmd.Parameters.AddWithValue("@image1", file);
                                cmd.Parameters.AddWithValue("@imgtyp", test.imgtyp);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                response = "Error occured : " + ex.Message;
                                transaction.Rollback();
                            }
                            finally
                            {
                                if (connection.State == ConnectionState.Open)
                                {
                                    connection.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    response = "Error occured : " + ex.Message;



                }

              
            }

            return test;
        }
    }
}
