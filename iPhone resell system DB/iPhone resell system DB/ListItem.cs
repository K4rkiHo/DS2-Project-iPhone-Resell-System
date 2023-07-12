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

namespace iPhone_resell_system_DB.Forms
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        private void ListItem_Load(object sender, EventArgs e)
        {

        }

        private string _nazev;

        public string Nazev
        {
            get { return _nazev; }
            set { _nazev = value; iphone_name.Text = value; }
        }

        private string _cena;

        public string Cena
        {
            get { return _cena; }
            set { _cena = value; cena.Text = value; }
        }

        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; i.Text = value.ToString(); }
        }

        private bool _v_kosiku;
        public bool v_kosiku
        {
            get { return _v_kosiku; }
            set { _v_kosiku = value; kosik.Text = value.ToString(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            iPhoneTable.selecttedIP = id;
            IP_detail ipdetail = new IP_detail();
            ipdetail.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database db = new Database();
            db.Connect();
            iPhoneTable.selecttedIP = id;
            iPhoneTable.IP_do_kosiku(id, db);
            db.Close();
            MessageBox.Show("iPhone by přidán ko košíku!", "ERROR");
        }
    }
}
