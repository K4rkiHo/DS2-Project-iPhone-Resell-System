using iPhone_resell_system_DB.Core;
using iPhone_resell_system_DB.Core.dao_sql;
using iPhone_resell_system_DB.Forms;
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
    public partial class IP_detail : Form
    {
        public IP_detail()
        {
            InitializeComponent();
        }

        private void IP_detail_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            db.Connect();

            foreach (Zaznam item in iPhoneTable.Filter(db, keyword: null, iphone_id: iPhoneTable.selecttedIP))
            {
                iphone.Text = item.iphone.model;
                barva.Text = item.iphone.barva;
                pamet.Text = item.iphone.velikost_uloziste.ToString();
                stav.Text = item.stav.stav;
                sklad.Text = item.sklad.mesto;
                if (item.iphone.skladem == true)
                {
                    dostupnost.Text = "Není skladem";
                }
                else
                {
                    dostupnost.Text = "Skladem";
                }
                cena.Text = item.iphone.cena.ToString();
            }

            db.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iPhoneTable.selecttedIP = 0;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            db.Connect();

            iPhoneTable.Koupit_IP(iPhoneTable.selecttedIP, db);

            db.Close();

            MessageBox.Show("iPhone byl zaplacen!", "ERROR");
        }
    }
}
