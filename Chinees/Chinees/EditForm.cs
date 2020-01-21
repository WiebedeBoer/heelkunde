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
    public partial class EditForm : Form
    {
        Thread th;
        public SqlConnection conn;
        private int selimiter, maxi;
        private int prev, next;
        private string sekind;
        private string search;
        private string number;

        public EditForm(string search, string sekind, int selimiter)
        {
            InitializeComponent();
            this.search = search;
            this.sekind = sekind;
            this.selimiter = selimiter;

        }

        private void form_load(object sender, EventArgs e)
        {
            if (this.search !="NULL" && this.sekind !="NULL")
            {
                Search(this.search, this.sekind, this.selimiter);
            }            
        }

        //search
        private void Search(string searchtext, string searchtype, int searchstart)
        {
            this.search = searchtext;
            this.sekind = searchtype;
            this.selimiter = searchstart;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd, ccmd;
            SqlDataReader mdataReader;
            SqlDataReader cmdataReader;
            String query, cquery, output, did;
            //db open
            conn.Open();
            //select query according to type search
            switch (this.sekind)
            {
                //kruiden
                case "Nederlandse naam kruid":
                    query = "SELECT TOP 10 ID, Nederlands FROM Kruiden WHERE Nederlands LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Kruiden WHERE Nederlands LIKE @search";
                    break;
                case "Latijnse naam kruid":
                    query = "SELECT TOP 10 ID, Latijns FROM Kruiden WHERE Latijns LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Kruiden WHERE Latijns LIKE @search";
                    break;
                case "Thermodynamisch in kruid":
                    query = "SELECT TOP 10 ID, Thermodynamisch FROM Kruiden WHERE Thermodynamisch LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Kruiden WHERE Thermodynamisch LIKE @search";
                    break;
                //kruidenformules
                case "Indicaties in kruidenformule":
                    query = "SELECT TOP 10 ID, Indicaties FROM Kruidenformules WHERE Indicaties LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Kruidenformules WHERE Indicaties LIKE @search";
                    break;
                case "Naam kruidenformule":
                    query = "SELECT TOP 10 ID, Naam FROM Kruidenformules WHERE Naam LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Kruidenformules WHERE Naam LIKE @search ";
                    break;
                case "Kruid in kruidenformule":
                    query = "SELECT TOP 10 FormulesEnKruiden.ID, Kruidenformules.Naam FROM Kruidenformules, FormulesEnKruiden, Kruiden WHERE Kruidenformules.ID=FormulesEnKruiden.IDKruidenformule AND FormulesEnKruiden.IDKruiden=Kruiden.ID AND Kruiden.Nederlands LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(FormulesEnKruiden.ID) as counter FROM Kruidenformules, FormulesEnKruiden, Kruiden WHERE Kruidenformules.ID=FormulesEnKruiden.IDKruidenformule AND FormulesEnKruiden.IDKruiden=Kruiden.ID AND Kruiden.Nederlands LIKE @search";
                    break;
                //patent formules
                case "Nederlandse naam patentformule":
                    query = "SELECT TOP 10 ID, Nederlands FROM Patentformules WHERE Nederlands LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Patentformules WHERE Nederlands LIKE @search";
                    break;
                case "Engelse naam patentformule":
                    query = "SELECT TOP 10 ID, Engels FROM Patentformules WHERE Engels LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Patentformules WHERE Engels LIKE @search";
                    break;
                case "Pinjin naam patentformule":
                    query = "SELECT TOP 10 ID, Pinjin FROM Patentformules WHERE Pinjin LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Patentformules WHERE Pinjin LIKE @search";
                    break;
                //syndromen
                case "Syndroom naam":
                    query = "SELECT TOP 10 ID, Syndroom FROM Syndromen WHERE Syndroom LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Syndromen WHERE Syndroom LIKE @search ";
                    break;
                case "Syndroom op symptomen pols en tong":
                    query = "SELECT TOP 10 ID, Pols FROM Syndromen WHERE Pols LIKE @search OR Tong LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Syndromen WHERE Pols LIKE @search OR Tong LIKE @search";
                    break;
                //complex
                case "Patentformule op symptoom":
                    query = "SELECT TOP 10 Actieformules.ID, Patentformules.Nederlands FROM Syndromen, Actiesformules, Patentformules WHERE Syndromen.ID=Actieformules.Syndroom AND Actieformules.Patentformule=Patentformules.ID AND Syndromen.Hoofdsymptoom LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(Actieformules.ID) AS counter FROM Syndromen, Actiesformules, Patentformules WHERE Syndromen.ID=Actieformules.Syndroom AND Actieformules.Patentformule=Patentformules.ID AND Syndromen.Hoofdsymptoom LIKE @search";
                    break;
                default:
                    query = "SELECT TOP 10 ID, Nederlands FROM Kruiden WHERE Nederlands LIKE @search ORDER BY ID ASC";
                    cquery = "SELECT COUNT(ID) AS counter FROM Kruiden WHERE Nederlands LIKE @search";
                    break;
            }
            //execute select
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", this.search));
            cmd.Parameters.Add(new SqlParameter("@limiter", this.selimiter));
            int verticalpos = 110;
            mdataReader = cmd.ExecuteReader();
            while (mdataReader.Read())
            {
                //display id
                did = Convert.ToString(mdataReader.GetInt32(0));
                Label idlabel = new System.Windows.Forms.Label();
                idlabel.Location = new System.Drawing.Point(20, verticalpos);
                idlabel.Name = "label1";
                idlabel.Size = new System.Drawing.Size(40, 20);
                idlabel.Text = did;
                Controls.Add(idlabel);
                //display output
                output = mdataReader.GetString(1);
                Label outputlabel = new System.Windows.Forms.Label();
                outputlabel.Location = new System.Drawing.Point(80, verticalpos);
                outputlabel.Name = "label2";
                outputlabel.Size = new System.Drawing.Size(280, 20);
                outputlabel.Text = output;
                Controls.Add(outputlabel);
                //edit
                Button buttonedit = new System.Windows.Forms.Button();
                buttonedit.Location = new System.Drawing.Point(380, verticalpos);
                buttonedit.Text = "Aanpassen";
                buttonedit.Size = new System.Drawing.Size(75, 35);
                buttonedit.Click += new System.EventHandler(this.buttonedit_Click);
                buttonedit.Name = did;
                Controls.Add(buttonedit);
                //verwijder
                Button buttondelete = new System.Windows.Forms.Button();
                buttondelete.Location = new System.Drawing.Point(500, verticalpos);
                buttondelete.Text = "Verwijderen";
                buttondelete.Size = new System.Drawing.Size(75, 35);
                buttondelete.Click += new System.EventHandler(this.buttondelete_Click);
                buttondelete.Name = did;
                Controls.Add(buttondelete);
                //increment position
                verticalpos = verticalpos + 45;

            }
            //close reader
            mdataReader.Close();
            cmd.Dispose();
            //count maximum
            ccmd = new SqlCommand(cquery, conn);
            ccmd.Parameters.Add(new SqlParameter("@search", search));
            cmdataReader = ccmd.ExecuteReader();
            cmdataReader.Read();
            this.maxi = cmdataReader.GetInt32(0);
            //db close
            cmdataReader.Close();
            ccmd.Dispose();
            conn.Close();
            //vorige en volgende button
            this.prev = this.selimiter - 10;
            this.next = this.selimiter + 10;
            //if vorige
            if (this.prev > 0)
            {
                Button buttonprev = new System.Windows.Forms.Button();
                buttonprev.Location = new System.Drawing.Point(20, 65);
                buttonprev.Text = "Vorige";
                buttonprev.Size = new System.Drawing.Size(75, 35);
                buttonprev.Click += new System.EventHandler(this.buttonvorige_Click);
                buttonprev.Name = "Vorige";
                Controls.Add(buttonprev);
            }
            //if volgende
            if (this.next < this.maxi)
            {
                Button buttonnext = new System.Windows.Forms.Button();
                buttonnext.Location = new System.Drawing.Point(380, 65);
                buttonnext.Text = "Volgende";
                buttonnext.Size = new System.Drawing.Size(75, 35);
                buttonnext.Click += new System.EventHandler(this.buttonvolgende_Click);
                buttonnext.Name = "Volgende";
                Controls.Add(buttonnext);
            }

        }

        //wijzig
        private void buttonedit_Click(object sender, EventArgs e)
        {
            Button buttonedit = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttonedit.Name);
            //switch form
            Switchform(ClickedNum);
        }

        //verwijder
        private void buttondelete_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            this.number = Convert.ToString(ClickedNum);
            //verwijder
            bool rem = Removal(ClickedNum);
            //refresh
            if (rem ==true)
            {
                Renew();
            }            
        }

        //switch to editing form
        private void Switchform(int num)
        {
            int number = num;
            string mystage = Convert.ToString(number);

            //invoking switch commands
            Switching switcher = new Switching(mystage);
            Westersekruiden westersekruidenSwitch = new Westersekruiden(switcher);
            Kruidenformules kruidenformulesSwitch = new Kruidenformules(switcher);
            Chinesekruiden chinesekruidenSwitch = new Chinesekruiden(switcher);
            Patentformules patentformulesSwitch = new Patentformules(switcher);
            Syndromes syndromesSwitch = new Syndromes(switcher);
            Syndromeactions syndromeactionsSwitch = new Syndromeactions(switcher);
            Invoker invoked = new Invoker();

            switch (this.sekind)
            {
                //kruiden
                case "Nederlandse naam kruid":
                    //closing thread
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(() => invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Latijnse naam kruid":
                    //closing thread
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(() => invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Thermodynamisch in kruid":
                    //closing thread
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(() => invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                //kruidenformules
                case "Indicaties in kruidenformule":
                    //closing thread
                    this.Close();
                    //th = new Thread(openwesterskruiden);
                    th = new Thread(() => invoked.Switchform(kruidenformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Naam kruidenformule":
                    //closing thread
                    this.Close();
                    //th = new Thread(openwesterskruiden);
                    th = new Thread(() => invoked.Switchform(kruidenformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Kruid in kruidenformule":
                    //closing thread
                    this.Close();
                    //th = new Thread(openwesterskruiden);
                    th = new Thread(() => invoked.Switchform(kruidenformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                //patent formules
                case "Nederlandse naam patentformule":
                    //closing thread
                    this.Close();
                    //th = new Thread(openchinesekruiden);
                    th = new Thread(() => invoked.Switchform(patentformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Engelse naam patentformule":
                    //closing thread
                    this.Close();
                    //th = new Thread(openchinesekruiden);
                    th = new Thread(() => invoked.Switchform(patentformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Pinjin naam patentformule":
                    //closing thread
                    this.Close();
                    //th = new Thread(openchinesekruiden);
                    th = new Thread(() => invoked.Switchform(patentformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                //syndromen
                case "Syndroom naam":
                    //closing thread
                    this.Close();
                    //th = new Thread(opensyndromen);
                    th = new Thread(() => invoked.Switchform(syndromesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Syndroom op symptomen pols en tong":
                    //closing thread
                    this.Close();
                    //th = new Thread(opensyndromen);
                    th = new Thread(() => invoked.Switchform(syndromesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                //complex
                case "Patentformule op symptoom":
                    //closing thread
                    this.Close();
                    //th = new Thread(opensyndromen);
                    th = new Thread(() => invoked.Switchform(syndromesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                default:
                    //closing thread
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(() => invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
            }
        }

        //renewing
        //refresh search form
        private void Renew()
        {
            this.Close();
            th = new Thread(opensearch);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //open search forms
        private void opensearch(object obj)
        {
            Application.Run(new EditForm(this.search,this.sekind,this.selimiter));
        }

        //remove item
        private bool Removal(int delid)
        {
            int deleteid = delid;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            String query;
            //db open
            conn.Open();
            //type
            switch (this.sekind)
            {
                //kruiden
                case "Nederlandse naam kruid":
                    query = "DELETE FROM Kruiden WHERE ID=@deleteid";
                    break;
                case "Latijnse naam kruid":
                    query = "DELETE FROM Kruiden WHERE ID=@deleteid";
                    break;
                case "Thermodynamisch in kruid":
                    query = "DELETE FROM Kruiden WHERE ID=@deleteid";
                    break;
                //kruidenformules
                case "Indicaties in kruidenformule":
                    query = "DELETE FROM Kruidenformules WHERE ID=@deleteid";
                    break;
                case "Naam kruidenformule":
                    query = "DELETE FROM Kruidenformules WHERE ID=@deleteid";
                    break;
                case "Kruid in kruidenformule":
                    query = "DELETE FROM FormulesEnKruiden WHERE ID=@deleteid";
                    break;
                //patent formules
                case "Nederlandse naam patentformule":
                    query = "DELETE FROM Patentformules WHERE ID=@deleteid";
                    break;
                case "Engelse naam patentformule":
                    query = "DELETE FROM Patentformules WHERE ID=@deleteid";
                    break;
                case "Pinjin naam patentformule":
                    query = "DELETE FROM Patentformules WHERE ID=@deleteid";
                    break;
                //syndromen
                case "Syndroom naam":
                    query = "DELETE FROM Syndromen WHERE ID=@deleteid";
                    break;
                case "Syndroom op symptomen pols en tong":
                    query = "DELETE FROM Syndromen WHERE ID=@deleteid";
                    break;
                //complex
                case "Patentformule op symptoom":
                    query = "DELETE FROM Actiesformules WHERE ID=@deleteid";
                    break;
                default:
                    query = "DELETE FROM Kruiden WHERE ID=@deleteid";
                    break;
            }
            //execute delete
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", deleteid));
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
            //return type
            return true;
        }

        //hoofdmenu
        private void openhoofdmenu(object obj)
        {
            Application.Run(new Form1());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //closing thread
            this.Close();
            th = new Thread(openhoofdmenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //volgende en vorige buttons
        //volgende
        private void buttonvolgende_Click(object sender, EventArgs e)
        {
            //string searchtext = this.search;
            //string searchtype = this.sekind;
            this.selimiter = this.next;
            //Search(searchtext, searchtype, searchstart);
            //refresh
            Renew();
        }

        //vorige
        private void buttonvorige_Click(object sender, EventArgs e)
        {
            //string searchtext = this.search;
            //string searchtype = this.sekind;
            this.selimiter = this.prev;
            //Search(searchtext, searchtype, searchstart);
            //refresh
            Renew();
        }

        //zoeken
        private void button1_Click(object sender, EventArgs e)
        {
            this.search = textBox1.Text;
            this.sekind = comboBox1.SelectedText;
            this.selimiter = 0;
            //Search(searchtext, searchtype, searchstart);
            //refresh
            Renew();
        }

        /*
//open other input forms
//kruiden
private void openenkelkruiden(object obj)
{
    string numbr = this.number;
    Application.Run(new Kruiden(numbr));
}

//kruidenformules
private void openwesterskruiden(object obj)
{
    string numbr = this.number;
    Application.Run(new KruidenFormules(numbr));
}

//patentformules
private void openchinesekruiden(object obj)
{
    string numbr = this.number;
    Application.Run(new PatentFormule(numbr));
}

//syndromen
private void opensyndromen(object obj)
{
    string numbr = this.number;
    Application.Run(new Syndromen(numbr));
}

//syndromenacties
private void openactiessyndromen(object obj)
{
    string numbr = this.number;
    Application.Run(new SyndroomActie(numbr));
}

//chinesekruiden
private void openpinjinkruiden(object obj)
{
    string numbr = this.number;
    Application.Run(new ChineseKruiden(numbr));
}
*/


    }
}
