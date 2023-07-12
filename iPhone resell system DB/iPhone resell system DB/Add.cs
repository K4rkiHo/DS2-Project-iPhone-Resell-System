using iPhone_resell_system_DB.Core;
using iPhone_resell_system_DB.Core.dao_sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPhone_resell_system_DB
{
    public partial class Add : Form
    {
        static int x;
        public string connection = @"Data Source=dbsys.cs.vsb.cz\STUDENT;Initial Catalog=KAR0229;Persist Security Info=True;User ID=KAR0229;Password=L1OGyD3r8vQNaW78";
        public Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void load_data_combobox()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Sklad", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd); 
            DataSet data = new DataSet();
            da.Fill(data);
            cmd.ExecuteNonQuery();
            con.Close();

            comboBox1.DataSource = data.Tables[0];
            comboBox1.DisplayMember = "mesto";
            comboBox1.ValueMember = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                Database db = new Database();
                db.Connect();
                int count = iPhoneTable.Select(db).Count;
                iPhone i = new iPhone();
                i.iphone_id = count + 1;
                i.model = textBox1.Text;
                i.velikost_uloziste = int.Parse(textBox2.Text);
                i.barva = textBox3.Text;
                if (radioButton1.Checked == true)
                {
                    i.stav_id = 1;
                }
                if (radioButton2.Checked == true)
                {
                    i.stav_id = 2;
                }
                if (radioButton3.Checked == true)
                {
                    i.stav_id = 3;
                }
                i.sklad_id = x + 1;
                i.cena = int.Parse(textBox4.Text);
                i.sleva = 0;
                i.skladem = Convert.ToBoolean((byte)1);
                i.v_kosiku = Convert.ToBoolean((byte)0);
                i.zakoupen = Convert.ToBoolean((byte)0);
                i.datum_vytvoreni = DateTime.Now;
                i.datum_vykoupeni = null;

                iPhoneTable.Insert(i, db);
                db.Close();
                MessageBox.Show("iPhone byl přidán do systému", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Nevyplnili jste všechny údaje!", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Add_Load(object sender, EventArgs e)
        {
            load_data_combobox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            x = comboBox1.SelectedIndex;
        }
    }
}
