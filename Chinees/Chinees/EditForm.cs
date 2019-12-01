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

        public EditForm()
        {
            InitializeComponent();
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
            String query, output;
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
            int verticalpos = 110;
            mdataReader = cmd.ExecuteReader();
            while (mdataReader.Read())
            {
                //display output
                output = mdataReader.GetString(0);
                Label outputlabel = new System.Windows.Forms.Label();
                outputlabel.Text = output;
                Button buttonedit = new System.Windows.Forms.Button();
                buttonedit.Location = new System.Drawing.Point(380, verticalpos);
                buttonedit.Text = "Aanpassen";
                buttonedit.Size = new System.Drawing.Size(75, 35);
                buttonedit.Click += new System.EventHandler(this.button2_Click);
                Button buttondelete = new System.Windows.Forms.Button();
                buttondelete.Location = new System.Drawing.Point(500, verticalpos);
                buttondelete.Text = "Verwijderen";
                buttondelete.Size = new System.Drawing.Size(75, 35);
                buttondelete.Click += new System.EventHandler(this.button2_Click);
                verticalpos = verticalpos + 45;
            }
            //db close
            mdataReader.Close();
            cmd.Dispose();
            conn.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Execute()
        {

        }

        /*
                            string data = "SELECT ID, Nederlands, Latijn, Familie, Inhoudsstof, Toepassingen, Eigenschappen, Actie, Gebruik, Contraindicaties, Foto,Smaak, Dosering, Thermodynamisch, GebruikteDelen, Orgaan FROM Kruiden LIMIT 0,100";
                                dynamic courses = db.Query(data, ID, i);


          string data = "SELECT ID, Naam, Indicaties, Werking, Smaak, Meridiaan, Qi, Contraindicaties FROM Kruidenformules LIMIT 0,100";
                                dynamic courses = db.Query(data, userID, userType, i);


           string data = "SELECT ID, Nederlands, Engels, Pinjin, Indicaties, Werking, Tong, Pols, Contraindicaties, Indicaties FROM Patentformules LIMIT 0,100";
                                dynamic courses = db.Query(data, userID, userType, i);
         
         
         */



    }
}
