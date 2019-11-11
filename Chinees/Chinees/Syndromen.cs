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

namespace Chinees
{
    public partial class Syndromen : Form
    {
        Thread th;

        /*
             Layout = "~/_layout.cshtml";
    Database db = app.db;
    dynamic currentuser = app.user.getUser();
    int userID = int.Parse(currentuser["Id"].ToString());
    int userType = int.Parse(currentuser["usertype"].ToString());
    var vstudieID = db.QueryValue("SELECT StudieID FROM users WHERE Id=@0", userID);
             
             */

        public Syndromen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openhoofdmenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void openhoofdmenu(object obj)
        {
            Application.Run(new Form1());
        }

        /*
private void Execute()
{

}
*/

    }
}
