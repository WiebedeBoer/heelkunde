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
        private string updatestage;

        public SyndroomActie()
        {
            InitializeComponent();
        }

        private void form_load(object sender, EventArgs e)
        {
            buttonmaker();
        }

        private void buttonmaker()
        {
            //check stage
            Button button1 = new System.Windows.Forms.Button();
            if (this.updatestage != null)
            {
                button1.Location = new System.Drawing.Point(394, 161);
                button1.Name = updatestage;
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
            }
            else
            {
                button1.Location = new System.Drawing.Point(394, 161);
                button1.Name = "button";
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Invoeren";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
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
            mquery = "SELECT * FROM Syndromenacties WHERE ID =@search";
            mcmd = new SqlCommand(mquery, conn);
            mcmd.Parameters.Add(new SqlParameter("@search", maxi));
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            //convert to string
            textBox1.Text = Convert.ToString(mdataReader.GetValue(1));
            textBox2.Text = Convert.ToString(mdataReader.GetValue(2));
            textBox3.Text = Convert.ToString(mdataReader.GetValue(3));
            textBox4.Text = Convert.ToString(mdataReader.GetValue(4));
            //data form variables
            string Syndroom = textBox1.Text;
            string Actie = textBox2.Text;
            string Acupunctuurpunten = textBox3.Text;
            string Opmerkingen = textBox4.Text;
            //updating
            query = "UPDATE Syndromenacties SET Syndroom =@0, Actie =@1, Acupunctuurpunten =@2, Opmerkingen =@3 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", maxi));
            cmd.Parameters.AddWithValue("@0", Syndroom);
            cmd.Parameters.AddWithValue("@1", Actie);
            cmd.Parameters.AddWithValue("@2", Acupunctuurpunten);
            cmd.Parameters.AddWithValue("@3", Opmerkingen);
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
            string Actie = textBox2.Text;
            string Acupunctuurpunten = textBox3.Text;
            string Opmerkingen = textBox4.Text;
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
            query = "INSERT INTO Syndromenacties (Syndroom, Actie, Acupunctuurpunten, Opmerkingen) VALUES (@0, @1, @2, @3)";
            cmd = new SqlCommand(query, conn);
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", Syndroom);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Actie);
            adapter.InsertCommand.Parameters.AddWithValue("@2", Acupunctuurpunten);
            adapter.InsertCommand.Parameters.AddWithValue("@3", Opmerkingen);
            adapter.InsertCommand.ExecuteNonQuery();
            //select max
            mquery = "SELECT MAX(ID) AS Maxid FROM Syndromenacties";
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
