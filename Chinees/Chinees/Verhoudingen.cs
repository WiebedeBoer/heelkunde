using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Chinees
{
    class Verhoudingen
    {
        public SqlConnection conn;
        private string coupling;
        private int selectedid;

        public Verhoudingen(string coupling, int selectedid)
        {
            this.coupling = coupling;
            this.selectedid = selectedid;
        }

        //dropdown checking
        public int VerhoudingCheck()
        {
            //search id
            int sid = this.selectedid;
            string couple = this.coupling;
            int leftcount, rightcount, checking;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand ccmd, accmd;
            String cquery, acquery;
            SqlDataReader cdataReader, acdataReader;
            //db open
            conn.Open();
            //queries
            switch (couple)
            {
                case "Kruiden":
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden";
                    break;
                case "Kruidenformules":
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden";
                    break;
                case "Chinesekruiden":
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Patentformules";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM ChineseKruiden";
                    break;
                case "Patentformules":
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Patentformules";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM ChineseKruiden";
                    break;
                default:
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden";
                    break;
            }
            //counting
            ccmd = new SqlCommand(cquery, conn);
            cdataReader = ccmd.ExecuteReader();
            cdataReader.Read();
            leftcount = cdataReader.GetInt32(0);
            //leftcount = Convert.ToInt32(leftstr);
            //close reader
            cdataReader.Close();
            ccmd.Dispose();
            //counting
            accmd = new SqlCommand(acquery, conn);
            acdataReader = accmd.ExecuteReader();
            acdataReader.Read();
            rightcount = Convert.ToInt32(acdataReader.GetInt32(0));
            //db close
            acdataReader.Close();
            accmd.Dispose();
            conn.Close();
            if (leftcount > 0 && rightcount > 0)
            {
                checking = 1;
            }
            else
            {
                checking = 999;
            }
            return checking;
        }

        //list checking
        public int ListCheck()
        {
            //search id
            int sid = this.selectedid;
            int ccount, checking;
            string sekind = this.coupling;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand ccmd;
            String cquery;
            SqlDataReader cdataReader;
            //db open
            conn.Open();
            //counting
            switch (sekind)
            {
                case "Kruiden":
                    cquery = "SELECT COUNT(IDKruiden) AS cco FROM FormulesEnKruiden WHERE IDKruiden =@sid";
                    break;
                case "Kruidenformules":
                    cquery = "SELECT COUNT(IDKruidenformule) AS cco FROM FormulesEnKruiden WHERE IDKruidenformule=@sid";
                    break;
                case "Chinesekruiden":
                    cquery = "SELECT COUNT(Chinesekruiden) AS cco FROM PatentEnKruiden WHERE Chinesekruiden =@sid";
                    break;
                case "Patentformules":
                    cquery = "SELECT COUNT(Patentformule) AS cco FROM PatentEnKruiden WHERE Patentformule=@sid";
                    break;
                default:
                    cquery = "SELECT COUNT(ID) AS cco FROM FormulesEnKruiden WHERE ID =@sid";
                    break;
            }
            ccmd = new SqlCommand(cquery, conn);
            ccmd.Parameters.Add(new SqlParameter("@sid", sid));
            cdataReader = ccmd.ExecuteReader();
            cdataReader.Read();
            ccount = Convert.ToInt32(cdataReader.GetInt32(0));
            //ccount = (Int32)ccmd.ExecuteScalar();

            //db close
            cdataReader.Close();
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

        //list remove item
        public bool Removal(int delid)
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
                    query = "DELETE FROM FormulesEnKruiden WHERE ID=@deleteid";
                    break;
                //kruidenformules
                case "Kruidenformules":
                    query = "DELETE FROM FormulesEnKruiden WHERE ID=@deleteid";
                    break;
                //chinese kruiden
                case "Chinesekruiden":
                    query = "DELETE FROM PatentEnKruiden WHERE ID=@deleteid";
                    break;
                //patent formules
                case "Patentformules":
                    query = "DELETE FROM PatentEnKruiden WHERE ID=@deleteid";
                    break;
                default:
                    query = "DELETE FROM FormulesEnKruiden WHERE ID=@deleteid";
                    break;
            }
            //execute delete
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", deleteid));
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
            return true;
        }

        //list inserting
        public bool Inserter(int kruidid, int formuleid, int hoeveelheid)
        {
            int kruid = kruidid;
            int formule = formuleid;
            int verhouding = hoeveelheid;
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
                    query = "INSERT INTO FormulesEnKruiden (IDKruidenformule, IDKruiden, Verhouding) VALUES(@0, @1, @2)";
                    break;
                //kruidenformules
                case "Kruidenformules":
                    query = "INSERT INTO FormulesEnKruiden (IDKruidenformule, IDKruiden, Verhouding) VALUES(@0, @1, @2)";
                    break;
                //chinese kruiden
                case "Chinesekruiden":
                    query = "INSERT INTO PatentEnKruiden (Patentformule, Chinesekruiden, Verhouding) VALUES(@0, @1, @2)";
                    break;
                //patent formules
                case "Patentformules":
                    query = "INSERT INTO PatentEnKruiden (Patentformule, Chinesekruiden, Verhouding) VALUES(@0, @1, @2)";
                    break;
                default:
                    query = "INSERT INTO FormulesEnKruiden (IDKruidenformule, IDKruiden, Verhouding) VALUES(@0, @1, @2)";
                    break;
            }
            //execute delete
            cmd = new SqlCommand(query, conn);
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", kruid);
            adapter.InsertCommand.Parameters.AddWithValue("@1", formule);
            adapter.InsertCommand.Parameters.AddWithValue("@2", verhouding);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
            return true;
        }













        /*

        //dropdown selecting
        private List<List<string>> Verhoudingmaker()
        {
            List<List<string>> list = new List<List<string>>();
            //search id
            int sid = this.selectedid;
            string couple = this.coupling;
            string did, output, aid, aoutput;

            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd, acmd;
            String query, aquery;
            SqlDataReader dataReader, adataReader;
            //db open
            conn.Open();
            //queries
            switch (couple)
            {
                case "Kruiden":
                    query = "SELECT ID, Naam FROM Kruidenformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM Kruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                case "Kruidenformules":
                    query = "SELECT ID, Naam FROM Kruidenformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM Kruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                case "Chinesekruiden":
                    query = "SELECT ID, Naam FROM Patentformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM ChineseKruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                case "Patentformules":
                    query = "SELECT ID, Naam FROM Patentformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM ChineseKruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                default:
                    query = "SELECT ID, Naam FROM Kruidenformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM Kruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
            }
            //selecting
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@sid", sid));
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                did = dataReader.GetString(0);
                output = dataReader.GetString(1);
                //list.Add(did,output);
            }
            acmd = new SqlCommand(aquery, conn);
            acmd.Parameters.Add(new SqlParameter("@sid", sid));
            adataReader = acmd.ExecuteReader();
            while (adataReader.Read())
            {
                aid = adataReader.GetString(0);
                aoutput = adataReader.GetString(1);
                //list.Add(aid,aoutput);
            }
            //db close
            dataReader.Close();
            adataReader.Close();
            cmd.Dispose();
            acmd.Dispose();
            conn.Close();
            return list;
        }

        */







        /*
        //dropdown display
        public void DropdownMaker ()
        {
            string couple = this.coupling;
            String query, aquery;

            switch (couple)
            {
                case "Kruiden":
                    query = "SELECT ID, Naam FROM Kruidenformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM Kruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                case "Kruidenformules":
                    query = "SELECT ID, Naam FROM Kruidenformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM Kruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                case "Chinesekruiden":
                    query = "SELECT ID, Naam FROM Patentformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM ChineseKruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                case "Patentformules":
                    query = "SELECT ID, Naam FROM Patentformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM ChineseKruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
                default:
                    query = "SELECT ID, Naam FROM Kruidenformules WHERE ID =@sid ORDER BY Naam ASC";
                    aquery = "SELECT ID, Nederlands FROM Kruiden WHERE ID =@sid ORDER BY Nederlands ASC";
                    break;
            }

            ComboBox comboBox1 = new System.Windows.Forms.ComboBox();
            comboBox1.FormattingEnabled = true;
            //data binding
            
            //comboBox1.Items.AddRange(new object[] {
            //    foreach (string name in names)
             //   {
             //   ""
             //
             //
             //   }
            // });
            
            comboBox1.Location = new System.Drawing.Point(700, 40);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(180, 23);

            //connection
            conn = new DBHandler().getConnection();

            //first select
            SqlCommand sc = new SqlCommand(query, conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Naam", typeof(string));
            dt.Load(reader);

            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Naam";
            comboBox1.DataSource = dt;

            ComboBox comboBox2 = new System.Windows.Forms.ComboBox();
            comboBox1.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(900, 40);
            comboBox2.Name = "comboBox1";
            comboBox2.Size = new System.Drawing.Size(180, 23);

            //first select
            SqlCommand sca = new SqlCommand(aquery, conn);
            SqlDataReader areader;

            areader = sca.ExecuteReader();
            DataTable dta = new DataTable();

            dta.Columns.Add("ID", typeof(string));
            dta.Columns.Add("Naam", typeof(string));
            dta.Load(areader);

            comboBox2.ValueMember = "ID";
            comboBox2.DisplayMember = "Naam";
            comboBox2.DataSource = dt;


            //db close
            reader.Close();
            areader.Close();
            sc.Dispose();
            sca.Dispose();
            conn.Close();

        }
        */

        /*
    //list display
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
                query = "SELECT FormulesEnKruiden.ID, Kruidenformules.Naam, Kruiden.Nederlands, FormulesEnKruiden.Verhouding FROM Kruidenformules, FormulesEnKruiden, Kruiden WHERE Kruidenformules.ID=FormulesEnKruiden.IDKruidenformule AND FormulesEnKruiden.IDKruiden=Kruiden.ID AND FormulesEnKruiden.ID=@sid";
                break;
            case "Kruidenformules":
                query = "SELECT FormulesEnKruiden.ID, Kruidenformules.Naam, Kruiden.Nederlands, FormulesEnKruiden.Verhouding FROM Kruidenformules, FormulesEnKruiden, Kruiden WHERE Kruidenformules.ID=FormulesEnKruiden.IDKruidenformule AND FormulesEnKruiden.IDKruiden=Kruiden.ID AND FormulesEnKruiden.ID=@sid";
                break;
            case "Chinesekruiden":
                query = "SELECT PatentEnKruiden.ID, Patentformules.Nederlands, ChineseKruiden.Engels, PatentEnKruiden.Verhouding FROM Patentformules, PantentEnKruiden, ChineseKruiden WHERE Patentformules.ID=PatentEnKruiden.Patentformule AND PatentEnKruiden.ChineseKruiden=ChineseKruiden.ID AND PatentEnKruiden.ID=@sid";
                break;
            case "Patentformules":
                query = "SELECT PatentEnKruiden.ID, Patentformules.Nederlands, ChineseKruiden.Engels, PatentEnKruiden.Verhouding FROM Patentformules, PantentEnKruiden, ChineseKruiden WHERE Patentformules.ID=PatentEnKruiden.Patentformule AND PatentEnKruiden.ChineseKruiden=ChineseKruiden.ID AND PatentEnKruiden.ID=@sid";
                break;
            default:
                query = "SELECT FormulesEnKruiden.ID, Kruidenformules.Naam, Kruiden.Nederlands, FormulesEnKruiden.Verhouding FROM Kruidenformules, FormulesEnKruiden, Kruiden WHERE Kruidenformules.ID=FormulesEnKruiden.IDKruidenformule AND FormulesEnKruiden.IDKruiden=Kruiden.ID AND FormulesEnKruiden.ID=@sid";
                break;
        }
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

            i++;
        }           

    }   

    */

        /*
    //list removal event
    private void buttonrem_Click(object sender, EventArgs e)
    {
        Button buttondelete = (Button)sender;
        int ClickedNum = Convert.ToInt32(buttondelete.Name);
        Removal(ClickedNum);
    }
    */





    }
}
