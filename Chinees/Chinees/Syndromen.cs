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
        private string updatestage;

        public Syndromen()
        {
            InitializeComponent();
            //check stage
            Button button1 = new System.Windows.Forms.Button();
            if (updatestage != null)
            {
                button1.Location = new System.Drawing.Point(321, 315);
                button1.Name = updatestage;
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
            }
            else
            {
                button1.Location = new System.Drawing.Point(321, 315);
                button1.Name = "button1";
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Invoeren";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button buttoned = (Button)sender;
            string ClickedButton = buttoned.Name;
            if (ClickedButton == "button")
            {
                //executing storage
                Execute();
            }
            else
            {
                //executing update
                Updating(ClickedButton);
            }
        }

        private void Updating(string Clicking)
        {
            //connection
            conn = new DBHandler().getConnection();
            //which one
            int maxi = Convert.ToInt32(Clicking);
            //command and query strings
            SqlCommand cmd;
            SqlCommand mcmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataReader mdataReader;
            String query;
            String mquery;
            //select max
            mquery = "SELECT * FROM Syndromen WHERE ID =@search";
            mcmd = new SqlCommand(mquery, conn);
            mcmd.Parameters.Add(new SqlParameter("@search", maxi));
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            //convert to string
            textBox1.Text = Convert.ToString(mdataReader.GetValue(1));
            textBox2.Text = Convert.ToString(mdataReader.GetValue(2));
            textBox3.Text = Convert.ToString(mdataReader.GetValue(3));
            textBox4.Text = Convert.ToString(mdataReader.GetValue(4));
            textBox5.Text = Convert.ToString(mdataReader.GetValue(5));
            textBox6.Text = Convert.ToString(mdataReader.GetValue(6));
            textBox7.Text = Convert.ToString(mdataReader.GetValue(7));
            textBox8.Text = Convert.ToString(mdataReader.GetValue(8));
            textBox9.Text = Convert.ToString(mdataReader.GetValue(9));
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
            //updating
            query = "UPDATE Syndromen SET Syndroom =@0, Symptoom =@1, Hoofdsymptoom =@2, Tong =@3, Pols =@4, Etiologie =@5, Pathologie =@6, Voorlopers =@7, Ontwikkelingen =@8 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", maxi));
            cmd.Parameters.AddWithValue("@0", Syndroom);
            cmd.Parameters.AddWithValue("@1", Symptoom);
            cmd.Parameters.AddWithValue("@2", Hoofdsymptoom);
            cmd.Parameters.AddWithValue("@3", Tong);
            cmd.Parameters.AddWithValue("@4", Pols);
            cmd.Parameters.AddWithValue("@5", Etiologie);
            cmd.Parameters.AddWithValue("@6", Pathologie);
            cmd.Parameters.AddWithValue("@7", Voorlopers);
            cmd.Parameters.AddWithValue("@8", Ontwikkelingen);
            cmd.ExecuteNonQuery();

            //db close
            mdataReader.Close();
            cmd.Dispose();
            mcmd.Dispose();
            conn.Close();
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
            //maximum
            int MaxID;
            //command and query strings
            SqlCommand cmd;
            SqlCommand mcmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataReader mdataReader;
            String query;
            String mquery;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Syndromen (Syndroom, Symptoom, Hoofdsymptoom, Tong, Pols, Etiologie, Pathologie, Voorlopers, Ontwikkelingen) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
            cmd = new SqlCommand(query, conn);
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
            //select max
            mquery = "SELECT MAX(ID) AS Maxid FROM Syndromen";
            mcmd = new SqlCommand(mquery, conn);
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            //convert to string
            string Max = Convert.ToString(mdataReader.GetValue(0));
            MaxID = Convert.ToInt32(Max);
            //set updatestage
            this.updatestage = Max;
            //db close
            mdataReader.Close();
            cmd.Dispose();
            mcmd.Dispose();
            conn.Close();
        }

        //hoofdmenu
        private void openhoofdmenu(object obj)
        {
            Application.Run(new Form1());
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
