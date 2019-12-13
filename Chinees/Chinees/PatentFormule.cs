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
        private string updatestage;

        public PatentFormule()
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
                button1.Location = new System.Drawing.Point(544, 228);
                button1.Name = updatestage;
                button1.Size = new System.Drawing.Size(185, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
            }
            else
            {
                button1.Location = new System.Drawing.Point(544, 228);
                button1.Name = "button";
                button1.Size = new System.Drawing.Size(185, 23);
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
            mquery = "SELECT * FROM Patentformules WHERE ID =@search";
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
            //updating
            query = "UPDATE Patentformules SET Nederlands =@0, Engels =@1, Pinjin =@2, Werking =@3, Tong =@4, Pols =@5, Contraindicaties =@6, Indicaties =@7 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", maxi));
            cmd.Parameters.AddWithValue("@0", Nederlands);
            cmd.Parameters.AddWithValue("@1", Engels);
            cmd.Parameters.AddWithValue("@2", Pinjin);
            cmd.Parameters.AddWithValue("@3", Werking);
            cmd.Parameters.AddWithValue("@4", Tong);
            cmd.Parameters.AddWithValue("@5", Pols);
            cmd.Parameters.AddWithValue("@6", Contraindicaties);
            cmd.Parameters.AddWithValue("@7", Indicaties);
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
            String query;
            String mquery;
            String aquery;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Patentformules (Nederlands, Engels, Pinjin, Werking, Tong, Pols, Contraindicaties, Indicaties) VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            cmd = new SqlCommand(query, conn);
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
            //convert to string
            string Max = Convert.ToString(mdataReader.GetValue(0));
            MaxID = Convert.ToInt32(Max);
            //set updatestage
            this.updatestage = Max;
            //aantekening
            aquery = "INSERT INTO Patentaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
            acmd = new SqlCommand(aquery, conn);
            adapter.InsertCommand = new SqlCommand(aquery, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", MaxID);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Aantekeningen);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            mdataReader.Close();
            cmd.Dispose();
            mcmd.Dispose();
            acmd.Dispose();
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
