using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Chinees
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(36, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Hoofdmenu";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Zoek";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 

            this.comboBox1.FormattingEnabled = true;
            /*
            this.comboBox1.Items.AddRange(new object[] {
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
            */

            
            this.comboBox1.Items.AddRange(new string[]{"Nederlandse naam kruid","Latijnse naam kruid",
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


            /*
            ComboboxItem item = new ComboboxItem();
            item.Text = "Nederlandse naam kruid";
            item.Value = "Nederlandse naam kruid";
            this.comboBox1.Items.Add(item);
            item.Text = "Syndroom naam";
            item.Value = "Syndroom naam";
            this.comboBox1.Items.Add(item);
            */

            /*
            Dictionary<string,string> comboSource = new Dictionary<string,string>();
            comboSource.Add("Nederlandse naam kruid", "Nederlandse naam kruid");
            comboSource.Add("Latijnse naam kruid", "Latijnse naam kruid");
            comboSource.Add("Thermodynamisch in kruid", "Thermodynamisch in kruid");
            comboSource.Add("Indicaties in kruidenformule", "Indicaties in kruidenformule");
            comboSource.Add("Naam kruidenformule", "Naam kruidenformule");
            comboSource.Add("Kruid in kruidenformule", "Kruid in kruidenformule");
            comboSource.Add("Nederlandse naam patentformule", "Nederlandse naam patentformule");
            comboSource.Add("Engelse naam patentformule", "Engelse naam patentformule");
            comboSource.Add("Pinjin naam patentformule", "Pinjin naam patentformule");
            comboSource.Add("Syndroom naam", "Syndroom naam");
            comboSource.Add("Syndroom op symptomen pols en tong", "Syndroom op symptomen pols en tong");
            comboSource.Add("Patentformule op symptoom", "Patentformule op symptoom");
            this.comboBox1.DataSource = new BindingSource(comboSource, null);
            */

            //DataTable dtab = new DataTable();
            //dtab.Columns.Add("ID", typeof(string));
            //dtab.Columns.Add("Naam", typeof(string));
            //dtab.Load(comboSource);
            //this.comboBox1.ValueMember = "ID";
            //this.comboBox1.DisplayMember = "Naam";
            /*
            this.comboBox1.Items.Insert(0, "Nederlandse naam kruid");
            this.comboBox1.Items.Insert(1, "Latijnse naam kruid");
            this.comboBox1.Items.Insert(2, "Thermodynamisch in kruid");
            this.comboBox1.Items.Insert(3, "Indicaties in kruidenformule");
            this.comboBox1.Items.Insert(4, "Naam kruidenformule");
            this.comboBox1.Items.Insert(5, "Kruid in kruidenformule");
            this.comboBox1.Items.Insert(6, "Nederlandse naam patentformule");
            this.comboBox1.Items.Insert(7, "Engelse naam patentformule");
            this.comboBox1.Items.Insert(8, "Pinjin naam patentformule");
            this.comboBox1.Items.Insert(9, "Syndroom naam");
            this.comboBox1.Items.Insert(10, "Syndroom op symptomen pols en tong");
            this.comboBox1.Items.Insert(11, "Patentformule op symptoom");
            */
            /*
            //connection
            conn = new DBHandler().getConnection();
            //db open
            conn.Open();
            //combo query
            SqlCommand sct = new SqlCommand("SELECT ID, Naam FROM Search ORDER BY ID ASC", conn);
            SqlDataReader treader;
            treader = sct.ExecuteReader();
            treader.Read();
            DataTable dtab = new DataTable();
            dtab.Columns.Add("ID", typeof(int));
            dtab.Columns.Add("Naam", typeof(string));
            dtab.Load(treader);
            comboBox1.ValueMember = "Naam";
            comboBox1.DisplayMember = "Naam";
            comboBox1.DataSource = dtab;
            //reader close
            treader.Close();
            sct.Dispose();
            conn.Close();
            */
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
            this.comboBox1.Location = new System.Drawing.Point(370, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(162, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 20);
            this.textBox1.TabIndex = 6;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Name = "EditForm";
            this.Text = "Zoeken";
            this.ResumeLayout(false);
            this.Load += new System.EventHandler(this.form_load);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}