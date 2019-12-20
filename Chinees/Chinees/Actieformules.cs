using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Chinees
{
    class Actieformules
    {
        public SqlConnection conn;
        private int selectedid;

        List<Dictionary<string, int>> list = new List<Dictionary<string, int>>();

        ComboBox comboBox1 = new System.Windows.Forms.ComboBox();
        ComboBox comboBox2 = new System.Windows.Forms.ComboBox();
        ComboBox comboBox3 = new System.Windows.Forms.ComboBox();

        public Actieformules(int selectedid)
        {            
            this.selectedid = selectedid;
        }

        //dropdown checking
        public int VerhoudingCheck()
        {
            //search id
            int sid = this.selectedid;
            int leftcount, rightcount, bottomcount, checking;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand ccmd, accmd, cccmd;
            String cquery, acquery, ccquery;
            SqlDataReader cdataReader, acdataReader, ccdataReader;
            //db open
            conn.Open();
            //queries
            cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules";
            acquery = "SELECT COUNT(ID) AS Maxid FROM Patentformules";
            ccquery = "SELECT COUNT(ID) AS Maxid FROM Syndromen";
            //counting
            ccmd = new SqlCommand(cquery, conn);
            //ccmd.Parameters.Add(new SqlParameter("@sid", sid));
            cdataReader = ccmd.ExecuteReader();
            cdataReader.Read();
            leftcount = Convert.ToInt32(cdataReader.GetString(0));
            accmd = new SqlCommand(acquery, conn);
            //accmd.Parameters.Add(new SqlParameter("@sid", sid));
            acdataReader = accmd.ExecuteReader();
            acdataReader.Read();
            rightcount = Convert.ToInt32(acdataReader.GetString(0));
            cccmd = new SqlCommand(acquery, conn);
            //cccmd.Parameters.Add(new SqlParameter("@sid", sid));
            ccdataReader = accmd.ExecuteReader();
            ccdataReader.Read();
            bottomcount = Convert.ToInt32(ccdataReader.GetString(0));
            //db close
            cdataReader.Close();
            acdataReader.Close();
            ccmd.Dispose();
            accmd.Dispose();
            conn.Close();
            if (leftcount > 0 && rightcount > 0 && bottomcount > 0)
            {
                checking = 1;
            }
            else
            {
                checking = 999;
            }
            return checking;
        }

        /*
        public void add_tolist(int name, Dictionary<string, int> edges)
        {
            list[name] = edges;
        }
        */

        /*
        //dropdown selecting
        private List<Dictionary<string, int>> Verhoudingmaker()
        {
            
            //search id
            int sid = this.selectedid;
            string output, aoutput, coutput;
            int did, aid, cid;

            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd, acmd, ccmd;
            String query, aquery, cquery;
            SqlDataReader dataReader, adataReader, cdataReader;
            //db open
            conn.Open();
            //queries
            query = "SELECT ID, Naam FROM Kruidenformules ORDER BY Naam ASC";
            aquery = "SELECT ID, Nederlands FROM Patentformules ORDER BY Nederlands ASC";
            cquery = "SELECT ID, Syndroom FROM Syndromen ORDER BY Syndroom ASC";
            //selecting
            cmd = new SqlCommand(query, conn);
            //cmd.Parameters.Add(new SqlParameter("@sid", sid));
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                did = Convert.ToInt32(dataReader.GetString(0));
                output = dataReader.GetString(1);
                //list.add_tolist(did, output);
            }
            acmd = new SqlCommand(aquery, conn);
            //acmd.Parameters.Add(new SqlParameter("@sid", sid));
            adataReader = acmd.ExecuteReader();
            while (adataReader.Read())
            {
                aid = Convert.ToInt32(adataReader.GetString(0));
                aoutput = adataReader.GetString(1);
                //list.Add(aid,aoutput);
            }
            ccmd = new SqlCommand(cquery, conn);
            //ccmd.Parameters.Add(new SqlParameter("@sid", sid));
            cdataReader = ccmd.ExecuteReader();
            while (cdataReader.Read())
            {
                cid = Convert.ToInt32(adataReader.GetString(0));
                coutput = adataReader.GetString(1);
                //list.Add(aid,aoutput);
            }
            //db close
            dataReader.Close();
            adataReader.Close();
            cdataReader.Close();
            cmd.Dispose();
            acmd.Dispose();
            ccmd.Dispose();
            conn.Close();
            return list;
        }
        */



        private void Combo_Load()
        {
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(700, 40);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(180, 23);
            //connection
            conn = new DBHandler().getConnection();
            SqlCommand sc = new SqlCommand("SELECT ID, Naam FROM Kruidenformules ORDER BY Naam ASC", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Naam", typeof(string));
            dt.Load(reader);

            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Naam";
            comboBox1.DataSource = dt;
            //db close
            reader.Close();
            sc.Dispose();
            conn.Close();
        }





        /*
    private void ShowDropDown()
    {
        //search id
        string output, aoutput, coutput;
        int did, aid, cid;

        //connection
        conn = new DBHandler().getConnection();
        //command and query strings
        SqlCommand cmd, acmd, ccmd;
        String query, aquery, cquery;
        SqlDataReader dataReader, adataReader, cdataReader;
        //db open
        conn.Open();
        //queries
        query = "SELECT ID, Naam FROM Kruidenformules ORDER BY Naam ASC";
        aquery = "SELECT ID, Nederlands FROM Patentformules ORDER BY Nederlands ASC";
        cquery = "SELECT ID, Syndroom FROM Syndromen ORDER BY Syndroom ASC";
        //selecting
        cmd = new SqlCommand(query, conn);
        //cmd.Parameters.Add(new SqlParameter("@sid", sid));
        dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            did = Convert.ToInt32(dataReader.GetString(0));
            output = dataReader.GetString(1);
            //list.add_tolist(did, output);
            //cmdparameter.Items.Add(did,output);
            cmdparameter.DisplayMember = "ItemName"; //display member which displays on screen
            cmdparameter.ValueMember = "ItemID"; //ID
        }
        acmd = new SqlCommand(aquery, conn);
        //acmd.Parameters.Add(new SqlParameter("@sid", sid));
        adataReader = acmd.ExecuteReader();
        while (adataReader.Read())
        {
            aid = Convert.ToInt32(adataReader.GetString(0));
            aoutput = adataReader.GetString(1);
            //list.Add(aid,aoutput);
        }
        ccmd = new SqlCommand(cquery, conn);
        //ccmd.Parameters.Add(new SqlParameter("@sid", sid));
        cdataReader = ccmd.ExecuteReader();
        while (cdataReader.Read())
        {
            cid = Convert.ToInt32(adataReader.GetString(0));
            coutput = adataReader.GetString(1);
            //list.Add(aid,aoutput);
        }
        //db close
        dataReader.Close();
        adataReader.Close();
        cdataReader.Close();
        cmd.Dispose();
        acmd.Dispose();
        ccmd.Dispose();
        conn.Close();
        return list;
    }
    */

            /*
        //dropdown display
        public void DropdownMaker()
        {
            List<List<string>> list = new List<List<string>>();

            //ComboBox comboBox1 = new System.Windows.Forms.ComboBox();
            comboBox1.FormattingEnabled = true;

            //data binding
            //ComboboxItem selectedItem = (ComboboxItem)comboBox1.SelectedItem;
            //ComboboxItem item1 = new ComboboxItem();

            //item1.Text = "test";
            //item1.Value = "123";

            
            comboBox1.Items.AddRange(new object[] {
            "Nederlandse naam kruid",
            "Latijnse naam kruid",
            "Thermodynamisch in kruid",
            "Indicaties in kruidenformule",
            "Naam kruidenformule",
            "Kruid in kruidenformule",
            "Nederlandse naam patentformule",
            "Engelse naam patentformule",
            "Pinjin naam patentformule",
            "Syndroom naam",
            "Syndroom op symptomen pols en tong",
            "Patentformule op symptoom"});
            

            //comboBox1.AddItem "Left Top";

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            //List<ComboboxItem> items = new List<ComboboxItem>;
            //comboBox1.DataSource = items;

            comboBox1.Location = new System.Drawing.Point(700, 40);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(180, 23);

            //ComboBox comboBox2 = new System.Windows.Forms.ComboBox();
            comboBox2.FormattingEnabled = true;
            
            comboBox2.Items.AddRange(new object[] {
                foreach (string name in names)
                {
                ""


                }
            });
            
            comboBox2.Location = new System.Drawing.Point(900, 40);
            comboBox2.Name = "comboBox1";
            comboBox2.Size = new System.Drawing.Size(180, 23);

            //ComboBox comboBox3 = new System.Windows.Forms.ComboBox();
            comboBox3.FormattingEnabled = true;
            
            comboBox2.Items.AddRange(new object[] {
                foreach (string name in names)
                {
                ""


                }
            });
            
            comboBox3.Location = new System.Drawing.Point(1100, 40);
            comboBox3.Name = "comboBox1";
            comboBox3.Size = new System.Drawing.Size(180, 23);

            //insert button
            Button buttonin = new System.Windows.Forms.Button();
            buttonin.Location = new System.Drawing.Point(1160, 40);
            buttonin.Text = "Koppel";
            buttonin.Size = new System.Drawing.Size(75, 35);
            buttonin.Click += new System.EventHandler(this.buttonin_Click);
            buttonin.Name = "Invoeren";

        }
    */

        //insert event
        private void buttonin_Click(object sender, EventArgs e)
        {
            int syndroomid = Convert.ToInt32(comboBox1.SelectedValue);
            int formuleid = Convert.ToInt32(comboBox2.SelectedValue);
            int patentid = Convert.ToInt32(comboBox3.SelectedValue);
            Inserter(syndroomid, formuleid, patentid);
        }

        //list checking
        public int ListCheck()
        {
            //search id
            int sid = this.selectedid;
            int ccount, checking;
            
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand ccmd;
            String cquery;
            SqlDataReader cdataReader;
            //db open
            conn.Open();
            //counting
            cquery = "SELECT COUNT(ID) AS cco FROM Actieformules WHERE ID=@sid";
            ccmd = new SqlCommand(cquery, conn);
            ccmd.Parameters.Add(new SqlParameter("@sid", sid));
            cdataReader = ccmd.ExecuteReader();
            cdataReader.Read();
            ccount = Convert.ToInt32(cdataReader.GetString(0));
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

        //list display
        public void MenuMaker(int searchid)
        {
            
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

            query = "SELECT Actieformules.ID, Kruidenformules.Naam, Patentformules.Nederlands, Syndromen.Syndroom FROM Actieformules, Kruidenformules, Patentformules, Syndromen WHERE Actieformules.Syndroom=Syndromen.ID AND Actieformules.Patentformule=Patentformules.ID AND Actieformules.Kruidenformule=Kruidenformules.ID AND Actieformules.ID=@sid";

            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@sid", selimiter));

            mdataReader = cmd.ExecuteReader();
            while (mdataReader.Read())
            {
                //kruidenformule naam
                Label outlabel = new System.Windows.Forms.Label();
                outlabel.Location = new System.Drawing.Point(700, verticalpos);
                outlabel.Name = "outlabel";
                outlabel.Size = new System.Drawing.Size(180, 20);
                outlabel.Text = Convert.ToString(mdataReader.GetString(1));
                //patent formule nederlands
                Label outlabel2 = new System.Windows.Forms.Label();
                outlabel2.Location = new System.Drawing.Point(900, verticalpos);
                outlabel2.Name = "outlabel2";
                outlabel2.Size = new System.Drawing.Size(180, 20);
                outlabel2.Text = Convert.ToString(mdataReader.GetString(2));
                //syndroom
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

        //list removal event
        private void buttonrem_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            Removal(ClickedNum);
        }

        //list remove item
        private void Removal(int delid)
        {
            int deleteid = delid;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            String query;
            query = "DELETE FROM Actieformules WHERE ID=@deleteid";                  
            //execute delete
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", deleteid));
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
        }

        //list inserting
        private void Inserter(int syndroomid, int formuleid, int patentid)
        {
            int syndroom = syndroomid;
            int formule = formuleid;
            int patent = patentid;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            String query;
            SqlDataAdapter adapter = new SqlDataAdapter();          
            query = "INSERT INTO Actieformules (Syndroom, Patentformule, Kruidenformule) VALUES(@0, @1, @2)";                  
            //execute delete
            cmd = new SqlCommand(query, conn);
            adapter.InsertCommand = new SqlCommand(query, conn);
            adapter.InsertCommand.Parameters.AddWithValue("@0", syndroom);
            adapter.InsertCommand.Parameters.AddWithValue("@1", patent);
            adapter.InsertCommand.Parameters.AddWithValue("@1", formule);
            adapter.InsertCommand.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
        }

    }
}
