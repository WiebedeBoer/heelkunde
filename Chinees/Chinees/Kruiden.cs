using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Data.SqlClient;
//using WebMatrix.Data;

namespace Chinees
{


    //form
    public partial class Kruiden : Form
    {
        Thread th;
        public SqlConnection conn;

        public Kruiden()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //executing storage
            Execute();
            //closing thread
            this.Close();
            th = new Thread(openhoofdmenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openhoofdmenu(object obj)
        {
            Application.Run(new Form1());
        }

        private void Execute()
        {
            //connection
            conn = new DBHandler().getConnection();
            //data form variables
            string Nederlands = textBox1.Text;
            string Latijns = textBox2.Text;
            string Familie = textBox3.Text;
            string Inhoudsstoffen = textBox4.Text;
            string GebruikteDelen = textBox5.Text;
            string Eigenschappen = textBox6.Text;
            string Smaak = textBox7.Text;
            string Thermodynamisch = textBox8.Text;
            string Orgaan = textBox9.Text;
            string Toepassingen = textBox10.Text;
            string Actie = textBox11.Text;
            string Gebruik = textBox12.Text;
            string Aantekeningen = textBox13.Text;
            //maximum
            int MaxID;
            //command and query strings
            SqlCommand cmd;
            SqlCommand mcmd;
            SqlCommand acmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataReader mdataReader;
            SqlDataReader adataReader;
            String query;
            String mquery;
            String aquery;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Kruiden (Nederlands, Latijn, Familie, Inhoudsstoffen,GebruikteDelen,Eigenschappen,Smaak,Thermodynamisch,Orgaan,Toepassingen,Actie,Gebruik) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11), Nederlands, Latijns, Familie, Inhoudsstoffen,GebruikteDelen,Eigenschappen,Smaak,Thermodynamisch,Orgaan,Toepassingen,Actie,Gebruik";
            cmd = new SqlCommand(query, conn);
            //cmd.ExecuteReader();
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.ExecuteNonQuery();
            //select max
            mquery = "SELECT MAX(ID) AS Maxid FROM Kruiden";
            mcmd = new SqlCommand(mquery, conn);
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            MaxID = Convert.ToInt32(mdataReader.GetValue(0));
            //aantekening
            aquery = "INSERT INTO Kruidenaantekeningen (Kruid, Aantekening) VALUES(@0, @1), MaxID, Aantekeningen";
            acmd = new SqlCommand(aquery, conn);
            //adataReader = acmd.ExecuteReader();
            adapter.InsertCommand = new SqlCommand(aquery, conn);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            mdataReader.Close();
            //adataReader.Close();
            cmd.Dispose();
            mcmd.Dispose();
            acmd.Dispose();
            conn.Close();
        }

    }

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
