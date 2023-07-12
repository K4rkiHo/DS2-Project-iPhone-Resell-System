using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core
{
    class Historie
    {
        public int historie_id { get; set; }
        public DateTime cas_zmeny_old { get; set; }
        public DateTime cas_zmeny_new { get; set; }
        public int cena_old { get; set; }
        public int cena_new { get; set; }
        public int iphone_id { get; set; }
        public int ucet_id { get; set; }
    }
}
