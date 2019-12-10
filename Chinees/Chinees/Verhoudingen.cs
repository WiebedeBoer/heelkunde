using System;
using System.Collections.Generic;
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

        //list checking
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
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules WHERE ID =@sid";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden WHERE ID =@sid";
                    break;
                case "Kruidenformules":
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules WHERE ID =@sid";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden WHERE ID =@sid";
                    break;
                case "Chinesekruiden":
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules WHERE ID =@sid";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden WHERE ID =@sid";
                    break;
                case "Patentformules":
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules WHERE ID =@sid";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden WHERE ID =@sid";
                    break;
                default:
                    cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules WHERE ID =@sid";
                    acquery = "SELECT COUNT(ID) AS Maxid FROM Kruiden WHERE ID =@sid";
                    break;
            }
            //counting
            ccmd = new SqlCommand(cquery, conn);
            ccmd.Parameters.Add(new SqlParameter("@sid", sid));
            cdataReader = ccmd.ExecuteReader();
            cdataReader.Read();
            leftcount = Convert.ToInt32(cdataReader.GetString(0));
            accmd = new SqlCommand(acquery, conn);
            accmd.Parameters.Add(new SqlParameter("@sid", sid));
            acdataReader = accmd.ExecuteReader();
            acdataReader.Read();
            rightcount = Convert.ToInt32(acdataReader.GetString(0));
            //db close
            cdataReader.Close();
            acdataReader.Close();
            ccmd.Dispose();
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

        //list selecting
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

        //list display
        public void MenuMaker()
        {
            int verticalpos = 110;
            //string did, output;

            int i = 0;

            //List<List<string>> list = new List<List<string>>();
            var names = new List<string>();
            foreach (string name in names)
            {
                Label sidlabel = new System.Windows.Forms.Label();
                sidlabel.Location = new System.Drawing.Point(700, verticalpos);
                sidlabel.Name = "label1";
                sidlabel.Size = new System.Drawing.Size(40, 20);
                sidlabel.Text = Convert.ToString(names[i]);
                Label outlabel = new System.Windows.Forms.Label();
                outlabel.Location = new System.Drawing.Point(780, verticalpos);
                outlabel.Name = "label1";
                outlabel.Size = new System.Drawing.Size(40, 20);
                outlabel.Text = Convert.ToString(names[i]);
                Button buttonrem = new System.Windows.Forms.Button();
                buttonrem.Location = new System.Drawing.Point(500, verticalpos);
                buttonrem.Text = "Verwijderen";
                buttonrem.Size = new System.Drawing.Size(75, 35);
                buttonrem.Click += new System.EventHandler(this.buttonrem_Click);
                buttonrem.Name = Convert.ToString(names[i]);
                i++;
            }           

        }

        //verwijder
        private void buttonrem_Click(object sender, EventArgs e)
        {
            Button buttondelete = (Button)sender;
            int ClickedNum = Convert.ToInt32(buttondelete.Name);
            Removal(ClickedNum);
        }

        //remove item
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
        }

    }
}
