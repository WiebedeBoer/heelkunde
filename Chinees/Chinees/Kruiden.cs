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
            //SqlDataReader adataReader;
            String query;
            String mquery;
            String aquery;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Kruiden (Nederlands, Latijn, Familie, Inhoudsstoffen,GebruikteDelen,Eigenschappen,Smaak,Thermodynamisch,Orgaan,Toepassingen,Actie,Gebruik) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)";
            cmd = new SqlCommand(query, conn);
            //cmd.ExecuteReader();
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", Nederlands);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Latijns);
            adapter.InsertCommand.Parameters.AddWithValue("@2", Familie);
            adapter.InsertCommand.Parameters.AddWithValue("@3", Inhoudsstoffen);
            adapter.InsertCommand.Parameters.AddWithValue("@4", GebruikteDelen);
            adapter.InsertCommand.Parameters.AddWithValue("@5", Eigenschappen);
            adapter.InsertCommand.Parameters.AddWithValue("@6", Smaak);
            adapter.InsertCommand.Parameters.AddWithValue("@7", Thermodynamisch);
            adapter.InsertCommand.Parameters.AddWithValue("@8", Orgaan);
            adapter.InsertCommand.Parameters.AddWithValue("@9", Toepassingen);
            adapter.InsertCommand.Parameters.AddWithValue("@10", Actie);
            adapter.InsertCommand.Parameters.AddWithValue("@11", Gebruik);
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
            adapter.InsertCommand.Parameters.AddWithValue("@0", MaxID);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Aantekeningen);
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




}
