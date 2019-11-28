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
    public partial class SyndroomActie : Form
    {
        Thread th;
        public SqlConnection conn;

        public SyndroomActie()
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
            string Syndroom = textBox1.Text;
            string Actie = textBox2.Text;
            string Acupunctuurpunten = textBox3.Text;
            string Opmerkingen = textBox4.Text;
            //command and query strings
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            //SqlDataReader adataReader;
            String query;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Syndromenacties (Syndroom, Actie, Acupunctuurpunten, Opmerkingen) VALUES (@0, @1, @2, @3), Syndroom, Actie, Acupunctuurpunten, Opmerkingen";
            cmd = new SqlCommand(query, conn);
            //cmd.ExecuteReader();
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", Syndroom);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Actie);
            adapter.InsertCommand.Parameters.AddWithValue("@2", Acupunctuurpunten);
            adapter.InsertCommand.Parameters.AddWithValue("@3", Opmerkingen);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            //adataReader.Close();
            cmd.Dispose();
            conn.Close();
        }

    }
}
