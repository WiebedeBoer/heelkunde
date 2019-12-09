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
    public partial class ChineseKruiden : Form
    {
        Thread th;
        public SqlConnection conn;

        public ChineseKruiden()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //executing storage
            Execute();
        }

        private void openhoofdmenu(object obj)
        {
            Application.Run(new Form1());
        }

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
            SqlDataReader mdataReader;
            //SqlDataReader adataReader;
            String query;
            String mquery;
            String aquery;
            //db open
            conn.Open();
            //insert
            query = "INSERT INTO ChineseKruiden (Engels, Latijn, Pinjin, Klasse, Thermodynamisch, Meridiaan, Qi, Werking, Indicaties, Dosering) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9)";
            cmd = new SqlCommand(query, conn);
            //cmd.ExecuteReader();
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
            //select max
            mquery = "SELECT MAX(ID) AS Maxid FROM ChineseKruiden";
            mcmd = new SqlCommand(mquery, conn);
            mdataReader = mcmd.ExecuteReader();
            mdataReader.Read();
            MaxID = Convert.ToInt32(mdataReader.GetValue(0));
            //aantekening
            aquery = "INSERT INTO Chineesaantekeningen (Kruid, Aantekening) VALUES(@0, @1), MaxID, Aantekeningen";
            acmd = new SqlCommand(aquery, conn);
            //adataReader = acmd.ExecuteReader();
            adapter.InsertCommand = new SqlCommand(aquery, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", MaxID);
            adapter.InsertCommand.Parameters.AddWithValue("@1", Aantekeningen);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            mdataReader.Close();
            //adataReader.Close();
            cmd.Dispose();
            mcmd.Dispose();
            acmd.Dispose();
            conn.Close();
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