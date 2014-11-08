using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace StressTest
{
    class Worker
    {
        private string Id;
        private int Looping=1;
        private SqlConnectionStringBuilder SqlConnString;
        public Worker(string Id, int Looping, string DBServer, string InitialCatalog,string UserName, string Password)
        {
            SqlConnString = new SqlConnectionStringBuilder();
            this.Id = Id;
            this.Looping = Looping;
            SqlConnString.DataSource = DBServer;
            SqlConnString.InitialCatalog = InitialCatalog;
            SqlConnString.UserID = UserName;
            SqlConnString.Password = Password;
            //SqlConnString.MaxPoolSize = 100;

        }

        public void DoTest()
        {
            SqlConnection SqlConn = new SqlConnection(SqlConnString.ConnectionString);
            SqlCommand SqlComm = new SqlCommand("TestingSP");
            SqlComm.CommandType = System.Data.CommandType.StoredProcedure;
            SqlComm.Connection = SqlConn;


            for (int i = 0;i<Looping;i++)
            {
                //System.Threading.Thread.Sleep(2000);
                //Console.WriteLine("This is from thread " + Id);
                try
                {
                    SqlConn.Open();
                    SqlComm.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    if (SqlConn!=null)
                        SqlConn.Close();
                }
            }
        }

        

    }
}
