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
    public partial class KruidenFormules : Form
    {
        Thread th;
        public SqlConnection conn;
        private string updatestage;

        public KruidenFormules()
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
            if (this.updatestage != null && this.updatestage != "0")
            {
                //connection
                conn = new DBHandler().getConnection();
                String mquery;
                SqlCommand mcmd;
                SqlDataReader mdataReader;
                //which one
                int maxi = Convert.ToInt32(this.updatestage);
                //select max
                mquery = "SELECT * FROM Kruidenformules WHERE ID =@search";
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
                //db close
                mdataReader.Close();
                mcmd.Dispose();
                conn.Close();
                //button
                button1.Location = new System.Drawing.Point(315, 357);
                button1.Name = this.updatestage;
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
            }
            else
            {
                button1.Location = new System.Drawing.Point(315, 357);
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
            SqlDataAdapter adapter = new SqlDataAdapter();
            //SqlDataReader adataReader;
            String query;
            //data form variables
            string Naam = textBox1.Text;
            string Indicaties = textBox2.Text;
            string Werking = textBox3.Text;
            string Klasse = textBox4.Text;
            string Smaak = textBox5.Text;
            string Meridiaan = textBox6.Text;
            string Qi = textBox7.Text;
            string Contraindicaties = textBox8.Text;
            //updating
            query = "UPDATE Kruidenformules SET Naam =@0, Indicaties =@1, Werking =@2, Klasse =@3, Smaak =@4, Meridiaan =@5, Qi =@6, Contraindicaties =@7 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", maxi));
            cmd.Parameters.AddWithValue("@0", Naam);
            cmd.Parameters.AddWithValue("@1", Indicaties);
            cmd.Parameters.AddWithValue("@2", Werking);
            cmd.Parameters.AddWithValue("@3", Klasse);
            cmd.Parameters.AddWithValue("@4", Smaak);
            cmd.Parameters.AddWithValue("@5", Meridiaan);
            cmd.Parameters.AddWithValue("@6", Qi);
            cmd.Parameters.AddWithValue("@7", Contraindicaties);
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
        }

        private void Execute()
        {
            //connection
            conn = new DBHandler().getConnection();
            //data form variables
            string Naam = textBox1.Text;
            string Indicaties = textBox2.Text;
            string Werking = textBox3.Text;
            string Klasse = textBox4.Text;
            string Smaak = textBox5.Text;
            string Meridiaan = textBox6.Text;
            string Qi = textBox7.Text;
            string Contraindicaties = textBox8.Text;
            string Aantekeningen = textBox9.Text;
            //maximum
            int MaxID;
            //command and query strings
            SqlCommand cmd;
            SqlCommand mcmd;
            SqlCommand acmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataAdapter nadapter = new SqlDataAdapter();
            SqlDataReader mdataReader;
            String query;
            String mquery;
            String aquery;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Kruidenformules (Naam, Indicaties, Werking, Klasse, Smaak, Meridiaan, Qi, Contraindicaties) VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            cmd = new SqlCommand(query, conn);
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", Naam);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Indicaties);
            adapter.InsertCommand.Parameters.AddWithValue("@2", Werking);
            adapter.InsertCommand.Parameters.AddWithValue("@3", Klasse);
            adapter.InsertCommand.Parameters.AddWithValue("@4", Smaak);
            adapter.InsertCommand.Parameters.AddWithValue("@5", Meridiaan);
            adapter.InsertCommand.Parameters.AddWithValue("@6", Qi);
            adapter.InsertCommand.Parameters.AddWithValue("@7", Contraindicaties);
            adapter.InsertCommand.ExecuteNonQuery();
            //select max
            mquery = "SELECT MAX(ID) AS Maxid FROM Kruidenformules";
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
            //aantekening
            aquery = "INSERT INTO Formulesaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
            acmd = new SqlCommand(aquery, conn);
            nadapter.InsertCommand = new SqlCommand(aquery, conn);
            nadapter.InsertCommand.Parameters.AddWithValue("@0", MaxID);
            nadapter.InsertCommand.Parameters.AddWithValue("@1", Aantekeningen);
            nadapter.InsertCommand.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            mcmd.Dispose();
            acmd.Dispose();
            conn.Close();
            //refresh
            this.Refresh();
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
