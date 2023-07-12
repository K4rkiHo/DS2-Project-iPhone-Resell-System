using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core
{
    class Zaznam
    {
        public iPhone iphone { get; set; } = new iPhone();
        public Stav stav { get; set; } = new Stav();
        public Sklad sklad { get; set; } = new Sklad();

        public string stav_skladu { get; set; }
    }
}
