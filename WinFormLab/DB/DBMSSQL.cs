using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;                  //Add
using System.Data.SqlClient;        //Add
using System.Data.Common;           //Add
using System.Transactions;          //Add

namespace WinFormLab.DB
{
    public class DBMSSQL
    {
        #region DB func


        /// <summary>DB查詢
        /// </summary>
        public DataTable DBQuery(string connstr, string SQL, List<SqlParameter> dbParameters, ref string errmsg)
        {
            DataTable dtResult = new DataTable();
            try
            {
                SqlCommand dbCmd = new SqlCommand();
                dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandText = SQL;
                if (dbParameters != null)
                    dbCmd.Parameters.AddRange(dbParameters.ToArray());

                DataSet ds = new DataSet();
                SqlConnection dbConn = new SqlConnection(connstr);
                dbConn.Open();
                dbCmd.Connection = dbConn;
                dbCmd.CommandTimeout = 10;
                DataAdapter dbAdp = new SqlDataAdapter(dbCmd);
                dbAdp.Fill(ds);
                dtResult = (ds.Tables.Count > 0) ? ds.Tables[0] : dtResult;

                dbConn.Close();
                dbConn.Dispose();
                dbCmd.Dispose();
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
            }
            return dtResult;
        }

        /// <summary>DB單筆作業
        /// </summary>
        public bool DBExec(string connstr, string SQL, List<SqlParameter> dbParameters, ref string errmsg)
        {
            bool result = true;
            SqlCommand dbCmd = new SqlCommand();
            SqlConnection dbConn = new SqlConnection(connstr);
            dbConn.Open();
            CommittableTransaction cTra = new CommittableTransaction();
            dbConn.EnlistTransaction(cTra);
            dbCmd.Connection = dbConn;

            try
            {
                dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandText = SQL;
                if (dbParameters != null)
                    dbCmd.Parameters.AddRange(dbParameters.ToArray());

                try
                {
                    dbCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    errmsg = ex.Message;
                    cTra.Rollback();
                    result = false;
                }

                cTra.Commit();
            }
            catch (TransactionException tex)
            {
                if (!cTra.TransactionInformation.Status.Equals(TransactionStatus.Aborted))
                {
                    cTra.Rollback(tex);
                    result = false;
                }
            }
            catch (Exception)
            {
                cTra.Rollback();
                result = false;
            }
            finally
            {
                cTra.Dispose();
                dbCmd.Dispose();
                dbConn.Close();
                dbConn.Dispose();
            }

            return result;
        }

        /// <summary>DB批次作業
        /// </summary>
        public bool DBExec(string connstr, string SQL, List<List<SqlParameter>> dbParametersList, ref string errmsg)
        {
            bool result = true;
            SqlCommand dbCmd = new SqlCommand();
            SqlConnection dbConn = new SqlConnection(connstr);
            dbConn.Open();
            CommittableTransaction cTra = new CommittableTransaction();
            dbConn.EnlistTransaction(cTra);
            dbCmd.Connection = dbConn;
            dbCmd.CommandTimeout = 10;

            try
            {
                dbCmd.CommandType = CommandType.Text;
                dbCmd.CommandText = SQL;
                if (dbParametersList != null)
                {
                    errmsg = "";
                    foreach (List<SqlParameter> dbParameters in dbParametersList)
                    {
                        dbCmd.Parameters.Clear();
                        dbCmd.Parameters.AddRange(dbParameters.ToArray());

                        try
                        {
                            dbCmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            errmsg = ex.Message;
                            cTra.Rollback();
                            result = false;
                            break;
                        }
                    }
                }

                //全部成功才 commit
                if (errmsg.Trim().Length == 0)
                    cTra.Commit();
            }
            catch (TransactionException tex)
            {
                if (!cTra.TransactionInformation.Status.Equals(TransactionStatus.Aborted))
                {
                    errmsg = tex.Message;
                    cTra.Rollback(tex);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;
                cTra.Rollback();
                result = false;
            }
            finally
            {
                cTra.Dispose();
                dbCmd.Dispose();
                dbConn.Close();
                dbConn.Dispose();
            }

            return result;
        }

        #endregion

    }
}
