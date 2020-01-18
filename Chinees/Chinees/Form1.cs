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
        //Thread th;
        public SqlConnection conn;
        private string stager = "0";

        public Form1()
        {
            InitializeComponent();

        }

        //input forms button triggers
        private void button1_Click(object sender, EventArgs e)
        {
            Button buttontrig = (Button)sender;
            string trigname = buttontrig.Name;
            //Thread th = new Thread(() => myMethod(myGrid));
            Thread th;

            //invoking switch commands
            Switching switcher = new Switching(this.stager);
            Westersekruiden westersekruidenSwitch = new Westersekruiden(switcher);
            Kruidenformules kruidenformulesSwitch = new Kruidenformules(switcher);
            Chinesekruiden chinesekruidenSwitch = new Chinesekruiden(switcher);
            Patentformules patentformulesSwitch = new Patentformules(switcher);
            Syndromes syndromesSwitch = new Syndromes(switcher);
            Syndromeactions syndromeactionsSwitch = new Syndromeactions(switcher);
            Invoker invoked = new Invoker();

            switch (trigname)
            {
                case "button1":
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(()=>invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "button2":
                    this.Close();
                    //th = new Thread(openwesterskruiden);
                    th = new Thread(()=>invoked.Switchform(kruidenformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "button3":
                    this.Close();
                    //th = new Thread(openchinesekruiden);
                    th = new Thread(()=>invoked.Switchform(patentformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "button4":
                    this.Close();
                    //th = new Thread(opensyndromen);
                    th = new Thread(()=>invoked.Switchform(syndromesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "button5":
                    this.Close();
                    //th = new Thread(openactiessyndromen);
                    th = new Thread(()=>invoked.Switchform(syndromeactionsSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "button8":
                    this.Close();
                    //th = new Thread(openpinjinkruiden);
                    th = new Thread(()=>invoked.Switchform(chinesekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                default:
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(()=>invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
            }

        }



        //searching
        //search trigger
        private void button6_Click(object sender, EventArgs e)
        {
            Thread th;
            this.Close();
            th = new Thread(opensearch);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //open search forms
        private void opensearch(object obj)
        {
            string search ="NULL";
            string kind = "NULL";
            int start =0;
            Application.Run(new EditForm(search,kind,start));
        }

        /*
        //open other input forms
        //kruiden
        private void openenkelkruiden(object obj)
        {
            Application.Run(new Kruiden(this.stager));
        }

        //kruidenformules
        private void openwesterskruiden(object obj)
        {
            Application.Run(new KruidenFormules(this.stager));
        }

        //patentformules
        private void openchinesekruiden(object obj)
        {
            Application.Run(new PatentFormule(this.stager));
        }

        //syndromen
        private void opensyndromen(object obj)
        {
            Application.Run(new Syndromen(this.stager));
        }

        //syndromenacties
        private void openactiessyndromen(object obj)
        {
            Application.Run(new SyndroomActie(this.stager));
        }

        //chinesekruiden
        private void openpinjinkruiden(object obj)
        {
            Application.Run(new ChineseKruiden(this.stager));
        }
        */

        /*
private void button2_Click(object sender, EventArgs e)
{
    this.Close();
    th = new Thread(openwesterskruiden);
    //th = new Thread(invoked.Switchform(westersekruidenSwitch));
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

private void button8_Click(object sender, EventArgs e)
{
    this.Close();
    th = new Thread(openpinjinkruiden);
    th.SetApartmentState(ApartmentState.STA);
    th.Start();
}
*/

    }
}
