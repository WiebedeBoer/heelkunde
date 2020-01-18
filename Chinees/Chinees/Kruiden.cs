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
        private string updatestage;
        public string updatetype = "kruiden";
        private int updatenum;

        TextBox textBox13 = new System.Windows.Forms.TextBox();

        public Kruiden(string updatestage)
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
            if (this.updatestage != null && this.updatestage !="0")
            {
                //fill text
                //connection
                conn = new DBHandler().getConnection();
                SqlCommand mcmd;
                String mquery;
                SqlDataReader mdataReader;
                int maxi = Convert.ToInt32(this.updatestage);
                //select max
                mquery = "SELECT * FROM Kruiden WHERE ID =@search";
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
                textBox10.Text = Convert.ToString(mdataReader.GetValue(10));
                textBox11.Text = Convert.ToString(mdataReader.GetValue(11));
                textBox12.Text = Convert.ToString(mdataReader.GetValue(12));
                //close
                mdataReader.Close();
                mcmd.Dispose();
                conn.Close();
                //button
                button1.Location = new System.Drawing.Point(324, 358);
                button1.Name = this.updatestage;
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
                //aantekening button
                Button buttonnote = new System.Windows.Forms.Button();
                buttonnote.Location = new System.Drawing.Point(405, 289);
                buttonnote.Name = "Aantekening";
                buttonnote.Size = new System.Drawing.Size(100, 20);
                buttonnote.Text = "Terug";
                buttonnote.UseVisualStyleBackColor = true;
                buttonnote.Click += new System.EventHandler(buttonnote_Click);
                Controls.Add(buttonnote);
            }
            else
            {
                button1.Location = new System.Drawing.Point(324, 358);
                button1.Name = "button";
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Invoeren";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);

                Label label13 = new System.Windows.Forms.Label();
                

                // 
                // label13
                // 
                label13.AutoSize = true;
                label13.Location = new System.Drawing.Point(405, 289);
                label13.Name = "label13";
                label13.Size = new System.Drawing.Size(79, 13);
                label13.TabIndex = 24;
                label13.Text = "Aantekeningen";
                // 
                // textBox13
                // 
                textBox13.Location = new System.Drawing.Point(490, 248);
                textBox13.Multiline = true;
                textBox13.Name = "textBox13";
                textBox13.Size = new System.Drawing.Size(100, 80);
                textBox13.TabIndex = 25;

                Controls.Add(textBox13);
                Controls.Add(label13);
            }
        }

        //aantekening
        private void buttonnote_Click(object sender, EventArgs e)
        {
            //refresh
            this.Close();
            th = new Thread(openaantekening);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openaantekening(object obj)
        {
            int updatenum = Convert.ToInt32(this.updatestage);
            Application.Run(new Aantekening("Kruiden",updatenum));
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
            //string Aantekeningen = textBox13.Text;
            //updating
            query = "UPDATE Kruiden SET Nederlands =@0, Latijn =@1, Familie =@2, Inhoudsstoffen =@3, GebruikteDelen =@4, Eigenschappen =@5, Smaak =@6, Thermodynamisch =@7, Orgaan =@8, Toepassingen =@9, Actie =@10, Gebruik =@11 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", maxi));
            cmd.Parameters.AddWithValue("@0", Nederlands);
            cmd.Parameters.AddWithValue("@1", Latijns);
            cmd.Parameters.AddWithValue("@2", Familie);
            cmd.Parameters.AddWithValue("@3", Inhoudsstoffen);
            cmd.Parameters.AddWithValue("@4", GebruikteDelen);
            cmd.Parameters.AddWithValue("@5", Eigenschappen);
            cmd.Parameters.AddWithValue("@6", Smaak);
            cmd.Parameters.AddWithValue("@7", Thermodynamisch);
            cmd.Parameters.AddWithValue("@8", Orgaan);
            cmd.Parameters.AddWithValue("@9", Toepassingen);
            cmd.Parameters.AddWithValue("@10", Actie);
            cmd.Parameters.AddWithValue("@11", Gebruik);
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
            SqlDataAdapter nadapter = new SqlDataAdapter();
            SqlDataReader mdataReader;
            String query;
            String mquery;
            String aquery;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO Kruiden (Nederlands, Latijn, Familie, Inhoudsstoffen, GebruikteDelen, Eigenschappen, Smaak, Thermodynamisch, Orgaan, Toepassingen, Actie, Gebruik) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)";
            cmd = new SqlCommand(query, conn);
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
            //convert to string
            string Max = Convert.ToString(mdataReader.GetValue(0));
            MaxID = Convert.ToInt32(Max);
            //db close
            mdataReader.Close();
            //set updatestage
            this.updatestage = Max;
            //aantekening
            aquery = "INSERT INTO Kruidenaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
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
            this.updatenum = MaxID;
            this.Close();
            th = new Thread(openupdate);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //update
        private void openupdate(object obj)
        {
            //Application.Run(new Updateform(this.updatenum,this.updatetype));
            Application.Run(new Kruiden(this.updatestage));
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
