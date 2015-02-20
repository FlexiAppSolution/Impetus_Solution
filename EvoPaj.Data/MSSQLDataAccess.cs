using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;


namespace EvoPaj.Data
{
    public class MSSQLDataAccess
    {       
            SqlCommand sqlCmd;          // holds the command
            SqlConnection sqlConn;       // holds the connection
            SqlTransaction sqlTrans;   // holds the transaction
            private static SqlConnection connection;

            public MSSQLDataAccess()
            {

            }
            public bool testConnection()
            {
                return false;
            }

            public static SqlConnection getNewConnection()
            {
                connection = new SqlConnection();
                //string conString = "Data Source=.;Initial Catalog=NuGrutto;Integrated Security=True";
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["EvoPajDB"].ConnectionString;

                connection.ConnectionString = conString;
                return connection;
            }

            public MSSQLDataAccess(bool IsTransaction)
            {
                // setup the connection object
                sqlConn = new SqlConnection();

                sqlConn = getNewConnection();

                // open the connection
                OpenConnection();

                // begin the transaction (if required)
                if (IsTransaction == true)
                {
                    sqlTrans = sqlConn.BeginTransaction();
                }

                // reset the state of the object
                Reset();
            }
            public void ClosedbConnection()
            {
                CloseConnection();
            }

            private void OpenConnection()
            {
                if (sqlConn.State == ConnectionState.Closed)
                {
                    try
                    {
                        sqlConn.Open();
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                    }
                }
            }
            public void Reset()
            {
                // setup the command object
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;

                // add the transaction if we need to
                if (sqlTrans != null)
                {
                    sqlCmd.Transaction = sqlTrans;
                }
            }

            public void CommitTransaction()
            {
                sqlTrans.Commit();
            }


            public void RollbackTransaction()
            {
                sqlTrans.Rollback();
            }
            private void CloseConnection()
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
            }

            public void AddParamAndValue(string param, object value)
            {
                sqlCmd.Parameters.AddWithValue(param, value);
            }
            public void AddOutputParameter(SqlParameter output, string param, SqlDbType objType)
            {
                output.ParameterName = param;
                output.SqlDbType = objType;
                output.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(output);
            }

            public object GetOutputParameter(SqlParameter output)
            {
                object obj = output.Value;
                return obj;
            }

            public void RemoveParameter(string strName)
            {

                sqlCmd.Parameters.RemoveAt(sqlCmd.Parameters.IndexOf(strName));


            }
            public SqlDataAdapter ExecuteAdapter(string strCommand, CommandType objType)
            {
                // set the properties correctly
                sqlCmd.CommandText = strCommand;
                sqlCmd.CommandType = objType;

                // create the data adapater object
                SqlDataAdapter objAdapter = new SqlDataAdapter(sqlCmd);

                // return the adapter
                return (objAdapter);
            }

            public DataSet GetDataset(string sqlCommand, CommandType objType)
            {
                SqlDataAdapter adapter = ExecuteAdapter(sqlCommand, objType);

                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
            public int ExecuteNonQuery(string strCommand, CommandType objType)
            {
                int intReturn = -1;

                sqlCmd.CommandText = strCommand;
                sqlCmd.CommandType = objType;
                intReturn = sqlCmd.ExecuteNonQuery();

                return (intReturn);
            }
        
    }
}