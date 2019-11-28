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
    public partial class PatentFormule : Form
    {
        Thread th;
        public SqlConnection conn;

        public PatentFormule()
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
            string Engels = textBox2.Text;
            string Pinjin = textBox3.Text;
            string Werking = textBox4.Text;
            string Tong = textBox5.Text;
            string Pols = textBox6.Text;
            string Contraindicaties = textBox7.Text;
            string Indicaties = textBox8.Text;
            string Aantekeningen = textBox9.Text;
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
            query = "INSERT INTO Patentformules (Nederlands, Engels, Pinjin, Werking, Tong, Pols, Contraindicaties, Indicaties) VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            cmd = new SqlCommand(query, conn);
            //cmd.ExecuteReader();
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", Nederlands);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Engels);
            adapter.InsertCommand.Parameters.AddWithValue("@2", Pinjin);
            adapter.InsertCommand.Parameters.AddWithValue("@3", Werking);
            adapter.InsertCommand.Parameters.AddWithValue("@4", Tong);
            adapter.InsertCommand.Parameters.AddWithValue("@5", Pols);
            adapter.InsertCommand.Parameters.AddWithValue("@6", Contraindicaties);
            adapter.InsertCommand.Parameters.AddWithValue("@7", Indicaties);
            adapter.InsertCommand.ExecuteNonQuery();
            //select max
            mquery = "SELECT MAX(ID) AS Maxid FROM Patentformules";
            mcmd = new SqlCommand(mquery, conn);
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            MaxID = Convert.ToInt32(mdataReader.GetValue(0));
            //aantekening
            aquery = "INSERT INTO Patentaantekeningen (Kruid, Aantekening) VALUES(@0, @1), MaxID, Aantekeningen";
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
