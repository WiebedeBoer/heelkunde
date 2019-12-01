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
            //Search(searchtext, searchtype);

            this.Close();
            th = new Thread(openactiessyndromen);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openpinjinkruiden);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
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
