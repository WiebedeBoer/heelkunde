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
using System.Data.SqlClient;

namespace Chinees
{
    public partial class Aantekening : Form
    {

        public SqlConnection conn;
        private string coupling;
        private int selectedid;
        Thread th;

        public Aantekening(string coupling, int selectedid)
        {
            InitializeComponent();
            this.coupling = coupling;
            this.selectedid = selectedid;
        }

        //menu making
        private void form_load(object sender, EventArgs e)
        {
            int countnote = ListCheck(this.coupling);
            if (countnote == 1)
            {
                MenuMaker(this.coupling, this.selectedid);
            }
        }

        //terug event
        private void button3_Click(object sender, EventArgs e)
        {
            string trigstage = Convert.ToString(this.selectedid);

            //invoking switch commands
            Switching switcher = new Switching(trigstage);
            Westersekruiden westersekruidenSwitch = new Westersekruiden(switcher);
            Kruidenformules kruidenformulesSwitch = new Kruidenformules(switcher);
            Chinesekruiden chinesekruidenSwitch = new Chinesekruiden(switcher);
            Patentformules patentformulesSwitch = new Patentformules(switcher);
            Syndromes syndromesSwitch = new Syndromes(switcher);
            Syndromeactions syndromeactionsSwitch = new Syndromeactions(switcher);
            Invoker invoked = new Invoker();

            switch (this.coupling)
            {
                case "Kruiden":
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(() => invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Kruidenformules":
                    this.Close();
                    //th = new Thread(openwesterskruiden);
                    th = new Thread(() => invoked.Switchform(kruidenformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Patentformules":
                    this.Close();
                    //th = new Thread(openchinesekruiden);
                    th = new Thread(() => invoked.Switchform(patentformulesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Actieformules":
                    this.Close();
                    //th = new Thread(opensyndromen);
                    th = new Thread(() => invoked.Switchform(syndromesSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                case "Chinesekruiden":
                    this.Close();
                    //th = new Thread(openpinjinkruiden);
                    th = new Thread(() => invoked.Switchform(chinesekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
                default:
                    this.Close();
                    //th = new Thread(openenkelkruiden);
                    th = new Thread(() => invoked.Switchform(westersekruidenSwitch));
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    break;
            }
        }

        //hoofdmenu
        private void button1_Click(object sender, EventArgs e)
        {
            //closing thread
            this.Close();
            th = new Thread(openhoofdmenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        //hoofdmenu
        private void openhoofdmenu(object obj)
        {
            Application.Run(new Form1());
        }

        //invoeren
        private void button2_Click(object sender, EventArgs e)
        {
            Button buttonin = (Button)sender;
            string commentaar = textBox1.Text;
            //data binding
            bool ins = Inserter(selectedid, commentaar);
            if (ins == true)
            {
                //closing thread
                this.Close();
                th = new Thread(Renew);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        //aantekening checking
        public int ListCheck(string searchtype)
        {
            //search id
            int sid = this.selectedid;
            int ccount, checking;
            string sekind = searchtype;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand ccmd;
            String cquery;
            //SqlDataReader cdataReader;
            //db open
            conn.Open();
            //counting
            switch (sekind)
            {
                case "Kruiden":
                    cquery = "SELECT COUNT(ID) AS cco FROM Kruidenaantekeningen WHERE Kruid =@sid";
                    break;
                case "Kruidenformules":
                    cquery = "SELECT COUNT(ID) AS cco FROM Formulesaantekeningen WHERE Kruid =@sid";
                    break;
                case "Chinesekruiden":
                    cquery = "SELECT COUNT(ID) AS cco FROM Chineesaantekeningen WHERE Kruid =@sid";
                    break;
                case "Patentformules":
                    cquery = "SELECT COUNT(ID) AS cco FROM Patentaantekeningen WHERE Patent =@sid";
                    break;
                case "Actieformules":
                    cquery = "SELECT COUNT(ID) AS cco FROM Actiesaantekeningen WHERE Actie =@sid";
                    break;
                default:
                    cquery = "SELECT COUNT(ID) AS cco FROM Formulesaantekeningen WHERE Kruid =@sid";
                    break;
            }
            ccmd = new SqlCommand(cquery, conn);
            ccmd.Parameters.Add(new SqlParameter("@sid", sid));
            //cdataReader = ccmd.ExecuteReader();
            //cdataReader.Read();
            //ccount = cdataReader.GetInt32(0);
            ccmd.CommandText = cquery;
            ccount = (Int32)ccmd.ExecuteScalar();
            //db close
            //cdataReader.Close();
            ccmd.Dispose();
            conn.Close();
            if (ccount > 0)
            {
                checking = 1;
            }
            else
            {
                checking = 999;
            }
            return checking;
        }

        //aantekening display
        public void MenuMaker(string searchtype, int searchid)
        {
            string sekind = searchtype;
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
            int verticalpos = 110;
            int i = 0;
            switch (sekind)
            {
                case "Kruiden":
                    query = "SELECT ID, Aantekening FROM Kruidenaantekeningen WHERE Kruid=@sid";
                    break;
                case "Kruidenformules":
                    query = "SELECT ID, Aantekening FROM Formulesaantekeningen WHERE Kruid=@sid";
                    break;
                case "Chinesekruiden":
                    query = "SELECT ID, Aantekening FROM Chineesaantekeningen WHERE Kruid=@sid";
                    break;
                case "Patentformules":
                    query = "SELECT ID, Aantekening FROM Patentaantekeningen WHERE Patent=@sid";
                    break;
                case "Actieformules":
                    query = "SELECT ID, Aantekening FROM Actiesaantekeningen WHERE Actie=@sid";
                    break;
                default:
                    query = "SELECT ID, Aantekening FROM Formulesaantekeningen WHERE Kruid=@sid";
                    break;
            }
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@sid", selimiter));
            mdataReader = cmd.ExecuteReader();
            //add
            Label outlabel = new System.Windows.Forms.Label();
            Button buttonrem = new System.Windows.Forms.Button();
            while (mdataReader.Read())
            {
                //aantekening

                outlabel.Location = new System.Drawing.Point(140, verticalpos);
                outlabel.Name = "outlabel";
                outlabel.Size = new System.Drawing.Size(600, 100);
                outlabel.Text = mdataReader.GetString(1);

                //id button

                buttonrem.Location = new System.Drawing.Point(40, verticalpos);
                buttonrem.Text = "Verwijderen";
                buttonrem.Size = new System.Drawing.Size(75, 35);
                buttonrem.Click += new System.EventHandler(this.buttonrem_Click);
                buttonrem.Name = Convert.ToString(mdataReader.GetInt32(0));
                i++;
                verticalpos = verticalpos + 110;
            }
            //controls
            Controls.Add(outlabel);
            Controls.Add(buttonrem);
        }

        //aantekening removal event
        private void buttonrem_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            bool rem = Removal(ClickedNum);
            if (rem ==true)
            {
                //closing thread
                this.Close();
                th = new Thread(Renew);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }

        //aantekening remove item
        private bool Removal(int delid)
        {
            int deleteid = delid;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            String query;
            string couple = this.coupling;
            //db open
            conn.Open();
            switch (couple)
            {
                //kruiden
                case "Kruiden":
                    query = "DELETE FROM Kruidenaantekeningen WHERE ID=@deleteid";
                    break;
                //kruidenformules
                case "Kruidenformules":
                    query = "DELETE FROM Formulesaantekeningen WHERE ID=@deleteid";
                    break;
                //chinese kruiden
                case "Chinesekruiden":
                    query = "DELETE FROM Chineesaantekeningen WHERE ID=@deleteid";
                    break;
                //patent formules
                case "Patentformules":
                    query = "DELETE FROM Patentaantekeningen WHERE ID=@deleteid";
                    break;
                case "Actieformules":
                    query = "DELETE FROM Actiesaantekeningen WHERE ID=@deleteid";
                    break;
                default:
                    query = "DELETE FROM Formulesaantekeningen WHERE ID=@deleteid";
                    break;
            }
            //execute delete
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@deleteid", deleteid));
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
            return true;
        }

        //aantekening inserting
        private bool Inserter(int kruidid, string commentaar)
        {
            int kruid = kruidid;
            string aantekeningen = commentaar;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            String query;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string couple = this.coupling;
            //db open
            conn.Open();
            switch (couple)
            {
                //kruiden
                case "Kruiden":
                    query = "INSERT INTO Kruidenaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
                    break;
                //kruidenformules
                case "Kruidenformules":
                    query = "INSERT INTO Formulesaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
                    break;
                //chinese kruiden
                case "Chinesekruiden":
                    query = "INSERT INTO Chineesaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
                    break;
                //patent formules
                case "Patentformules":
                    query = "INSERT INTO Patentaantekeningen (Patent, Aantekening) VALUES(@0, @1)";
                    break;
                //patent formules
                case "Actieformules":
                    query = "INSERT INTO Aciesaantekeningen (Actie, Aantekening) VALUES(@0, @1)";
                    break;
                default:
                    query = "INSERT INTO Formulesaantekeningen (Kruid, Aantekening) VALUES(@0, @1)";
                    break;
            }
            //execute delete
            cmd = new SqlCommand(query, conn);
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", kruid);
            adapter.InsertCommand.Parameters.AddWithValue("@1", aantekeningen);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
            return true;
        }

        //renew
        public void Renew(object obj)
        {
            Application.Run(new Aantekening(this.coupling,this.selectedid));
        }

    }
}
