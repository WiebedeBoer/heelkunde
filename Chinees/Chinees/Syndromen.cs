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

        ComboBox comboBox2 = new System.Windows.Forms.ComboBox();
        ComboBox comboBox3 = new System.Windows.Forms.ComboBox();

        public Syndromen(string updatestage)
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
                //which one
                int maxi = Convert.ToInt32(this.updatestage);
                SqlCommand mcmd;
                SqlDataReader mdataReader;
                String mquery;
                //db open
                conn.Open();
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
                //db close
                mdataReader.Close();
                mcmd.Dispose();
                conn.Close();
                //button
                button1.Location = new System.Drawing.Point(321, 315);
                button1.Name = updatestage;
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
                //actieformules
                int updatenum = Convert.ToInt32(this.updatestage);
                int verhouding = new Actieformules(updatenum).VerhoudingCheck();
                if (verhouding >= 1)
                {
                    Combo_Load();
                }
                int listnum = new Actieformules(updatenum).ListCheck();
                if (listnum ==1)
                {
                    MenuMaker(updatenum);
                }
            }
            else
            {
                //invoer button
                button1.Location = new System.Drawing.Point(321, 315);
                button1.Name = "button";
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Invoeren";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
            }
        }

        //comboboxes
        private void Combo_Load()
        {
            //connection
            conn = new DBHandler().getConnection();
            //db open
            conn.Open();
            //combo
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(700, 340);
            comboBox2.Name = "comboBox1";
            comboBox2.Size = new System.Drawing.Size(180, 23);
            //combo query
            SqlCommand sc = new SqlCommand("SELECT ID, Naam FROM Kruidenformules ORDER BY ID ASC", conn);
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            reader.Read();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Naam", typeof(string));
            dt.Load(reader);
            comboBox2.ValueMember = "ID";
            comboBox2.DisplayMember = "Naam";
            comboBox2.DataSource = dt;
            //reader close
            reader.Close();
            sc.Dispose();
            //combo
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new System.Drawing.Point(900, 340);
            comboBox3.Name = "comboBox1";
            comboBox3.Size = new System.Drawing.Size(180, 23);
            //combo query
            SqlCommand sca = new SqlCommand("SELECT ID, Nederlands FROM Patentformules ORDER BY ID ASC", conn);
            SqlDataReader readera;
            readera = sca.ExecuteReader();
            readera.Read();
            DataTable dta = new DataTable();
            dta.Columns.Add("ID", typeof(int));
            dta.Columns.Add("Nederlands", typeof(string));
            dta.Load(readera);
            comboBox3.ValueMember = "ID";
            comboBox3.DisplayMember = "Nederlands";
            comboBox3.DataSource = dta;
            //reader close
            readera.Close();
            sca.Dispose();
            //db close
            conn.Close();
            //button
            Button buttoningre = new System.Windows.Forms.Button();
            buttoningre.Location = new System.Drawing.Point(900, 400);
            buttoningre.Name = "Actieformule Invoer";
            buttoningre.Size = new System.Drawing.Size(160, 20);
            buttoningre.Text = "Actieformule";
            buttoningre.UseVisualStyleBackColor = true;
            buttoningre.Click += new System.EventHandler(buttonin_Click);
            //controls
            Controls.Add(comboBox2);
            Controls.Add(comboBox3);
            Controls.Add(buttoningre);
        }

        //update
        private void openupdate(object obj)
        {
            Application.Run(new Syndromen(this.updatestage));
        }

        //actieformules
        //insert event
        private void buttonin_Click(object sender, EventArgs e)
        {
            
            int formuleid = Convert.ToInt32(comboBox2.SelectedValue);
            int patentid = Convert.ToInt32(comboBox3.SelectedValue);
            int updatenum = Convert.ToInt32(this.updatestage);
            Actieformules aformule = new Actieformules(updatenum);
            bool ins = aformule.Inserter(updatenum, formuleid, patentid);
            if (ins == true)
            {
                //refresh
                this.Close();
                th = new Thread(openupdate);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        //list removal event
        private void buttonrem_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            int updatenum = Convert.ToInt32(this.updatestage);
            Actieformules aformule = new Actieformules(updatenum);
            bool rem = aformule.Removal(ClickedNum);
            if (rem == true)
            {
                //refresh
                this.Close();
                th = new Thread(openupdate);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        //actieformules list display
        public void MenuMaker(int searchid)
        {
            int selimiter = searchid;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            SqlDataReader mdataReader;
            String query;
            //db open
            conn.Open();
            //select query according to type search
            //start position
            int verticalpos = 425;
            int i = 0;
            query = "SELECT Actieformules.ID, Kruidenformules.Naam, Patentformules.Nederlands, Syndromen.Syndroom FROM Actieformules, Kruidenformules, Patentformules, Syndromen WHERE Actieformules.Syndroom=Syndromen.ID AND Actieformules.Patentformule=Patentformules.ID AND Actieformules.Kruidenformule=Kruidenformules.ID AND Actieformules.ID=@sid";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@sid", selimiter));



            mdataReader = cmd.ExecuteReader();
            while (mdataReader.Read())
            {
                Label outlabel = new System.Windows.Forms.Label();
                Label outlabel2 = new System.Windows.Forms.Label();
                Label outlabel3 = new System.Windows.Forms.Label();
                Button buttonrem = new System.Windows.Forms.Button();
                Button buttonaan = new System.Windows.Forms.Button();
                //kruidenformule naam
                outlabel.Location = new System.Drawing.Point(400, verticalpos);
                outlabel.Name = "outlabel";
                outlabel.Size = new System.Drawing.Size(180, 20);
                outlabel.Text = Convert.ToString(mdataReader.GetString(1));
                //outlabel.ID = i.ToString();
                //patent formule nederlands                
                outlabel2.Location = new System.Drawing.Point(600, verticalpos);
                outlabel2.Name = "outlabel2";
                outlabel2.Size = new System.Drawing.Size(180, 20);
                outlabel2.Text = Convert.ToString(mdataReader.GetString(2));
                //syndroom                
                outlabel3.Location = new System.Drawing.Point(800, verticalpos);
                outlabel3.Name = "outlabel3";
                outlabel3.Size = new System.Drawing.Size(40, 20);
                outlabel3.Text = Convert.ToString(mdataReader.GetString(3));
                //id                
                buttonrem.Location = new System.Drawing.Point(860, verticalpos);
                buttonrem.Text = "Verwijderen";
                buttonrem.Size = new System.Drawing.Size(75, 35);
                buttonrem.Click += new System.EventHandler(this.buttonrem_Click);
                buttonrem.Name = Convert.ToString(mdataReader.GetInt32(0));
                //aantekening                
                buttonaan.Location = new System.Drawing.Point(960, verticalpos);
                buttonaan.Text = "Aantekening";
                buttonaan.Size = new System.Drawing.Size(75, 35);
                buttonaan.Click += new System.EventHandler(this.buttonaan_Click);
                buttonaan.Name = Convert.ToString(mdataReader.GetInt32(0));
                i++;
                verticalpos = verticalpos + 30;
                //control
                this.Controls.Add(outlabel);
                this.Controls.Add(outlabel2);
                this.Controls.Add(outlabel3);
                this.Controls.Add(buttonrem);
                this.Controls.Add(buttonaan);
            }

        }

        //insert or update event
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
            cmd.Dispose();
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
            //refresh
            this.Close();
            th = new Thread(openupdate);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
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

        private void buttonaan_Click(object sender, EventArgs e)
        {
            //closing thread
            this.Close();
            th = new Thread(openaantekening);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openaantekening(object obj)
        {
            int updatenum = Convert.ToInt32(this.updatestage);
            string typering = "Actieformules";
            Application.Run(new Aantekening(typering, updatenum));
        }


    }
}
