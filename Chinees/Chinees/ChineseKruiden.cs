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
    public partial class ChineseKruiden : Form
    {
        Thread th;
        public SqlConnection conn;
        private string updatestage;

        TextBox textBox11 = new System.Windows.Forms.TextBox();

        public ChineseKruiden(string updatestage)
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
                //connection
                conn = new DBHandler().getConnection();
                SqlDataReader mdataReader;
                String mquery;
                SqlCommand mcmd;
                //db open
                conn.Open();
                int maxi = Convert.ToInt32(this.updatestage);
                //select max
                mquery = "SELECT * FROM ChineseKruiden WHERE ID =@search";
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
                //close
                mdataReader.Close();
                mcmd.Dispose();
                conn.Close();
                //button
                button1.Location = new System.Drawing.Point(322, 369);
                button1.Name = this.updatestage;
                button1.Size = new System.Drawing.Size(108, 30);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
                //aantekening button
                Button buttonnote = new System.Windows.Forms.Button();
                buttonnote.Location = new System.Drawing.Point(558, 11);
                buttonnote.Name = "Aantekening";
                buttonnote.Size = new System.Drawing.Size(100, 20);
                buttonnote.Text = "Aantekening";
                buttonnote.UseVisualStyleBackColor = true;
                buttonnote.Click += new System.EventHandler(buttonnote_Click);
                Controls.Add(buttonnote);
            }
            else
            {
                button1.Location = new System.Drawing.Point(322, 369);
                button1.Name = "button";
                button1.Size = new System.Drawing.Size(108, 30);
                button1.Text = "Invoeren";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);

                Label label11 = new System.Windows.Forms.Label();

                // 
                // label11
                // 
                label11.AutoSize = true;
                label11.Location = new System.Drawing.Point(433, 274);
                label11.Name = "label11";
                label11.Size = new System.Drawing.Size(79, 13);
                label11.TabIndex = 11;
                label11.Text = "Aantekeningen";

                // 
                // textBox11
                // 
                textBox11.Location = new System.Drawing.Point(536, 271);
                textBox11.Name = "textBox11";
                textBox11.Size = new System.Drawing.Size(202, 20);
                textBox11.TabIndex = 22;

                Controls.Add(textBox11);
                Controls.Add(label11);

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
            string typering = "Chinesekruiden";
            Application.Run(new Aantekening(typering, updatenum));
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

        //updating
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
            //SqlDataReader adataReader;
            String query;          
            //data form variables
            string Engels = textBox1.Text;
            string Latijns = textBox2.Text;
            string Pinjin = textBox3.Text;
            string Klasse = textBox4.Text;
            string Thermodynamisch = textBox5.Text;
            string Meridiaan = textBox6.Text;
            string Qi = textBox7.Text;
            string Werking = textBox8.Text;
            string Indicaties = textBox9.Text;
            string Dosering = textBox10.Text;
            string Aantekeningen = textBox10.Text;
            //updating
            query = "UPDATE ChineseKruiden SET Engels =@0, Latijn =@1, Pinjin =@2, Klasse =@3, Thermodynamisch =@4, Meridiaan =@5, Qi =@6, Werking =@7, Indicaties =@8, Dosering =@9 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", maxi));
            cmd.Parameters.AddWithValue("@0", Engels);
            cmd.Parameters.AddWithValue("@1", Latijns);
            cmd.Parameters.AddWithValue("@2", Pinjin);
            cmd.Parameters.AddWithValue("@3", Klasse);
            cmd.Parameters.AddWithValue("@4", Thermodynamisch);
            cmd.Parameters.AddWithValue("@5", Meridiaan);
            cmd.Parameters.AddWithValue("@6", Qi);
            cmd.Parameters.AddWithValue("@7", Werking);
            cmd.Parameters.AddWithValue("@8", Indicaties);
            cmd.Parameters.AddWithValue("@9", Dosering);
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
        }

        //inserting
        private void Execute()
        {
            //connection
            conn = new DBHandler().getConnection();
            //data form variables
            string Engels = textBox1.Text;
            string Latijns = textBox2.Text;
            string Pinjin = textBox3.Text;
            string Klasse = textBox4.Text;
            string Thermodynamisch = textBox5.Text;
            string Meridiaan = textBox6.Text;
            string Qi = textBox7.Text;
            string Werking = textBox8.Text;
            string Indicaties = textBox9.Text;
            string Dosering = textBox10.Text;
            string Aantekeningen = textBox10.Text;
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
            query = "INSERT INTO ChineseKruiden (Engels, Latijn, Pinjin, Klasse, Thermodynamisch, Meridiaan, Qi, Werking, Indicaties, Dosering) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9)";
            cmd = new SqlCommand(query, conn);
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", Engels);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Latijns);
            adapter.InsertCommand.Parameters.AddWithValue("@2", Pinjin);
            adapter.InsertCommand.Parameters.AddWithValue("@3", Klasse);
            adapter.InsertCommand.Parameters.AddWithValue("@4", Thermodynamisch);
            adapter.InsertCommand.Parameters.AddWithValue("@5", Meridiaan);
            adapter.InsertCommand.Parameters.AddWithValue("@6", Qi);
            adapter.InsertCommand.Parameters.AddWithValue("@7", Werking);
            adapter.InsertCommand.Parameters.AddWithValue("@8", Indicaties);
            adapter.InsertCommand.Parameters.AddWithValue("@9", Dosering);
            adapter.InsertCommand.ExecuteNonQuery();
            cmd.Dispose();
            //select max
            mquery = "SELECT MAX(ID) AS Maxid FROM ChineseKruiden";
            mcmd = new SqlCommand(mquery, conn);
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            //convert to string
            string Max = Convert.ToString(mdataReader.GetValue(0));
            MaxID = Convert.ToInt32(Max);
            //closer
            mdataReader.Close();
            mcmd.Dispose();
            //set updatestage
            this.updatestage = Max;
            //aantekening
            aquery = "INSERT INTO Chineesaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
            acmd = new SqlCommand(aquery, conn);
            nadapter.InsertCommand = new SqlCommand(aquery, conn);
            nadapter.InsertCommand.Parameters.AddWithValue("@0", MaxID);
            nadapter.InsertCommand.Parameters.AddWithValue("@1", Aantekeningen);
            nadapter.InsertCommand.ExecuteNonQuery();
            //db close
            acmd.Dispose();
            conn.Close();
            this.Close();
            th = new Thread(openupdate);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //koppelen

        //Controls.Add(outlabel);
        //Controls.Add(outlabel2);
        //Controls.Add(outlabel3);
        //Controls.Add(buttonrem);

        //update
        private void openupdate(object obj)
        {
            //Application.Run(new Updateform(this.updatenum,this.updatetype));
            Application.Run(new ChineseKruiden(this.updatestage));
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
