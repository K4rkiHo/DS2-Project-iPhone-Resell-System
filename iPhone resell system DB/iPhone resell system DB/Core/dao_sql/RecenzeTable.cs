using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPhone_resell_system_DB.Core;

namespace iPhone_resell_system_DB.Core.dao_sql
{
    class RecenzeTable
    {
        public static String TABLE_NAME = "Recenze";

        public static String SQL_SELECT = "SELECT * FROM \"Recenze\"";
        public static String SQL_SELECT_ID = "SELECT * FROM \"Recenze\" WHERE recenze_id=@id";
        public static String SQL_DELETE_ID = "DELETE FROM \"Recenze\" WHERE recenze_id=@recenze_id";
        public static String SQL_INSERT = "INSERT INTO \"Recenze\" VALUES (@hvezdy, @datum, @komentar, @objednavka_id)";
        public static String SQL_UPDATE = "UPDATE \"iPhone\" SET hvezdy=@hvezdy, datum=@datum, komentar=@komentar, objednavka_id=@objednavka_id WHERE recenze_id=@recenze_id";
        public static String SQL_STATISTICS = "SELECT Recenze.objednavka_id, avg(Recenze.hvezdy) AS avg_hvezdy FROM Recenze WHERE year(Recenze.datum) = @year GROUP BY objednavka_id";
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
            List<int> list_sum = new List<int>();
            while (reader.Read())
            {
                int i = 0;
                list_id.Add(reader.GetInt32(i++));
                list_sum.Add(reader.GetInt32(i++));
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }
            foreach (int item in list_id)
            {
                Console.Write($"Objednavka ID: {item} ");
            }
            Console.WriteLine();
            foreach (int item in list_sum)
            {
                Console.Write($"AVG hvezdy: {item} ");
            }
            Console.WriteLine();
        }
        public static Collection<Recenze> Select(Database pDb = null)
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

            Collection<Recenze> users = Read(reader);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return users;
        }
        public static Recenze Select(int id, Database pDb = null)
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

            Collection<Recenze> Users = Read(reader);
            Recenze User = null;
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

        private static Collection<Recenze> Read(SqlDataReader reader)
        {
            Collection<Recenze> users = new Collection<Recenze>();

            while (reader.Read())
            {
                int i = -1;
                Recenze user = new Recenze();
                user.recenze_id = reader.GetInt32(++i);
                user.hvezdy = reader.GetInt32(++i);
                user.datum = reader.GetDateTime(++i);
                //user.komentar = reader.GetString(++i);
                if (!reader.IsDBNull(++i))
                {
                    user.komentar = reader.GetString(i);
                }
                user.objednavka_id = reader.GetInt32(++i);

                users.Add(user);
            }
            return users;
        }


        public static int Delete(int idrecenze, Database pDb = null)
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

            command.Parameters.AddWithValue("@recenze_id", idrecenze);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Insert(Recenze recenze, Database pDb = null)
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
            PrepareCommand(command, recenze);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static void PrepareCommand(SqlCommand command, Recenze recenze)
        {
            command.Parameters.AddWithValue("@recenze_id", recenze.recenze_id);
            command.Parameters.AddWithValue("@hvezdy", recenze.hvezdy);
            command.Parameters.AddWithValue("@datum", recenze.datum);
            command.Parameters.AddWithValue("@komentar", recenze.komentar == null ? DBNull.Value : (object)recenze.komentar);
            command.Parameters.AddWithValue("@objednavka_id", recenze.objednavka_id);
        }

        public static int Update(Recenze recenze, Database pDb = null)
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
            PrepareCommand(command, recenze);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
        public static string Nova_recenze(int ucet_id, int objednavka_id, DateTime cas, int pocet_hvezd, Database pDb)
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

            SqlCommand command = db.CreateCommand("Nova_recenze");
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter input = new SqlParameter();
            input.ParameterName = "@ucet_id";
            input.DbType = DbType.Int32;
            input.Value = ucet_id;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            input = new SqlParameter();
            input.ParameterName = "@objednavka_id";
            input.DbType = DbType.Int32;
            input.Value = objednavka_id;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            input = new SqlParameter();
            input.ParameterName = "@cas";
            input.DbType = DbType.DateTime;
            input.Value = cas;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);

            input = new SqlParameter();
            input.ParameterName = "@pocet_hvezd";
            input.DbType = DbType.Int32;
            input.Value = pocet_hvezd;
            input.Direction = ParameterDirection.Input;
            command.Parameters.Add(input);


            int ret = db.ExecuteNonQuery(command);


            if (pDb == null)
            {
                db.Close();
            }

            if (ret == 0)
            {
                return "Recenze byla vložena!";
            }
            else
            {
                return "V tento den byla na objednávku již recenze vytvořena!";
            }
        }
    }
}
