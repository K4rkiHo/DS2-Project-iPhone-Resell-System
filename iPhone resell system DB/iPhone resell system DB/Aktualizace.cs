using iPhone_resell_system_DB.Core;
using iPhone_resell_system_DB.Core.dao_sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPhone_resell_system_DB
{
    public partial class Aktualizace : Form
    {
        public Aktualizace()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Aktualizace_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            db.Connect();
            foreach (Zaznam x in iPhoneTable.Filter(db, keyword: null, iphone_id: Tabs.id))
            {
                label3.Text = x.iphone.model;
                label4.Text = x.iphone.barva;
                label5.Text = x.iphone.cena.ToString();
                label6.Text = x.sklad.mesto;
                label7.Text = x.stav.stav.ToString();
            }

            db.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Database db = new Database();
                db.Connect();

                iPhoneTable.Akualizace_IP(Tabs.id, Form1.id_ucet, int.Parse(textBox1.Text), db);

                db.Close();
                MessageBox.Show("Cena iPhonu byla aktualizována!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Nevyplnili jste všechny údaje!", "EMPTY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
