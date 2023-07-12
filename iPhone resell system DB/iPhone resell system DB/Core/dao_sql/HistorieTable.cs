using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPhone_resell_system_DB.Core.dao_sql
{
    class HistorieTable
    {
        public static String TABLE_NAME = "Historie";

        public static String SQL_SELECT = "SELECT * FROM \"Historie\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Historie\" WHERE historie_id=@id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Historie\" WHERE historie_id=@historie_id";
        public static String SQL_INSERT = "INSERT INTO \"Historie\" VALUES (@cas_zmeny_old, @cas_zmeny_new, @cena_old, @cena_new, @iphone_id, @ucet_id)";
        public static String SQL_UPDATE = "UPDATE \"Historie\" SET cas_zmeny_old=@cas_zmeny_old, cas_zmeny_new=@cas_zmeny_new, cena_old=@cena_old, cena_new=@cena_new, iphone_id=@iphone_id,ucet_id=@ucet_id WHERE historie_id=@historie_id";

        public static Collection<Historie> Select(Database pDb = null)
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

            Collection<Historie> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }
        public static Historie Select(int id, Database pDb = null)
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

            Collection<Historie> Users = Read(reader);
            Historie User = null;
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

        private static Collection<Historie> Read(SqlDataReader reader)
        {
            Collection<Historie> users = new Collection<Historie>();

            while (reader.Read())
            {
                int i = -1;
                Historie user = new Historie();
                user.historie_id = reader.GetInt32(++i);
                if (!reader.IsDBNull(++i))
                {
                    user.cas_zmeny_old = reader.GetDateTime(i);
                }


                if (!reader.IsDBNull(++i))
                {
                    user.cas_zmeny_new = reader.GetDateTime(i);
                }

                user.cena_old = reader.GetInt32(++i);
                user.cena_new = reader.GetInt32(++i);
                user.iphone_id = reader.GetInt32(++i);
                user.ucet_id = reader.GetInt32(++i);

                users.Add(user);
            }
            return users;
        }

        public static int Delete(int historieid, Database pDb = null)
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

            command.Parameters.AddWithValue("@historie_id", historieid);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
        public static int Insert(Historie historie, Database pDb = null)
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
            PrepareCommand(command, historie);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static void PrepareCommand(SqlCommand command, Historie historie)
        {
            command.Parameters.AddWithValue("@historie_id", historie.historie_id);
            command.Parameters.AddWithValue("@cas_zmeny_old", historie.cas_zmeny_old == null ? DBNull.Value : (object)historie.cas_zmeny_old);
            command.Parameters.AddWithValue("@cas_zmeny_new", historie.cas_zmeny_new == null ? DBNull.Value : (object)historie.cas_zmeny_new);
            command.Parameters.AddWithValue("@cena_old", historie.cena_old);
            command.Parameters.AddWithValue("@cena_new", historie.cena_new);
            command.Parameters.AddWithValue("@iphone_id", historie.iphone_id);
            command.Parameters.AddWithValue("@ucet_id", historie.ucet_id);
        }

        public static int Update(Historie historie, Database pDb = null)
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
            PrepareCommand(command, historie);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
    }
}
