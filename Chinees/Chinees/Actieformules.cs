using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Chinees
{
    class Actieformules
    {
        public SqlConnection conn;
        private int selectedid;

        public Actieformules(int selectedid)
        {            
            this.selectedid = selectedid;
        }

        //list checking
        public int VerhoudingCheck()
        {
            //search id
            int sid = this.selectedid;
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
            cquery = "SELECT COUNT(ID) AS Maxid FROM Kruidenformules WHERE ID =@sid";
            acquery = "SELECT COUNT(ID) AS Maxid FROM Patentformules WHERE ID =@sid";
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
            query = "SELECT ID, Naam FROM Kruidenformules WHERE ID =@sid ORDER BY Naam ASC";
            aquery = "SELECT ID, Nederlands FROM Patentformules WHERE ID =@sid ORDER BY Nederlands ASC";
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
                //Controls.Add(sidlabel);
                Label outlabel = new System.Windows.Forms.Label();
                outlabel.Location = new System.Drawing.Point(780, verticalpos);
                outlabel.Name = "label1";
                outlabel.Size = new System.Drawing.Size(40, 20);
                outlabel.Text = Convert.ToString(names[i]);
                //Controls.Add(outlabel);
                Button buttonrem = new System.Windows.Forms.Button();
                buttonrem.Location = new System.Drawing.Point(500, verticalpos);
                buttonrem.Text = "Verwijderen";
                buttonrem.Size = new System.Drawing.Size(75, 35);
                buttonrem.Click += new System.EventHandler(this.buttonrem_Click);
                buttonrem.Name = Convert.ToString(names[i]);
                //Controls.Add(buttonrem);
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
            query = "DELETE FROM Actieformules WHERE ID=@deleteid";                  
            //execute delete
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@search", deleteid));
            cmd.ExecuteNonQuery();
            //db close
            cmd.Dispose();
            conn.Close();
        }

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
