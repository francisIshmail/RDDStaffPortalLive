using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RDDStaffPortal.DAL.InitialSetup
{
   public class TestDbOperation
    {
        CommonFunction Com = new CommonFunction();
        /*Save & Update Data Operation*/
        public RDD_Test save1(RDD_Test test)
        {           
            StringBuilder sb = new StringBuilder();
            var Drecord = new List<string>();
            if (!test.EditFlag)
            {               
                int i = test.RDD_TestDetailnew.Count;
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        while (i > 0)
                        {
                            int k = Com.ExecuteScalar("select count(*) from RDD_Test where Code='" + test.RDD_TestDetailnew[i - 1].CODE + "'");
                            if (k == 0)
                            {
                                sb.Clear();
                                sb.Append("(Code,");
                                sb.Append("IsDefault,");
                                sb.Append("Descriptions)");
                                sb.Append("values");
                                sb.Append("('" + test.RDD_TestDetailnew[i - 1].CODE + "',");
                                sb.Append("'" + test.RDD_TestDetailnew[i - 1].IsDefault + "',");
                                sb.Append("'" + test.RDD_TestDetailnew[i - 1].DESCRIPTION + "')");
                                Com.ExecuteNonQuery("insert into RDD_Test" + sb + "");
                            }
                            else
                            {
                                Drecord.Add( test.RDD_TestDetailnew[i - 1].CODE);
                            }

                            i--;
                        }
                        test.SaveFlag = true;
                        test.ErrorMessage = "";
                        test.Drecord = Drecord;

                        scope.Complete();
                    }

                }
                catch (Exception)
                {
                    test.SaveFlag = false;
                    test.ErrorMessage = "Error Occur";
                    
                }               
            }
            else
            {
                try
                {
                    if (test.CellNo == 0) {
                        sb.Clear();
                        sb.Append("Descriptions=");
                        sb.Append("'" + test.RDD_TestDetailnew[0].DESCRIPTION + "', ");
                        sb.Append("IsDefault=");
                        sb.Append("'" + test.RDD_TestDetailnew[0].IsDefault + "' ");

                        test.CODE = test.RDD_TestDetailnew[0].CODE;
                    }
                    else
                    {
                        var ColumName = "";
                        switch (test.CellNo)
                        {
                           
                            case 1:
                                ColumName = "Descriptions";
                                break;
                            case 2:
                                ColumName = "IsDefault";
                                break;


                        }
                        sb.Clear();
                        sb.Append(""+ColumName+"=");
                        sb.Append("'" + test.SingleVal + "' ");
                    }
                    using (TransactionScope scope =new TransactionScope())
                    {
                        Com.ExecuteNonQuery("update RDD_Test set " + sb + " where Code='" + test.CODE + "'");
                        scope.Complete();
                    }
                    

                    test.SaveFlag = true;
                    test.ErrorMessage = "";
                    test.Drecord = Drecord;

                }
                catch (Exception)
                {

                    test.SaveFlag = false;
                    test.ErrorMessage = "Error Occur";
                }               
            }          
            return test;
        }
        /*Get LIST Operation*/
        public List<RDD_Test> GetList()
        {
            List<RDD_Test> ObjList = new List<RDD_Test>();
            DataSet ds=  Com.ExecuteDataSet("Select * from RDD_Test");
            DataTable dt = ds.Tables[0];
            DataRowCollection drc = dt.Rows;
            foreach (DataRow dr in drc)
            {
                ObjList.Add(new RDD_Test()
                {                 
                    CODE = !string.IsNullOrWhiteSpace(dr["CODE"].ToString()) ? dr["CODE"].ToString() : "",
                    DESCRIPTION = !string.IsNullOrWhiteSpace(dr["DESCRIPTIONS"].ToString()) ? dr["DESCRIPTIONS"].ToString() : "",
                    IsDefault    = !string.IsNullOrWhiteSpace(dr["IsDefault"].ToString()) ? Convert.ToBoolean(dr["IsDefault"].ToString()) : false

                });
            }

            return ObjList;

        }
        /*Delete Operation*/
        public bool DeleteFlag(string Code)
        {
            bool tf = false;
            try
            {
                using(TransactionScope scope=new TransactionScope())
                {
                    int n = Com.ExecuteNonQuery("delete from RDD_Test where Code='" + Code + "'");
                    if (n > 0)
                    {
                        tf = true;
                    }
                    scope.Complete();
                }
                

            }
            catch (Exception)
            {

                tf = false;
            }

           
            return tf;
        }
    }
}
