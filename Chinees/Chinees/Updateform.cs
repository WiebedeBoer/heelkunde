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
    public partial class Updateform : Form
    {

        Thread th;
        public SqlConnection conn;
        private int updatestage;
        private string updatetype;

        public Updateform(int editnum, string edittype)
        {
            InitializeComponent();
            this.updatestage = editnum;
            this.updatetype = edittype;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //closing thread
            this.Close();
            th = new Thread(openhoofdmenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void form_load(object sender, EventArgs e)
        {
            buttonmaker();
        }

        private void buttonmaker()
        {
            //check stage
            Button button1 = new System.Windows.Forms.Button();

                button1.Location = new System.Drawing.Point(324, 358);
                button1.Name = Convert.ToString(this.updatestage);
                button1.Size = new System.Drawing.Size(75, 23);
                button1.Text = "Aanpassen";
                button1.UseVisualStyleBackColor = true;
                button1.Click += new System.EventHandler(button2_Click);
                Controls.Add(button1);

        }


        //hoofdmenu
        private void openhoofdmenu(object obj)
        {
            Application.Run(new Form1());
        }

        //updating event
        private void button2_Click(object sender, EventArgs e)
        {
            Button buttoned = (Button)sender;
            string ClickedButton = buttoned.Name;
            Updating(ClickedButton);
        }

        //updating
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
            //SqlDataReader adataReader;
            String query;
            String mquery;
            //select max
            mquery = "SELECT * FROM Kruiden WHERE ID =@search";
            mcmd = new SqlCommand(mquery, conn);
            mcmd.Parameters.Add(new SqlParameter("@search", maxi));
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            //convert to string
            /*
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
            */
            //data form variables
            /*
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
            */
            //updating
            query = "UPDATE Kruiden SET Nederlands =@0, Latijn =@1, Familie =@2, Inhoudsstoffen =@3, GebruikteDelen =@4, Eigenschappen =@5, Smaak =@6, Thermodynamisch =@7, Orgaan =@8, Toepassingen =@9, Actie =@10, Gebruik =@11 WHERE ID =@search";
            cmd = new SqlCommand(query, conn);
            /*
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
            */
            cmd.ExecuteNonQuery();
            //db close
            mdataReader.Close();
            cmd.Dispose();
            mcmd.Dispose();
            conn.Close();
        }
    }
}