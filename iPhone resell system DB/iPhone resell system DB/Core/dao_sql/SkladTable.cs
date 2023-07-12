using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core.dao_sql
{
    class SkladTable
    {
        public static String TABLE_NAME = "Sklad";

        public static String SQL_SELECT = "SELECT * FROM \"Sklad\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Sklad\" WHERE sklad_id=@id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Sklad\" WHERE sklad_id=@sklad_id";
        public static String SQL_INSERT = "INSERT INTO \"Sklad\" VALUES (@mesto, @ulice, @PSC)";
        public static String SQL_UPDATE = "UPDATE \"Sklad\" SET mesto=@mesto, ulice=@ulice, PSC=@PSC";

        public static Collection<Sklad> Select(Database pDb = null)
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

            Collection<Sklad> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }
        public static Sklad Select(int id, Database pDb = null)
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

            Collection<Sklad> Users = Read(reader);
            Sklad User = null;
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

        private static Collection<Sklad> Read(SqlDataReader reader)
        {
            Collection<Sklad> users = new Collection<Sklad>();

            while (reader.Read())
            {
                int i = -1;
                Sklad user = new Sklad();
                user.sklad_id = reader.GetInt32(++i);
                user.mesto = reader.GetString(++i);
                user.ulice = reader.GetString(++i);
                user.PSC = reader.GetInt32(++i);
                users.Add(user);
            }
            return users;
        }

        public static int Delete(int idsklad, Database pDb = null)
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

            command.Parameters.AddWithValue("@sklad_id", idsklad);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Insert(Sklad sklad, Database pDb = null)
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
            PrepareCommand(command, sklad);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static void PrepareCommand(SqlCommand command, Sklad sklad)
        {
            command.Parameters.AddWithValue("@sklad_id", sklad.sklad_id);
            command.Parameters.AddWithValue("@mesto", sklad.mesto);
            command.Parameters.AddWithValue("@ulice", sklad.ulice);
            command.Parameters.AddWithValue("@PSC", sklad.PSC);
        }

        public static int Update(Sklad sklad, Database pDb = null)
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
            PrepareCommand(command, sklad);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
    }
}
