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
    public partial class Tabs : Form
    {
        public string connection = @"Data Source=dbsys.cs.vsb.cz\STUDENT;Initial Catalog=KAR0229;Persist Security Info=True;User ID=KAR0229;Password=L1OGyD3r8vQNaW78";
        public static int id;
        public static string command;
        public Tabs()
        {
            InitializeComponent();
        }
        private async void Tabs_Load(object sender, EventArgs e)
        {
            if (Form1.id_ucet != 1)
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }
            dataGridView1.DataSource = await GetData();
            dataGridView1.Columns[11].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.DataSource = await GetData();
        }
        private async Task<DataTable> GetData()
        {
            DataTable data = new DataTable();

            if (iPhoneTable.a == "iPhone")
            {
                command = @"select iphone.iphone_id as RowNum, iphone.model, iphone.velikost_uloziste, iphone.barva, stav.stav, iphone.cena, iphone.sleva,  iphone.skladem, iphone.v_kosiku, iphone.zakoupen, iphone.datum_vykoupeni, iphone.datum_vytvoreni, sklad.mesto from iphone join stav on iphone.stav_id = stav.stav_id join sklad on iphone.sklad_id = sklad.sklad_id";
            }
            else if (iPhoneTable.a == "Historie")
            {
                command = @"select iphone.model, ucet.jmeno, historie.cas_zmeny_old, historie.cas_zmeny_new, historie.cena_old, historie.cena_new from historie join ucet on historie.ucet_id = ucet.ucet_id join iphone on historie.historie_id = iphone.iphone_id ";
            }
            else if (iPhoneTable.a == "Ucet")
            {
                command = @"select jmeno, prijmeni, email, heslo, pocet_vykoupenych_IP, role, aktivni, smazany, podezdrely from ucet";
            }
            else if (iPhoneTable.a == "Recenze")
            {
                command = @"select hvezdy, datum, komentar, objednavka.cislo_objednavky as cislo_objednavky from recenze join objednavka on recenze.objednavka_id = objednavka.objednavka_id";
            }
            else if (iPhoneTable.a == "Sklad")
            {
                command = @"select mesto, ulice, psc from sklad";
            }
            else if (iPhoneTable.a == "Stav")
            {
                command = @"select stav from stav";
            }
            else //if (iPhoneTable.a == "Ucet")
            {
                command = @"SELECT * FROM " + iPhoneTable.a;
            }

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(command, con))
                {
                    await con.OpenAsync();
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    data.Load(reader);
                }
            }
            return data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "iPhone";
            this.Close();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            iPhoneTable.a = comboBox1.Text;
            dataGridView1.DataSource = await GetData();

            if (comboBox1.Text == "iPhone")
            {
                text_tabulky.Text = "Tabulka iPhonů";
                label1.Show();
                label2.Show();
                button2.Show();
                button3.Show();
                button4.Show();
                button5.Show();
                dataGridView1.Columns[11].DefaultCellStyle.Format = "dd.MM.yyyy";
            }

            if (comboBox1.Text == "Stav")
            {
                text_tabulky.Text = "Tabulka Stavů";
                label1.Hide();
                label2.Hide();
                button2.Hide();
                button3.Hide();
                button4.Hide();
                button5.Hide();
            }

            if (comboBox1.Text == "Recenze")
            {
                text_tabulky.Text = "Tabulka Recenzí";
                button2.Hide();
                button3.Hide();
                button4.Hide();
                button5.Hide();
                label1.Hide();
                label2.Hide();
                dataGridView1.Columns[1].DefaultCellStyle.Format = "dd.MM.yyyy";
            }

            if (comboBox1.Text == "Ucet")
            {
                text_tabulky.Text = "Tabulka Účtů";
                button2.Hide();
                button3.Hide();
                button4.Hide();
                button5.Hide();
                label1.Hide();
                label2.Hide();
            }

            if (comboBox1.Text == "Sklad")
            {
                text_tabulky.Text = "Tabulka Skladů";
                button2.Hide();
                button3.Hide();
                button4.Hide();
                button5.Hide();
                label1.Hide();
                label2.Hide();
            }

            if (comboBox1.Text == "Historie")
            {
                text_tabulky.Text = "Tabulka Historie";
                button2.Hide();
                button3.Hide();
                button4.Hide();
                button5.Hide();
                label1.Hide();
                label2.Hide();
                dataGridView1.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy";
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd.MM.yyyy";
            }

        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["RowNum"].FormattedValue.ToString());
            Database db = new Database();
            db.Connect();
            iPhone i = iPhoneTable.Select(id, db);


            db.Close();

            if (i.skladem == false)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["RowNum"].FormattedValue.ToString());
            label1.Text = id.ToString();
            Database db = new Database();
            db.Connect();
            iPhone i = iPhoneTable.Select(id, db);


            db.Close();

            if (i.skladem == false || id < 0)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Chcete smazat iphone?", "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Database db = new Database();
                db.Connect();
                iPhoneTable.Delete(id, db);
                db.Close();

                dataGridView1.DataSource = await GetData();
            }
            else if (dialogResult == DialogResult.No)
            {
                dataGridView1.DataSource = await GetData();
            }

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Aktualizace a = new Aktualizace();
            a.ShowDialog();
            dataGridView1.DataSource = await GetData();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Chcete nastavit iphone aby nebyl v košíku?", "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Database db = new Database();
                db.Connect();
                iPhoneTable.IP_z_kosiku(id, db);
                db.Close();

                dataGridView1.DataSource = await GetData();
            }
            else if (dialogResult == DialogResult.No)
            {
                dataGridView1.DataSource = await GetData();
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Chcete nastavit iphone aby nebyl zaplacen?", "INFO", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Database db = new Database();
                db.Connect();
                iPhoneTable.NEKoupit_IP(id, db);
                db.Close();

                dataGridView1.DataSource = await GetData();
            }
            else if (dialogResult == DialogResult.No)
            {
                dataGridView1.DataSource = await GetData();
            }
        }
    }
}
