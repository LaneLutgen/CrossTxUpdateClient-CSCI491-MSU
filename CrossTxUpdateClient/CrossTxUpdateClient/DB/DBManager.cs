using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace CrossTxUpdateClient.DB
{
    /// <summary>
    /// This class will be responsible for anything and everything database related
    /// </summary>
    public class DBManager
    {

        /// Example connection string:
        /// "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ccwebgrity;Data Source=SURAJIT\SQLEXPRESS;database=master"
        string connectionString;

        public DBManager() 
        { 
        }

        public DBManager(SqlConnection conn, string connectionString) 
        {
            this.connectionString = connectionString;
        }

        protected SqlConnection initalizeDB(object sender, EventArgs e) 
        {

            ///Make sure your file path points to the schema file, wherever it is.
            string schema_script = File.ReadAllText(@"C:\CrossTxUpdateClient-CSCI491-MSU\CrossTxUpdateClient\CrossTxUpdateClient\DB");

            var query = schema_script;
            var conn = new SqlConnection(connectionString);
            var command = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();

                Console.Write("DB Successfully Initiated.");

                return conn;
            }
            catch (System.Exception ex)
            {
                Console.Write("Exception occurred:" + ex);

                return conn;
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                }
            }

        }
    }
}
