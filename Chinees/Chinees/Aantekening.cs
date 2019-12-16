using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Chinees
{
    class Aantekening
    {
        public SqlConnection conn;
        private string coupling;
        private int selectedid;

        public Aantekening(string coupling, int selectedid)
        {
            this.coupling = coupling;
            this.selectedid = selectedid;
        }

        //aantekening insert formulier
        public void AantekeningFormulier()
        {
            Label label11 = new System.Windows.Forms.Label();
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(40, 20);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(80, 23);
            label11.Text = "Aantekening";

            TextBox textBox11 = new System.Windows.Forms.TextBox();
            textBox11.Location = new System.Drawing.Point(160, 20);
            textBox11.Name = "textBox11";
            textBox11.Size = new System.Drawing.Size(180, 20);

            Button button11 = new System.Windows.Forms.Button();
            button11.Location = new System.Drawing.Point(360, 20);
            button11.Name = "button11";
            button11.Size = new System.Drawing.Size(100, 23);
            button11.Text = "Aantekening Invoeren";
            button11.UseVisualStyleBackColor = true;
            button11.Click += new System.EventHandler(button11_Click);

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
            SqlDataReader cdataReader;
            //db open
            conn.Open();
            //counting
            switch (sekind)
            {
                case "Kruiden":
                    cquery = "SELECT COUNT(ID) AS cco FROM Kruidenaantekeningen WHERE Kruid=@sid";
                    break;
                case "Kruidenformules":
                    cquery = "SELECT COUNT(ID) AS cco FROM Formulesaantekeningen WHEREKruid=@sid";
                    break;
                case "Chinesekruiden":
                    cquery = "SELECT COUNT(ID) AS cco FROM Chineesaantekeningen WHERE Kruid=@sid";
                    break;
                case "Patentformules":
                    cquery = "SELECT COUNT(ID) AS cco FROM Patentaantekeningen WHERE Patent=@sid";
                    break;
                case "Actieformules":
                    cquery = "SELECT COUNT(ID) AS cco FROM Actiesaantekeningen WHERE Actie=@sid";
                    break;
                default:
                    cquery = "SELECT COUNT(ID) AS cco FROM Formulesaantekeningen WHERE Kruid=@sid";
                    break;
            }
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
            while (mdataReader.Read())
            {
                //aantekening
                Label outlabel = new System.Windows.Forms.Label();
                outlabel.Location = new System.Drawing.Point(700, verticalpos);
                outlabel.Name = "outlabel";
                outlabel.Size = new System.Drawing.Size(180, 20);
                outlabel.Text = Convert.ToString(mdataReader.GetString(1));
                //id button
                Button buttonrem = new System.Windows.Forms.Button();
                buttonrem.Location = new System.Drawing.Point(1160, verticalpos);
                buttonrem.Text = "Verwijderen";
                buttonrem.Size = new System.Drawing.Size(75, 35);
                buttonrem.Click += new System.EventHandler(this.buttonrem_Click);
                buttonrem.Name = Convert.ToString(mdataReader.GetString(0));

                i++;
            }

        }

        //aantekening removal event
        private void buttonrem_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            Removal(ClickedNum);
        }

        //aantekening insert event
        private void button11_Click(object sender, EventArgs e)
        {
            Button buttonin = (Button)sender;
            //string commentaar = textBox11.Text;
            //data binding
            string commentaar = ""; 
            Inserter(selectedid, commentaar);
        }        

        //aantekening remove item
        private void Removal(int delid)
        {
            int deleteid = delid;
            //connection
            conn = new DBHandler().getConnection();
            //command and query strings
            SqlCommand cmd;
            String query;
            string couple = this.coupling;

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
            cmd.Parameters.Add(new SqlParameter("@search", deleteid));
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
        }

        //aantekening inserting
        private void Inserter(int kruidid, string commentaar)
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
        }

    }

}
