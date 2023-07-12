using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace iPhone_resell_system_DB.Core.dao_sql
{
    class iPhoneTable
    {
        public static int selecttedIP { get; set; }
        public static string a = "iPhone";

        public static String TABLE_NAME = "iPhone";

        public static String SQL_SELECT = "SELECT * FROM \"iPhone\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"iPhone\" WHERE iphone_id=@id";
        public static String SQL_DELETE_ID = "DELETE FROM \"iPhone\" WHERE iphone_id=@iphone_id";
        public static String SQL_INSERT = "INSERT INTO \"iPhone\" VALUES (@model, @velikost_uloziste, @barva, @cena, @sleva, @skladem, @v_kosiku, @zakoupen, @datum_vytvoreni, @datum_vykoupeni, @sklad_id, @stav_id)";
        public static String SQL_UPDATE = "UPDATE \"iPhone\" SET model=@model, velikost_uloziste=@velikost_uloziste, barva=@barva, cena=@cena, sleva=@sleva, sklad_id=@sklad_id, stav_id=@stav_id, skladem=@skladem, v_kosiku=@v_kosiku, zakoupen=@zakoupen, datum_vytvoreni=@datum_vytvoreni, datum_vykoupeni=@datum_vykoupeni WHERE iphone_id=@iphone_id";
        public static String SQL_STATISTICS = "SELECT avg(cena) AS cena_ip_avg, COUNT(*) AS pocet FROM iPhone WHERE year(iphone.datum_vytvoreni) = @year GROUP BY iphone.datum_vytvoreni";
        public static String SQL_IPHONE_DETAIL = "SELECT model, velikost_uloziste, barva, cena FROM iphone where iphone_id=@iphone_id";
        public static String SQL_HISTORIE_CENY = "select iphone.model, cas_zmeny_old, cas_zmeny_new, cena_old, cena_new from historie join iphone on historie.iphone_id = iphone.iphone_id where iphone.iphone_id=@iphone_id";
        public static String SQL_SEZNAM_IP = "SELECT iPhone.iPhone_id, iPhone.model, iPhone.velikost_uloziste, iPhone.barva, iPhone.cena, iPhone.sleva, Stav.stav_id, Stav.stav as stav_IP, Sklad.sklad_id, Sklad.mesto as sklad_IP, CASE WHEN iPhone.skladem = 1 THEN 'Na skladě' ELSE 'Není na skladě' END AS stav_skladu FROM IPhone JOIN Stav on iPhone.stav_id = Stav.stav_id JOIN Sklad on iPhone.sklad_id = Sklad.sklad_id WHERE ";
        public static String SQL_KOUPENI_IP = "update iphone set zakoupen = 1, v_kosiku = 0, skladem = 0 where iphone_id =@iphone_id";
        public static String SQL_NEKOUPENI_IP = "update iphone set zakoupen = 0, v_kosiku = 0, skladem = 1 where iphone_id =@iphone_id";
        public static String SQL_PRIDAT_IP_DO_KOSIKU = "update iphone set v_kosiku = 1 where iphone_id =@iphone_id";
        public static String SQL_NEPRIDAT_IP_DO_KOSIKU = "update iphone set v_kosiku = 0 where iphone_id =@iphone_id";
        public static String SQL_VYKUP_STAV = "INSERT INTO \"iPhone\" VALUES (@model, @velikost_uloziste, @barva, @cena, @sleva, @skladem, @v_kosiku, @zakoupen, @datum_vytvoreni, @datum_vykoupeni, @sklad_id, @stav_id)";
        public static String SQL_VYKUP_CENA = "INSERT INTO \"iPhone\" VALUES (@model, @velikost_uloziste, @barva, @cena, @sleva, @skladem, @v_kosiku, @zakoupen, @datum_vytvoreni, @datum_vykoupeni, @sklad_id, @stav_id)";
        public static String SQL_IP_MODELS = "Select distinct model from iphone order by model ASC";
        public static String SQL_SEZNAM_BEZ_ZAPLACENI = "SELECT * FROM IPHONE WHERE zakoupen=0";
        public static Collection<iPhone> Select_bez_zaplaceni(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SEZNAM_BEZ_ZAPLACENI);
            SqlDataReader reader = db.Select(command);

            Collection<iPhone> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }
        public static Collection<iPhone> Select_models(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_IP_MODELS);
            SqlDataReader reader = db.Select(command);

            Collection<iPhone> users = Read3(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }
        public static Collection<Zaznam> Filter(Database pDb = null, string keyword = null, int? iphone_id = null, int? stav_id = null, int? sklad_id = null, bool skladem = false, bool zakoupen = false)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }
            SqlCommand command = db.CreateCommand(GetFilterCommand(keyword, iphone_id, stav_id, sklad_id, skladem, zakoupen));
            if (keyword != null)
            {
                command.Parameters.AddWithValue("@keyword", keyword);
            }
            if (iphone_id != null)
            {
                command.Parameters.AddWithValue("@iphone_id", iphone_id);
            }
            if (stav_id != null)
            {
                command.Parameters.AddWithValue("@stav_id", stav_id);
            }
            if (sklad_id != null)
            {
                command.Parameters.AddWithValue("@sklad_id", sklad_id);
            }
            if (skladem != false)
            {
                command.Parameters.AddWithValue("@only_na_sklade", skladem);
            }
            if (skladem != false)
            {
                command.Parameters.AddWithValue("@zakoupen", zakoupen);
            }


            SqlDataReader reader = db.Select(command);


            Collection<Zaznam> users = Read2(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }
        private static string GetFilterCommand(string keyword = null, int? iphone_id = null, int? stav_id = null, int? sklad_id = null, bool? skladem = false, bool? zakoupen = false)
        {
            StringBuilder sb = new StringBuilder(SQL_SEZNAM_IP);
            if (keyword != null)
            {
                sb.Append($"(iPhone.model LIKE '%' + @keyword + '%') and ");
            }
            if (iphone_id != null)
            {
                sb.Append($"(iPhone.iphone_id = @iphone_id) and ");
            }
            if (stav_id != null)
            {
                sb.Append($"EXISTS (SELECT * FROM Stav WHERE iPhone.stav_id = Stav.stav_id AND iPhone.stav_id = @stav_id) and ");
            }
            if (sklad_id != null)
            {
                sb.Append($"EXISTS (SELECT * FROM Sklad WHERE iPhone.sklad_id = Sklad.sklad_id AND iPhone.sklad_id = @sklad_id) and ");
            }
            if (skladem != false)
            {
                sb.Append($"(iPhone.skladem = 1) and ");
            }
            if (zakoupen != false)
            {
                sb.Append($"(iPhone.zakoupen = 0) and ");
            }

            sb.Append("1 = 1");

            return sb.ToString();
        }

        public static int Vykup_IP_cena(iPhone iphone, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_VYKUP_CENA);
            PrepareCommand(command, iphone);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
        public static int Vykup_IP_stav(iPhone iphone, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_VYKUP_STAV);
            PrepareCommand(command, iphone);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
        public static int IP_do_kosiku(int iphone_id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_PRIDAT_IP_DO_KOSIKU);
            command.Parameters.AddWithValue("@iphone_id", iphone_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            Console.WriteLine("iPhone byl přidán do košíku!");

            return ret;
        }
        public static int IP_z_kosiku(int iphone_id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_NEPRIDAT_IP_DO_KOSIKU);
            command.Parameters.AddWithValue("@iphone_id", iphone_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            Console.WriteLine("iPhone byl přidán do košíku!");

            return ret;
        }
        public static int Koupit_IP(int iphone_id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_KOUPENI_IP);
            command.Parameters.AddWithValue("@iphone_id", iphone_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            Console.WriteLine("iPhone byl zakoupen!");

            return ret;
        }
        public static int NEKoupit_IP(int iphone_id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_NEKOUPENI_IP);
            command.Parameters.AddWithValue("@iphone_id", iphone_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            Console.WriteLine("iPhone byl zakoupen!");

            return ret;
        }
        public static void Seznam_IP(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_IPHONE_DETAIL);

            //command.Parameters.AddWithValue("@iphone_id", iphone_id);
            SqlDataReader reader = db.Select(command);

            List<iPhone> users = new List<iPhone>();

            while (reader.Read())
            {
                int i = -1;
                iPhone user = new iPhone();
                user.model = reader.GetString(++i);
                user.velikost_uloziste = reader.GetInt32(++i);
                user.barva = reader.GetString(++i);
                user.cena = reader.GetInt32(++i);
                users.Add(user);
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }
            foreach (iPhone item in users)
            {
                Console.Write($"#iPhone Details: {item.model} | {item.velikost_uloziste}GB | {item.barva} | {item.cena}Kč");
            }
            Console.WriteLine();
        }
        public static void Historie_ceny(int iphone_id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_HISTORIE_CENY);

            command.Parameters.AddWithValue("@iphone_id", iphone_id);
            SqlDataReader reader = db.Select(command);

            List<iPhone> iphones = new List<iPhone>();
            List<Historie> histories = new List<Historie>();

            while (reader.Read())
            {
                int i = -1;
                iPhone iphone = new iPhone();
                Historie historie = new Historie();
                iphone.model = reader.GetString(++i);
                historie.cas_zmeny_old = reader.GetDateTime(++i);
                historie.cas_zmeny_new = reader.GetDateTime(++i);
                historie.cena_old = reader.GetInt32(++i);
                historie.cena_new = reader.GetInt32(++i);
                histories.Add(historie);
                iphones.Add(iphone);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }
            foreach (iPhone item in iphones)
            {
                Console.Write($"#Historie ceny: {item.model} | ");
            }
            foreach (Historie his in histories)
            {
                Console.Write($"{his.cas_zmeny_old.ToString("yyyy-MM-dd")} -> {his.cena_old}Kč | {his.cas_zmeny_new.ToString("yyyy-MM-dd")} -> {his.cena_new}Kč");
            }
            Console.WriteLine();
        }
        public static void Detail(int iphone_id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_IPHONE_DETAIL);

            command.Parameters.AddWithValue("@iphone_id", iphone_id);
            SqlDataReader reader = db.Select(command);

            List<iPhone> users = new List<iPhone>();

            while (reader.Read())
            {
                int i = -1;
                iPhone user = new iPhone();
                user.model = reader.GetString(++i);
                user.velikost_uloziste = reader.GetInt32(++i);
                user.barva = reader.GetString(++i);
                user.cena = reader.GetInt32(++i);
                users.Add(user);
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }
            foreach (iPhone item in users)
            {
                Console.Write($"#iPhone Details: {item.model} | {item.velikost_uloziste}GB | {item.barva} | {item.cena}Kč");
            }
            Console.WriteLine();
        }
        public static void Statistika(int year, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_STATISTICS);

            command.Parameters.AddWithValue("@year", year);
            SqlDataReader reader = db.Select(command);
            List<int> list_id = new List<int>();
            List<int> list_cena = new List<int>();
            while (reader.Read())
            {
                int i = 0;
                list_id.Add(reader.GetInt32(i++));
                list_cena.Add(reader.GetInt32(i++));
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }
            foreach (int item in list_id)
            {
                Console.Write($"AVG cena: {item} ");
            }
            Console.WriteLine();
            foreach (int item in list_cena)
            {
                Console.Write($"Pocet IP: {item} ");
            }
            Console.WriteLine();
        }
        public static Collection<iPhone> Select(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<iPhone> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }
        public static iPhone Select(int id, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = db.Select(command);

            Collection<iPhone> Users = Read(reader);
            iPhone User = null;
            if (Users.Count == 1)
            {
                User = Users[0];
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return User;
        }

        private static Collection<iPhone> Read(SqlDataReader reader)
        {
            Collection<iPhone> users = new Collection<iPhone>();

            while (reader.Read())
            {
                int i = -1;
                iPhone user = new iPhone();
                user.iphone_id = reader.GetInt32(++i);
                user.model = reader.GetString(++i);
                user.velikost_uloziste = reader.GetInt32(++i);
                user.barva = reader.GetString(++i);
                user.cena = reader.GetInt32(++i);
                user.sleva = reader.GetInt32(++i);
                user.skladem = reader.GetBoolean(++i);
                user.v_kosiku = reader.GetBoolean(++i);
                user.zakoupen = reader.GetBoolean(++i);
                user.datum_vytvoreni = reader.GetDateTime(++i);
                if (!reader.IsDBNull(++i))
                {
                    user.datum_vykoupeni = reader.GetDateTime(i);
                }
                user.sklad_id = reader.GetInt32(++i);
                user.stav_id = reader.GetInt32(++i);

                users.Add(user);
            }
            return users;
        }
        private static Collection<Zaznam> Read2(SqlDataReader reader)
        {
            Collection<Zaznam> users = new Collection<Zaznam>();

            while (reader.Read())
            {
                int i = -1;
                Zaznam user = new Zaznam();
                user.iphone.iphone_id = reader.GetInt32(++i);
                user.iphone.model = reader.GetString(++i);
                user.iphone.velikost_uloziste = reader.GetInt32(++i);
                user.iphone.barva = reader.GetString(++i);
                user.iphone.cena = reader.GetInt32(++i);
                user.iphone.sleva = reader.GetInt32(++i);
                //user.iphone.skladem = reader.GetBoolean(++i);
                user.stav.stav_id = reader.GetInt32(++i);
                user.stav.stav = reader.GetString(++i);
                user.sklad.sklad_id = reader.GetInt32(++i);
                user.sklad.mesto = reader.GetString(++i);
                user.stav_skladu = reader.GetString(++i);
                users.Add(user);
            }
            return users;
        }
        private static Collection<iPhone> Read3(SqlDataReader reader)
        {
            Collection<iPhone> users = new Collection<iPhone>();

            while (reader.Read())
            {
                int i = -1;
                iPhone user = new iPhone();
                user.model = reader.GetString(++i);
                users.Add(user);
            }
            return users;
        }

        public static int Delete(int idiphone, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@iphone_id", idiphone);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Insert(iPhone iphone, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, iphone);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static void PrepareCommand(SqlCommand command, iPhone iphone)
        {
            command.Parameters.AddWithValue("@iphone_id", iphone.iphone_id);
            command.Parameters.AddWithValue("@model", iphone.model);
            command.Parameters.AddWithValue("@velikost_uloziste", iphone.velikost_uloziste);
            command.Parameters.AddWithValue("@barva", iphone.barva);
            command.Parameters.AddWithValue("@cena", iphone.cena);
            command.Parameters.AddWithValue("@sleva", iphone.sleva == null ? DBNull.Value : (object)iphone.sleva);
            command.Parameters.AddWithValue("@sklad_id", iphone.sklad_id);
            command.Parameters.AddWithValue("@stav_id", iphone.stav_id);
            command.Parameters.AddWithValue("@skladem", iphone.skladem);
            command.Parameters.AddWithValue("@v_kosiku", iphone.v_kosiku);
            command.Parameters.AddWithValue("@zakoupen", iphone.zakoupen);
            command.Parameters.AddWithValue("@datum_vytvoreni", iphone.datum_vytvoreni);
            command.Parameters.AddWithValue("@datum_vykoupeni", iphone.datum_vykoupeni == null ? DBNull.Value : (object)iphone.datum_vykoupeni);
        }

        public static int Update(iPhone iphone, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, iphone);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
        public static int Akualizace_IP(int iphone_id, int ucet_id, int iphone_cena, Database pDb)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            // 1.  create a command object identifying the stored procedure
            SqlCommand command = db.CreateCommand("Aktualizace_IP");

            // 2. set the command object so it knows to execute a stored procedure
            command.CommandType = CommandType.StoredProcedure;

            // 3. create input parameters
            SqlParameter input = new SqlParameter();
            input.ParameterName = "@iphone_id";
            input.DbType = DbType.Int32;
            input.Value = iphone_id;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            input = new SqlParameter();
            input.ParameterName = "@ucet_id";
            input.DbType = DbType.Int32;
            input.Value = ucet_id;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            input = new SqlParameter();
            input.ParameterName = "@iphone_new_cena";
            input.DbType = DbType.Int32;
            input.Value = iphone_cena;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);


            int ret = db.ExecuteNonQuery(command);


            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
        public static string Novy_vykoupeny_IP(int ucet_id, int iphone_id, DateTime cas, Database pDb)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand("Novy_vykoupeny_IP");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter input = new SqlParameter();
            input.ParameterName = "@iphone_id";
            input.DbType = DbType.Int32;
            input.Value = iphone_id;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            input = new SqlParameter();
            input.ParameterName = "@ucet_id";
            input.DbType = DbType.Int32;
            input.Value = ucet_id;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            input = new SqlParameter();
            input.ParameterName = "@cas";
            input.DbType = DbType.DateTime;
            input.Value = cas;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);


            int ret = db.ExecuteNonQuery(command);


            if (pDb == null)
            {
                db.Close();
            }

            if (ret == 0)
            {
                return "Tento zákazník není podezřelý!";
            }
            else
            {
                return "Tento zákazník dnes již prodal 2 nebo více zařízení!";
            }
        }
    }
}
