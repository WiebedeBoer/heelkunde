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
    public partial class Form1 : Form
    {
        Thread th;
        public SqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        //input forms button triggers
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openenkelkruiden);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openwesterskruiden);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openchinesekruiden);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opensyndromen);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openactiessyndromen);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //search trigger
        private void button6_Click(object sender, EventArgs e)
        {
            string searchtext = textBox1.Text;
            string searchtype = comboBox1.SelectedText;
            //executing search
            Search(searchtext, searchtype);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openpinjinkruiden);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //search
        private void Search(string searchtext, string searchtype)
        {
            string search = searchtext;
            string searchkind = searchtype;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            SqlDataReader mdataReader;
            String query,output;
            //db open
            conn.Open();
            //select query according to type search
            switch (searchkind)
            {
                //kruiden
                case "Nederlandse naam kruid":
                    query = "SELECT * FROM Kruiden WHERE Nederlands=search";
                    break;
                case "Latijnse naam kruid":
                    query = "SELECT * FROM Kruiden WHERE Latijns=search";
                    break;
                case "Thermodynamisch in kruid":
                    query = "SELECT * FROM Kruiden WHERE Thermodynamisch=search";
                    break;
                //kruidenformules
                case "Indicaties in kruidenformule":
                    query = "SELECT * FROM Kruidenformules WHERE Indicaties=search";
                    break;
                case "Naam kruidenformule":
                    query = "SELECT * FROM Kruidenformules WHERE Naam=search";
                    break;
                case "Kruid in kruidenformule":
                    query = "SELECT * FROM Kruidenformules, FormulesEnKruiden, Kruiden WHERE Kruidenformules.ID=FormulesEnKruiden.IDKruidenformule AND FormulesEnKruiden.IDKruiden=Kruiden.ID AND Kruiden.Nederlands=search";
                    break;
                //patent formules
                case "Nederlandse naam patentformule":
                    query = "SELECT * FROM Patentformules WHERE Nederlands=search";
                    break;
                case "Engelse naam patentformule":
                    query = "SELECT * FROM Patentformules WHERE Engels=search";
                    break;
                case "Pinjin naam patentformule":
                    query = "SELECT * FROM Patentformules WHERE Pinjin=search";
                    break;
                //syndromen
                case "Syndroom naam":
                    query = "SELECT * FROM Syndromen WHERE Syndroom=search";
                    break;
                case "Syndroom op symptomen pols en tong":
                    query = "SELECT * FROM Syndromen WHERE Pols=search OR Tong=search";
                    break;
                //complex
                case "Patentformule op symptoom":
                    query = "SELECT * FROM Syndromen, Actiesformules, Patentformules WHERE Syndromen.ID=Actieformules.Syndroom AND Actieformules.Patentformule=Patentformules.ID AND Syndromen.Hoofdsymptoom =search";
                    break;
                default:
                    query = "SELECT * FROM Kruiden WHERE Nederlands=search";
                    break;
            }          
            //execute select
            cmd = new SqlCommand(query, conn);
            mdataReader = cmd.ExecuteReader();
            mdataReader.Read();
            //display output
            output = mdataReader.GetString(0);
            //db close
            mdataReader.Close();
            cmd.Dispose();
            conn.Close();
        }

        //open other input forms
        private void openenkelkruiden(object obj)
        {
            
            Application.Run(new Kruiden());
        }

        private void openwesterskruiden(object obj)
        {
            
            Application.Run(new KruidenFormules());
        }

        private void openchinesekruiden(object obj)
        {
            
            Application.Run(new PatentFormule());
        }

        private void opensyndromen(object obj)
        {
            
            Application.Run(new Syndromen());
        }

        private void openactiessyndromen(object obj)
        {
            
            Application.Run(new PatentFormule());
        }

        private void openpinjinkruiden(object obj)
        {

            Application.Run(new ChineseKruiden());
        }


    }
}
