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

        ComboBox comboBox1 = new System.Windows.Forms.ComboBox();

        public SyndroomActie(string updatestage)
        {
            InitializeComponent();
            this.updatestage = updatestage;
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
                //db open
                conn.Open();
                //which one
                int maxi = Convert.ToInt32(this.updatestage);
                String mquery, aquery;
                SqlCommand mcmd, acmd;
                SqlDataReader mdataReader, aReader;
                //select max
                mquery = "SELECT * FROM Syndromenacties WHERE ID =@search";
                mcmd = new SqlCommand(mquery, conn);
                mcmd.Parameters.Add(new SqlParameter("@search", maxi));
                mdataReader = mcmd.ExecuteReader();
                mdataReader.Read();
                //convert to string
                int actie = Convert.ToInt32(mdataReader.GetValue(1));
                textBox2.Text = Convert.ToString(mdataReader.GetValue(2));
                textBox3.Text = Convert.ToString(mdataReader.GetValue(3));
                textBox4.Text = Convert.ToString(mdataReader.GetValue(4));
                //reader close
                mdataReader.Close();
                mcmd.Dispose();
                //display syndroom
                aquery = "SELECT Syndroom FROM Syndromen WHERE ID =@search";
                acmd = new SqlCommand(aquery, conn);
                acmd.Parameters.Add(new SqlParameter("@search", actie));
                aReader = acmd.ExecuteReader();
                aReader.Read();

                Label label5 = new System.Windows.Forms.Label();
                label5.AutoSize = true;
                label5.Location = new System.Drawing.Point(244, 54);
                label5.Name = "label5";
                label5.Size = new System.Drawing.Size(70, 13);
                label5.TabIndex = 5;
                label5.Text = Convert.ToString(aReader.GetValue(0));

                //reader close
                aReader.Close();
                acmd.Dispose();
                //db close
                conn.Close();
                //button
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
                ComboMaker();
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
            //db open
            conn.Open();
            //which one
            int maxi = Convert.ToInt32(Clicking);
            //command and query strings
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String query;
            //data form variables
            string Syndroom = Convert.ToString(comboBox1.SelectedValue);
            string Actie = textBox2.Text;
            string Acupunctuurpunten = textBox3.Text;
            string Opmerkingen = textBox4.Text;
            //updating
            query = "UPDATE Syndromenacties SET Actie =@0, Acupunctuurpunten =@1, Opmerkingen =@2 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", maxi));
            //cmd.Parameters.AddWithValue("@0", Syndroom);
            cmd.Parameters.AddWithValue("@0", Actie);
            cmd.Parameters.AddWithValue("@1", Acupunctuurpunten);
            cmd.Parameters.AddWithValue("@2", Opmerkingen);
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
        }

        public void ComboMaker()
        {
            
            String query;
            query = "SELECT ID, Syndroom FROM Syndromen ORDER BY Syndroom ASC";
            //combobox
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(244, 54);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(140, 23);
            //connection
            conn = new DBHandler().getConnection();
            //db open
            conn.Open();
            //first select
            SqlCommand sc = new SqlCommand(query, conn);
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            reader.Read();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Syndroom", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Syndroom";
            comboBox1.DataSource = dt;
            Controls.Add(comboBox1);
            //close
            reader.Close();
            sc.Dispose();
            conn.Close();
        }

        private void Execute()
        {
            //connection
            conn = new DBHandler().getConnection();
            //data form variables
            int Syndroom = (int)comboBox1.SelectedValue;
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
            //refresh
            this.Close();
            th = new Thread(openupdate);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //update
        private void openupdate(object obj)
        {
            Application.Run(new SyndroomActie(this.updatestage));
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
