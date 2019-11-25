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
            //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\wiebe\Documents\minor\heelkunde\Chinees\Chinees\TCM2.mdb
            //string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\wiebe\Documents\minor\heelkunde\Chinees\TCM2.mdb;Jet OLEDB:Database Password=admin123";
            // string connectionString = @"Data Source=C:\Users\wiebe\Documents\minor\heelkunde\Chinees\TCM2.mdb;Jet OLEDB:Database Password=admin123";
            //string connectionString = @"Data Source=C:\Users\wiebe\Documents\minor\heelkunde\Chinees\TCM2.mdb";
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TCM2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
