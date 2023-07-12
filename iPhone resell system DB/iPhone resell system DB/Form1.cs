using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using iPhone_resell_system_DB.Core;
using iPhone_resell_system_DB.Core.dao_sql;
using System.Collections.ObjectModel;
using iPhone_resell_system_DB.Forms;

namespace iPhone_resell_system_DB
{
    public partial class Form1 : Form
    {
        public static int id_ucet = 0;
        public string connection = @"Data Source=dbsys.cs.vsb.cz\STUDENT;Initial Catalog=KAR0229;Persist Security Info=True;User ID=KAR0229;Password=L1OGyD3r8vQNaW78";
        public Form1()
        {
            InitializeComponent();
            this.timer2.Start();
        }

        private void Load_data()
        {
            Database db = new Database();
            db.Connect();

            foreach (iPhone item in iPhoneTable.Select_models(db))
            {
                listBox1.Items.Add(item.model);
            }
            db.Close();
        }

        private void GetListItem()
        {
            Database db = new Database();
            db.Connect();

            foreach (iPhone item in iPhoneTable.Select_bez_zaplaceni(db))
            {
                ListItem listItem = new ListItem();
                listItem.Nazev = item.model;
                listItem.Cena = item.cena.ToString();
                listItem.id = item.iphone_id;
                listItem.v_kosiku = item.v_kosiku;

                flowLayoutPanel1.Controls.Add(listItem);
            }
            db.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add a = new Add();
            a.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tabs t = new Tabs();
            t.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_data();
            GetListItem();
            load_data_combobox();
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            GetListItem();
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);

            if (text == "Všechny iPhony")
            {
                flowLayoutPanel1.Controls.Clear();
                GetListItem();
                timer2.Start();
            }
            else
            {
                Database db = new Database();
                db.Connect();
                flowLayoutPanel1.Controls.Clear();

                foreach (Zaznam item in iPhoneTable.Filter(db, text, null, null, null, true, true))
                {
                    ListItem listItem = new ListItem();
                    listItem.Nazev = item.iphone.model;
                    listItem.Cena = item.iphone.cena.ToString();
                    listItem.id = item.iphone.iphone_id;
                    listItem.v_kosiku = item.iphone.v_kosiku;

                    flowLayoutPanel1.Controls.Add(listItem);
                }
                db.Close();
                timer2.Stop();
            }
        }
        private void load_data_combobox()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT jmeno FROM Ucet", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet data = new DataSet();
            da.Fill(data);
            cmd.ExecuteNonQuery();

            con.Close();

            comboBox1.DataSource = data.Tables[0];
            comboBox1.DisplayMember = "jmeno";
            comboBox1.ValueMember = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_ucet = comboBox1.SelectedIndex + 1;
            load_ucet_role();
        }

        private void load_ucet_role()
        {
            Database db = new Database();
            db.Connect();

            Ucet u = UcetTable.Select(id_ucet, db);

            if (u.role == "A")
            {
                label3.Text = "ADMIN";
                button1.Enabled = true;
                button2.Enabled = true;
            }
            if (u.role == "P")
            {
                label3.Text = "PRODEJCE";
                button1.Enabled = true;
                button2.Enabled = true;
            }
            if (u.role == "Z")
            {
                label3.Text = "ZAKAZNIK";
                button1.Enabled = false;
                button2.Enabled = false;
            }

            db.Close();
        }
    }
}
