using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core
{
    class iPhone
    {
        public int iphone_id { get; set; }
        public string model { get; set; }
        public int velikost_uloziste { get; set; }
        public string barva { get; set; }
        public int cena { get; set; }
        public int? sleva { get; set; }
        public int sklad_id { get; set; }
        public int stav_id { get; set; }
        public bool skladem { get; set; }
        public bool v_kosiku { get; set; }
        public bool zakoupen { get; set; }
        public DateTime datum_vytvoreni { get; set; }
        public DateTime? datum_vykoupeni { get; set; }

        //public iPhone(int id, string m, int vl, string b, int c, int sl, int sklid, int stavid, bool skld, bool vk, bool za, DateTime dvyt, DateTime dvyk)
        //{
        //    iphone_id = id;
        //    model = m;
        //    velikost_uloziste = vl;
        //    barva = b;
        //    cena = c;
        //    sleva = sl;
        //    sklad_id = sklid;
        //    stav_id = stavid;
        //    skladem = skld;
        //    v_kosiku = vk;
        //    zakoupen = za;
        //    datum_vytvoreni = dvyt;
        //    datum_vykoupeni = dvyk;
        //}
    }
}
