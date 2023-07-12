using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core.dao_sql
{
    class UcetTable
    {
        public static String TABLE_NAME = "Ucet";

        public static String SQL_SELECT = "SELECT * FROM \"Ucet\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Ucet\" WHERE Ucet_id=@id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Ucet\" WHERE Ucet_id=@Ucet_id";
        public static String SQL_INSERT = "INSERT INTO \"Ucet\" VALUES (@jmeno, @prijmeni, @email, @heslo, @pocet_vykoupenych_IP, @role, @aktivni, @smazany, @podezdrely)";
        public static String SQL_UPDATE = "UPDATE \"Ucet\" SET jmeno=@jmeno, prijmeni=@prijmeni, email=@email, heslo=@heslo, pocet_vykoupenych_IP=@pocet_vykoupenych_IP, role=@role, aktivni=@aktivni, smazany=@smazany, podezdrely=@podezdrely WHERE ucet_id=@ucet_id";
        public static String SQL_SEZNAM_ROLI = "SELECT jmeno, prijmeni, role FROM ucet";
        public static String SQL_UCET_DETAIL = "SELECT jmeno, prijmeni, email, heslo, pocet_vykoupenych_IP, role, aktivni, smazany, podezdrely FROM ucet where ucet_id=@ucet_id";
        public static String SQL_UCET_OBNOVENI = "UPDATE \"Ucet\" set smazany = 0 where ucet_id=@ucet_id";

        public static int Obnovit_ucet(int ucet_id, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_UCET_OBNOVENI);
            command.Parameters.AddWithValue("@ucet_id", ucet_id);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            Console.WriteLine("Ucet byl obnoven!");

            return ret;
        }
        public static void Detail(int ucet_id, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_UCET_DETAIL);

            command.Parameters.AddWithValue("@ucet_id", ucet_id);
            SqlDataReader reader = db.Select(command);

            List<Ucet> users = new List<Ucet>();

            while (reader.Read())
            {
                int i = -1;
                Ucet user = new Ucet();
                user.jmeno = reader.GetString(++i);
                user.prijmeni = reader.GetString(++i);
                user.email = reader.GetString(++i);
                user.heslo = reader.GetString(++i);
                user.pocet_vykoupenych_IP = reader.GetInt32(++i);
                user.role = reader.GetString(++i);
                user.aktivni = reader.GetBoolean(++i);
                user.smazany = reader.GetBoolean(++i);
                user.podezdrely = reader.GetBoolean(++i);
                users.Add(user);
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }
            foreach (Ucet item in users)
            {
                Console.Write($"#Ucet Details: {item.jmeno} | {item.prijmeni} | {item.email} | {item.heslo} | {item.pocet_vykoupenych_IP} | {item.role} | {item.aktivni} | {item.smazany} | {item.podezdrely}");
            }
            Console.WriteLine();
        }
        public static void Seznam_roli(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SEZNAM_ROLI);

            //command.Parameters.AddWithValue("@ucet_id", ucet_id);
            SqlDataReader reader = db.Select(command);

            Collection<Ucet> users = new Collection<Ucet>();

            while (reader.Read())
            {
                int i = -1;
                Ucet user = new Ucet();
                user.jmeno = reader.GetString(++i);
                user.prijmeni = reader.GetString(++i);
                user.role = reader.GetString(++i);
                users.Add(user);
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }
            foreach (Ucet item in users)
            {
                Console.WriteLine($"#Ucty Role: {item.jmeno} | {item.prijmeni} | {item.role} ");
            }
            Console.WriteLine();
        }
        public static Collection<Ucet> Select(Database pDb = null)
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

            Collection<Ucet> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }

        public static Ucet Select(int id, Database pDb = null)
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

            Collection<Ucet> Users = Read(reader);
            Ucet User = null;
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

        private static Collection<Ucet> Read(SqlDataReader reader)
        {
            Collection<Ucet> users = new Collection<Ucet>();

            while (reader.Read())
            {
                int i = -1;
                Ucet user = new Ucet();
                user.ucet_id = reader.GetInt32(++i);
                user.jmeno = reader.GetString(++i);
                user.prijmeni = reader.GetString(++i);
                user.email = reader.GetString(++i);
                user.heslo = reader.GetString(++i);
                user.pocet_vykoupenych_IP = reader.GetInt32(++i);
                user.role = reader.GetString(++i);
                user.aktivni = reader.GetBoolean(++i);
                user.smazany = reader.GetBoolean(++i);
                user.podezdrely = reader.GetBoolean(++i);
                users.Add(user);
            }
            return users;
        }

        public static int Delete(int iducet, Database pDb = null)
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

            command.Parameters.AddWithValue("@Ucet_id", iducet);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Insert(Ucet ucet, Database pDb = null)
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
            PrepareCommand(command, ucet);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static void PrepareCommand(SqlCommand command, Ucet ucet)
        {
            command.Parameters.AddWithValue("@ucet_id", ucet.ucet_id);
            command.Parameters.AddWithValue("@jmeno", ucet.jmeno);
            command.Parameters.AddWithValue("@prijmeni", ucet.prijmeni);
            command.Parameters.AddWithValue("@email", ucet.email);
            command.Parameters.AddWithValue("@heslo", ucet.heslo);
            command.Parameters.AddWithValue("@pocet_vykoupenych_IP", ucet.pocet_vykoupenych_IP == 0 ? DBNull.Value : (object)ucet.pocet_vykoupenych_IP);
            command.Parameters.AddWithValue("@role", ucet.role);
            command.Parameters.AddWithValue("@aktivni", ucet.aktivni);
            command.Parameters.AddWithValue("@smazany", ucet.smazany);
            command.Parameters.AddWithValue("@podezdrely", ucet.podezdrely);
        }

        public static int Update(Ucet ucet, Database pDb = null)
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
            PrepareCommand(command, ucet);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
    }
}
