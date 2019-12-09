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
    public partial class Syndromen : Form
    {
        Thread th;
        public SqlConnection conn;

        public Syndromen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //executing storage
            Execute();

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
            string Symptoom = textBox2.Text;
            string Hoofdsymptoom = textBox3.Text;
            string Tong = textBox4.Text;
            string Pols = textBox5.Text;
            string Etiologie = textBox6.Text;
            string Pathologie = textBox7.Text;
            string Voorlopers = textBox8.Text;
            string Ontwikkelingen = textBox9.Text;            
            //command and query strings
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            //SqlDataReader adataReader;
            String query;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Syndromen (Syndroom, Symptoom, Hoofdsymptoom, Tong, Pols, Etiologie, Pathologie, Voorlopers, Ontwikkelingen) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
            cmd = new SqlCommand(query, conn);
            //cmd.ExecuteReader();
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", Syndroom);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Symptoom);
            adapter.InsertCommand.Parameters.AddWithValue("@2", Hoofdsymptoom);
            adapter.InsertCommand.Parameters.AddWithValue("@3", Tong);
            adapter.InsertCommand.Parameters.AddWithValue("@4", Pols);
            adapter.InsertCommand.Parameters.AddWithValue("@5", Etiologie);
            adapter.InsertCommand.Parameters.AddWithValue("@6", Pathologie);
            adapter.InsertCommand.Parameters.AddWithValue("@7", Voorlopers);
            adapter.InsertCommand.Parameters.AddWithValue("@8", Ontwikkelingen);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            //adataReader.Close();
            cmd.Dispose();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //closing thread
            this.Close();
            th = new Thread(openhoofdmenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
