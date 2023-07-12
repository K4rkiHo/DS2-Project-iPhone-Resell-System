using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core
{
    class Ucet
    {
        public int ucet_id { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public string email { get; set; }
        public string heslo { get; set; }
        public int pocet_vykoupenych_IP { get; set; }
        public string role { get; set; }
        public bool aktivni { get; set; }
        public bool smazany { get; set; }
        public bool podezdrely { get; set; }

        public String FullName { get { return this.jmeno + " " + this.prijmeni; } }
    }
}
