using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core
{
    class Recenze
    {
        public int recenze_id { get; set; }
        public int hvezdy { get; set; }
        public DateTime datum { get; set; }
        public string komentar { get; set; }
        public int objednavka_id { get; set; }
    }
}
