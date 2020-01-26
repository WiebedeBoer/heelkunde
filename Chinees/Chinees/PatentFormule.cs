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

        TextBox textBoxhoe = new System.Windows.Forms.TextBox();
        ComboBox comboBox1 = new System.Windows.Forms.ComboBox();

        public PatentFormule(string updatestage)
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
                String mquery;
                SqlCommand mcmd;
                SqlDataReader mdataReader;
                //db open
                conn.Open();
                //which one
                int maxi = Convert.ToInt32(this.updatestage);
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
                //close
                mdataReader.Close();
                mcmd.Dispose();
                conn.Close();
                //button
                button1.Location = new System.Drawing.Point(544, 228);
                button1.Name = updatestage;
                button1.Size = new System.Drawing.Size(185, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button1_Click);
                Controls.Add(button1);
                //aantekening button
                Button buttonnote = new System.Windows.Forms.Button();
                buttonnote.Location = new System.Drawing.Point(658, 11);
                buttonnote.Name = "Aantekening";
                buttonnote.Size = new System.Drawing.Size(100, 20);
                buttonnote.Text = "Aantekening";
                buttonnote.UseVisualStyleBackColor = true;
                buttonnote.Click += new System.EventHandler(buttonnote_Click);
                Controls.Add(buttonnote);
                //ingredienten
                int updatenum = Convert.ToInt32(this.updatestage);
                Verhoudingen ingredient = new Verhoudingen("Patentformules", updatenum);
                int verhoudingcheck = ingredient.VerhoudingCheck();
                if (verhoudingcheck ==1)
                {
                    ComboMaker();
                }
                int menucheck = ingredient.ListCheck();
                if (menucheck ==1)
                {
                    MenuMaker(updatenum);
                }
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
                // 
                // label8
                // 
                label8.AutoSize = true;
                label8.Location = new System.Drawing.Point(448, 145);
                label8.Name = "label8";
                label8.Size = new System.Drawing.Size(70, 13);
                label8.TabIndex = 14;
                label8.Text = "Opmerkingen";
                Controls.Add(label8);
                // 
                // textBox9
                // 
                textBox9.Location = new System.Drawing.Point(544, 125);
                textBox9.Multiline = true;
                textBox9.Name = "textBox9";
                textBox9.Size = new System.Drawing.Size(185, 77);
                textBox9.TabIndex = 18;
                Controls.Add(textBox9);
            }
        }

        public void ComboMaker()
        {
            String query;
            query = "SELECT ID, Engels FROM ChineseKruiden ORDER BY Engels ASC";
            //combobox
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(565, 276);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(180, 23);
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
            dt.Columns.Add("Engels", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Engels";
            comboBox1.DataSource = dt;
            Controls.Add(comboBox1);
            //hoeveelheid
            textBoxhoe.Location = new System.Drawing.Point(565, 128);
            textBoxhoe.Name = "textBox7";
            textBoxhoe.Size = new System.Drawing.Size(100, 20);
            textBoxhoe.TabIndex = 19;
            Controls.Add(textBoxhoe);
            //button
            Button buttoningre = new System.Windows.Forms.Button();
            buttoningre.Location = new System.Drawing.Point(565, 300);
            buttoningre.Name = "Ingredient Invoer";
            buttoningre.Size = new System.Drawing.Size(160, 20);
            buttoningre.Text = "Ingredient Invoer";
            buttoningre.UseVisualStyleBackColor = true;
            buttoningre.Click += new System.EventHandler(buttoningre_Click);
            Controls.Add(buttoningre);
        }

        //insert ingredient event
        private void buttoningre_Click(object sender, EventArgs e)
        {
            int updatenum = Convert.ToInt32(this.updatestage);
            string hoeveelheid = textBoxhoe.Text;
            int hoeveel;
            if (hoeveelheid =="" || hoeveelheid == null)
            {
                hoeveel = 1;
            }
            else
            {
                hoeveel = Convert.ToInt32(hoeveelheid);
            }
            int selectedVal = (int)comboBox1.SelectedValue;
            Verhoudingen ingredient = new Verhoudingen("Patentformules", updatenum);
            bool ins = ingredient.Inserter(selectedVal, updatenum, hoeveel);
            if (ins == true)
            {
                //refresh
                this.Close();
                th = new Thread(openupdate);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        //ingredient list display
        public void MenuMaker(int searchid)
        {
            //string sekind = searchtype;
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
            int verticalpos = 390;
            int i = 0;
            //query = "SELECT PatentEnKruiden.ID, Patentformules.Nederlands, ChineseKruiden.Engels, PatentEnKruiden.Verhouding FROM Patentformules, PantentEnKruiden, ChineseKruiden WHERE Patentformules.ID=PatentEnKruiden.Patentformule AND PatentEnKruiden.ChineseKruiden=ChineseKruiden.ID AND PatentEnKruiden.ID=@sid";
            //query = "SELECT ID, Chinesekruiden, Verhouding FROM PatentEnKruiden WHERE Chinesekruiden = @sid";
            query = "SELECT PatentEnKruiden.ID, ChineseKruiden.Engels, PatentEnKruiden.Verhouding FROM PatentEnKruiden LEFT JOIN ChineseKruiden ON PatentEnKruiden.Patentformule = ChineseKruiden.ID WHERE PatentEnKruiden.Chinesekruiden = @sid";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@sid", selimiter));
            mdataReader = cmd.ExecuteReader();
            while (mdataReader.Read())
            {
                //formule naam
                //Label outlabel = new System.Windows.Forms.Label();
                //outlabel.Location = new System.Drawing.Point(700, verticalpos);
                //outlabel.Name = "outlabel" + i.ToString();
                //outlabel.Size = new System.Drawing.Size(180, 20);
                //outlabel.Text = Convert.ToString(mdataReader.GetString(1));
                //Controls.Add(outlabel);
                //kruid naam
                Label outlabel2 = new System.Windows.Forms.Label();
                outlabel2.Location = new System.Drawing.Point(500, verticalpos);
                outlabel2.Name = "outlabel2" + i.ToString();
                outlabel2.Size = new System.Drawing.Size(180, 20);
                //outlabel2.Text = Convert.ToString(mdataReader.GetInt32(1)); //str
                outlabel2.Text = mdataReader.GetString(1);
                Controls.Add(outlabel2);
                //verhouding
                Label outlabel3 = new System.Windows.Forms.Label();
                outlabel3.Location = new System.Drawing.Point(700, verticalpos);
                outlabel3.Name = "outlabel3" + i.ToString();
                outlabel3.Size = new System.Drawing.Size(40, 20);
                outlabel3.Text = Convert.ToString(mdataReader.GetInt32(2));
                Controls.Add(outlabel3);
                //id
                Button buttonrem = new System.Windows.Forms.Button();
                buttonrem.Location = new System.Drawing.Point(760, verticalpos);
                buttonrem.Text = "Verwijderen";
                buttonrem.Size = new System.Drawing.Size(75, 35);
                buttonrem.Click += new System.EventHandler(this.buttonrem_Click);
                buttonrem.Name = Convert.ToString(mdataReader.GetInt32(0));
                Controls.Add(buttonrem);
                verticalpos = verticalpos + 40;
                i++;
            }

        }

        //list removal event
        private void buttonrem_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            int updatenum = Convert.ToInt32(this.updatestage);
            Verhoudingen ingredient = new Verhoudingen("Patentformules", updatenum);
            bool rem = ingredient.Removal(ClickedNum);
            if (rem == true)
            {
                //refresh
                this.Close();
                th = new Thread(openupdate);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        //update
        private void openupdate(object obj)
        {
            Application.Run(new PatentFormule(this.updatestage));
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
            string typering = "Patenformules";
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

        private void Updating(string Clicking)
        {
            //connection
            conn = new DBHandler().getConnection();
            //which one
            int maxi = Convert.ToInt32(Clicking);
            //command and query strings
            SqlCommand cmd;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String query;
            //db open
            conn.Open();
            //data form variables
            string Nederlands = textBox1.Text;
            string Engels = textBox2.Text;
            string Pinjin = textBox3.Text;
            string Werking = textBox4.Text;
            string Tong = textBox5.Text;
            string Pols = textBox6.Text;
            string Contraindicaties = textBox7.Text;
            string Indicaties = textBox8.Text;
            //string Aantekeningen = textBox9.Text;
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
            cmd.Dispose();
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
            SqlDataAdapter nadapter = new SqlDataAdapter();
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
            //db close
            mdataReader.Close();
            cmd.Dispose();
            //aantekening
            aquery = "INSERT INTO Patentaantekeningen (Patent, Aantekening) VALUES(@0, @1)";
            acmd = new SqlCommand(aquery, conn);
            nadapter.InsertCommand = new SqlCommand(aquery, conn);
            nadapter.InsertCommand.Parameters.AddWithValue("@0", MaxID);
            nadapter.InsertCommand.Parameters.AddWithValue("@1", Aantekeningen);
            nadapter.InsertCommand.ExecuteNonQuery();
            //db close
            mcmd.Dispose();
            acmd.Dispose();
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
    }
}
