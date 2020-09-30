using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;

/// <summary>
/// Summary description for importOrder
/// </summary>
public class importOrder
{
	public importOrder()
	{
		// TODO: Add constructor logic here
	}

    public static string writePOToDbFromExcel(DataSet result1, int importOrderID, string importOrderUser, int processTypeId, string connecToStr)
    {
        //if order id is 0 it means new import, else edit case for this order id 
        string v0, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, vcomments, vtotal, vtotalcost, vtotalSell, vtotalMargin, vprodManager, vheadOffFin, v22;
        v0 = result1.Tables[0].Rows[0][6].ToString();
        v1 = result1.Tables[0].Rows[0][13].ToString();
        v2 = result1.Tables[0].Rows[1][13].ToString();
        v3 = result1.Tables[0].Rows[2][13].ToString();
        v4 = result1.Tables[0].Rows[3][13].ToString();
        v5 = result1.Tables[0].Rows[4][12].ToString();
        v6 = result1.Tables[0].Rows[6][8].ToString();
        v7 = result1.Tables[0].Rows[6][14].ToString();
        v8 = result1.Tables[0].Rows[7][1].ToString();
        v9 = result1.Tables[0].Rows[7][2].ToString();
        v10 = result1.Tables[0].Rows[7][3].ToString();
        v11 = result1.Tables[0].Rows[7][4].ToString();
        v12 = result1.Tables[0].Rows[7][5].ToString();
        v13 = result1.Tables[0].Rows[7][6].ToString();
        v14 = result1.Tables[0].Rows[7][7].ToString();
        v15 = result1.Tables[0].Rows[7][8].ToString();
        v16 = result1.Tables[0].Rows[7][9].ToString();
        v17 = result1.Tables[0].Rows[7][10].ToString();
        v18 = result1.Tables[0].Rows[7][11].ToString();
        v19 = result1.Tables[0].Rows[7][12].ToString();
        v20 = result1.Tables[0].Rows[7][13].ToString();
        v21 = result1.Tables[0].Rows[7][14].ToString();

        if (v0 == "VENDOR" && v1 == "EVO PO NO" && v2 == "PO DATE" && v3 == "REQ DEL DATE" && v4 == "OPG CODE" && v5 == "CBN NO" && v6 == "Stock Available " && v7 == "ORDER TYPE" && v8 == "CUST" && v9 == "REG" && v10 == "Part" && v11 == "SMALL" && v12 == "QTY" && v13 == "CURR" && v14 == "Amount" && v15 == "REBATE" && v16 == "COST" && v17 == "TOTAL" && v18 == "SELLING" && v19 == "TOTAL" && v20 == "MARGIN" && v21 == "BTB/STOCK")
        {
            vtotal = "";
            vtotalcost = "";
            vtotalSell = "";
            vtotalMargin = "";
            vprodManager = "";
            vheadOffFin = "";
            vcomments = "";
            string qry;
            v0 = result1.Tables[0].Rows[0][7].ToString();
            v1 = result1.Tables[0].Rows[0][14].ToString();
            vcomments = result1.Tables[0].Rows[3][4].ToString();
            v2 = result1.Tables[0].Rows[1][14].ToString();
            v3 = result1.Tables[0].Rows[2][14].ToString();
            v4 = result1.Tables[0].Rows[3][14].ToString();
            v5 = result1.Tables[0].Rows[4][13].ToString();

            for (int n = 10; n < result1.Tables[0].Rows.Count; n++)
            {
                if (result1.Tables[0].Rows[n][6].ToString() == "TOTAL")
                {
                    if (result1.Tables[0].Rows[n][7].ToString() == "")
                    {
                        vtotal = "0";
                    }
                    else
                    {
                        vtotal = result1.Tables[0].Rows[n][7].ToString();
                    }

                    if (result1.Tables[0].Rows[n][10].ToString() == "")
                    {
                        vtotalcost = "0";
                    }
                    else
                    {
                        vtotalcost = result1.Tables[0].Rows[n][10].ToString();
                    }

                    if (result1.Tables[0].Rows[n][12].ToString() == "")
                    {
                        vtotalSell = "0";
                    }
                    else
                    {
                        vtotalSell = result1.Tables[0].Rows[n][12].ToString();
                    }

                    if (result1.Tables[0].Rows[n][13].ToString() == "")
                    {
                        vtotalMargin = "0";
                    }
                    else
                    {
                        vtotalMargin = result1.Tables[0].Rows[n][13].ToString();
                    }
                    break;
                }
            }

            for (int x = 10; x < result1.Tables[0].Rows.Count; x++)
            {
                if (result1.Tables[0].Rows[x][1].ToString() == "PRODUCT MANAGER")
                {
                    vprodManager = result1.Tables[0].Rows[x + 1][1].ToString();
                    vheadOffFin = result1.Tables[0].Rows[x + 1][9].ToString();
                    break;
                }
            }

            string cmts = "";
            int sts, esc;
            SqlDataReader sdrd;
            Db.constr = connecToStr;
            sdrd = Db.myGetReader("select nextprocessStatusID,nextRole from dbo.processStatus where processStatusID=1 and fk_processId=" + processTypeId.ToString());
            sdrd.Read();
            sts = Convert.ToInt32(sdrd["nextprocessStatusID"]);
            esc = Convert.ToInt32(sdrd["nextRole"]);
            sdrd.Close();

            if (importOrderID == 0)
            {
                qry = string.Format("INSERT INTO PurchaseOrders(vendor,evoPoNo,comments,PoDate,reqDelDate,opgCode,cbnNo,total,totalCostAfterRebate,totalSelling,margin,productManager,headOfFinance) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},{9},{10},'{11}','{12}')", v0, v1, vcomments, v2, v3, v4, v5, vtotal, vtotalcost, vtotalSell, vtotalMargin, vprodManager, vheadOffFin);
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);

                qry = "select max(poId) from PurchaseOrders";
                Db.constr = connecToStr;
                v22 = Db.myExecuteScalar(qry).ToString();

                string cdate = DateTime.Now.ToString();
                cmts = "New";

                //qry = "insert into [dbo].[orderRequest] values(" + v22 + ",'',2,'" + importOrderUser + "','" + cdate + "',1,'PO','" + v1 + "')";
                qry = "insert into processRequest(refId,refValue,comments,fk_statusId,ByUser,lastModified,fk_EscalateLevelId,fk_processId) values(" + v22 + ",'" + v1 + "','" + cmts + "'," + sts.ToString() + ",'" + importOrderUser + "','" + cdate + "'," + esc.ToString() + "," + processTypeId.ToString() + ")";
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);

                int tmp;
                //qry = "select max(processRequestId) from orderRequest";
                qry = "select max(processRequestId) from processRequest";
                Db.constr = connecToStr;
                tmp = Db.myExecuteScalar(qry);
                //qry = "insert into dbo.orderStatusTrack values(" + tmp + ",2,1,'" + importOrderUser + "','Accept','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','')";
                qry = "insert into processStatusTrack(fk_processRequestId,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + tmp + "," + sts.ToString() + "," + esc.ToString() + ",'" + importOrderUser + "','Created','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + cmts + "'," + processTypeId.ToString() + ")";
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);

            }
            else
            {
                v22 = importOrderID.ToString();
                qry = "update PurchaseOrders set vendor='" + v0 + "',evoPoNo='" + v1 + "',comments='" + vcomments + "',PoDate='" + v2 + "',reqDelDate='" + v3 + "',opgCode='" + v4 + "',cbnNo='" + v5 + "',total=" + vtotal + ",totalCostAfterRebate=" + vtotalcost + ",totalSelling=" + vtotalSell + ",margin=" + vtotalMargin + ",productManager='" + vprodManager + "',headOfFinance='" + vheadOffFin + "'  where poId=" + v22 + "";
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);

                string cdate = DateTime.Now.ToString();
                cmts = "Re-Created";
                //qry = "update [dbo].[orderRequest] set orderNo='" + v1 + "', lastModified='" + cdate + "' where orderId=" + v22 + " and orderType='PO'";
                qry = "update [dbo].[processRequest] set refValue='" + v1 + "',comments='" + cmts + "', lastModified='" + cdate + "' where refId=" + v22 + " and fk_processId=" + processTypeId.ToString();
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);

                qry = "delete from dbo.PurchaseOrderlines where fk_poId=" + v22 + "";
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);

                int tmp;
                //qry = "select processRequestId from dbo.orderRequest where orderId=" + v22 + " and orderType='PO'";
                qry = "select processRequestId from dbo.processRequest where refId=" + v22 + " and fk_processId=" + processTypeId.ToString();
                Db.constr = connecToStr;
                tmp = Db.myExecuteScalar(qry);
                //qry = "insert into dbo.orderStatusTrack values(" + tmp + ",2,1,'" + importOrderUser + "','Accept','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','')";
                qry = "insert into processStatusTrack(fk_processRequestId,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + tmp + "," + sts.ToString() + "," + esc.ToString() + ",'" + importOrderUser + "','Modified','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + cmts + "'," + processTypeId.ToString() + ")";
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);
            }


            qry = "begin BEGIN TRANSACTION ";


            for (int i = 10; i < result1.Tables[0].Rows.Count; i++)
            {
                if (result1.Tables[0].Rows[i][0].ToString() != "")
                {
                    v6 = result1.Tables[0].Rows[i][0].ToString();
                    v7 = result1.Tables[0].Rows[i][1].ToString();
                    v8 = result1.Tables[0].Rows[i][2].ToString();
                    v9 = result1.Tables[0].Rows[i][3].ToString();
                    v10 = result1.Tables[0].Rows[i][4].ToString();

                    if (result1.Tables[0].Rows[i][5].ToString() == "")
                    {
                        v11 = "0";
                    }
                    else
                    {
                        v11 = result1.Tables[0].Rows[i][5].ToString();
                    }

                    if (result1.Tables[0].Rows[i][6].ToString() == "")
                    {
                        v12 = "0";
                    }
                    else
                    {
                        v12 = result1.Tables[0].Rows[i][6].ToString();
                    }

                    if (result1.Tables[0].Rows[i][7].ToString() == "")
                    {
                        v13 = "0";
                    }
                    else
                    {
                        v13 = result1.Tables[0].Rows[i][7].ToString();
                    }

                    if (result1.Tables[0].Rows[i][8].ToString() == "")
                    {
                        v14 = "0";
                    }
                    else
                    {
                        v14 = result1.Tables[0].Rows[i][8].ToString();
                    }

                    if (result1.Tables[0].Rows[i][9].ToString() == "")
                    {
                        v15 = "0";
                    }
                    else
                    {
                        v15 = result1.Tables[0].Rows[i][9].ToString();
                    }

                    if (result1.Tables[0].Rows[i][10].ToString() == "")
                    {
                        v16 = "0";
                    }
                    else
                    {
                        v16 = result1.Tables[0].Rows[i][10].ToString();
                    }

                    if (result1.Tables[0].Rows[i][11].ToString() == "")
                    {
                        v17 = "0";
                    }
                    else
                    {
                        v17 = result1.Tables[0].Rows[i][11].ToString();
                    }

                    if (result1.Tables[0].Rows[i][12].ToString() == "")
                    {
                        v18 = "0";
                    }
                    else
                    {
                        v18 = result1.Tables[0].Rows[i][12].ToString();
                    }

                    if (result1.Tables[0].Rows[i][13].ToString() == "")
                    {
                        v19 = "0";
                    }
                    else
                    {
                        v19 = result1.Tables[0].Rows[i][13].ToString();
                    }

                    v20 = result1.Tables[0].Rows[i][14].ToString();

                    qry = qry + "  " + string.Format("INSERT INTO PurchaseOrderlines(fk_poId,lineNum,customerName,region ,partNo,smallDescription,qty,currPrice ,amountTotal ,rebatePerUnit,CostAfterRebate ,totalCostAfterRebate,sellingPrice,totalSelling,margin,orderType) VALUES( {0},{1},'{2}','{3}','{4}','{5}',{6},{7},{8},{9},{10},{11},{12},{13},{14},'{15}')", v22, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20);

                }
            }
            qry = qry + "  IF @@ERROR = 0 COMMIT TRANSACTION ELSE ROLLBACK TRANSACTION end";
            try
            {
                Db.constr = connecToStr;
                Db.myExecuteSQL(qry);
                return ("Insertion Successfully Done");
            }
            catch (Exception ex)
            {
                return ("Error Creating rows, " + ex.Message);
            }

        }
        else
            return "Incompatible PO Format ! Seems to be invalid Excel file format for PO ,Can't process ";
    }

    public static string writeROToDbFromExcel(DataSet result1, int importOrderID, string importOrderUser, int processTypeId, string connecToStr)
    {
        //if order id is 0 it means new import, else edit case for this order id 
        string v0, v1, vqty, vnetTotall, vdiscount, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37, v38, v39, v40, v41, v42, v43, v44, v45, v46, v47, v48, v49, v50, v51, v52, v53, v54, v55, v56, v57, v58;
        v0 = result1.Tables[0].Rows[0][0].ToString();
        v1 = result1.Tables[0].Rows[0][3].ToString();
        v2 = result1.Tables[0].Rows[0][6].ToString();
        v3 = result1.Tables[0].Rows[0][8].ToString();
        v4 = result1.Tables[0].Rows[1][8].ToString();
        v5 = result1.Tables[0].Rows[2][1].ToString();
        v6 = result1.Tables[0].Rows[2][4].ToString();
        v7 = result1.Tables[0].Rows[2][6].ToString();
        v8 = result1.Tables[0].Rows[2][8].ToString();
        v9 = result1.Tables[0].Rows[3][0].ToString();
        v10 = result1.Tables[0].Rows[4][0].ToString();
        v11 = result1.Tables[0].Rows[6][0].ToString();
        v12 = result1.Tables[0].Rows[7][0].ToString();
        v13 = result1.Tables[0].Rows[8][0].ToString();
        v14 = result1.Tables[0].Rows[11][0].ToString();
        v15 = result1.Tables[0].Rows[11][1].ToString();
        v16 = result1.Tables[0].Rows[11][5].ToString();
        v17 = result1.Tables[0].Rows[11][6].ToString();
        v18 = result1.Tables[0].Rows[11][7].ToString();
        v19 = result1.Tables[0].Rows[11][8].ToString();
        v20 = result1.Tables[0].Rows[11][9].ToString();
        v21 = result1.Tables[0].Rows[11][10].ToString();
        v22 = result1.Tables[0].Rows[11][11].ToString();
        if (v0 == "RELEASE FORM" && v1 == "CUST ORDER NO" && v2 == "V.O. STATUS" && v3 == "RO NO." && v4 == "RO DATE" && v5 == "BILL TO" && v6 == "SHIP TO" && v7 == "CONSIGNEE:" && v8 == "NOTIFY:" && v9 == "NAME" && v10 == "ADDRESS" && v11 == "CITY" && v12 == "COUNTRY" && v13 == "CONTACT" && v14 == "PART NO" && v15 == "DESCRIPTION" && v16 == "VIRTUAL WHS" && v17 == "QTY" && v18 == "UNIT SELLING PRICE" && v19 == "TOTAL\nSELLING PRICE" && v20 == "UNIT COST PRICE" && v21 == "TOTAL COST PRICE" && v22 == "MARGIN %")
        {
            int l = 0;
            int i = 12;
            int r = 0;
            string var, var1;
            for (int m = 10; m < i; m++)
            {
                if (r == 0)
                {
                    var = "";
                    var = result1.Tables[0].Rows[m][0].ToString();
                    if (var == "PART NO")
                    {
                        for (r = m; r < result1.Tables[0].Rows.Count; r++)
                        {
                            var1 = "";
                            var1 = result1.Tables[0].Rows[r][0].ToString();
                            if (var1 == "")
                            {
                                l = r;

                                break;
                            }
                        }
                    }
                    i++;
                }
                if (r > 0)
                {
                    break;
                }
            }

            if (result1.Tables[0].Rows[l][7].ToString() == "")
            {
                string var2;
                var2 = "NET TOTAL";
                for (int c = l; c < result1.Tables[0].Rows.Count; c++)
                {
                    if (result1.Tables[0].Rows[c][7].ToString() == var2)
                    {
                        l = c;
                        break;

                    }
                }

            }

            v23 = result1.Tables[0].Rows[l][7].ToString();
            v24 = result1.Tables[0].Rows[l + 1][7].ToString();
            v25 = result1.Tables[0].Rows[l + 2][7].ToString();
            v26 = result1.Tables[0].Rows[l + 3][0].ToString();
            v27 = result1.Tables[0].Rows[l + 3][1].ToString();
            v28 = result1.Tables[0].Rows[l + 3][2].ToString();
            v29 = result1.Tables[0].Rows[l + 3][3].ToString();
            v30 = result1.Tables[0].Rows[l + 4][6].ToString();
            v31 = result1.Tables[0].Rows[l + 4][8].ToString();
            v32 = result1.Tables[0].Rows[l + 11][0].ToString();
            v33 = result1.Tables[0].Rows[l + 11][3].ToString();
            v34 = result1.Tables[0].Rows[l + 14][3].ToString();
            v35 = result1.Tables[0].Rows[l + 15][6].ToString();
            v36 = result1.Tables[0].Rows[l + 16][4].ToString();
            if (v23 == "NET TOTAL" && v24 == "DISCOUNT" && v25 == "GRAND TOTAL" && v26 == "TERMS" && v27 == "TICK" && v28 == "DAYS" && v29 == "DETAILED DESCRIPTION" && v30 == "Special Shipping Intructions" && v31 == "Special  Instructions" && v32 == "MODE OF SHIPMENT" && v33 == "PACKING" && v34 == "LABEL/MARK" && v35 == "DOCS REDQ BY CUST" && v36 == "YES/NO")
            {
                string qry;
                v0 = result1.Tables[0].Rows[0][9].ToString();
                v1 = result1.Tables[0].Rows[1][9].ToString();
                if (result1.Tables[0].Rows[l][6].ToString() == "")
                {
                    vqty = result1.Tables[0].Rows[l - 1][6].ToString();
                }
                else
                {
                    vqty = result1.Tables[0].Rows[l][6].ToString();
                }

                if (result1.Tables[0].Rows[l][8].ToString() == "")
                {
                    vnetTotall = "0";
                }
                else
                {
                    vnetTotall = result1.Tables[0].Rows[l][8].ToString();
                }

                if (result1.Tables[0].Rows[l + 1][8].ToString() == "")
                {
                    vdiscount = "0";
                }
                else
                {
                    vdiscount = result1.Tables[0].Rows[l + 1][8].ToString();
                }

                v2 = result1.Tables[0].Rows[l + 2][8].ToString();
                v3 = result1.Tables[0].Rows[3][1].ToString();
                v4 = result1.Tables[0].Rows[4][1].ToString();
                v5 = result1.Tables[0].Rows[6][1].ToString();
                v6 = result1.Tables[0].Rows[7][1].ToString();
                v7 = result1.Tables[0].Rows[8][1].ToString();
                v8 = result1.Tables[0].Rows[3][4].ToString();
                v9 = result1.Tables[0].Rows[4][4].ToString();
                v10 = result1.Tables[0].Rows[6][4].ToString();
                v11 = result1.Tables[0].Rows[7][4].ToString();
                v12 = result1.Tables[0].Rows[8][4].ToString();
                v13 = result1.Tables[0].Rows[3][6].ToString();
                v14 = result1.Tables[0].Rows[4][6].ToString();
                v15 = result1.Tables[0].Rows[6][6].ToString();
                v16 = result1.Tables[0].Rows[7][6].ToString();
                v17 = result1.Tables[0].Rows[8][6].ToString();
                v18 = result1.Tables[0].Rows[3][8].ToString();
                v19 = result1.Tables[0].Rows[4][8].ToString();
                v20 = result1.Tables[0].Rows[6][8].ToString();
                v21 = result1.Tables[0].Rows[7][8].ToString();
                v22 = result1.Tables[0].Rows[8][8].ToString();
                int len = 0;
                if (result1.Tables[0].Rows[l + 4][1].ToString() == "YES")
                {
                    v23 = "1";
                }
                else
                {
                    v23 = "0";
                }

                if (result1.Tables[0].Rows[l + 4][2].ToString() == "")
                {
                    v24 = "";
                }
                else
                {
                    v24 = result1.Tables[0].Rows[l + 1][2].ToString();
                }


                if (result1.Tables[0].Rows[l + 5][1].ToString() == "Yes")
                {
                    v25 = "1";
                }
                else
                {
                    v25 = "0";
                }

                if (result1.Tables[0].Rows[l + 5][2].ToString() == "")
                {
                    v26 = "";
                }
                else
                {
                    v26 = result1.Tables[0].Rows[l + 5][2].ToString();
                }

                if (result1.Tables[0].Rows[l + 6][1].ToString() == "Yes")
                {
                    v27 = "1";
                }
                else
                {
                    v27 = "0";
                }

                if (result1.Tables[0].Rows[l + 6][2].ToString() == "")
                {
                    v28 = "";
                }
                else
                {
                    v28 = result1.Tables[0].Rows[l + 6][2].ToString();
                }

                if (result1.Tables[0].Rows[l + 7][1].ToString() == "Yes")
                {
                    v29 = "1";
                }
                else
                {
                    v29 = "0";
                }

                if (result1.Tables[0].Rows[l + 7][2].ToString() == "")
                {
                    v30 = "";
                }
                else
                {
                    v30 = result1.Tables[0].Rows[l + 7][2].ToString();
                }

                if (result1.Tables[0].Rows[l + 8][1].ToString() == "Yes")
                {
                    v31 = "1";
                }
                else
                {
                    v31 = "0";
                }

                if (result1.Tables[0].Rows[l + 8][2].ToString() == "")
                {
                    v32 = "";
                }
                else
                {
                    v32 = result1.Tables[0].Rows[l + 8][2].ToString();
                }

                v33 = result1.Tables[0].Rows[l + 4][3].ToString();
                v34 = result1.Tables[0].Rows[l + 5][6].ToString();
                v35 = result1.Tables[0].Rows[l + 5][8].ToString();

                if (result1.Tables[0].Rows[l + 12][1].ToString() == "")
                {
                    int k = 0;
                    string varmode;
                    varmode = "X";
                    for (int d = l + 12; d < result1.Tables[0].Rows.Count; d++)
                    {
                        if (result1.Tables[0].Rows[d][1].ToString() == varmode)
                        {
                             k = 1;
                            break;
                        }


                        if (result1.Tables[0].Rows[d + 1][1].ToString() == varmode)
                        {
                            k = 2;
                            break;
                        }


                        if (result1.Tables[0].Rows[d + 2][1].ToString() == varmode)
                        {
                            k = 3;
                            break;
                        }

                    }

                    v36 = k.ToString();
                }
                else
                {
                    v36 = "1";
                }


                if (result1.Tables[0].Rows[l + 12][4].ToString() == "")
                {
                    v37 = "SPECIAL";
                }
                else
                {
                    v37 = "NORMAL";
                }

                v38 = result1.Tables[0].Rows[l + 14][4].ToString();
                v39 = result1.Tables[0].Rows[l + 11][8].ToString();
                v40 = result1.Tables[0].Rows[l + 12][8].ToString();
                v41 = result1.Tables[0].Rows[l + 13][8].ToString();
                v42 = result1.Tables[0].Rows[l + 18][4].ToString();
                v43 = result1.Tables[0].Rows[l + 18][4].ToString();
                v44 = result1.Tables[0].Rows[l + 19][4].ToString();
                v45 = result1.Tables[0].Rows[l + 16][8].ToString();
                v46 = result1.Tables[0].Rows[l + 17][8].ToString();
                v47 = result1.Tables[0].Rows[l + 18][8].ToString();
                v48 = result1.Tables[0].Rows[l + 19][8].ToString();

                for (int e = 0; e < result1.Tables[0].Rows.Count; e++)
                {
                    if (result1.Tables[0].Rows[e][0].ToString() == "PART NO")
                    {
                        len = e;
                        break;
                    }
                }

                if (result1.Tables[0].Rows[len + 1][0].ToString() != "")
                {
                    string cmts = "";
                    int sts, esc;

                    SqlDataReader sdrd;
                    Db.constr = connecToStr;
                    sdrd = Db.myGetReader("select nextprocessStatusID,nextRole from dbo.processStatus where processStatusID=1 and fk_processId=" + processTypeId.ToString());
                    sdrd.Read();
                    sts = Convert.ToInt32(sdrd["nextprocessStatusID"]);
                    esc = Convert.ToInt32(sdrd["nextRole"]);
                    sdrd.Close();

                    if (importOrderID == 0)
                    {
                        qry = string.Format("INSERT INTO releaseOrders(releaseOrdNo,relOrdDate,qty,netTotal,discount,grandtotal,billToName,billToAddress,billToCity,billToCountry,billToContact,shipToName,shipToAddress,shipToCity,shipToCountry,shipToContact,consigneeToName,consigneeToAddress,consigneeToCity,consigneeToCountry,consigneeToContact,notifyToName,notifyToAddress,notifyToCity,notifyToCountry,notifyToContact,tickCDC,daysCDC,tickPDC,daysPDC,tickCash,daysCash,tickCAD,daysCAD,tickCredit,daysCredit,termsDescription,specShippingIns,specInstruction,shipmentModeID,ordPacking,ordPackingLabel,ordExWorks,ordFOB,ordCF,ordInspecMandatory,ordDocSubmission,ordBankSubmission,ordAWB,ordFinalInvoice,ordCOO,ordPackList) Values('{0}','{1}',{2},{3},{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}',{26},'{27}',{28},'{29}',{30},'{31}',{32},'{33}',{34},'{35}','{36}','{37}','{38}',{39},'{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}')", v0, v1, vqty, vnetTotall, vdiscount, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37, v38, v39, v40, v41, v42, v43, v44, v45, v46, v47, v48);
                        try
                        {
                            Db.constr = connecToStr;
                            Db.myExecuteSQL(qry);
                        }
                        catch (Exception exp)
                        {
                            return ("Error creating Order Header Row , " + exp.Message);
                        }

                        qry = "select max(roId) from releaseOrders";
                        Db.constr = connecToStr;
                        v49 = Db.myExecuteScalar(qry).ToString();

                        string cdate = DateTime.Now.ToString();
                        cmts = "New";

                        //qry = "insert into [dbo].[orderRequest] values(" + v49 + ",'',1,'" + importOrderUser + "','" + cdate + "',1,'RO','" + v0 + "')";
                        qry = "insert into processRequest(refId,refValue,comments,fk_statusId,ByUser,lastModified,fk_EscalateLevelId,fk_processId) values(" + v49 + ",'" + v0 + "','" + cmts + "'," + sts.ToString() + ",'" + importOrderUser + "','" + cdate + "'," + esc.ToString() + "," + processTypeId.ToString() + ")";
                        Db.constr = connecToStr;
                        Db.myExecuteSQL(qry);

                        int tmp;
                        //qry = "select max(processRequestId) from orderRequest";
                        qry = "select max(processRequestId) from processRequest";
                        Db.constr = connecToStr;
                        tmp = Db.myExecuteScalar(qry);
                        //qry = "insert into dbo.orderStatusTrack values(" + tmp + ",1,1,'" + importOrderUser + "','Accept','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','')";
                        qry = "insert into processStatusTrack(fk_processRequestId,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + tmp + "," + sts.ToString() + "," + esc.ToString() + ",'" + importOrderUser + "','Created','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + cmts + "',"+ processTypeId.ToString()+")";
                        Db.constr = connecToStr;
                        Db.myExecuteSQL(qry);
                    }
                    else
                    {
                        v22 = importOrderID.ToString();
                        v49 = importOrderID.ToString();

                        qry = string.Format("update releaseOrders set releaseOrdNo='" + v0 + "', relOrdDate='" + v1 + "', qty=" + vqty + ", netTotal=" + vnetTotall + ",discount=" + vdiscount + ", grandtotal=" + v2 + ", billToName='" + v3 + "', billToAddress='" + v4 + "', billToCity='" + v5 + "', billToCountry='" + v6 + "', billToContact='" + v7 + "',shipToName='" + v8 + "', shipToAddress='" + v9 + "', shipToCity='" + v10 + "', shipToCountry='" + v11 + "', shipToContact='" + v12 + "', consigneeToName='" + v13 + "', consigneeToAddress='" + v14 + "', consigneeToCity='" + v15 + "', consigneeToCountry='" + v16 + "', consigneeToContact='" + v17 + "',notifyToName='" + v18 + "', notifyToAddress='" + v19 + "', notifyToCity='" + v20 + "', notifyToCountry='" + v21 + "', notifyToContact='" + v22 + "', tickCDC=" + v23 + ", daysCDC='" + v24 + "', tickPDC=" + v25 + ", daysPDC='" + v26 + "', tickCash=" + v27 + ", daysCash='" + v28 + "', tickCAD=" + v29 + ", daysCAD='" + v30 + "',tickCredit=" + v31 + ",daysCredit='" + v32 + "',termsDescription='" + v33 + "',specShippingIns='" + v34 + "',specInstruction='" + v35 + "',shipmentModeID=" + v36 + ",ordPacking='" + v37 + "',ordPackingLabel='" + v38 + "',ordExWorks='" + v39 + "',ordFOB='" + v40 + "',ordCF='" + v41 + "',ordInspecMandatory='" + v42 + "',ordDocSubmission='" + v43 + "',ordBankSubmission='" + v44 + "',ordAWB='" + v45 + "' ,ordFinalInvoice='" + v46 + "',ordCOO='" + v47 + "',ordPackList='" + v48 + "' where roId=" + v22 + "");
                        try
                        {
                            Db.constr = connecToStr;
                            Db.myExecuteSQL(qry);
                        }
                        catch (Exception exp)
                        {
                            return ("Error creating Order Header Row , " + exp.Message);
                        }

                        string cdate = DateTime.Now.ToString();
                        cmts = "Re-Created";
                        //qry = "update [dbo].[orderRequest] set orderNo='" + v0 + "', lastModified='" + cdate + "' where orderId=" + v22 + " and orderType='RO'";
                        qry = "update [dbo].[processRequest] set refValue='" + v0 + "',comments='" + cmts + "',lastModified='" + cdate + "' where refId=" + v22 + " and fk_processId=" + processTypeId.ToString();
                        Db.constr = connecToStr;
                        Db.myExecuteSQL(qry);

                        qry = "delete from dbo.ReleaseOrderlines where fk_roId=" + v22 + "";
                        Db.constr = connecToStr;
                        Db.myExecuteSQL(qry);

                        int tmp;
                        //qry = "select processRequestId from dbo.orderRequest where orderId=" + v22 + " and orderType='RO'";
                        qry = "select processRequestId from dbo.processRequest where refId=" + v22 + " and fk_processId=" + processTypeId.ToString();
                        Db.constr = connecToStr;
                        tmp = Db.myExecuteScalar(qry);
                        //qry = "insert into dbo.orderStatusTrack values(" + tmp + ",1,1,'" + importOrderUser + "','Accept','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','')";
                        qry = "insert into processStatusTrack(fk_processRequestId,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + tmp + "," + sts.ToString() + "," + esc.ToString() + ",'" + importOrderUser + "','Modified','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + cmts + "'," + processTypeId.ToString() + ")";
                        Db.constr = connecToStr;
                        Db.myExecuteSQL(qry);

                    }

                    int ctr = 1;
                    string vctr = "";
                    qry = "begin BEGIN TRANSACTION ";
                    for (int f = len + 1; f < result1.Tables[0].Rows.Count; f++)
                    {
                        if (result1.Tables[0].Rows[f][0].ToString() != "")
                        {
                            vctr = ctr.ToString();
                            v50 = result1.Tables[0].Rows[f][0].ToString();
                            v51 = result1.Tables[0].Rows[f][1].ToString();
                            v52 = result1.Tables[0].Rows[f][5].ToString();

                            if (result1.Tables[0].Rows[f][6].ToString() == "")
                            {
                                v53 = "0";
                            }
                            else
                            {
                                v53 = result1.Tables[0].Rows[f][6].ToString();
                            }

                            if (result1.Tables[0].Rows[f][7].ToString() == "")
                            {
                                v54 = "0";
                            }
                            else
                            {
                                v54 = result1.Tables[0].Rows[f][7].ToString();
                            }

                            if (result1.Tables[0].Rows[f][8].ToString() == "")
                            {
                                v55 = "0";
                            }
                            else
                            {
                                v55 = result1.Tables[0].Rows[f][8].ToString();
                            }

                            if (result1.Tables[0].Rows[f][9].ToString() == "")
                            {
                                v56 = "0";
                            }
                            else
                            {
                                v56 = result1.Tables[0].Rows[f][9].ToString();
                            }

                            if (result1.Tables[0].Rows[f][10].ToString() == "")
                            {
                                v57 = "0";
                            }
                            else
                            {
                                v57 = result1.Tables[0].Rows[f][10].ToString();
                            }

                            if (result1.Tables[0].Rows[f][11].ToString() == "")
                            {
                                v58 = "0";
                            }
                            else
                            {
                                v58 = result1.Tables[0].Rows[f][11].ToString();
                            }

                            qry = qry + "  " + string.Format("INSERT INTO releaseOrderLines(fk_roId,lineNum,partNo,description,virtualWHS,qty,unitSellingPrc,totallSellingPrc,unitCostPrc,totallCostPrc,margin) VALUES ({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10})", v49, vctr, v50, v51, v52, v53, v54, v55, v56, v57, v58);
                            ctr++;
                        }
                        else
                        {

                            break;
                        }
                    }

                    string g = "YES";
                    qry = qry + " update releaseOrders set itemStatus='" + g + "' where roId=" + v49;
                    qry = qry + "  IF @@ERROR = 0 COMMIT TRANSACTION ELSE ROLLBACK TRANSACTION end";
                    try
                    {
                        Db.constr = connecToStr;
                        Db.myExecuteSQL(qry);
                        return ("Insertion Successfully Done");
                    }
                    catch (Exception ex)
                    {
                        return ("Error Creating rows, " + ex.Message);
                    }
                }
                else
                {
                    return ("Cannot Insert If there is no order lines");
                }
            }
            else
            {
                return ("Format of Reporting Excel is not correct");
            }
        }
        else
            return "Incompatible RO Format ! Seems to be invalid Excel file format for RO ,Can't process ";
    }
}