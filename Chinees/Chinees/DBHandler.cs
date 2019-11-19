using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;

namespace Chinees
{
    //database handler class
    public class DBHandler
    {
        //private Database db;
        private SqlConnection con;
        public DBHandler()
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\wiebe\Documents\minor\heelkunde\Chinees\TCM2.mdb;Jet OLEDB:Database Password=admin123";
            //string provider = "System.Data.SqlClient";
            con = new SqlConnection(connectionString);
            //db = OpenConnectionString(connectionString, provider);

        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
