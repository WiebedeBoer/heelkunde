﻿using System;
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

        TextBox textBoxhoe = new System.Windows.Forms.TextBox();
        ComboBox comboBox1 = new System.Windows.Forms.ComboBox();

        public KruidenFormules(string updatestage)
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
                //aantekening button
                Button buttonnote = new System.Windows.Forms.Button();
                buttonnote.Location = new System.Drawing.Point(558, 11);
                buttonnote.Name = "Aantekening";
                buttonnote.Size = new System.Drawing.Size(100, 20);
                buttonnote.Text = "Aantekening";
                buttonnote.UseVisualStyleBackColor = true;
                buttonnote.Click += new System.EventHandler(buttonnote_Click);
                Controls.Add(buttonnote);
                //ingredienten
                int updatenum = Convert.ToInt32(this.updatestage);
                Verhoudingen ingredient = new Verhoudingen("Kruidenformules", updatenum);
                int verhoudingcheck = ingredient.VerhoudingCheck();
                if (verhoudingcheck ==1)
                {
                    ComboMaker();                    
                }
                int menucheck = ingredient.ListCheck();
                if (menucheck == 1)
                {
                    MenuMaker(updatenum);
                }

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
                // 
                // label9
                // 
                label9.AutoSize = true;
                label9.Location = new System.Drawing.Point(89, 273);
                label9.Name = "label9";
                label9.Size = new System.Drawing.Size(70, 13);
                label9.TabIndex = 8;
                label9.Text = "Opmerkingen";
                Controls.Add(label9);
                // 
                // textBox9
                // 
                textBox9.Location = new System.Drawing.Point(564, 210);
                textBox9.Name = "textBox9";
                textBox9.Size = new System.Drawing.Size(101, 20);
                textBox9.TabIndex = 18;
                Controls.Add(textBox9);
            }
        }



        public void ComboMaker()
        {
            String query;
            //query = "SELECT ID, Nederlands FROM Kruiden ORDER BY Nederlands ASC";
            query = "SELECT ID, Nederlands FROM Kruiden";
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
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Naam", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Naam";
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
            if (hoeveelheid == "" || hoeveelheid == null)
            {
                hoeveel = 1;
            }
            else
            {
                hoeveel = Convert.ToInt32(hoeveelheid);
            }
            int selectedVal = (int)comboBox1.SelectedValue;
            Verhoudingen ingredient = new Verhoudingen("Kruidenformules", updatenum);
            bool ins = ingredient.Inserter(selectedVal,updatenum,hoeveel);
            if (ins ==true)
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
            int verticalpos = 330;
            //int i = 0;
            query = "SELECT FormulesEnKruiden.ID, Kruidenformules.Naam, Kruiden.Nederlands, FormulesEnKruiden.Verhouding FROM Kruidenformules, FormulesEnKruiden, Kruiden WHERE Kruidenformules.ID=FormulesEnKruiden.IDKruidenformule AND FormulesEnKruiden.IDKruiden=Kruiden.ID AND FormulesEnKruiden.ID=@sid";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@sid", selimiter));
            mdataReader = cmd.ExecuteReader();
            while (mdataReader.Read())
            {
                //formule naam
                Label outlabel = new System.Windows.Forms.Label();
                outlabel.Location = new System.Drawing.Point(700, verticalpos);
                outlabel.Name = "outlabel";
                outlabel.Size = new System.Drawing.Size(180, 20);
                outlabel.Text = Convert.ToString(mdataReader.GetString(1));
                //kruid naam
                Label outlabel2 = new System.Windows.Forms.Label();
                outlabel2.Location = new System.Drawing.Point(900, verticalpos);
                outlabel2.Name = "outlabel2";
                outlabel2.Size = new System.Drawing.Size(180, 20);
                outlabel2.Text = Convert.ToString(mdataReader.GetString(2));
                //verhouding
                Label outlabel3 = new System.Windows.Forms.Label();
                outlabel3.Location = new System.Drawing.Point(1100, verticalpos);
                outlabel3.Name = "outlabel3";
                outlabel3.Size = new System.Drawing.Size(40, 20);
                outlabel3.Text = Convert.ToString(mdataReader.GetString(3));
                //id
                Button buttonrem = new System.Windows.Forms.Button();
                buttonrem.Location = new System.Drawing.Point(1160, verticalpos);
                buttonrem.Text = "Verwijderen";
                buttonrem.Size = new System.Drawing.Size(75, 35);
                buttonrem.Click += new System.EventHandler(this.buttonrem_Click);
                buttonrem.Name = Convert.ToString(mdataReader.GetString(0));

                verticalpos = verticalpos + 40;
            }

        }

        //list removal event
        private void buttonrem_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            int updatenum = Convert.ToInt32(this.updatestage);
            Verhoudingen ingredient = new Verhoudingen("Kruidenformules", updatenum);
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
            string typering = "Kruidenformules";
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
            //db open
            conn.Open();
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
            this.Close();
            th = new Thread(openupdate);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //update
        private void openupdate(object obj)
        {
            Application.Run(new KruidenFormules(this.updatestage));
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
