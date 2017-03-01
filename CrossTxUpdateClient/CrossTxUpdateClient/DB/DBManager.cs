using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data.SqlClient;


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
            string schema_script = File.readAllText(@"C:\CrossTxUpdateClient-CSCI491-MSU\CrossTxUpdateClient\CrossTxUpdateClient\DB");

            SqlConnection conn = new SqlConnection(connectionString);

            Server server = new Server(new ServerConnection(conn));

            try
            {
                server.ConnectionContext.ExecuteNonQuery(script);

                Console.Write("DB Successfully Initiated.");

                return conn;
            }
            catch (System.Exception ex)
            {
                Console.Write("Exception occurred:" + ex);

                break;
            }
        }
    }
}
